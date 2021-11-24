using CourseProject.DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseProject.DAL.EntityExtensions; 

public static class CarInStockEquipmentItemValueEntityExtensions {

    public static void Configure(this EntityTypeBuilder<CarInStockEquipmentItemValue> builder) {

        builder.HasKey(c => new {c.CarInStockId, c.EquipmentItemValueId});

        builder.HasData(new CarInStockEquipmentItemValue[] {
            //new() { CarInStockId = , EquipmentItemValueId = },
        });
    }

}