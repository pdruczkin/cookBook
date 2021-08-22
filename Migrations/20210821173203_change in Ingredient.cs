using Microsoft.EntityFrameworkCore.Migrations;

namespace cookBook.Migrations
{
    public partial class changeinIngredient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RecipeIngredient_IngredientId",
                table: "RecipeIngredient");

            migrationBuilder.DropColumn(
                name: "RecipeIngredientId",
                table: "Ingredients");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredient_IngredientId",
                table: "RecipeIngredient",
                column: "IngredientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RecipeIngredient_IngredientId",
                table: "RecipeIngredient");

            migrationBuilder.AddColumn<int>(
                name: "RecipeIngredientId",
                table: "Ingredients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredient_IngredientId",
                table: "RecipeIngredient",
                column: "IngredientId",
                unique: true);
        }
    }
}
