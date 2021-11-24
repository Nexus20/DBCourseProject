using CourseProject.DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseProject.DAL.EntityExtensions; 

public static class SupplyOrderPartEquipmentItemValueEntityExtensions {

    public static void Configure(this EntityTypeBuilder<SupplyOrderPartEquipmentItemValue> builder) {

        builder.HasKey(s => new {s.EquipmentItemValueId, s.SupplyOrderPartId});

        builder.HasData(new SupplyOrderPartEquipmentItemValue[] {
            //new() { SupplyOrderPartId = , EquipmentItemValueId = },
        });
    }

}