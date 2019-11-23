using Microsoft.EntityFrameworkCore.Migrations;

namespace SociFilarmonicaApp.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TipologiaSocio",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descrizione = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipologiaSocio", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Socio",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(maxLength: 100, nullable: false),
                    Cognome = table.Column<string>(maxLength: 100, nullable: false),
                    Telefono = table.Column<string>(maxLength: 50, nullable: true),
                    Email = table.Column<string>(maxLength: 200, nullable: true),
                    TipologiaSocioID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Socio", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Socio_TipologiaSocio_TipologiaSocioID",
                        column: x => x.TipologiaSocioID,
                        principalTable: "TipologiaSocio",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Socio_TipologiaSocioID",
                table: "Socio",
                column: "TipologiaSocioID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Socio");

            migrationBuilder.DropTable(
                name: "TipologiaSocio");
        }
    }
}
