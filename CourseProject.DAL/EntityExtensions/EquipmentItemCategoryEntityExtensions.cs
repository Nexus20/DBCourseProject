using CourseProject.DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseProject.DAL.EntityExtensions; 

public static class EquipmentItemCategoryEntityExtensions {

    public static void Configure(this EntityTypeBuilder<EquipmentItemCategory> builder) {

        builder.HasData(new EquipmentItemCategory[] {
            new() { Name = "Engine" },
            new() { Name = "Color" },
            new() { Name = "Transmission" },
        });
    }

}