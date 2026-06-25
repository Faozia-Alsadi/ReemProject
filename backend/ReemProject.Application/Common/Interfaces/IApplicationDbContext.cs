using Microsoft.EntityFrameworkCore;
using ReemProject.Domain.Entities;

namespace ReemProject.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    DbSet<Value> Values { get; }
    DbSet<Project> Projects { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
