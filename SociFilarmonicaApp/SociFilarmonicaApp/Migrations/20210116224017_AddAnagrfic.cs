using Microsoft.EntityFrameworkCore.Migrations;

namespace SociFilarmonicaApp.Migrations
{
    public partial class AddAnagrfic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnagraficaFilarmonica",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RagioneSociale = table.Column<string>(nullable: true),
                    Indirizzo = table.Column<string>(nullable: true),
                    Citta = table.Column<string>(nullable: true),
                    Cap = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Tel = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnagraficaFilarmonica", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnagraficaFilarmonica");
        }
    }
}
