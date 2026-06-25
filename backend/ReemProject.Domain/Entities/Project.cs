using ReemProject.Domain.Common;
using ReemProject.Domain.Enums;

namespace ReemProject.Domain.Entities;

public class Project : BaseEntity
{
    public string NameAr { get; set; } = string.Empty;
    public string NameEn { get; set; } = string.Empty;
    public string? DescriptionAr { get; set; }
    public string? DescriptionEn { get; set; }
    public ProjectStatus Status { get; set; } = ProjectStatus.NotStarted;
    public int ValueId { get; set; }
    public Value Value { get; set; } = null!;
    public int? ManagerId { get; set; }
    public User? Manager { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? ExpectedEndDate { get; set; }
    public DateTime? ActualEndDate { get; set; }
    public string? Notes { get; set; }
}
