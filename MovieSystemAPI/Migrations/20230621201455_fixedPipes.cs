using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieSystemAPI.Migrations
{
    public partial class fixedPipes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonGenres_Genres_GenresGenreId",
                table: "PersonGenres");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonGenres_Persons_PersonsPersonId",
                table: "PersonGenres");

            migrationBuilder.RenameColumn(
                name: "PersonsPersonId",
                table: "PersonGenres",
                newName: "PersonId");

            migrationBuilder.RenameColumn(
                name: "GenresGenreId",
                table: "PersonGenres",
                newName: "GenreId");

            migrationBuilder.RenameIndex(
                name: "IX_PersonGenres_PersonsPersonId",
                table: "PersonGenres",
                newName: "IX_PersonGenres_PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_PersonGenres_GenresGenreId",
                table: "PersonGenres",
                newName: "IX_PersonGenres_GenreId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonGenres_Genres_GenreId",
                table: "PersonGenres",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "GenreId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonGenres_Persons_PersonId",
                table: "PersonGenres",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonGenres_Genres_GenreId",
                table: "PersonGenres");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonGenres_Persons_PersonId",
                table: "PersonGenres");

            migrationBuilder.RenameColumn(
                name: "PersonId",
                table: "PersonGenres",
                newName: "PersonsPersonId");

            migrationBuilder.RenameColumn(
                name: "GenreId",
                table: "PersonGenres",
                newName: "GenresGenreId");

            migrationBuilder.RenameIndex(
                name: "IX_PersonGenres_PersonId",
                table: "PersonGenres",
                newName: "IX_PersonGenres_PersonsPersonId");

            migrationBuilder.RenameIndex(
                name: "IX_PersonGenres_GenreId",
                table: "PersonGenres",
                newName: "IX_PersonGenres_GenresGenreId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonGenres_Genres_GenresGenreId",
                table: "PersonGenres",
                column: "GenresGenreId",
                principalTable: "Genres",
                principalColumn: "GenreId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonGenres_Persons_PersonsPersonId",
                table: "PersonGenres",
                column: "PersonsPersonId",
                principalTable: "Persons",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
