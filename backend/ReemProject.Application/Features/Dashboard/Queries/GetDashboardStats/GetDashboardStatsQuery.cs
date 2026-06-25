using MediatR;

namespace ReemProject.Application.Features.Dashboard.Queries.GetDashboardStats;

public record GetDashboardStatsQuery(int? ManagerId = null) : IRequest<DashboardStatsDto>;

public record DashboardStatsDto(
    int TotalProjects,
    int NotStarted,
    int InProgress,
    int Completed,
    List<ValueStatDto> ByValue,
    List<RecentProjectDto> RecentProjects
);

public record ValueStatDto(string NameAr, string Color, int Total, int Completed, int InProgress, int NotStarted);
public record RecentProjectDto(int Id, string NameAr, string StatusAr, string ValueNameAr, string ValueColor, string? ManagerNameAr);
