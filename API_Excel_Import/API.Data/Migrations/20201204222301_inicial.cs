using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Data.Migrations
{
    public partial class inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LoteEntregas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DescricaoLoteArquivo = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    DataImportacao = table.Column<DateTime>(type: "datetime", nullable: false),
                    NumeroRegistros = table.Column<int>(type: "int", nullable: false),
                    NumeroTotalProdutos = table.Column<int>(type: "int", nullable: false),
                    ValorTotalImportado = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    DataEntregaMenor = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoteEntregas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExcelDatas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdLoteEntrega = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataEntrega = table.Column<DateTime>(type: "date", nullable: false),
                    DescricaoProduto = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    ValorUnitario = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    QtdProduto = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExcelDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExcelDatas_LoteEntregas_IdLoteEntrega",
                        column: x => x.IdLoteEntrega,
                        principalTable: "LoteEntregas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExcelDatas_IdLoteEntrega",
                table: "ExcelDatas",
                column: "IdLoteEntrega");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExcelDatas");

            migrationBuilder.DropTable(
                name: "LoteEntregas");
        }
    }
}
