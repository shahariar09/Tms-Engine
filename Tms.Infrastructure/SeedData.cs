//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Hosting;
//using Tms.Domain.Entity;
//using Tms.Infrastructure;

//public static class SeedData
//{
//    public static void Initialize(IServiceProvider serviceProvider)
//    {
//        var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

//        // Ensure database is created
//        context.Database.EnsureCreated();

//        // Check if roles already exist to prevent duplicate seeding
//        if (!context.Roles.Any())
//        {
//            context.Roles.AddRange(
//                new Role { Name = "Admin" },
//                new Role { Name = "User" },
//                new Role { Name = "Manager" }
//            );

//            context.SaveChanges();
//        }
//    }
//}
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tms.Domain.Entity;
using Tms.Infrastructure;

public static class SeedData
{
    public static async Task Initialize(IServiceProvider serviceProvider)
    {
        // Ensure proper disposal of ApplicationDbContext
        using (var context = new ApplicationDbContext(
            serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
        {
            // Ensure the database is created
            context.Database.EnsureCreated();

            // Seed roles if they do not exist
            if (!context.Roles.Any())
            {
                context.Roles.AddRange(
                    new Role { Name = "Admin" },
                    new Role { Name = "User" },
                    new Role { Name = "Manager" }
                );
                await context.SaveChangesAsync();
            }

            // Seed projects and tasks if they do not exist
            if (!context.Projects.Any() && !context.TaskItems.Any())
            {
                // Add test project
                var project = new Project
                {
                    Name = "Test Project",
                    Description = "A test project",
                    DueDate = DateTime.Now.AddMonths(1)
                };
                context.Projects.Add(project);
                await context.SaveChangesAsync();

                // Add test tasks
                var tasks = new List<TaskItem>
                {
                    new TaskItem
                    {
                        Title = "Task 1",
                        Description = "First test task",
                        Priority = "High",
                        Status = TaskStatusEnum.Open,
                        DueDate = DateTime.Now.AddDays(7),
                        ProjectId = project.Id
                    },
                    new TaskItem
                    {
                        Title = "Task 2",
                        Description = "Second test task",
                        Priority = "Medium",
                        Status = TaskStatusEnum.InProgress,
                        DueDate = DateTime.Now.AddDays(14),
                        ProjectId = project.Id
                    }
                };

                context.TaskItems.AddRange(tasks);
                await context.SaveChangesAsync();
            }
        }
    }
}
