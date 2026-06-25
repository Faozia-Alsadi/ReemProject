using ReemProject.Domain.Entities;

namespace ReemProject.Application.Common.Interfaces;

public interface IJwtService
{
    string GenerateToken(User user);
}
