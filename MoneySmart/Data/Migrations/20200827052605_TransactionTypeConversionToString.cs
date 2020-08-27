using Microsoft.EntityFrameworkCore.Migrations;

namespace MoneySmart.Data.Migrations
{
    public partial class TransactionTypeConversionToString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TransactionType",
                table: "Transactions",
                maxLength: 7,
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TransactionType",
                table: "Transactions",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 7);
        }
    }
}
