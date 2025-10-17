using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChallengeCompraGamer_Backend.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class TablaChofer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ChoferDNI",
                table: "micro",
                type: "varchar(16)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "chofer",
                columns: table => new
                {
                    dni = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    nombre = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    apellido = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    clase_licencia = table.Column<string>(type: "varchar(2)", maxLength: 2, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    telefono = table.Column<int>(type: "int", maxLength: 16, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime(3)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(3)"),
                    updated_at = table.Column<DateTime>(type: "datetime(3)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(3)")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chofer", x => x.dni);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_micro_ChoferDNI",
                table: "micro",
                column: "ChoferDNI",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_micro_chofer_ChoferDNI",
                table: "micro",
                column: "ChoferDNI",
                principalTable: "chofer",
                principalColumn: "dni",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_micro_chofer_ChoferDNI",
                table: "micro");

            migrationBuilder.DropTable(
                name: "chofer");

            migrationBuilder.DropIndex(
                name: "IX_micro_ChoferDNI",
                table: "micro");

            migrationBuilder.DropColumn(
                name: "ChoferDNI",
                table: "micro");
        }
    }
}
