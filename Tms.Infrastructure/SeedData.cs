using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Tms.Domain.Entity;
using Tms.Infrastructure;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

        // Ensure database is created
        context.Database.EnsureCreated();

        // Check if roles already exist to prevent duplicate seeding
        if (!context.Roles.Any())
        {
            context.Roles.AddRange(
                new Role { Name = "Admin" },
                new Role { Name = "User" },
                new Role { Name = "Manager" }
            );

            context.SaveChanges();
        }
    }
}
