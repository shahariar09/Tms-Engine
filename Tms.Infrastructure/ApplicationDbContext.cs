using System.Reflection.Emit;
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

        public DbSet<Project> Projects { get; set; }    

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            //builder.Entity<UserTask>()
            //.HasOne(ut => ut.TaskItem)
            //.WithMany() // or .WithOne(), depending on your design
            //.HasForeignKey(ut => ut.TaskItemId);
        }
    }
}






