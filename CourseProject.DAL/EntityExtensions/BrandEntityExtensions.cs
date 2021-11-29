using CourseProject.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseProject.DAL.EntityExtensions; 

public static class BrandEntityExtensions {

    public static void Configure(this EntityTypeBuilder<Brand> builder) {

        builder.HasIndex(b => b.Name).IsUnique();

        builder.HasMany(b => b.Suppliers)
            .WithOne(s => s.Brand)
            .HasForeignKey(s => s.BrandId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(b => b.Models)
            .WithOne(m => m.Brand)
            .HasForeignKey(m => m.BrandId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasData(new Brand[] {
            new() { Id = 1, Name = "BMW" },
            new() { Id = 2, Name = "Audi" },
            new() { Id = 3, Name = "Citroen" },
            new() { Id = 4, Name = "Skoda" },
            new() { Id = 5, Name = "Volkswagen" },
            new() { Id = 6, Name = "Volvo" },
        });
    }

}