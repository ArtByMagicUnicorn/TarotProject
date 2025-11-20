using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tarot.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MeaningCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    CategoryName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeaningCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TarotCards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Suit = table.Column<string>(type: "TEXT", nullable: true),
                    Number = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TarotCards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CardMeanings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Text = table.Column<string>(type: "TEXT", nullable: true),
                    CardMeaningId = table.Column<int>(type: "INTEGER", nullable: false),
                    TarotCardId = table.Column<int>(type: "INTEGER", nullable: false),
                    MeaningCategoryId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardMeanings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CardMeanings_MeaningCategories_MeaningCategoryId",
                        column: x => x.MeaningCategoryId,
                        principalTable: "MeaningCategories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CardMeanings_TarotCards_TarotCardId",
                        column: x => x.TarotCardId,
                        principalTable: "TarotCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CardMeanings_MeaningCategoryId",
                table: "CardMeanings",
                column: "MeaningCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CardMeanings_TarotCardId",
                table: "CardMeanings",
                column: "TarotCardId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CardMeanings");

            migrationBuilder.DropTable(
                name: "MeaningCategories");

            migrationBuilder.DropTable(
                name: "TarotCards");
        }
    }
}
