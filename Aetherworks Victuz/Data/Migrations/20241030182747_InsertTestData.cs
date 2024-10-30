using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Aetherworks_Victuz.Data.Migrations
{
    /// <inheritdoc />
    public partial class InsertTestData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "1af7f355-2f08-4e24-ab82-eafdacb471a4", 0, "3d08f465-c409-412d-85c1-f4a212fc2e25", "admin@gmail.com", true, true, null, "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", "AQAAAAIAAYagAAAAEBCO7kfhleA+rJgzblvMlQh/8EzLDeKO1hRDHFxuAX4hRaLAOZEICsYhYKoI97QYew==", null, false, "MRKIS7ZM3PEX7XJX7FGMPZY4NKTH6Z76", false, "admin@gmail.com" },
                    { "8438c007-d757-4663-b064-a085090e230b", 0, "bf636d49-9342-4af5-aa7b-b1e9dd4a3a10", "guest@gmail.com", true, true, null, "GUEST@GMAIL.COM", "GUEST@GMAIL.COM", "AQAAAAIAAYagAAAAEC9Tmh0HNHm5EQL0YPRmTJTZRmjRX4OnzusW767S7O2uW5XKJov6oSZPrQx/RGEcRA==", null, false, "RQLSCP23C4O43IDZW3SETEUO2GI7VZOP", false, "guest@gmail.com" },
                    { "f17d8517-6643-4e91-ab17-b4c18d89f05e", 0, "6f44b994-4920-49ff-84a3-37edfc164be6", "member@gmail.com", true, true, null, "MEMBER@GMAIL.COM", "MEMBER@GMAIL.COM", "AQAAAAIAAYagAAAAEO/MrnGzjJfNjh+vU2Zv9Dv1TR4ZFhiYqBkKFPYFFSVIT+S4DNyYqlNlFb/+ba/vjw==", null, false, "LFSRBIXYR4P6ZTHPXRWDIQ7M5GTLJXK7", false, "member@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "Location",
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
                table: "User",
                columns: new[] { "Id", "CredentialId" },
                values: new object[,]
                {
                    { 1, "1af7f355-2f08-4e24-ab82-eafdacb471a4" },
                    { 2, "f17d8517-6643-4e91-ab17-b4c18d89f05e" },
                    { 3, "8438c007-d757-4663-b064-a085090e230b" }
                });

            migrationBuilder.InsertData(
                table: "Penalties",
                columns: new[] { "Id", "EndDate", "RoleId", "UserId", "UserId1" },
                values: new object[] { 1, new DateTime(2024, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "3", 2, null });

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
                columns: new[] { "Id", "ActivityDate", "Category", "Description", "HostId", "LocationId", "MemberPrice", "Name", "ParticipantLimit", "Price" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 11, 25, 18, 30, 0, 0, DateTimeKind.Unspecified), 3, "Book Club Meetup", 1, 1, 0.00m, "Book Club Meetup", 25, 0.00m },
                    { 2, new DateTime(2024, 11, 20, 14, 0, 0, 0, DateTimeKind.Unspecified), 1, "Photography Workshop", 1, 2, 15.00m, "Photography Workshop", 20, 25.00m },
                    { 3, new DateTime(2024, 11, 22, 17, 0, 0, 0, DateTimeKind.Unspecified), 0, "Battlebot Wars", 1, 2, 12.00m, "Battlebot Wars", 10, 0.00m }
                });

            migrationBuilder.InsertData(
                table: "Participation",
                columns: new[] { "Id", "ActivityId", "UserId" },
                values: new object[,]
                {
                    { 1, 1, 2 },
                    { 2, 2, 2 },
                    { 3, 3, 2 },
                    { 4, 1, 3 },
                    { 5, 2, 3 },
                    { 6, 3, 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Participation",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Participation",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Participation",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Participation",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Participation",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Participation",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Penalties",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Suggestions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Suggestions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Suggestions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "VictuzActivities",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "VictuzActivities",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "VictuzActivities",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8438c007-d757-4663-b064-a085090e230b");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f17d8517-6643-4e91-ab17-b4c18d89f05e");

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1af7f355-2f08-4e24-ab82-eafdacb471a4");
        }
    }
}
