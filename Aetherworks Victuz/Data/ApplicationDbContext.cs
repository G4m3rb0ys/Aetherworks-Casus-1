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

        public DbSet<VictuzActivity> VictuzActivities { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Participation> Participation { get; set; }
        public DbSet<Penalty> Penalties { get; set; }
        public DbSet<Suggestion> Suggestions { get; set; }
        public DbSet<Product> Products { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string conn = @"Data Source=.;Initial Catalog=VictuzDb;Integrated Security=true;TrustServerCertificate=True;";
            optionsBuilder.UseSqlServer(conn);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ///
            /// Define Properties
            /// 

            // Product
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(6, 2);

            // VictuzActivity
            modelBuilder.Entity<VictuzActivity>()
                .Property(a => a.Description)
                .HasMaxLength(200);
            modelBuilder.Entity<VictuzActivity>()
                .Property(a => a.Price)
                .HasPrecision(6, 2);
            modelBuilder.Entity<VictuzActivity>()
                .Property(a => a.MemberPrice)
                .HasPrecision(6, 2);

            ///
            ///  Relations for all classes:
            ///

            // Relation for VictuzActivity -> Location
            modelBuilder.Entity<VictuzActivity>()
                .HasOne(a => a.Location)
                .WithMany()
                .HasForeignKey(a => a.LocationId)
                .OnDelete(DeleteBehavior.NoAction);

            // Relation for User <-> VictuzActivity
            modelBuilder.Entity<User>()
                .HasMany(u => u.Participations)
                .WithOne(ua => ua.User)
                .HasForeignKey(ua => ua.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<VictuzActivity>()
                .HasMany(a => a.ParticipantsList)
                .WithOne(ua => ua.Activity)
                .HasForeignKey(ua => ua.ActivityId);

            // Relation for User -> Suggestion
            modelBuilder.Entity<User>()
                .HasMany(u => u.Suggestions)
                .WithOne(s => s.User)
                .HasForeignKey(s => s.UserId);

            // Relation for Penalty -> User
            modelBuilder.Entity<Penalty>()
                .HasOne(bl => bl.User)
                .WithOne()
                .HasForeignKey<Penalty>(bl => bl.UserId);

            /// 
            /// Relations for Authentications
            /// 

            // Relation for User -> Credential
            modelBuilder.Entity<User>()
                .HasOne(u => u.Credential)
                .WithOne()
                .HasForeignKey<User>(u => u.CredentialId);

            // Relation for Blacklist -> Role
            modelBuilder.Entity<Penalty>()
                .HasOne(bl => bl.Role)
                .WithOne()
                .HasForeignKey<Penalty>(bl => bl.RoleId);

            ///
            /// Testdata for all classes:
            /// 

            
            //modelBuilder.Entity<VictuzActivity>().HasData(
            //    new VictuzActivity
            //    {
            //        Id = 1,
            //        Description = "Yoga in the Park",
            //        HostId = 101,
            //        Location = "Central Park",
            //        Price = 15.00m,
            //        MemberPrice = 10.00m,
            //        ActivityTime = new DateTime(2024, 11, 15, 10, 0, 0),
            //        ParticipantLimit = 30,
            //        Categories = VictuzActivity.ActivityCategories.PayAll
            //    },
            //    new VictuzActivity
            //    {
            //        Id = 2,
            //        Description = "Photography Workshop",
            //        HostId = 102,
            //        Location = "City Arts Center",
            //        Price = 25.00m,
            //        MemberPrice = 15.00m,
            //        ActivityTime = new DateTime(2024, 11, 20, 14, 0, 0),
            //        ParticipantLimit = 20,
            //        Categories = VictuzActivity.ActivityCategories.MemOnlyPay
            //    },
            //    new VictuzActivity
            //    {
            //        Id = 3,
            //        Description = "Hiking Adventure",
            //        HostId = 103,
            //        Location = "Mountain Trails",
            //        Price = 0.00m,
            //        MemberPrice = 0.00m,
            //        ActivityTime = new DateTime(2024, 11, 18, 9, 30, 0),
            //        ParticipantLimit = 15,
            //        Categories = VictuzActivity.ActivityCategories.Free
            //    },
            //    new VictuzActivity
            //    {
            //        Id = 4,
            //        Description = "Cooking Class",
            //        HostId = 104,
            //        Location = "Community Center Kitchen",
            //        Price = 20.00m,
            //        MemberPrice = 12.00m,
            //        ActivityTime = new DateTime(2024, 11, 22, 17, 0, 0),
            //        ParticipantLimit = 10,
            //        Categories = VictuzActivity.ActivityCategories.MemFree
            //    },
            //    new VictuzActivity
            //    {
            //        Id = 5,
            //        Description = "Book Club Meetup",
            //        HostId = 105,
            //        Location = "Local Library",
            //        Price = 0.00m,
            //        MemberPrice = 0.00m,
            //        ActivityTime = new DateTime(2024, 11, 25, 18, 30, 0),
            //        ParticipantLimit = 25,
            //        Categories = VictuzActivity.ActivityCategories.MemOnlyFree
            //    }
            //);

        }
    }
}
