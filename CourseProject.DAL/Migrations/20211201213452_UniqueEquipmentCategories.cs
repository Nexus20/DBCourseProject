using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseProject.DAL.Migrations
{
    public partial class UniqueEquipmentCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EquipmentItems_CarId",
                table: "EquipmentItems");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentItems_CarId_EquipmentItemCategoryId",
                table: "EquipmentItems",
                columns: new[] { "CarId", "EquipmentItemCategoryId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EquipmentItems_CarId_EquipmentItemCategoryId",
                table: "EquipmentItems");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentItems_CarId",
                table: "EquipmentItems",
                column: "CarId");
        }
    }
}
