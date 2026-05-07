using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GimnasioApi.Migrations
{
    /// <inheritdoc />
    public partial class CambioDePropiedad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado",
                table: "ClientesPlanes");

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "ClientesPlanes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Activo",
                table: "ClientesPlanes");

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "ClientesPlanes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
