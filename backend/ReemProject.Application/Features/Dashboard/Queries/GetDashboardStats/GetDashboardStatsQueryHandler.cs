using MediatR;
using Microsoft.EntityFrameworkCore;
using ReemProject.Application.Common.Interfaces;
using ReemProject.Domain.Enums;

namespace ReemProject.Application.Features.Dashboard.Queries.GetDashboardStats;

public class GetDashboardStatsQueryHandler(IApplicationDbContext db) : IRequestHandler<GetDashboardStatsQuery, DashboardStatsDto>
{
    public async Task<DashboardStatsDto> Handle(GetDashboardStatsQuery request, CancellationToken cancellationToken)
    {
        var projectsQ = db.Projects
            .Include(p => p.Value)
            .Include(p => p.Manager)
            .Where(p => !p.IsDeleted);

        if (request.ManagerId.HasValue)
            projectsQ = projectsQ.Where(p => p.ManagerId == request.ManagerId);

        var projects = await projectsQ.ToListAsync(cancellationToken);

        var byValue = await db.Values
            .Where(v => !v.IsDeleted)
            .OrderBy(v => v.DisplayOrder)
            .Select(v => new ValueStatDto(
                v.NameAr, v.Color,
                v.Projects.Count(p => !p.IsDeleted && (!request.ManagerId.HasValue || p.ManagerId == request.ManagerId)),
                v.Projects.Count(p => !p.IsDeleted && p.Status == ProjectStatus.Completed && (!request.ManagerId.HasValue || p.ManagerId == request.ManagerId)),
                v.Projects.Count(p => !p.IsDeleted && p.Status == ProjectStatus.InProgress && (!request.ManagerId.HasValue || p.ManagerId == request.ManagerId)),
                v.Projects.Count(p => !p.IsDeleted && p.Status == ProjectStatus.NotStarted && (!request.ManagerId.HasValue || p.ManagerId == request.ManagerId))
            ))
            .ToListAsync(cancellationToken);

        var recent = projects
            .OrderByDescending(p => p.UpdatedAt ?? p.CreatedAt)
            .Take(10)
            .Select(p => new RecentProjectDto(
                p.Id, p.NameAr,
                p.Status switch { ProjectStatus.Completed => "مكتمل", ProjectStatus.InProgress => "جارٍ التنفيذ", _ => "لم يبدأ" },
                p.Value.NameAr, p.Value.Color,
                p.Manager?.FullNameAr))
            .ToList();

        return new DashboardStatsDto(
            projects.Count,
            projects.Count(p => p.Status == ProjectStatus.NotStarted),
            projects.Count(p => p.Status == ProjectStatus.InProgress),
            projects.Count(p => p.Status == ProjectStatus.Completed),
            byValue, recent);
    }
}
