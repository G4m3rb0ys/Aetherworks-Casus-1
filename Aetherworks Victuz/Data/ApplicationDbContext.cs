using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Aetherworks_Victuz.Models;

namespace Aetherworks_Victuz.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<VictuzActivity> victuzActivities { get; set; }
        public DbSet<UserActivity> userActivities { get; set; }
        public DbSet<BlackList> blackLists { get; set; }
        public DbSet<Suggestion> suggestions { get; set; }
        public DbSet<Product> products { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string conn = @"Data Source=.;Initial Catalog=VictuzDb;Integrated Security=true;TrustServerCertificate=True;";
            optionsBuilder.UseSqlServer(conn);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


        }
    }
}
