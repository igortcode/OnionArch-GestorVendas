using Microsoft.EntityFrameworkCore.Migrations;

namespace Gestao.Data.Migrations
{
    public partial class V04 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Aberta",
                table: "AberturaDias",
                newName: "Status");

            migrationBuilder.AlterColumn<decimal>(
                name: "Total",
                table: "Comandas",
                type: "Decimal(12,2)",
                precision: 12,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Faturamento",
                table: "AberturaDias",
                type: "Decimal(12,2)",
                precision: 12,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "AberturaDias",
                newName: "Aberta");

            migrationBuilder.AlterColumn<decimal>(
                name: "Total",
                table: "Comandas",
                type: "decimal(65,30)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "Decimal(12,2)",
                oldPrecision: 12,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Faturamento",
                table: "AberturaDias",
                type: "decimal(65,30)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "Decimal(12,2)",
                oldPrecision: 12,
                oldScale: 2,
                oldNullable: true);
        }
    }
}
