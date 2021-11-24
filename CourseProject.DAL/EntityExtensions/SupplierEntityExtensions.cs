using CourseProject.DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseProject.DAL.EntityExtensions; 

public static class SupplierEntityExtensions {

    public static void Configure(this EntityTypeBuilder<Supplier> builder) {

        builder.HasIndex(m => m.Name).IsUnique();

        builder.HasData(new Supplier[] {
            new() { BrandId = 1, Name = "BMW supplier 1" },
            new() { BrandId = 1, Name = "BMW supplier 2" },
            new() { BrandId = 2, Name = "Audi supplier 1" },
            new() { BrandId = 3, Name = "Citroen supplier 2" },
            new() { BrandId = 4, Name = "Skoda supplier 1" },
            new() { BrandId = 5, Name = "Volkswagen supplier 1" },
            new() { BrandId = 6, Name = "Volvo supplier 1" },
        });
    }

}