using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RapportiWeb.Server.Migrations
{
    /// <inheritdoc />
    public partial class userTable2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Utenti_Clienti_Clienteid",
                table: "Utenti");

            migrationBuilder.DropIndex(
                name: "IX_Utenti_Clienteid",
                table: "Utenti");

            migrationBuilder.DropColumn(
                name: "Clienteid",
                table: "Utenti");

            migrationBuilder.AddColumn<int>(
                name: "Organizzazioneid",
                table: "Utenti",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Utenti_Organizzazioneid",
                table: "Utenti",
                column: "Organizzazioneid");

            migrationBuilder.AddForeignKey(
                name: "FK_Utenti_Clienti_Organizzazioneid",
                table: "Utenti",
                column: "Organizzazioneid",
                principalTable: "Clienti",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Utenti_Clienti_Organizzazioneid",
                table: "Utenti");

            migrationBuilder.DropIndex(
                name: "IX_Utenti_Organizzazioneid",
                table: "Utenti");

            migrationBuilder.DropColumn(
                name: "Organizzazioneid",
                table: "Utenti");

            migrationBuilder.AddColumn<int>(
                name: "Clienteid",
                table: "Utenti",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Utenti_Clienteid",
                table: "Utenti",
                column: "Clienteid");

            migrationBuilder.AddForeignKey(
                name: "FK_Utenti_Clienti_Clienteid",
                table: "Utenti",
                column: "Clienteid",
                principalTable: "Clienti",
                principalColumn: "id");
        }
    }
}
