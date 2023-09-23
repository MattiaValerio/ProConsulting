using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RapportiWeb.Server.Migrations
{
    /// <inheritdoc />
    public partial class controlloCreazioneRapporto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "RapportoCreato",
                table: "Richieste",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RapportoCreato",
                table: "Richieste");
        }
    }
}
