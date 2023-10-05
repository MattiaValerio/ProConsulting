using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RapportiWeb.Server.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clienti",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ragioneSociale = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NomeAbbreviato = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Indirizzo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Citta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Provincia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CAP = table.Column<int>(type: "int", nullable: false),
                    Stato = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Responsabile = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clienti", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Fad",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodRichiesta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClienteId = table.Column<int>(type: "int", nullable: false),
                    RespRic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IncaricatoFad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RifOfferta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CodSessione = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descrizione = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fad", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rapporti",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RichiestaId = table.Column<int>(type: "int", nullable: true),
                    Clienteid = table.Column<int>(type: "int", nullable: false),
                    RespRic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RespInt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Incaricato = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataIntervento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LuogoIntervento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdCollegamento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descrizione = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoIntervento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DurataIntervento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dataCreazioneRapporto = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Clienteid = table.Column<int>(type: "int", nullable: false),
                    ResponsabileRic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descrizione = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FiguraProfessionale = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipologiaIntervento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataIntervento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DurataIntervento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Firma = table.Column<bool>(type: "bit", nullable: false),
                    visible = table.Column<bool>(type: "bit", nullable: false),
                    RapportoCreato = table.Column<bool>(type: "bit", nullable: false)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Clienteid = table.Column<int>(type: "int", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ruolo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
