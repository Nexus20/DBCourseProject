using CourseProject.DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseProject.DAL.EntityExtensions; 

public static class EquipmentItemEntityExtensions {

    public static void Configure(this EntityTypeBuilder<EquipmentItem> builder) {

        builder.HasData(new EquipmentItem[] {
            new() { EquipmentItemCategoryId = 1, CarId = 1, Optional = false},
            new() { EquipmentItemCategoryId = 2, CarId = 1, Optional = false},
            new() { EquipmentItemCategoryId = 3, CarId = 1, Optional = false},
            new() { EquipmentItemCategoryId = 1, CarId = 2, Optional = false},
            new() { EquipmentItemCategoryId = 2, CarId = 2, Optional = false},
            new() { EquipmentItemCategoryId = 3, CarId = 2, Optional = false},
            new() { EquipmentItemCategoryId = 1, CarId = 3, Optional = false},
            new() { EquipmentItemCategoryId = 2, CarId = 3, Optional = false},
            new() { EquipmentItemCategoryId = 3, CarId = 3, Optional = false},
            new() { EquipmentItemCategoryId = 1, CarId = 4, Optional = false},
            new() { EquipmentItemCategoryId = 2, CarId = 4, Optional = false},
            new() { EquipmentItemCategoryId = 3, CarId = 4, Optional = false},
            new() { EquipmentItemCategoryId = 1, CarId = 5, Optional = false},
            new() { EquipmentItemCategoryId = 2, CarId = 5, Optional = false},
            new() { EquipmentItemCategoryId = 3, CarId = 5, Optional = false},
            new() { EquipmentItemCategoryId = 1, CarId = 6, Optional = false},
            new() { EquipmentItemCategoryId = 2, CarId = 6, Optional = false},
            new() { EquipmentItemCategoryId = 3, CarId = 6, Optional = false},
            new() { EquipmentItemCategoryId = 1, CarId = 7, Optional = false},
            new() { EquipmentItemCategoryId = 2, CarId = 7, Optional = false},
            new() { EquipmentItemCategoryId = 3, CarId = 7, Optional = false},
            new() { EquipmentItemCategoryId = 1, CarId = 8, Optional = false},
            new() { EquipmentItemCategoryId = 2, CarId = 8, Optional = false},
            new() { EquipmentItemCategoryId = 3, CarId = 8, Optional = false},
            new() { EquipmentItemCategoryId = 1, CarId = 9, Optional = false},
            new() { EquipmentItemCategoryId = 2, CarId = 9, Optional = false},
            new() { EquipmentItemCategoryId = 3, CarId = 9, Optional = false},
            new() { EquipmentItemCategoryId = 1, CarId = 10, Optional = false},
            new() { EquipmentItemCategoryId = 2, CarId = 10, Optional = false},
            new() { EquipmentItemCategoryId = 3, CarId = 10, Optional = false},
        });
    }

}