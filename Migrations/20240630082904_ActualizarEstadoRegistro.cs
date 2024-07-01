using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CasaDeEmpeño.Migrations
{
    public partial class ActualizarEstadoRegistro : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EstadoRegistro",
                table: "Ofertas",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstadoRegistro",
                table: "Ofertas");
        }
    }
}
