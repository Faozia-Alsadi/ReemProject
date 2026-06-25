using MediatR;
using Microsoft.EntityFrameworkCore;
using ReemProject.Application.Common.Interfaces;

namespace ReemProject.Application.Features.Users.Queries.GetUsers;

public class GetUsersQueryHandler(IApplicationDbContext db) : IRequestHandler<GetUsersQuery, List<UserDto>>
{
    public async Task<List<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        return await db.Users
            .Where(u => !u.IsDeleted)
            .OrderBy(u => u.FullNameAr)
            .Select(u => new UserDto(
                u.Id, u.FullNameAr, u.FullNameEn, u.Email, u.Department, u.Role,
                u.Projects.Count(p => !p.IsDeleted)))
            .ToListAsync(cancellationToken);
    }
}
