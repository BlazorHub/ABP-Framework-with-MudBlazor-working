using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MaterialeShop.Migrations
{
    public partial class Updated_Lista_22103114321688 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EnderecoId",
                schema: "apps",
                table: "Listas",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Listas_EnderecoId",
                schema: "apps",
                table: "Listas",
                column: "EnderecoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Listas_Enderecos_EnderecoId",
                schema: "apps",
                table: "Listas",
                column: "EnderecoId",
                principalSchema: "apps",
                principalTable: "Enderecos",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Listas_Enderecos_EnderecoId",
                schema: "apps",
                table: "Listas");

            migrationBuilder.DropIndex(
                name: "IX_Listas_EnderecoId",
                schema: "apps",
                table: "Listas");

            migrationBuilder.DropColumn(
                name: "EnderecoId",
                schema: "apps",
                table: "Listas");
        }
    }
}
