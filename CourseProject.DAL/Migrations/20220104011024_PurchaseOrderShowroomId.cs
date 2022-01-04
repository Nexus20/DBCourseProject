using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseProject.DAL.Migrations
{
    public partial class PurchaseOrderShowroomId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShowroomId",
                table: "PurchaseOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_ShowroomId",
                table: "PurchaseOrders",
                column: "ShowroomId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrders_Showrooms_ShowroomId",
                table: "PurchaseOrders",
                column: "ShowroomId",
                principalTable: "Showrooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrders_Showrooms_ShowroomId",
                table: "PurchaseOrders");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseOrders_ShowroomId",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "ShowroomId",
                table: "PurchaseOrders");
        }
    }
}
