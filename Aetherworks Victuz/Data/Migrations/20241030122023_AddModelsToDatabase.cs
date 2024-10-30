using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aetherworks_Victuz.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddModelsToDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(6,2)", precision: 6, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CredentialsId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.Id);
                    table.ForeignKey(
                        name: "FK_user_AspNetUsers_CredentialsId",
                        column: x => x.CredentialsId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "blackLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_blackLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_blackLists_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_blackLists_user_UserId",
                        column: x => x.UserId,
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "suggestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_suggestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_suggestions_user_UserId",
                        column: x => x.UserId,
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "victuzActivities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    HostId = table.Column<int>(type: "int", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(6,2)", precision: 6, scale: 2, nullable: false),
                    MemberPrice = table.Column<decimal>(type: "decimal(6,2)", precision: 6, scale: 2, nullable: false),
                    ActivityTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ParticipantLimit = table.Column<int>(type: "int", nullable: false),
                    Categories = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_victuzActivities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_victuzActivities_user_HostId",
                        column: x => x.HostId,
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "userActivities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ActivityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userActivities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_userActivities_user_UserId",
                        column: x => x.UserId,
                        principalTable: "user",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_userActivities_victuzActivities_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "victuzActivities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_blackLists_RoleId",
                table: "blackLists",
                column: "RoleId",
                unique: true,
                filter: "[RoleId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_blackLists_UserId",
                table: "blackLists",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_suggestions_UserId",
                table: "suggestions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_user_CredentialsId",
                table: "user",
                column: "CredentialsId",
                unique: true,
                filter: "[CredentialsId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_userActivities_ActivityId",
                table: "userActivities",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_userActivities_UserId",
                table: "userActivities",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_victuzActivities_HostId",
                table: "victuzActivities",
                column: "HostId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "blackLists");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "suggestions");

            migrationBuilder.DropTable(
                name: "userActivities");

            migrationBuilder.DropTable(
                name: "victuzActivities");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
