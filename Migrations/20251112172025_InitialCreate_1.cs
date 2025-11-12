using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FilmesAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessoes_Filmes_filme_id",
                table: "Sessoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Sessoes_cinemas_cinema_id",
                table: "Sessoes");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Filmes_TempId",
                table: "Filmes");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_cinemas_TempId",
                table: "cinemas");

            migrationBuilder.DropColumn(
                name: "TempId",
                table: "Filmes");

            migrationBuilder.DropColumn(
                name: "TempId",
                table: "cinemas");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessoes_Filmes_filme_id",
                table: "Sessoes",
                column: "filme_id",
                principalTable: "Filmes",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sessoes_cinemas_cinema_id",
                table: "Sessoes",
                column: "cinema_id",
                principalTable: "cinemas",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessoes_Filmes_filme_id",
                table: "Sessoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Sessoes_cinemas_cinema_id",
                table: "Sessoes");

            migrationBuilder.AddColumn<int>(
                name: "TempId",
                table: "Filmes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TempId",
                table: "cinemas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Filmes_TempId",
                table: "Filmes",
                column: "TempId");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_cinemas_TempId",
                table: "cinemas",
                column: "TempId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessoes_Filmes_filme_id",
                table: "Sessoes",
                column: "filme_id",
                principalTable: "Filmes",
                principalColumn: "TempId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sessoes_cinemas_cinema_id",
                table: "Sessoes",
                column: "cinema_id",
                principalTable: "cinemas",
                principalColumn: "TempId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
