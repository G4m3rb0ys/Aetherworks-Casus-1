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
        public DbSet<Location> Locations { get; set; }
        public DbSet<SuggestionLiked> SuggestionLiked { get; set; }


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

            // Relation for Suggestion -> SuggestionLiked
            modelBuilder.Entity<SuggestionLiked>()
                .HasOne(sl => sl.Suggestion)
                .WithMany(s => s.SuggestionLikeds)
                .HasForeignKey(sl => sl.SuggestionId);

            // Relation for User -> SuggestionLiked
            modelBuilder.Entity<SuggestionLiked>()
                .HasOne(sl => sl.User)
                .WithMany(u => u.SuggestionLikeds)
                .HasForeignKey(sl => sl.UserId);

            // Relation for Suggestion -> User
            modelBuilder.Entity<Suggestion>()
                .HasOne(s => s.User)
                .WithMany(u => u.Suggestions)
                .HasForeignKey(s => s.UserId);

            // Relation for Penalty -> User
            modelBuilder.Entity<Penalty>()
                .HasOne(bl => bl.Role)
                .WithOne()
                .HasForeignKey<Penalty>(bl => bl.RoleId);

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

            var RoleOrganizerId = Guid.NewGuid().ToString();
            var UserOrganizerId = Guid.NewGuid().ToString();
            var RoleMemberId = Guid.NewGuid().ToString();
            var UserMemberId = Guid.NewGuid().ToString();
            var RoleGuestId = Guid.NewGuid().ToString();
            var UserGuestId = Guid.NewGuid().ToString();
            var UserLockId = Guid.NewGuid().ToString();

            // AspNetRoles
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = RoleOrganizerId, Name = "Organizer" },
                new IdentityRole { Id = RoleMemberId, Name = "Member" },
                new IdentityRole { Id = RoleGuestId, Name = "Guest" }
                );

            // AspNetUsers
            modelBuilder.Entity<IdentityUser>().HasData(
                new IdentityUser
                {
                    Id = UserOrganizerId,
                    UserName = "Organizer",
                    NormalizedUserName = "ORGANIZER",
                    Email = "organizer@gmail.com",
                    NormalizedEmail = "ORGANIZER@GMAIL.COM",
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAIAAYagAAAAEBCO7kfhleA+rJgzblvMlQh/8EzLDeKO1hRDHFxuAX4hRaLAOZEICsYhYKoI97QYew==",
                    SecurityStamp = "MRKIS7ZM3PEX7XJX7FGMPZY4NKTH6Z76",
                    ConcurrencyStamp = "3d08f465-c409-412d-85c1-f4a212fc2e25",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = true,
                    AccessFailedCount = 0
                },
                new IdentityUser
                {
                    Id = UserMemberId,
                    UserName = "Member",
                    NormalizedUserName = "MEMBER",
                    Email = "member@gmail.com",
                    NormalizedEmail = "MEMBER@GMAIL.COM",
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAIAAYagAAAAEO/MrnGzjJfNjh+vU2Zv9Dv1TR4ZFhiYqBkKFPYFFSVIT+S4DNyYqlNlFb/+ba/vjw==",
                    SecurityStamp = "LFSRBIXYR4P6ZTHPXRWDIQ7M5GTLJXK7",
                    ConcurrencyStamp = "6f44b994-4920-49ff-84a3-37edfc164be6",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = true,
                    AccessFailedCount = 0
                },
                new IdentityUser
                {
                    Id = UserGuestId,
                    UserName = "Guest",
                    NormalizedUserName = "GUEST",
                    Email = "guest@gmail.com",
                    NormalizedEmail = "GUEST@GMAIL.COM",
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAIAAYagAAAAEC9Tmh0HNHm5EQL0YPRmTJTZRmjRX4OnzusW767S7O2uW5XKJov6oSZPrQx/RGEcRA==",
                    SecurityStamp = "RQLSCP23C4O43IDZW3SETEUO2GI7VZOP",
                    ConcurrencyStamp = "bf636d49-9342-4af5-aa7b-b1e9dd4a3a10",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = true,
                    AccessFailedCount = 0
                },
                new IdentityUser
                {
                    Id = UserLockId,
                    UserName = "Lock",
                    NormalizedUserName = "LOCK",
                    Email = "lock@gmail.com",
                    NormalizedEmail = "LOCK@GMAIL.COM",
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAIAAYagAAAAEBfVTJQI47BIH3mpec90MDkrbH8RxtAFRnFQ6UFD2pY5hY3ABSze9cDHDd88b5E2Pg==",
                    SecurityStamp = "6IS3TH2BMU4QPOKYPZNBUE3WJFM6R5MO",
                    ConcurrencyStamp = "cf94f079-f33c-42fb-be66-f15601fdb549",
                    PhoneNumber = null,
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnd = DateTime.Parse("2124-12-12T00:00:00.0000000+01:00"),
                    LockoutEnabled = true,
                    AccessFailedCount = 0
                }
            );

            // AspNetUserRoles
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    UserId = UserOrganizerId,
                    RoleId = RoleOrganizerId
                },
                new IdentityUserRole<string>
                {
                    UserId = UserMemberId,
                    RoleId = RoleMemberId
                },
                new IdentityUserRole<string>
                {
                    UserId = UserGuestId,
                    RoleId = RoleGuestId
                },
                new IdentityUserRole<string>
                {
                    UserId = UserLockId,
                    RoleId = RoleGuestId
                }
                );

            /// 
            /// Testdata for all classes:
            /// 

            // Locations
            modelBuilder.Entity<Location>().HasData(
                new Location { Id = 1, Name = "B.3.211", MaxCapacity = 24 },
                new Location { Id = 2, Name = "B.3.305", MaxCapacity = 96 },
                new Location { Id = 3, Name = "B.2.114", MaxCapacity = 72 },
                new Location { Id = 4, Name = "C.0.105", MaxCapacity = 96 }
                );

            // Activities
            modelBuilder.Entity<VictuzActivity>().HasData(
                new VictuzActivity
                {
                    Id = 1,
                    Name = "Book Club Meetup",
                    Description = "Book Club Meetup",
                    Picture = "\\img\\BookClub.png",
                    LocationId = 1,
                    ActivityDate = new DateTime(2024, 11, 25, 18, 30, 0),
                    HostId = 1,
                    Price = 0.00m,
                    MemberPrice = 0.00m,
                    ParticipantLimit = 25,
                    Category = VictuzActivity.ActivityCategories.MemOnlyFree
                },
                new VictuzActivity
                {
                    Id = 2,
                    Name = "Photography Workshop",
                    Description = "Photography Workshop",
                    Picture = "\\img\\Photography.png",
                    LocationId = 2,
                    ActivityDate = new DateTime(2024, 11, 20, 14, 0, 0),
                    HostId = 1,
                    Price = 25.00m,
                    MemberPrice = 15.00m,
                    ParticipantLimit = 20,
                    Category = VictuzActivity.ActivityCategories.MemFree
                },
                new VictuzActivity
                {
                    Id = 3,
                    Name = "Battlebot Wars",
                    Description = "Battlebot Wars",
                    Picture = "\\img\\BattleBot.png",
                    LocationId = 2,
                    ActivityDate = new DateTime(2024, 11, 22, 17, 0, 0),
                    HostId = 1,
                    Price = 0.00m,
                    MemberPrice = 12.00m,
                    ParticipantLimit = 10,
                    Category = VictuzActivity.ActivityCategories.Free
                }
                );

            // Products
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Victuz T-Shirt", Price = 20.00m },
                new Product { Id = 2, Name = "Victuz Mok", Price = 20.00m },
                new Product { Id = 3, Name = "Victuz School Starter-Kit", Price = 20.00m }
                );

            // Users
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, CredentialId = UserOrganizerId },
                new User { Id = 2, CredentialId = UserMemberId },
                new User { Id = 3, CredentialId = UserGuestId },
                new User { Id = 4, CredentialId = UserLockId }
                );

            // Suggestions
            modelBuilder.Entity<Suggestion>().HasData(
                new Suggestion { Id = 1, Title = "More Cheese", Description = "I was expecting more cheese. And I was disturbed by the lack of cheese.", UserId = 2 },
                new Suggestion { Id = 2, Title = "Less Cheese", Description = "I was expecting no cheese. And I was disturbed by the amount of cheese present.", UserId = 2 },
                new Suggestion { Id = 3, Title = "Just enough Cheese", Description = "I was expecting there to not be enough cheese. And I was surprised by the perfect amount of cheese present.", UserId = 2 }
                );

            // Penalties
            modelBuilder.Entity<Penalty>().HasData(
                new Penalty { Id = 1, UserId = 2, RoleId = RoleGuestId, EndDate = new DateTime(2024, 10, 15)}
                );

            // Participations
            modelBuilder.Entity<Participation>().HasData(
                new Participation { Id = 1, UserId = 2, ActivityId = 1 },
                new Participation { Id = 2, UserId = 2, ActivityId = 2 },
                new Participation { Id = 3, UserId = 2, ActivityId = 3 },
                new Participation { Id = 4, UserId = 3, ActivityId = 1 },
                new Participation { Id = 5, UserId = 3, ActivityId = 2 },
                new Participation { Id = 6, UserId = 3, ActivityId = 3 }
                );
        }
    }
}
