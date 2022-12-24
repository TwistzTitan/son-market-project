using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace market.Migrations
{
    public partial class ChangedSaida : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ValorVenda",
                table: "Saidas",
                newName: "Valor");

            migrationBuilder.AddColumn<float>(
                name: "Quantidade",
                table: "Saidas",
                type: "float",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "VendaId",
                table: "Saidas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Saidas_VendaId",
                table: "Saidas",
                column: "VendaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Saidas_Vendas_VendaId",
                table: "Saidas",
                column: "VendaId",
                principalTable: "Vendas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Saidas_Vendas_VendaId",
                table: "Saidas");

            migrationBuilder.DropIndex(
                name: "IX_Saidas_VendaId",
                table: "Saidas");

            migrationBuilder.DropColumn(
                name: "Quantidade",
                table: "Saidas");

            migrationBuilder.DropColumn(
                name: "VendaId",
                table: "Saidas");

            migrationBuilder.RenameColumn(
                name: "Valor",
                table: "Saidas",
                newName: "ValorVenda");
        }
    }
}
