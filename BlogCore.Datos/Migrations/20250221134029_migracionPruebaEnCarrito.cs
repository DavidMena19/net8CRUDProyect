using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace net8Proyect.Data.Migrations
{
    /// <inheritdoc />
    public partial class migracionPruebaEnCarrito : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarritoDetalle_Carrito_CarritoId",
                table: "CarritoDetalle");

            migrationBuilder.AlterColumn<int>(
                name: "CarritoId",
                table: "CarritoDetalle",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CarritoDetalle_Carrito_CarritoId",
                table: "CarritoDetalle",
                column: "CarritoId",
                principalTable: "Carrito",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarritoDetalle_Carrito_CarritoId",
                table: "CarritoDetalle");

            migrationBuilder.AlterColumn<int>(
                name: "CarritoId",
                table: "CarritoDetalle",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_CarritoDetalle_Carrito_CarritoId",
                table: "CarritoDetalle",
                column: "CarritoId",
                principalTable: "Carrito",
                principalColumn: "Id");
        }
    }
}
