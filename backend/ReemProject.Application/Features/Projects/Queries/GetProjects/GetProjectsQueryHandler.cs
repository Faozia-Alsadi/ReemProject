using MediatR;
using Microsoft.EntityFrameworkCore;
using ReemProject.Application.Common.Interfaces;
using ReemProject.Domain.Enums;

namespace ReemProject.Application.Features.Projects.Queries.GetProjects;

public class GetProjectsQueryHandler(IApplicationDbContext db) : IRequestHandler<GetProjectsQuery, List<ProjectDto>>
{
    public async Task<List<ProjectDto>> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
    {
        var query = db.Projects
            .Include(p => p.Value)
            .Include(p => p.Manager)
            .Where(p => !p.IsDeleted);

        if (request.ManagerId.HasValue)
            query = query.Where(p => p.ManagerId == request.ManagerId);

        if (request.ValueId.HasValue)
            query = query.Where(p => p.ValueId == request.ValueId);

        if (request.Status.HasValue)
            query = query.Where(p => p.Status == request.Status);

        return await query
            .OrderBy(p => p.Value.DisplayOrder)
            .ThenBy(p => p.NameAr)
            .Select(p => new ProjectDto(
                p.Id, p.NameAr, p.NameEn, p.DescriptionAr, p.DescriptionEn,
                p.Status, StatusLabel(p.Status),
                p.ValueId, p.Value.NameAr, p.Value.Color,
                p.ManagerId, p.Manager != null ? p.Manager.FullNameAr : null,
                p.Manager != null ? p.Manager.Department : null,
                p.StartDate, p.ExpectedEndDate, p.ActualEndDate, p.Notes))
            .ToListAsync(cancellationToken);
    }

    private static string StatusLabel(ProjectStatus s) => s switch
    {
        ProjectStatus.NotStarted => "لم يبدأ",
        ProjectStatus.InProgress => "جارٍ التنفيذ",
        ProjectStatus.Completed  => "مكتمل",
        _ => string.Empty
    };
}
