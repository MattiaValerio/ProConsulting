using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RapportiWeb.Server.Migrations
{
    /// <inheritdoc />
    public partial class fix_registration4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Utenti_Clienti_Organizzazioneid",
                table: "Utenti");

            migrationBuilder.RenameColumn(
                name: "Organizzazioneid",
                table: "Utenti",
                newName: "ClienteId");

            migrationBuilder.RenameIndex(
                name: "IX_Utenti_Organizzazioneid",
                table: "Utenti",
                newName: "IX_Utenti_ClienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Utenti_Clienti_ClienteId",
                table: "Utenti",
                column: "ClienteId",
                principalTable: "Clienti",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Utenti_Clienti_ClienteId",
                table: "Utenti");

            migrationBuilder.RenameColumn(
                name: "ClienteId",
                table: "Utenti",
                newName: "Organizzazioneid");

            migrationBuilder.RenameIndex(
                name: "IX_Utenti_ClienteId",
                table: "Utenti",
                newName: "IX_Utenti_Organizzazioneid");

            migrationBuilder.AddForeignKey(
                name: "FK_Utenti_Clienti_Organizzazioneid",
                table: "Utenti",
                column: "Organizzazioneid",
                principalTable: "Clienti",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
