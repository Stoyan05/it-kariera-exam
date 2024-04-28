using DataLayer.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Net.Mime.MediaTypeNames;

namespace DataLayer
{
    public class BarDbContext : IdentityDbContext<User>
    {
        public BarDbContext() : base()
        {
            
        }
        public BarDbContext(DbContextOptions<BarDbContext> options) : base(options)
        { 
        
        }
        public DbSet<Bar> Bars { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BarDb;Integrated Security=False;Connect Timeout=30;Encrypt=False;");
            }

            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            #region Relationships
            //modelBuilder.Entity<Review>().HasMany(c => c.Bar).WithOne().HasForeignKey<Review>(c => c.BarId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            //modelBuilder.Entity<Review>().HasMany(c => c.User).WithOne().HasForeignKey<Review>(c => c.UserId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region Seeding
            modelBuilder.Entity<IdentityRole>().HasData(
             new IdentityRole { Id = "1", Name = "User", NormalizedName = "User" },
             new IdentityRole { Id = "2", Name = "Admin", NormalizedName = "Admin" }
            );

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = "1",
                    UserName = "Admin",
                    NormalizedUserName = "Admin",
                    Email = "admin@gmail.com",
                    NormalizedEmail = "ADMIN@GMAIL.COM",
                    EmailConfirmed = true,
                    FirstName = "Admin",
                    LastName = "User",
                    PasswordHash = new PasswordHasher<User>().HashPassword(null, "Password123"),
                    SecurityStamp = string.Empty
                }
            );
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { UserId = "1", RoleId = "2" }
            );
            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}
