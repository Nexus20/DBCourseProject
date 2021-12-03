using CourseProject.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseProject.DAL.EntityExtensions; 

public static class ManagerEntityExtensions {

    public static void Configure(this EntityTypeBuilder<Manager> builder) {

        builder.Property(m => m.Id).ValueGeneratedOnAdd();

        builder.HasMany(m => m.PurchaseOrders)
            .WithOne(o => o.Manager)
            .HasForeignKey(o => o.ManagerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(m => m.SupplyOrders)
            .WithOne(o => o.Manager)
            .HasForeignKey(o => o.ManagerId)
            .OnDelete(DeleteBehavior.Restrict);
    }

}