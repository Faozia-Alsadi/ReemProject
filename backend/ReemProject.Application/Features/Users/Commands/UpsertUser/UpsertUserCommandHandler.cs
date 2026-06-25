using MediatR;
using Microsoft.EntityFrameworkCore;
using ReemProject.Application.Common.Interfaces;
using ReemProject.Domain.Entities;

namespace ReemProject.Application.Features.Users.Commands.UpsertUser;

public class UpsertUserCommandHandler(IApplicationDbContext db) : IRequestHandler<UpsertUserCommand, int>
{
    public async Task<int> Handle(UpsertUserCommand request, CancellationToken cancellationToken)
    {
        User user;

        if (request.Id.HasValue)
        {
            user = await db.Users.FirstAsync(u => u.Id == request.Id && !u.IsDeleted, cancellationToken);
        }
        else
        {
            if (await db.Users.AnyAsync(u => u.Email == request.Email && !u.IsDeleted, cancellationToken))
                throw new InvalidOperationException("البريد الإلكتروني مستخدم بالفعل");

            user = new User();
            await db.Users.AddAsync(user, cancellationToken);
        }

        user.FullNameAr = request.FullNameAr;
        user.FullNameEn = request.FullNameEn;
        user.Email = request.Email;
        user.Department = request.Department;
        user.Role = request.Role;

        if (!string.IsNullOrEmpty(request.Password))
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        await db.SaveChangesAsync(cancellationToken);
        return user.Id;
    }
}
