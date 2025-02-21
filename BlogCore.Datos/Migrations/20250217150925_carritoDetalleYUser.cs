using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace net8Proyect.Data.Migrations
{
    /// <inheritdoc />
    public partial class carritoDetalleYUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ArticuloId",
                table: "CarritoDetalle",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "CarritoDetalle",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "CarritoDetalle",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CarritoDetalle_ArticuloId",
                table: "CarritoDetalle",
                column: "ArticuloId");

            migrationBuilder.CreateIndex(
                name: "IX_CarritoDetalle_UserId",
                table: "CarritoDetalle",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarritoDetalle_Articulo_ArticuloId",
                table: "CarritoDetalle",
                column: "ArticuloId",
                principalTable: "Articulo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CarritoDetalle_AspNetUsers_UserId",
                table: "CarritoDetalle",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarritoDetalle_Articulo_ArticuloId",
                table: "CarritoDetalle");

            migrationBuilder.DropForeignKey(
                name: "FK_CarritoDetalle_AspNetUsers_UserId",
                table: "CarritoDetalle");

            migrationBuilder.DropIndex(
                name: "IX_CarritoDetalle_ArticuloId",
                table: "CarritoDetalle");

            migrationBuilder.DropIndex(
                name: "IX_CarritoDetalle_UserId",
                table: "CarritoDetalle");

            migrationBuilder.DropColumn(
                name: "ArticuloId",
                table: "CarritoDetalle");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "CarritoDetalle");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "CarritoDetalle");
        }
    }
}
