using MediatR;
using Microsoft.EntityFrameworkCore;
using ReemProject.Application.Common.Interfaces;

namespace ReemProject.Application.Features.Auth.Commands.Login;

public class LoginCommandHandler(IApplicationDbContext db, IJwtService jwt) : IRequestHandler<LoginCommand, LoginResult>
{
    public async Task<LoginResult> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await db.Users
            .FirstOrDefaultAsync(u => u.Email == request.Email && !u.IsDeleted, cancellationToken)
            ?? throw new UnauthorizedAccessException("البريد الإلكتروني أو كلمة المرور غير صحيحة");

        if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            throw new UnauthorizedAccessException("البريد الإلكتروني أو كلمة المرور غير صحيحة");

        var token = jwt.GenerateToken(user);
        return new LoginResult(token, user.FullNameAr, user.FullNameEn, user.Role.ToString(), user.Id);
    }
}
