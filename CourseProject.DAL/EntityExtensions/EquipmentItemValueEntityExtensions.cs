using CourseProject.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseProject.DAL.EntityExtensions; 

public static class EquipmentItemValueEntityExtensions {

    public static void Configure(this EntityTypeBuilder<EquipmentItemValue> builder) {

        builder.HasMany(e => e.CarInStockEquipmentItemValues)
            .WithOne(c => c.EquipmentItemValue)
            .HasForeignKey(e => e.EquipmentItemValueId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(e => e.PurchaseOrderEquipmentItemsValues)
            .WithOne(c => c.EquipmentItemValue)
            .HasForeignKey(e => e.EquipmentItemValueId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(e => e.SupplyOrderPartEquipmentItemsValues)
            .WithOne(c => c.EquipmentItemValue)
            .HasForeignKey(e => e.EquipmentItemValueId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasData(new EquipmentItemValue[] {
            new() { Id = 1, EquipmentItemId = 1, Value = "Engine 1", Price = 10000 },
            new() { Id = 2, EquipmentItemId = 1, Value = "Engine 2", Price = 15000 },
            new() { Id = 3, EquipmentItemId = 1, Value = "Engine 3", Price = 8000 },
            new() { Id = 4, EquipmentItemId = 2, Value = "Color 1", Price = 1000 },
            new() { Id = 5, EquipmentItemId = 2, Value = "Color 2", Price = 1500 },
            new() { Id = 6, EquipmentItemId = 2, Value = "Color 3", Price = 800 },
            new() { Id = 7, EquipmentItemId = 3, Value = "Transmission 1", Price = 15000 },
            new() { Id = 8, EquipmentItemId = 3, Value = "Transmission 2", Price = 12000 },
            new() { Id = 9, EquipmentItemId = 3, Value = "Transmission 3", Price = 9000 },
            new() { Id = 10, EquipmentItemId = 4, Value = "Engine 1", Price = 10000 },
            new() { Id = 11, EquipmentItemId = 4, Value = "Engine 2", Price = 15000 },
            new() { Id = 12, EquipmentItemId = 4, Value = "Engine 3", Price = 8000 },
            new() { Id = 13, EquipmentItemId = 5, Value = "Color 1", Price = 1000 },
            new() { Id = 14, EquipmentItemId = 5, Value = "Color 2", Price = 1500 },
            new() { Id = 15, EquipmentItemId = 5, Value = "Color 3", Price = 800 },
            new() { Id = 16, EquipmentItemId = 6, Value = "Transmission 1", Price = 15000 },
            new() { Id = 17, EquipmentItemId = 6, Value = "Transmission 2", Price = 12000 },
            new() { Id = 18, EquipmentItemId = 6, Value = "Transmission 3", Price = 9000 },
            new() { Id = 19, EquipmentItemId = 7, Value = "Engine 1", Price = 10000 },
            new() { Id = 20, EquipmentItemId = 7, Value = "Engine 2", Price = 15000 },
            new() { Id = 21, EquipmentItemId = 7, Value = "Engine 3", Price = 8000 },
            new() { Id = 22, EquipmentItemId = 8, Value = "Color 1", Price = 1000 },
            new() { Id = 23, EquipmentItemId = 8, Value = "Color 2", Price = 1500 },
            new() { Id = 24, EquipmentItemId = 8, Value = "Color 3", Price = 800 },
            new() { Id = 25, EquipmentItemId = 9, Value = "Transmission 1", Price = 15000 },
            new() { Id = 26, EquipmentItemId = 9, Value = "Transmission 2", Price = 12000 },
            new() { Id = 27, EquipmentItemId = 9, Value = "Transmission 3", Price = 9000 },
            new() { Id = 28, EquipmentItemId = 10, Value = "Engine 1", Price = 10000 },
            new() { Id = 29, EquipmentItemId = 10, Value = "Engine 2", Price = 15000 },
            new() { Id = 30, EquipmentItemId = 10, Value = "Engine 3", Price = 8000 },
            new() { Id = 31, EquipmentItemId = 11, Value = "Color 1", Price = 1000 },
            new() { Id = 32, EquipmentItemId = 11, Value = "Color 2", Price = 1500 },
            new() { Id = 33, EquipmentItemId = 11, Value = "Color 3", Price = 800 },
            new() { Id = 34, EquipmentItemId = 12, Value = "Transmission 1", Price = 15000 },
            new() { Id = 35, EquipmentItemId = 12, Value = "Transmission 2", Price = 12000 },
            new() { Id = 36, EquipmentItemId = 12, Value = "Transmission 3", Price = 9000 },
            new() { Id = 37, EquipmentItemId = 13, Value = "Engine 1", Price = 10000 },
            new() { Id = 38, EquipmentItemId = 13, Value = "Engine 2", Price = 15000 },
            new() { Id = 39, EquipmentItemId = 13, Value = "Engine 3", Price = 8000 },
            new() { Id = 40, EquipmentItemId = 14, Value = "Color 1", Price = 1000 },
            new() { Id = 41, EquipmentItemId = 14, Value = "Color 2", Price = 1500 },
            new() { Id = 42, EquipmentItemId = 14, Value = "Color 3", Price = 800 },
            new() { Id = 43, EquipmentItemId = 15, Value = "Transmission 1", Price = 15000 },
            new() { Id = 44, EquipmentItemId = 15, Value = "Transmission 2", Price = 12000 },
            new() { Id = 45, EquipmentItemId = 15, Value = "Transmission 3", Price = 9000 },
            new() { Id = 46, EquipmentItemId = 16, Value = "Engine 1", Price = 10000 },
            new() { Id = 47, EquipmentItemId = 16, Value = "Engine 2", Price = 15000 },
            new() { Id = 48, EquipmentItemId = 16, Value = "Engine 3", Price = 8000 },
            new() { Id = 49, EquipmentItemId = 17, Value = "Color 1", Price = 1000 },
            new() { Id = 50, EquipmentItemId = 17, Value = "Color 2", Price = 1500 },
            new() { Id = 51, EquipmentItemId = 17, Value = "Color 3", Price = 800 },
            new() { Id = 52, EquipmentItemId = 18, Value = "Transmission 1", Price = 15000 },
            new() { Id = 53, EquipmentItemId = 18, Value = "Transmission 2", Price = 12000 },
            new() { Id = 54, EquipmentItemId = 18, Value = "Transmission 3", Price = 9000 },
            new() { Id = 55, EquipmentItemId = 19, Value = "Engine 1", Price = 10000 },
            new() { Id = 56, EquipmentItemId = 19, Value = "Engine 2", Price = 15000 },
            new() { Id = 57, EquipmentItemId = 19, Value = "Engine 3", Price = 8000 },
            new() { Id = 58, EquipmentItemId = 20, Value = "Color 1", Price = 1000 },
            new() { Id = 59, EquipmentItemId = 20, Value = "Color 2", Price = 1500 },
            new() { Id = 60, EquipmentItemId = 20, Value = "Color 3", Price = 800 },
            new() { Id = 61, EquipmentItemId = 21, Value = "Transmission 1", Price = 15000 },
            new() { Id = 62, EquipmentItemId = 21, Value = "Transmission 2", Price = 12000 },
            new() { Id = 63, EquipmentItemId = 21, Value = "Transmission 3", Price = 9000 },
            new() { Id = 64, EquipmentItemId = 22, Value = "Engine 1", Price = 10000 },
            new() { Id = 65, EquipmentItemId = 22, Value = "Engine 2", Price = 15000 },
            new() { Id = 66, EquipmentItemId = 22, Value = "Engine 3", Price = 8000 },
            new() { Id = 67, EquipmentItemId = 23, Value = "Color 1", Price = 1000 },
            new() { Id = 68, EquipmentItemId = 23, Value = "Color 2", Price = 1500 },
            new() { Id = 69, EquipmentItemId = 23, Value = "Color 3", Price = 800 },
            new() { Id = 70, EquipmentItemId = 24, Value = "Transmission 1", Price = 15000 },
            new() { Id = 71, EquipmentItemId = 24, Value = "Transmission 2", Price = 12000 },
            new() { Id = 72, EquipmentItemId = 24, Value = "Transmission 3", Price = 9000 },
            new() { Id = 73, EquipmentItemId = 25, Value = "Engine 1", Price = 10000 },
            new() { Id = 74, EquipmentItemId = 25, Value = "Engine 2", Price = 15000 },
            new() { Id = 75, EquipmentItemId = 25, Value = "Engine 3", Price = 8000 },
            new() { Id = 76, EquipmentItemId = 26, Value = "Color 1", Price = 1000 },
            new() { Id = 77, EquipmentItemId = 26, Value = "Color 2", Price = 1500 },
            new() { Id = 78, EquipmentItemId = 26, Value = "Color 3", Price = 800 },
            new() { Id = 79, EquipmentItemId = 27, Value = "Transmission 1", Price = 15000 },
            new() { Id = 80, EquipmentItemId = 27, Value = "Transmission 2", Price = 12000 },
            new() { Id = 81, EquipmentItemId = 27, Value = "Transmission 3", Price = 9000 },
            new() { Id = 82, EquipmentItemId = 28, Value = "Engine 1", Price = 10000 },
            new() { Id = 83, EquipmentItemId = 28, Value = "Engine 2", Price = 15000 },
            new() { Id = 84, EquipmentItemId = 28, Value = "Engine 3", Price = 8000 },
            new() { Id = 85, EquipmentItemId = 29, Value = "Color 1", Price = 1000 },
            new() { Id = 86, EquipmentItemId = 29, Value = "Color 2", Price = 1500 },
            new() { Id = 87, EquipmentItemId = 29, Value = "Color 3", Price = 800 },
            new() { Id = 88, EquipmentItemId = 30, Value = "Transmission 1", Price = 15000 },
            new() { Id = 89, EquipmentItemId = 30, Value = "Transmission 2", Price = 12000 },
            new() { Id = 90, EquipmentItemId = 30, Value = "Transmission 3", Price = 9000 },
        });
    }

}