using Microsoft.EntityFrameworkCore;
using ReemProject.Domain.Entities;
using ReemProject.Domain.Enums;

namespace ReemProject.Infrastructure.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(ApplicationDbContext db)
    {
        await db.Database.MigrateAsync();

        if (await db.Values.AnyAsync()) return;

        // ── القيم الخمس ──────────────────────────────────────────────────
        var values = new List<Value>
        {
            new() { NameAr = "التميز",          NameEn = "Excellence",             Color = "#7D3C98", IconClass = "bi-trophy",        DisplayOrder = 1 },
            new() { NameAr = "الجودة",           NameEn = "Quality",                Color = "#2E86C1", IconClass = "bi-patch-check",   DisplayOrder = 2 },
            new() { NameAr = "التواصل الفعال",   NameEn = "Effective Communication",Color = "#1E8449", IconClass = "bi-chat-dots",     DisplayOrder = 3 },
            new() { NameAr = "المسؤولية",        NameEn = "Responsibility",         Color = "#D35400", IconClass = "bi-shield-check",  DisplayOrder = 4 },
            new() { NameAr = "الابتكار",         NameEn = "Innovation",             Color = "#B7950B", IconClass = "bi-lightbulb",     DisplayOrder = 5 },
        };
        await db.Values.AddRangeAsync(values);
        await db.SaveChangesAsync();

        // ── مستخدم Admin ──────────────────────────────────────────────────
        var admin = new User
        {
            FullNameAr = "مدير النظام",
            FullNameEn = "System Admin",
            Email = "admin@moe.gov.qa",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
            Department = "المكتب الفني",
            Role = UserRole.Admin
        };
        await db.Users.AddAsync(admin);
        await db.SaveChangesAsync();

        // ── مشاريع نموذجية من الصورة ──────────────────────────────────────
        var tamayoz   = values[0];
        var jawda     = values[1];
        var tawasol   = values[2];
        var masolia   = values[3];
        var ibtkar    = values[4];

        var projects = new List<Project>
        {
            // التميز
            new() { NameAr = "شكراً للموظفين",                                          NameEn = "Employee Appreciation",                     ValueId = tamayoz.Id, Status = ProjectStatus.InProgress },
            new() { NameAr = "موظفينا من أين؟",                                          NameEn = "Where Are Our Employees From?",             ValueId = tamayoz.Id, Status = ProjectStatus.NotStarted },
            new() { NameAr = "تحسين بيئة العمل لذوي الإعاقة",                           NameEn = "Improving Work Environment for PWD",        ValueId = tamayoz.Id, Status = ProjectStatus.InProgress },
            new() { NameAr = "تطوير مكاتب الاستقبال / مداخل الوزارة",                  NameEn = "Developing Reception Offices",              ValueId = tamayoz.Id, Status = ProjectStatus.NotStarted },
            new() { NameAr = "مبادرة الصحة العقلية والجسدية",                           NameEn = "Mental & Physical Health Initiative",       ValueId = tamayoz.Id, Status = ProjectStatus.InProgress },
            new() { NameAr = "الكافيتيريا للموظفين / القهوة الإبداعية",                 NameEn = "Employee Cafeteria / Creative Coffee",      ValueId = tamayoz.Id, Status = ProjectStatus.NotStarted },
            new() { NameAr = "مبادرة التدوير في مبنى الوزارة والمؤسسات التابعة",        NameEn = "Recycling Initiative",                     ValueId = tamayoz.Id, Status = ProjectStatus.Completed },
            new() { NameAr = "ممر الوزارة / متحف أرشيف الوزارة",                        NameEn = "Ministry Corridor / Archive Museum",        ValueId = tamayoz.Id, Status = ProjectStatus.InProgress },
            new() { NameAr = "ممر الوزارة - الجداريات",                                 NameEn = "Ministry Corridor - Murals",               ValueId = tamayoz.Id, Status = ProjectStatus.NotStarted },

            // الجودة
            new() { NameAr = "إعداد ونشر سياسة الشفافية",                              NameEn = "Transparency Policy",                      ValueId = jawda.Id,   Status = ProjectStatus.InProgress },
            new() { NameAr = "مبادرة تدريب القادة على الثقافة المؤسسية وتمكين الموظفين",NameEn = "Leadership Culture Training",               ValueId = jawda.Id,   Status = ProjectStatus.NotStarted },
            new() { NameAr = "دليل الموظف الجديد",                                      NameEn = "New Employee Guide",                       ValueId = jawda.Id,   Status = ProjectStatus.Completed },
            new() { NameAr = "مشاركة الموظفين بسياسات الوزارة وحقوقهم",                NameEn = "Employee Policies Awareness",              ValueId = jawda.Id,   Status = ProjectStatus.InProgress },

            // التواصل الفعال
            new() { NameAr = "سياسة منهجية الاتصال والتواصل",                           NameEn = "Communication Policy",                     ValueId = tawasol.Id, Status = ProjectStatus.NotStarted },
            new() { NameAr = "المساحة المكانية للموظف",                                 NameEn = "Employee Space",                           ValueId = tawasol.Id, Status = ProjectStatus.InProgress },
            new() { NameAr = "مبادرة الورش التقاطعية",                                  NameEn = "Cross-Department Workshops",               ValueId = tawasol.Id, Status = ProjectStatus.NotStarted },
            new() { NameAr = "مبادرة فعالية بناء فرق العمل",                            NameEn = "Team Building Initiative",                 ValueId = tawasol.Id, Status = ProjectStatus.InProgress },
            new() { NameAr = "تطوير الموقع الإلكتروني",                                 NameEn = "Website Development",                     ValueId = tawasol.Id, Status = ProjectStatus.InProgress },
            new() { NameAr = "النشرة الأسبوعية",                                        NameEn = "Weekly Newsletter",                        ValueId = tawasol.Id, Status = ProjectStatus.Completed },

            // المسؤولية
            new() { NameAr = "جوائز وشهادات تقديرية للموظفين على المشاريع والبرامج",   NameEn = "Employee Awards",                          ValueId = masolia.Id, Status = ProjectStatus.NotStarted },
            new() { NameAr = "وضوح آلية الشكاوى وتطويرها",                              NameEn = "Complaints System Development",            ValueId = masolia.Id, Status = ProjectStatus.InProgress },

            // الابتكار
            new() { NameAr = "مبادرة صندوق التطوير",                                    NameEn = "Development Fund Initiative",              ValueId = ibtkar.Id,  Status = ProjectStatus.NotStarted },
            new() { NameAr = "مبادرة مسابقة فكر ونفذ",                                  NameEn = "Think & Execute Competition",              ValueId = ibtkar.Id,  Status = ProjectStatus.InProgress },
        };

        await db.Projects.AddRangeAsync(projects);
        await db.SaveChangesAsync();
    }
}
