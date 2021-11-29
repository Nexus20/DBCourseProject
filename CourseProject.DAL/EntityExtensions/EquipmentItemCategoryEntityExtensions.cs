using CourseProject.DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseProject.DAL.EntityExtensions; 

public static class EquipmentItemCategoryEntityExtensions {

    public static void Configure(this EntityTypeBuilder<EquipmentItemCategory> builder) {

        builder.HasData(new EquipmentItemCategory[] {
            new() { Id = 1, UnitsOfMeasure = "", Name = "Engine" },
            new() { Id = 2, UnitsOfMeasure = "",  Name = "Color" },
            new() { Id = 3, UnitsOfMeasure = "",  Name = "Transmission" },
        });
    }

}