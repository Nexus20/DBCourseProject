using CourseProject.DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseProject.DAL.EntityExtensions; 

public static class EquipmentItemEntityExtensions {

    public static void Configure(this EntityTypeBuilder<EquipmentItem> builder) {

        builder.HasIndex(b => new { b.CarId, b.EquipmentItemCategoryId }).IsUnique();

        builder.HasData(new EquipmentItem[] {
            new() { Id = 1, EquipmentItemCategoryId = 1, CarId = 1, Optional = false},
            new() { Id = 2, EquipmentItemCategoryId = 2, CarId = 1, Optional = false},
            new() { Id = 3, EquipmentItemCategoryId = 3, CarId = 1, Optional = false},
            new() { Id = 4, EquipmentItemCategoryId = 1, CarId = 2, Optional = false},
            new() { Id = 5, EquipmentItemCategoryId = 2, CarId = 2, Optional = false},
            new() { Id = 6, EquipmentItemCategoryId = 3, CarId = 2, Optional = false},
            new() { Id = 7, EquipmentItemCategoryId = 1, CarId = 3, Optional = false},
            new() { Id = 8, EquipmentItemCategoryId = 2, CarId = 3, Optional = false},
            new() { Id = 9, EquipmentItemCategoryId = 3, CarId = 3, Optional = false},
            new() { Id = 10, EquipmentItemCategoryId = 1, CarId = 4, Optional = false},
            new() { Id = 11, EquipmentItemCategoryId = 2, CarId = 4, Optional = false},
            new() { Id = 12, EquipmentItemCategoryId = 3, CarId = 4, Optional = false},
            new() { Id = 13, EquipmentItemCategoryId = 1, CarId = 5, Optional = false},
            new() { Id = 14, EquipmentItemCategoryId = 2, CarId = 5, Optional = false},
            new() { Id = 15, EquipmentItemCategoryId = 3, CarId = 5, Optional = false},
            new() { Id = 16, EquipmentItemCategoryId = 1, CarId = 6, Optional = false},
            new() { Id = 17, EquipmentItemCategoryId = 2, CarId = 6, Optional = false},
            new() { Id = 18, EquipmentItemCategoryId = 3, CarId = 6, Optional = false},
            new() { Id = 19, EquipmentItemCategoryId = 1, CarId = 7, Optional = false},
            new() { Id = 20, EquipmentItemCategoryId = 2, CarId = 7, Optional = false},
            new() { Id = 21, EquipmentItemCategoryId = 3, CarId = 7, Optional = false},
            new() { Id = 22, EquipmentItemCategoryId = 1, CarId = 8, Optional = false},
            new() { Id = 23, EquipmentItemCategoryId = 2, CarId = 8, Optional = false},
            new() { Id = 24, EquipmentItemCategoryId = 3, CarId = 8, Optional = false},
            new() { Id = 25, EquipmentItemCategoryId = 1, CarId = 9, Optional = false},
            new() { Id = 26, EquipmentItemCategoryId = 2, CarId = 9, Optional = false},
            new() { Id = 27, EquipmentItemCategoryId = 3, CarId = 9, Optional = false},
            new() { Id = 28, EquipmentItemCategoryId = 1, CarId = 10, Optional = false},
            new() { Id = 29, EquipmentItemCategoryId = 2, CarId = 10, Optional = false},
            new() { Id = 30, EquipmentItemCategoryId = 3, CarId = 10, Optional = false},
        });
    }

}