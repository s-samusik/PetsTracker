using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PT.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPrivacyPolicy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "privacy_policy",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false),
                    UserType = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_privacy_policy", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_privacy_policy_UserType_Version",
                table: "privacy_policy",
                columns: new[] { "UserType", "Version" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "privacy_policy");
        }
    }
}
