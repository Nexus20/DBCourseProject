using CourseProject.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseProject.DAL.EntityExtensions; 

public static class ClientEntityExtensions {

    public static void Configure(this EntityTypeBuilder<Client> builder) {

        builder.HasMany(m => m.PurchaseOrders)
            .WithOne(o => o.Client)
            .HasForeignKey(o => o.ClientId)
            .OnDelete(DeleteBehavior.Restrict);
    }

}