using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseProject.DAL.Migrations
{
    public partial class PurchaseOrderVinCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VinCode",
                table: "PurchaseOrders",
                type: "nvarchar(17)",
                maxLength: 17,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_VinCode",
                table: "PurchaseOrders",
                column: "VinCode",
                unique: true,
                filter: "[VinCode] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PurchaseOrders_VinCode",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "VinCode",
                table: "PurchaseOrders");
        }
    }
}
