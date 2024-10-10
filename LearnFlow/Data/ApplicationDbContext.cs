using Microsoft.EntityFrameworkCore;
using LearnFlow.Models;

namespace LearnFlow.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicationUser> RegisterUser { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // You can add custom model configurations here if needed
             modelBuilder.Entity<ApplicationUser>().HasKey(u => u.UserId);
           
        }
    }
}