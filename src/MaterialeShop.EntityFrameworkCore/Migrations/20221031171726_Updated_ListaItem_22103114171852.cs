using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MaterialeShop.Migrations
{
    public partial class Updated_ListaItem_22103114171852 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ListaId",
                schema: "apps",
                table: "ListaItems",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ListaItems_ListaId",
                schema: "apps",
                table: "ListaItems",
                column: "ListaId");

            migrationBuilder.AddForeignKey(
                name: "FK_ListaItems_Listas_ListaId",
                schema: "apps",
                table: "ListaItems",
                column: "ListaId",
                principalSchema: "apps",
                principalTable: "Listas",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ListaItems_Listas_ListaId",
                schema: "apps",
                table: "ListaItems");

            migrationBuilder.DropIndex(
                name: "IX_ListaItems_ListaId",
                schema: "apps",
                table: "ListaItems");

            migrationBuilder.DropColumn(
                name: "ListaId",
                schema: "apps",
                table: "ListaItems");
        }
    }
}
