using MediatR;
using ReemProject.Domain.Enums;

namespace ReemProject.Application.Features.Users.Queries.GetUsers;

public record GetUsersQuery : IRequest<List<UserDto>>;

public record UserDto(int Id, string FullNameAr, string FullNameEn, string Email, string Department, UserRole Role, int ProjectCount);
