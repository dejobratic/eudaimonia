using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eudaimonia.Infrastructure.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FullName = table.Column<string>(type: "text", nullable: false),
                    Bio = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: false),
                    Edition_PageCount = table.Column<int>(type: "integer", nullable: false),
                    Edition_FrontCover_Name = table.Column<string>(type: "text", nullable: false),
                    Edition_FrontCover_Url = table.Column<string>(type: "text", nullable: false),
                    Edition_Format = table.Column<string>(type: "text", nullable: false),
                    Edition_PublisherId = table.Column<Guid>(type: "uuid", nullable: false),
                    Edition_PublicationYear = table.Column<int>(type: "integer", nullable: false),
                    ReviewSummary_ReviewCount = table.Column<int>(type: "integer", nullable: false),
                    ReviewSummary_RatingCount = table.Column<int>(type: "integer", nullable: false),
                    ReviewSummary_FiveStarRatingCount = table.Column<int>(type: "integer", nullable: false),
                    ReviewSummary_FourStarRatingCount = table.Column<int>(type: "integer", nullable: false),
                    ReviewSummary_ThreeStarRatingCount = table.Column<int>(type: "integer", nullable: false),
                    ReviewSummary_TwoStarRatingCount = table.Column<int>(type: "integer", nullable: false),
                    ReviewSummary_OneStarRatingCount = table.Column<int>(type: "integer", nullable: false),
                    ReviewSummary_AverageRating = table.Column<double>(type: "double precision", nullable: false),
                    Genres = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorId",
                table: "Books",
                column: "AuthorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Authors");
        }
    }
}
