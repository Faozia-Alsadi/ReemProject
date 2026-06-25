using MediatR;

namespace ReemProject.Application.Features.Auth.Commands.Login;

public record LoginCommand(string Email, string Password) : IRequest<LoginResult>;

public record LoginResult(string Token, string FullNameAr, string FullNameEn, string Role, int UserId);
