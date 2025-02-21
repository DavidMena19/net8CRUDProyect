using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace net8Proyect.Data.Migrations
{
    /// <inheritdoc />
    public partial class articuloDetalle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CarritoDetalle_ArticuloId",
                table: "CarritoDetalle");

            migrationBuilder.CreateIndex(
                name: "IX_CarritoDetalle_ArticuloId",
                table: "CarritoDetalle",
                column: "ArticuloId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CarritoDetalle_ArticuloId",
                table: "CarritoDetalle");

            migrationBuilder.CreateIndex(
                name: "IX_CarritoDetalle_ArticuloId",
                table: "CarritoDetalle",
                column: "ArticuloId");
        }
    }
}
