using MediatR;
using ReemProject.Domain.Enums;

namespace ReemProject.Application.Features.Projects.Queries.GetProjects;

public record GetProjectsQuery(int? ManagerId = null, int? ValueId = null, ProjectStatus? Status = null) : IRequest<List<ProjectDto>>;

public record ProjectDto(
    int Id,
    string NameAr,
    string NameEn,
    string? DescriptionAr,
    string? DescriptionEn,
    ProjectStatus Status,
    string StatusAr,
    int ValueId,
    string ValueNameAr,
    string ValueColor,
    int? ManagerId,
    string? ManagerNameAr,
    string? Department,
    DateTime? StartDate,
    DateTime? ExpectedEndDate,
    DateTime? ActualEndDate,
    string? Notes
);
