using CourseProject.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseProject.DAL.EntityExtensions; 

public static class ShowroomEntityExtensions {

    public static void Configure(this EntityTypeBuilder<Showroom> builder) {

        builder.HasMany(s => s.PurchaseOrders)
            .WithOne(p => p.Showroom)
            .HasForeignKey(p => p.ShowroomId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(s => new {s.City, s.Street, s.House}).IsUnique();

        builder.HasData(new Showroom[] {
            new() { Id = 1, City = "City 1", Street = "Street 1", House = "11", Phone = "0987654321"},
            new() { Id = 2, City = "City 1", Street = "Street 2", House = "11", Phone = "0987654322"},
            new() { Id = 3, City = "City 2", Street = "Street 12", House = "1", Phone = "0997654322"},
        });
    }

}