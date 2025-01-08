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
        public DbSet<ProjectUser> ProjectUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

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
        }
    }
}
