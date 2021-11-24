using CourseProject.DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseProject.DAL.EntityExtensions; 

public static class ModelEntityExtensions {

    public static void Configure(this EntityTypeBuilder<Model> builder) {

        builder.HasIndex(m => m.Name).IsUnique();

        builder.HasData(new Model[] {
            new() { BrandId = 1, Name = "X445" },
            new() { BrandId = 1, Name = "X5" },
            new() { BrandId = 2, Name = "Q3" },
            new() { BrandId = 3, Name = "C34" },
            new() { BrandId = 4, Name = "Octavia" },
            new() { BrandId = 5, Name = "Jetta" },
            new() { BrandId = 6, Name = "XC90" },
        });
    }

}