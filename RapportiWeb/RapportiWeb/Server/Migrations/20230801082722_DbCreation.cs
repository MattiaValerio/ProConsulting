using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RapportiWeb.Server.Migrations
{
    /// <inheritdoc />
    public partial class DbCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clienti",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ragioneSociale = table.Column<string>(type: "TEXT", nullable: false),
                    Indirizzo = table.Column<string>(type: "TEXT", nullable: false),
                    Citta = table.Column<string>(type: "TEXT", nullable: false),
                    Provincia = table.Column<string>(type: "TEXT", nullable: false),
                    CAP = table.Column<int>(type: "INTEGER", nullable: false),
                    Stato = table.Column<string>(type: "TEXT", nullable: false),
                    telefono = table.Column<string>(type: "TEXT", nullable: false),
                    email = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clienti", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Fad",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CodRichiesta = table.Column<string>(type: "TEXT", nullable: false),
                    ClienteId = table.Column<int>(type: "INTEGER", nullable: false),
                    RespRic = table.Column<string>(type: "TEXT", nullable: false),
                    IncaricatoFad = table.Column<string>(type: "TEXT", nullable: false),
                    RifOfferta = table.Column<string>(type: "TEXT", nullable: false),
                    CodSessione = table.Column<string>(type: "TEXT", nullable: false),
                    Descrizione = table.Column<string>(type: "TEXT", nullable: false),
                    Note = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fad", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rapporti",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RichiestaId = table.Column<int>(type: "INTEGER", nullable: true),
                    Clienteid = table.Column<int>(type: "INTEGER", nullable: false),
                    RespRic = table.Column<string>(type: "TEXT", nullable: false),
                    RespInt = table.Column<string>(type: "TEXT", nullable: false),
                    Incaricato = table.Column<string>(type: "TEXT", nullable: false),
                    DataIntervento = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    MetodoIntervento = table.Column<string>(type: "TEXT", nullable: false),
                    IdCollegamento = table.Column<string>(type: "TEXT", nullable: false),
                    Descrizione = table.Column<string>(type: "TEXT", nullable: false),
                    TipoIntervento = table.Column<string>(type: "TEXT", nullable: false),
                    DurataIntervento = table.Column<string>(type: "TEXT", nullable: false),
                    dataCreazioneRapporto = table.Column<DateOnly>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rapporti", x => x.id);
                    table.ForeignKey(
                        name: "FK_Rapporti_Clienti_Clienteid",
                        column: x => x.Clienteid,
                        principalTable: "Clienti",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Richieste",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Data = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    Clienteid = table.Column<int>(type: "INTEGER", nullable: false),
                    ResponsabileRic = table.Column<string>(type: "TEXT", nullable: false),
                    Descrizione = table.Column<string>(type: "TEXT", nullable: false),
                    FiguraProfessionale = table.Column<string>(type: "TEXT", nullable: false),
                    TipologiaIntervento = table.Column<string>(type: "TEXT", nullable: false),
                    DataIntervento = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    DurataIntervento = table.Column<string>(type: "TEXT", nullable: false),
                    Firma = table.Column<bool>(type: "INTEGER", nullable: false),
                    visible = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Richieste", x => x.id);
                    table.ForeignKey(
                        name: "FK_Richieste_Clienti_Clienteid",
                        column: x => x.Clienteid,
                        principalTable: "Clienti",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Utenti",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Clienteid = table.Column<int>(type: "INTEGER", nullable: false),
                    password = table.Column<string>(type: "TEXT", nullable: false),
                    ruolo = table.Column<string>(type: "TEXT", nullable: false),
                    username = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utenti", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Utenti_Clienti_Clienteid",
                        column: x => x.Clienteid,
                        principalTable: "Clienti",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rapporti_Clienteid",
                table: "Rapporti",
                column: "Clienteid");

            migrationBuilder.CreateIndex(
                name: "IX_Richieste_Clienteid",
                table: "Richieste",
                column: "Clienteid");

            migrationBuilder.CreateIndex(
                name: "IX_Utenti_Clienteid",
                table: "Utenti",
                column: "Clienteid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fad");

            migrationBuilder.DropTable(
                name: "Rapporti");

            migrationBuilder.DropTable(
                name: "Richieste");

            migrationBuilder.DropTable(
                name: "Utenti");

            migrationBuilder.DropTable(
                name: "Clienti");
        }
    }
}
