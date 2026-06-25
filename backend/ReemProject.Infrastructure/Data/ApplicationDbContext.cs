using Microsoft.EntityFrameworkCore;
using ReemProject.Application.Common.Interfaces;

namespace ReemProject.Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : DbContext(options), IApplicationDbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        // Global soft-delete filter
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var isDeletedProperty = entityType.FindProperty("IsDeleted");
            if (isDeletedProperty != null && isDeletedProperty.ClrType == typeof(bool))
            {
                var parameter = System.Linq.Expressions.Expression.Parameter(entityType.ClrType);
                var filter = System.Linq.Expressions.Expression.Lambda(
                    System.Linq.Expressions.Expression.Equal(
                        System.Linq.Expressions.Expression.Property(parameter, "IsDeleted"),
                        System.Linq.Expressions.Expression.Constant(false)),
                    parameter);
                entityType.SetQueryFilter(filter);
            }
        }

        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.State == EntityState.Modified)
            {
                var updatedAt = entry.Properties.FirstOrDefault(p => p.Metadata.Name == "UpdatedAt");
                if (updatedAt != null)
                    updatedAt.CurrentValue = DateTime.UtcNow;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}
