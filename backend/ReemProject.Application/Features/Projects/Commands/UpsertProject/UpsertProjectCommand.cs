using MediatR;
using ReemProject.Domain.Enums;

namespace ReemProject.Application.Features.Projects.Commands.UpsertProject;

public record UpsertProjectCommand(
    int? Id,
    string NameAr,
    string NameEn,
    string? DescriptionAr,
    string? DescriptionEn,
    ProjectStatus Status,
    int ValueId,
    int? ManagerId,
    DateTime? StartDate,
    DateTime? ExpectedEndDate,
    DateTime? ActualEndDate,
    string? Notes
) : IRequest<int>;
