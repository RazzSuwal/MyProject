
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyProject.Models;

namespace MyProject.DataAccessLayer.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {

        }
        // Add your customizations after calling base.OnModelCreating(builder);

        public DbSet<Category> Categories { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        //public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Course>()
        //        .HasOne(p => p.ApplicationUser)
        //        .WithMany(c => c.Courses)
        //        .HasForeignKey(p => p.ApplicationUserId);

        //    modelBuilder.Entity<Cart>()
        //        .HasOne(p => p.ApplicationUser)
        //        .WithMany(c => c.Carts)
        //        .HasForeignKey(p => p.ApplicationUserId);
        //}
    }
}
