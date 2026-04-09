using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PT.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenamedTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_petcards_codes_CodeEntityId",
                table: "petcards");

            migrationBuilder.DropForeignKey(
                name: "FK_petcards_users_UserEntityId",
                table: "petcards");

            migrationBuilder.DropForeignKey(
                name: "FK_sociallinks_petcards_PetCardEntityId",
                table: "sociallinks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_sociallinks",
                table: "sociallinks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_petcards",
                table: "petcards");

            migrationBuilder.RenameTable(
                name: "sociallinks",
                newName: "social_links");

            migrationBuilder.RenameTable(
                name: "petcards",
                newName: "pet_cards");

            migrationBuilder.RenameIndex(
                name: "IX_sociallinks_PetCardEntityId_Type",
                table: "social_links",
                newName: "IX_social_links_PetCardEntityId_Type");

            migrationBuilder.RenameIndex(
                name: "IX_petcards_UserEntityId",
                table: "pet_cards",
                newName: "IX_pet_cards_UserEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_petcards_CodeEntityId",
                table: "pet_cards",
                newName: "IX_pet_cards_CodeEntityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_social_links",
                table: "social_links",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_pet_cards",
                table: "pet_cards",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_pet_cards_codes_CodeEntityId",
                table: "pet_cards",
                column: "CodeEntityId",
                principalTable: "codes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_pet_cards_users_UserEntityId",
                table: "pet_cards",
                column: "UserEntityId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_social_links_pet_cards_PetCardEntityId",
                table: "social_links",
                column: "PetCardEntityId",
                principalTable: "pet_cards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_pet_cards_codes_CodeEntityId",
                table: "pet_cards");

            migrationBuilder.DropForeignKey(
                name: "FK_pet_cards_users_UserEntityId",
                table: "pet_cards");

            migrationBuilder.DropForeignKey(
                name: "FK_social_links_pet_cards_PetCardEntityId",
                table: "social_links");

            migrationBuilder.DropPrimaryKey(
                name: "PK_social_links",
                table: "social_links");

            migrationBuilder.DropPrimaryKey(
                name: "PK_pet_cards",
                table: "pet_cards");

            migrationBuilder.RenameTable(
                name: "social_links",
                newName: "sociallinks");

            migrationBuilder.RenameTable(
                name: "pet_cards",
                newName: "petcards");

            migrationBuilder.RenameIndex(
                name: "IX_social_links_PetCardEntityId_Type",
                table: "sociallinks",
                newName: "IX_sociallinks_PetCardEntityId_Type");

            migrationBuilder.RenameIndex(
                name: "IX_pet_cards_UserEntityId",
                table: "petcards",
                newName: "IX_petcards_UserEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_pet_cards_CodeEntityId",
                table: "petcards",
                newName: "IX_petcards_CodeEntityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_sociallinks",
                table: "sociallinks",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_petcards",
                table: "petcards",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_petcards_codes_CodeEntityId",
                table: "petcards",
                column: "CodeEntityId",
                principalTable: "codes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_petcards_users_UserEntityId",
                table: "petcards",
                column: "UserEntityId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_sociallinks_petcards_PetCardEntityId",
                table: "sociallinks",
                column: "PetCardEntityId",
                principalTable: "petcards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
