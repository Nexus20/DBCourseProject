using CourseProject.DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseProject.DAL.EntityExtensions; 

public static class PurchaseOrderEquipmentItemValueEntityExtensions {

    public static void Configure(this EntityTypeBuilder<PurchaseOrderEquipmentItemValue> builder) {

        builder.HasKey(p => new {p.EquipmentItemValueId, p.PurchaseOrderId});

        builder.HasData(new PurchaseOrderEquipmentItemValue[] {
            //new() { PurchaseOrderId = , EquipmentItemValueId = },
        });
    }

}