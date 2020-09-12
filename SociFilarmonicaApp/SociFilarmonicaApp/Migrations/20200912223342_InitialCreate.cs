using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SociFilarmonicaApp.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InfoAuto",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TipoAuto = table.Column<string>(nullable: true),
                    Carburante = table.Column<string>(nullable: true),
                    RimborsoKm = table.Column<decimal>(nullable: false),
                    DataCreazione = table.Column<DateTime>(nullable: false),
                    DataUltimaModifica = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InfoAuto", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "RimborsoKm",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DataCreazione = table.Column<DateTime>(nullable: false),
                    DataUltimaModifica = table.Column<DateTime>(nullable: false),
                    DatiRimborsoSerializzati = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RimborsoKm", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TipologiaSocio",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Descrizione = table.Column<string>(maxLength: 100, nullable: false),
                    DataCreazione = table.Column<DateTime>(nullable: false),
                    DataUltimaModifica = table.Column<DateTime>(nullable: false)
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
                        .Annotation("Sqlite:Autoincrement", true),
                    NumeroSocio = table.Column<int>(nullable: false),
                    Nome = table.Column<string>(maxLength: 100, nullable: false),
                    Cognome = table.Column<string>(maxLength: 100, nullable: false),
                    CodiceFiscale = table.Column<string>(maxLength: 50, nullable: true),
                    Telefono = table.Column<string>(maxLength: 50, nullable: true),
                    Telefono2 = table.Column<string>(maxLength: 50, nullable: true),
                    Email = table.Column<string>(maxLength: 200, nullable: true),
                    Email2 = table.Column<string>(nullable: true),
                    Indirizzo = table.Column<string>(maxLength: 100, nullable: true),
                    Cap = table.Column<string>(maxLength: 50, nullable: true),
                    Citta = table.Column<string>(maxLength: 100, nullable: true),
                    NumeroTesseraAmbima = table.Column<string>(maxLength: 50, nullable: true),
                    TargaMacchina = table.Column<string>(maxLength: 100, nullable: true),
                    DescrizioneMacchina = table.Column<string>(maxLength: 100, nullable: true),
                    DatiAutoID = table.Column<int>(nullable: true),
                    TipologiaSocioID = table.Column<int>(nullable: true),
                    PrivacyFirmata = table.Column<bool>(nullable: false),
                    Annullato = table.Column<bool>(nullable: false),
                    DataNascita = table.Column<DateTime>(nullable: true),
                    LuogoNascita = table.Column<string>(maxLength: 100, nullable: true),
                    DataCreazione = table.Column<DateTime>(nullable: false),
                    DataUltimaModifica = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Socio", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Socio_InfoAuto_DatiAutoID",
                        column: x => x.DatiAutoID,
                        principalTable: "InfoAuto",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Socio_TipologiaSocio_TipologiaSocioID",
                        column: x => x.TipologiaSocioID,
                        principalTable: "TipologiaSocio",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Quote",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SocioID = table.Column<int>(nullable: false),
                    QuotaSociale = table.Column<decimal>(nullable: false),
                    Anno = table.Column<int>(nullable: false),
                    DataCreazione = table.Column<DateTime>(nullable: false),
                    DataUltimaModifica = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quote", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Quote_Socio_SocioID",
                        column: x => x.SocioID,
                        principalTable: "Socio",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Quote_SocioID",
                table: "Quote",
                column: "SocioID");

            migrationBuilder.CreateIndex(
                name: "IX_Socio_DatiAutoID",
                table: "Socio",
                column: "DatiAutoID");

            migrationBuilder.CreateIndex(
                name: "IX_Socio_TipologiaSocioID",
                table: "Socio",
                column: "TipologiaSocioID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Quote");

            migrationBuilder.DropTable(
                name: "RimborsoKm");

            migrationBuilder.DropTable(
                name: "Socio");

            migrationBuilder.DropTable(
                name: "InfoAuto");

            migrationBuilder.DropTable(
                name: "TipologiaSocio");
        }
    }
}
