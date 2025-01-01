using Microsoft.EntityFrameworkCore;
using Tms.Domain.Entity;

namespace Tms.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<TaskItem> TaskItems { get; set; }
        public DbSet<UserTask> UserTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configure UserTask as a join table
            builder.Entity<UserTask>()
                .HasKey(ut => new { ut.UserId, ut.TaskId }); // Composite primary key

            builder.Entity<UserTask>()
                .HasOne(ut => ut.User)
                .WithMany() // No navigation property in User
                .HasForeignKey(ut => ut.UserId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascading deletes if required

            builder.Entity<UserTask>()
                .HasOne(ut => ut.Task)
                .WithMany() // No navigation property in TaskItem
                .HasForeignKey(ut => ut.TaskId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascading deletes if required
        }
    }
}
