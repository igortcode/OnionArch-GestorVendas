using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gestao.Data.Migrations
{
    public partial class V03 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItensComanda_Produtos_ProdutoId",
                table: "ItensComanda");

            migrationBuilder.AddColumn<DateTime>(
                name: "DtCriacao",
                table: "Comandas",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_ItensComanda_Produtos_ProdutoId",
                table: "ItensComanda",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItensComanda_Produtos_ProdutoId",
                table: "ItensComanda");

            migrationBuilder.DropColumn(
                name: "DtCriacao",
                table: "Comandas");

            migrationBuilder.AddForeignKey(
                name: "FK_ItensComanda_Produtos_ProdutoId",
                table: "ItensComanda",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
