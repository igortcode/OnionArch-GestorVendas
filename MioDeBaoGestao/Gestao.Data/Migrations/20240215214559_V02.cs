using Microsoft.EntityFrameworkCore.Migrations;

namespace Gestao.Data.Migrations
{
    public partial class V02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CPF",
                table: "Clientes");

            migrationBuilder.AddColumn<decimal>(
                name: "Total",
                table: "Comandas",
                type: "decimal(65,30)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CPF_Value",
                table: "Clientes",
                type: "varchar(14)",
                maxLength: 14,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "Aberta",
                table: "AberturaDias",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "Faturamento",
                table: "AberturaDias",
                type: "decimal(65,30)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Total",
                table: "Comandas");

            migrationBuilder.DropColumn(
                name: "CPF_Value",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "Aberta",
                table: "AberturaDias");

            migrationBuilder.DropColumn(
                name: "Faturamento",
                table: "AberturaDias");

            migrationBuilder.AddColumn<string>(
                name: "CPF",
                table: "Clientes",
                type: "varchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
