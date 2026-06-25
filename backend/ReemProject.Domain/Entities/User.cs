using ReemProject.Domain.Common;
using ReemProject.Domain.Enums;

namespace ReemProject.Domain.Entities;

public class User : BaseEntity
{
    public string FullNameAr { get; set; } = string.Empty;
    public string FullNameEn { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    public UserRole Role { get; set; } = UserRole.Manager;
    public ICollection<Project> Projects { get; set; } = [];
}
