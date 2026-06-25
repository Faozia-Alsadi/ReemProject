using MediatR;
using ReemProject.Domain.Enums;

namespace ReemProject.Application.Features.Users.Commands.UpsertUser;

public record UpsertUserCommand(
    int? Id,
    string FullNameAr,
    string FullNameEn,
    string Email,
    string? Password,
    string Department,
    UserRole Role
) : IRequest<int>;
