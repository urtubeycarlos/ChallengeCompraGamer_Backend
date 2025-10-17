using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChallengeCompraGamer_Backend.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ActualizacionCamposTablaMicros : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_micro_chofer_ChoferDNI",
                table: "micro");

            migrationBuilder.RenameColumn(
                name: "ChoferDNI",
                table: "micro",
                newName: "chofer_dni");

            migrationBuilder.RenameIndex(
                name: "IX_micro_ChoferDNI",
                table: "micro",
                newName: "IX_micro_chofer_dni");

            migrationBuilder.AddColumn<int>(
                name: "cantidad_asientos",
                table: "micro",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "marca",
                table: "micro",
                type: "varchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "modelo",
                table: "micro",
                type: "varchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_micro_chofer_chofer_dni",
                table: "micro",
                column: "chofer_dni",
                principalTable: "chofer",
                principalColumn: "dni",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_micro_chofer_chofer_dni",
                table: "micro");

            migrationBuilder.DropColumn(
                name: "cantidad_asientos",
                table: "micro");

            migrationBuilder.DropColumn(
                name: "marca",
                table: "micro");

            migrationBuilder.DropColumn(
                name: "modelo",
                table: "micro");

            migrationBuilder.RenameColumn(
                name: "chofer_dni",
                table: "micro",
                newName: "ChoferDNI");

            migrationBuilder.RenameIndex(
                name: "IX_micro_chofer_dni",
                table: "micro",
                newName: "IX_micro_ChoferDNI");

            migrationBuilder.AddForeignKey(
                name: "FK_micro_chofer_ChoferDNI",
                table: "micro",
                column: "ChoferDNI",
                principalTable: "chofer",
                principalColumn: "dni",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
