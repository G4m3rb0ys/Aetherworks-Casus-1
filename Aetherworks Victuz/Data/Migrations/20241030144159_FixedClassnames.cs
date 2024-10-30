using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aetherworks_Victuz.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixedClassnames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_suggestions_user_UserId",
                table: "suggestions");

            migrationBuilder.DropForeignKey(
                name: "FK_user_AspNetUsers_CredentialsId",
                table: "user");

            migrationBuilder.DropForeignKey(
                name: "FK_victuzActivities_user_HostId",
                table: "victuzActivities");

            migrationBuilder.DropTable(
                name: "blackLists");

            migrationBuilder.DropTable(
                name: "userActivities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_victuzActivities",
                table: "victuzActivities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user",
                table: "user");

            migrationBuilder.DropIndex(
                name: "IX_user_CredentialsId",
                table: "user");

            migrationBuilder.DropPrimaryKey(
                name: "PK_suggestions",
                table: "suggestions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_products",
                table: "products");

            migrationBuilder.RenameTable(
                name: "victuzActivities",
                newName: "VictuzActivities");

            migrationBuilder.RenameTable(
                name: "user",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "suggestions",
                newName: "Suggestions");

            migrationBuilder.RenameTable(
                name: "products",
                newName: "Products");

            migrationBuilder.RenameColumn(
                name: "Location",
                table: "VictuzActivities",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Categories",
                table: "VictuzActivities",
                newName: "LocationId");

            migrationBuilder.RenameColumn(
                name: "ActivityTime",
                table: "VictuzActivities",
                newName: "ActivityDate");

            migrationBuilder.RenameIndex(
                name: "IX_victuzActivities_HostId",
                table: "VictuzActivities",
                newName: "IX_VictuzActivities_HostId");

            migrationBuilder.RenameColumn(
                name: "CredentialsId",
                table: "User",
                newName: "CredentialId");

            migrationBuilder.RenameIndex(
                name: "IX_suggestions_UserId",
                table: "Suggestions",
                newName: "IX_Suggestions_UserId");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "VictuzActivities",
                type: "decimal(6,2)",
                precision: 6,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,2)",
                oldPrecision: 6,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "MemberPrice",
                table: "VictuzActivities",
                type: "decimal(6,2)",
                precision: 6,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,2)",
                oldPrecision: 6,
                oldScale: 2);

            migrationBuilder.AddColumn<int>(
                name: "Category",
                table: "VictuzActivities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Suggestions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_VictuzActivities",
                table: "VictuzActivities",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Suggestions",
                table: "Suggestions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaxCapacity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Participation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ActivityId = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "Penalties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId1 = table.Column<int>(type: "int", nullable: true)
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
                    table.ForeignKey(
                        name: "FK_Penalties_User_UserId1",
                        column: x => x.UserId1,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_VictuzActivities_LocationId",
                table: "VictuzActivities",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_User_CredentialId",
                table: "User",
                column: "CredentialId",
                unique: true,
                filter: "[CredentialId] IS NOT NULL");

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
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Penalties_UserId1",
                table: "Penalties",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Suggestions_User_UserId",
                table: "Suggestions",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_AspNetUsers_CredentialId",
                table: "User",
                column: "CredentialId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VictuzActivities_Location_LocationId",
                table: "VictuzActivities",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VictuzActivities_User_HostId",
                table: "VictuzActivities",
                column: "HostId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Suggestions_User_UserId",
                table: "Suggestions");

            migrationBuilder.DropForeignKey(
                name: "FK_User_AspNetUsers_CredentialId",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_VictuzActivities_Location_LocationId",
                table: "VictuzActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_VictuzActivities_User_HostId",
                table: "VictuzActivities");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropTable(
                name: "Participation");

            migrationBuilder.DropTable(
                name: "Penalties");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VictuzActivities",
                table: "VictuzActivities");

            migrationBuilder.DropIndex(
                name: "IX_VictuzActivities_LocationId",
                table: "VictuzActivities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_CredentialId",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Suggestions",
                table: "Suggestions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "VictuzActivities");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Suggestions");

            migrationBuilder.RenameTable(
                name: "VictuzActivities",
                newName: "victuzActivities");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "user");

            migrationBuilder.RenameTable(
                name: "Suggestions",
                newName: "suggestions");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "products");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "victuzActivities",
                newName: "Location");

            migrationBuilder.RenameColumn(
                name: "LocationId",
                table: "victuzActivities",
                newName: "Categories");

            migrationBuilder.RenameColumn(
                name: "ActivityDate",
                table: "victuzActivities",
                newName: "ActivityTime");

            migrationBuilder.RenameIndex(
                name: "IX_VictuzActivities_HostId",
                table: "victuzActivities",
                newName: "IX_victuzActivities_HostId");

            migrationBuilder.RenameColumn(
                name: "CredentialId",
                table: "user",
                newName: "CredentialsId");

            migrationBuilder.RenameIndex(
                name: "IX_Suggestions_UserId",
                table: "suggestions",
                newName: "IX_suggestions_UserId");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "victuzActivities",
                type: "decimal(6,2)",
                precision: 6,
                scale: 2,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,2)",
                oldPrecision: 6,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MemberPrice",
                table: "victuzActivities",
                type: "decimal(6,2)",
                precision: 6,
                scale: 2,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,2)",
                oldPrecision: 6,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_victuzActivities",
                table: "victuzActivities",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user",
                table: "user",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_suggestions",
                table: "suggestions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_products",
                table: "products",
                column: "Id");

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
                name: "userActivities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActivityId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
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
                name: "IX_user_CredentialsId",
                table: "user",
                column: "CredentialsId",
                unique: true,
                filter: "[CredentialsId] IS NOT NULL");

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
                name: "IX_userActivities_ActivityId",
                table: "userActivities",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_userActivities_UserId",
                table: "userActivities",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_suggestions_user_UserId",
                table: "suggestions",
                column: "UserId",
                principalTable: "user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_AspNetUsers_CredentialsId",
                table: "user",
                column: "CredentialsId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_victuzActivities_user_HostId",
                table: "victuzActivities",
                column: "HostId",
                principalTable: "user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
