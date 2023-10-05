using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RapportiWeb.Server.Migrations
{
    /// <inheritdoc />
    public partial class userTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Utenti_Clienti_Clienteid",
                table: "Utenti");

            migrationBuilder.RenameColumn(
                name: "username",
                table: "Utenti",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "ruolo",
                table: "Utenti",
                newName: "Nome");

            migrationBuilder.RenameColumn(
                name: "password",
                table: "Utenti",
                newName: "Cognome");

            migrationBuilder.AlterColumn<int>(
                name: "Clienteid",
                table: "Utenti",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Amministratore",
                table: "Utenti",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Attivo",
                table: "Utenti",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Figuraprof",
                table: "Utenti",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<byte[]>(
                name: "Hash",
                table: "Utenti",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "Salt",
                table: "Utenti",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<int>(
                name: "TipoUtente",
                table: "Utenti",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Utenti_Clienti_Clienteid",
                table: "Utenti",
                column: "Clienteid",
                principalTable: "Clienti",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Utenti_Clienti_Clienteid",
                table: "Utenti");

            migrationBuilder.DropColumn(
                name: "Amministratore",
                table: "Utenti");

            migrationBuilder.DropColumn(
                name: "Attivo",
                table: "Utenti");

            migrationBuilder.DropColumn(
                name: "Figuraprof",
                table: "Utenti");

            migrationBuilder.DropColumn(
                name: "Hash",
                table: "Utenti");

            migrationBuilder.DropColumn(
                name: "Salt",
                table: "Utenti");

            migrationBuilder.DropColumn(
                name: "TipoUtente",
                table: "Utenti");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Utenti",
                newName: "username");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Utenti",
                newName: "ruolo");

            migrationBuilder.RenameColumn(
                name: "Cognome",
                table: "Utenti",
                newName: "password");

            migrationBuilder.AlterColumn<int>(
                name: "Clienteid",
                table: "Utenti",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Utenti_Clienti_Clienteid",
                table: "Utenti",
                column: "Clienteid",
                principalTable: "Clienti",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
