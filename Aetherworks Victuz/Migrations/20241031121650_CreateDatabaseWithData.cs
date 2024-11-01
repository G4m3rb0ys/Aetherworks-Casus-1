using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Aetherworks_Victuz.Migrations
{
    /// <inheritdoc />
    public partial class CreateDatabaseWithData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaxCapacity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(6,2)", precision: 6, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CredentialId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_AspNetUsers_CredentialId",
                        column: x => x.CredentialId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Penalties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Penalties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Penalties_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Penalties_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Suggestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suggestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Suggestions_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VictuzActivities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Picture = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    ActivityDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HostId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(6,2)", precision: 6, scale: 2, nullable: true),
                    MemberPrice = table.Column<decimal>(type: "decimal(6,2)", precision: 6, scale: 2, nullable: true),
                    ParticipantLimit = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VictuzActivities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VictuzActivities_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_VictuzActivities_User_HostId",
                        column: x => x.HostId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Participation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ActivityId = table.Column<int>(type: "int", nullable: false),
                    Attended = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Participation_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Participation_VictuzActivities_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "VictuzActivities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5cc78e5a-bd90-4adc-91c6-1b0bf91708ad", null, "Member", null },
                    { "b136b696-dd1d-4ca8-bc09-dc9a242a8f9b", null, "Guest", null },
                    { "b3cb4480-7af2-4cc5-90dc-0028c7b94190", null, "Organizer", null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "2e333d4b-4c24-448f-a7aa-7608197d5fad", 0, "3d08f465-c409-412d-85c1-f4a212fc2e25", "organizer@gmail.com", true, true, null, "ORGANIZER@GMAIL.COM", "ORGANIZER@GMAIL.COM", "AQAAAAIAAYagAAAAEBCO7kfhleA+rJgzblvMlQh/8EzLDeKO1hRDHFxuAX4hRaLAOZEICsYhYKoI97QYew==", null, false, "MRKIS7ZM3PEX7XJX7FGMPZY4NKTH6Z76", false, "organizer@gmail.com" },
                    { "37373097-0ed7-4677-b07f-954fb9eb08ae", 0, "bf636d49-9342-4af5-aa7b-b1e9dd4a3a10", "guest@gmail.com", true, true, null, "GUEST@GMAIL.COM", "GUEST@GMAIL.COM", "AQAAAAIAAYagAAAAEC9Tmh0HNHm5EQL0YPRmTJTZRmjRX4OnzusW767S7O2uW5XKJov6oSZPrQx/RGEcRA==", null, false, "RQLSCP23C4O43IDZW3SETEUO2GI7VZOP", false, "guest@gmail.com" },
                    { "eaab06bd-a9b6-4785-90bb-d84573827962", 0, "6f44b994-4920-49ff-84a3-37edfc164be6", "member@gmail.com", true, true, null, "MEMBER@GMAIL.COM", "MEMBER@GMAIL.COM", "AQAAAAIAAYagAAAAEO/MrnGzjJfNjh+vU2Zv9Dv1TR4ZFhiYqBkKFPYFFSVIT+S4DNyYqlNlFb/+ba/vjw==", null, false, "LFSRBIXYR4P6ZTHPXRWDIQ7M5GTLJXK7", false, "member@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "MaxCapacity", "Name" },
                values: new object[,]
                {
                    { 1, 24, "B.3.211" },
                    { 2, 96, "B.3.305" },
                    { 3, 72, "B.2.114" },
                    { 4, 96, "C.0.105" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Victuz T-Shirt", 20.00m },
                    { 2, "Victuz Mok", 20.00m },
                    { 3, "Victuz School Starter-Kit", 20.00m }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "b3cb4480-7af2-4cc5-90dc-0028c7b94190", "2e333d4b-4c24-448f-a7aa-7608197d5fad" },
                    { "b136b696-dd1d-4ca8-bc09-dc9a242a8f9b", "37373097-0ed7-4677-b07f-954fb9eb08ae" },
                    { "5cc78e5a-bd90-4adc-91c6-1b0bf91708ad", "eaab06bd-a9b6-4785-90bb-d84573827962" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "CredentialId" },
                values: new object[,]
                {
                    { 1, "2e333d4b-4c24-448f-a7aa-7608197d5fad" },
                    { 2, "eaab06bd-a9b6-4785-90bb-d84573827962" },
                    { 3, "37373097-0ed7-4677-b07f-954fb9eb08ae" }
                });

            migrationBuilder.InsertData(
                table: "Penalties",
                columns: new[] { "Id", "EndDate", "RoleId", "UserId" },
                values: new object[] { 1, new DateTime(2024, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "b136b696-dd1d-4ca8-bc09-dc9a242a8f9b", 2 });

            migrationBuilder.InsertData(
                table: "Suggestions",
                columns: new[] { "Id", "Description", "Title", "UserId" },
                values: new object[,]
                {
                    { 1, "I was expecting more cheese. And I was disturbed by the lack of cheese.", "More Cheese", 2 },
                    { 2, "I was expecting no cheese. And I was disturbed by the amount of cheese present.", "Less Cheese", 2 },
                    { 3, "I was expecting there to not be enough cheese. And I was surprised by the perfect amount of cheese present.", "Just enough Cheese", 2 }
                });

            migrationBuilder.InsertData(
                table: "VictuzActivities",
                columns: new[] { "Id", "ActivityDate", "Category", "Description", "HostId", "LocationId", "MemberPrice", "Name", "ParticipantLimit", "Picture", "Price" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 11, 25, 18, 30, 0, 0, DateTimeKind.Unspecified), 3, "Book Club Meetup", 1, 1, 0.00m, "Book Club Meetup", 25, "/img/BookClub.png", 0.00m },
                    { 2, new DateTime(2024, 11, 20, 14, 0, 0, 0, DateTimeKind.Unspecified), 1, "Photography Workshop", 1, 2, 15.00m, "Photography Workshop", 20, "/img/Photography.png", 25.00m },
                    { 3, new DateTime(2024, 11, 22, 17, 0, 0, 0, DateTimeKind.Unspecified), 0, "Battlebot Wars", 1, 2, 12.00m, "Battlebot Wars", 10, "/img/BattleBot.png", 0.00m }
                });

            migrationBuilder.InsertData(
                table: "Participation",
                columns: new[] { "Id", "ActivityId", "Attended", "UserId" },
                values: new object[,]
                {
                    { 1, 1, false, 2 },
                    { 2, 2, false, 2 },
                    { 3, 3, false, 2 },
                    { 4, 1, false, 3 },
                    { 5, 2, false, 3 },
                    { 6, 3, false, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Participation_ActivityId",
                table: "Participation",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_Participation_UserId",
                table: "Participation",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Penalties_RoleId",
                table: "Penalties",
                column: "RoleId",
                unique: true,
                filter: "[RoleId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Penalties_UserId",
                table: "Penalties",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Suggestions_UserId",
                table: "Suggestions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_User_CredentialId",
                table: "User",
                column: "CredentialId",
                unique: true,
                filter: "[CredentialId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_VictuzActivities_HostId",
                table: "VictuzActivities",
                column: "HostId");

            migrationBuilder.CreateIndex(
                name: "IX_VictuzActivities_LocationId",
                table: "VictuzActivities",
                column: "LocationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Participation");

            migrationBuilder.DropTable(
                name: "Penalties");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Suggestions");

            migrationBuilder.DropTable(
                name: "VictuzActivities");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
