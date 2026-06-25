using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ReemProject.Application.Common.Interfaces;
using ReemProject.Infrastructure.Data;

namespace ReemProject.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            if (environment.IsDevelopment())
            {
                // SQLite for local development — no server required
                options.UseSqlite(
                    "Data Source=ReemProject.db",
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
            }
            else
            {
                // SQL Server 2025 for staging and production
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
            }
        });

        services.AddScoped<IApplicationDbContext>(provider =>
            provider.GetRequiredService<ApplicationDbContext>());

        return services;
    }
}
