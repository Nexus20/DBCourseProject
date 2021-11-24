using CourseProject.DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseProject.DAL.EntityExtensions; 

public static class BrandEntityExtensions {

    public static void Configure(this EntityTypeBuilder<Brand> builder) {

        builder.HasIndex(b => b.Name).IsUnique();

        builder.HasData(new Brand[] {
            new() { Name = "BMW" },
            new() { Name = "Audi" },
            new() { Name = "Citroen" },
            new() { Name = "Skoda" },
            new() { Name = "Volkswagen" },
            new() { Name = "Volvo" },
        });
    }

}