using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LojaShopping.CupomAPI.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarCupomBase01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "cupom",
                columns: new[] { "id", "Cod_Cupom", "Desconto_Total" },
                values: new object[,]
                {
                    { 1L, "ERUDIO_2022_10", 10m },
                    { 2L, "ERUDIO_2022_15", 15m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "cupom",
                keyColumn: "id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "cupom",
                keyColumn: "id",
                keyValue: 2L);
        }
    }
}
