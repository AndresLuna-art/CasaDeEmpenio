using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CasaDeEmpeño.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    ProductoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoProducto = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NombreProducto = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    EstadoProducto = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FechaIngreso = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValorCalculado = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FechaVencimientoDevolucion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.ProductoID);
                });

            migrationBuilder.CreateTable(
                name: "Devoluciones",
                columns: table => new
                {
                    DevolucionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductoID = table.Column<int>(type: "int", nullable: false),
                    Comentario = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FechaDevolucion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devoluciones", x => x.DevolucionID);
                    table.ForeignKey(
                        name: "FK_Devoluciones_Productos_ProductoID",
                        column: x => x.ProductoID,
                        principalTable: "Productos",
                        principalColumn: "ProductoID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ofertas",
                columns: table => new
                {
                    OfertaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductoID = table.Column<int>(type: "int", nullable: false),
                    NombreOfertante = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    NumeroCelular = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    MontoOferta = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FechaOferta = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ofertas", x => x.OfertaID);
                    table.ForeignKey(
                        name: "FK_Ofertas_Productos_ProductoID",
                        column: x => x.ProductoID,
                        principalTable: "Productos",
                        principalColumn: "ProductoID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Devoluciones_ProductoID",
                table: "Devoluciones",
                column: "ProductoID");

            migrationBuilder.CreateIndex(
                name: "IX_Ofertas_ProductoID",
                table: "Ofertas",
                column: "ProductoID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Devoluciones");

            migrationBuilder.DropTable(
                name: "Ofertas");

            migrationBuilder.DropTable(
                name: "Productos");
        }
    }
}
