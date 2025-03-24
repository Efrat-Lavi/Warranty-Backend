using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Warranty.Data.Migrations
{
    /// <inheritdoc />
    public partial class changeUsersToRecordsInWarranty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Warranties_WarrantyModelId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_WarrantyModelId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "WarrantyModelId",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WarrantyModelId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_WarrantyModelId",
                table: "Users",
                column: "WarrantyModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Warranties_WarrantyModelId",
                table: "Users",
                column: "WarrantyModelId",
                principalTable: "Warranties",
                principalColumn: "Id");
        }
    }
}
