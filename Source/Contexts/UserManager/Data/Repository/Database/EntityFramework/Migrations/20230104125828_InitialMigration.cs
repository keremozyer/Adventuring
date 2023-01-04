using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adventuring.Contexts.UserManager.Data.Repository.Database.EntityFramework.Migrations;

/// <inheritdoc />
public partial class InitialMigration : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        _ = migrationBuilder.CreateTable(
            name: "AppUsers",
            columns: table => new
            {
                ID = table.Column<string>(type: "text", nullable: false),
                Username = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                Password = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                Salt = table.Column<string>(type: "character varying(88)", maxLength: 88, nullable: false),
                CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                CreatedBy = table.Column<string>(type: "text", nullable: true),
                UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                UpdatedBy = table.Column<string>(type: "text", nullable: true),
                DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
            },
            constraints: table => table.PrimaryKey("PK_AppUsers", x => x.ID));

        _ = migrationBuilder.CreateTable(
            name: "Roles",
            columns: table => new
            {
                ID = table.Column<string>(type: "text", nullable: false),
                Name = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                CreatedBy = table.Column<string>(type: "text", nullable: true),
                UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                UpdatedBy = table.Column<string>(type: "text", nullable: true),
                DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
            },
            constraints: table => table.PrimaryKey("PK_Roles", x => x.ID));

        _ = migrationBuilder.CreateTable(
            name: "AppUserRole",
            columns: table => new
            {
                AppUsersID = table.Column<string>(type: "text", nullable: false),
                RolesID = table.Column<string>(type: "text", nullable: false)
            },
            constraints: table =>
            {
                _ = table.PrimaryKey("PK_AppUserRole", x => new { x.AppUsersID, x.RolesID });
                _ = table.ForeignKey(
                    name: "FK_AppUserRole_AppUsers_AppUsersID",
                    column: x => x.AppUsersID,
                    principalTable: "AppUsers",
                    principalColumn: "ID",
                    onDelete: ReferentialAction.Cascade);
                _ = table.ForeignKey(
                    name: "FK_AppUserRole_Roles_RolesID",
                    column: x => x.RolesID,
                    principalTable: "Roles",
                    principalColumn: "ID",
                    onDelete: ReferentialAction.Cascade);
            });

        _ = migrationBuilder.CreateIndex(
            name: "IX_AppUserRole_RolesID",
            table: "AppUserRole",
            column: "RolesID");

        _ = migrationBuilder.CreateIndex(
            name: "IX_AppUsers_DeletedAt",
            table: "AppUsers",
            column: "DeletedAt",
            unique: true);

        _ = migrationBuilder.CreateIndex(
            name: "IX_AppUsers_DeletedAt_Username",
            table: "AppUsers",
            columns: new[] { "DeletedAt", "Username" },
            unique: true);

        _ = migrationBuilder.CreateIndex(
            name: "IX_Roles_DeletedAt",
            table: "Roles",
            column: "DeletedAt",
            unique: true);

        _ = migrationBuilder.CreateIndex(
            name: "IX_Roles_DeletedAt_Name",
            table: "Roles",
            columns: new[] { "DeletedAt", "Name" },
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        _ = migrationBuilder.DropTable(
            name: "AppUserRole");

        _ = migrationBuilder.DropTable(
            name: "AppUsers");

        _ = migrationBuilder.DropTable(
            name: "Roles");
    }
}
