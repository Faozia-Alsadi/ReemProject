using ReemProject.Domain.Common;

namespace ReemProject.Domain.Entities;

public class Value : BaseEntity
{
    public string NameAr { get; set; } = string.Empty;
    public string NameEn { get; set; } = string.Empty;
    public string Color { get; set; } = "#1a5276";
    public string IconClass { get; set; } = "bi-star";
    public int DisplayOrder { get; set; }
    public ICollection<Project> Projects { get; set; } = [];
}
