using Microsoft.EntityFrameworkCore;
using Tms.Domain.Entity;

namespace Tms.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSet properties for your entities
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<TaskItem> TaskItems { get; set; }
        public DbSet<UserTask> UserTasks { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectUser> ProjectAssignUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Set custom table names
            builder.Entity<TaskItem>().ToTable("TMS_TASK_ITEM");
            builder.Entity<Project>().ToTable("TMS_PROJECT");

            // Define relationships for UserTask entity
            builder.Entity<UserTask>(entity =>
            {
                entity.HasOne(ut => ut.User)
                    .WithMany()
                    .HasForeignKey(ut => ut.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(ut => ut.TaskItem)
                    .WithMany(t => t.AssignedUsers)
                    .HasForeignKey(ut => ut.TaskItemId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<ProjectUser>(entity =>
            {
                // Composite key for the many-to-many table
                entity.HasKey(pu => new { pu.ProjectId, pu.UserId });

                // Relationship between Project and ProjectAssignUser
                entity.HasOne(pu => pu.Project)
                    .WithMany(p => p.ProjectUsers)
                    .HasForeignKey(pu => pu.ProjectId)
                    .OnDelete(DeleteBehavior.Restrict);

                // Relationship between User and ProjectAssignUser
                entity.HasOne(pu => pu.User)
                    .WithMany(u => u.ProjectAssignUsers)
                    .HasForeignKey(pu => pu.UserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<TaskItem>()
                .HasOne(t => t.Project)
                .WithMany(p => p.TaskItems)
                .HasForeignKey(t => t.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}

