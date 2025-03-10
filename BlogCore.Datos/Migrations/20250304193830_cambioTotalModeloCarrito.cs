using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace net8Proyect.Data.Migrations
{
    /// <inheritdoc />
    public partial class cambioTotalModeloCarrito : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Total",
                table: "Carrito",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Total",
                table: "Carrito");
        }
    }
}
