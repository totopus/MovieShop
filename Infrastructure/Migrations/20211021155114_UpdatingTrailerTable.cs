using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class UpdatingTrailerTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Trailers",
                table: "Trailers");

            migrationBuilder.RenameTable(
                name: "Trailers",
                newName: "Trailer");

            migrationBuilder.AlterColumn<string>(
                name: "TrailerUrl",
                table: "Trailer",
                type: "nvarchar(2084)",
                maxLength: 2084,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Trailer",
                type: "nvarchar(2084)",
                maxLength: 2084,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MovieId",
                table: "Trailer",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Trailer",
                table: "Trailer",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Trailer_MovieId",
                table: "Trailer",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trailer_Movie_MovieId",
                table: "Trailer",
                column: "MovieId",
                principalTable: "Movie",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trailer_Movie_MovieId",
                table: "Trailer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Trailer",
                table: "Trailer");

            migrationBuilder.DropIndex(
                name: "IX_Trailer_MovieId",
                table: "Trailer");

            migrationBuilder.DropColumn(
                name: "MovieId",
                table: "Trailer");

            migrationBuilder.RenameTable(
                name: "Trailer",
                newName: "Trailers");

            migrationBuilder.AlterColumn<string>(
                name: "TrailerUrl",
                table: "Trailers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(2084)",
                oldMaxLength: 2084,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Trailers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(2084)",
                oldMaxLength: 2084,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Trailers",
                table: "Trailers",
                column: "Id");
        }
    }
}
