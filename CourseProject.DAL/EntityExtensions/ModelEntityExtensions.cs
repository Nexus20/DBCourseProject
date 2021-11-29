using CourseProject.DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseProject.DAL.EntityExtensions; 

public static class ModelEntityExtensions {

    public static void Configure(this EntityTypeBuilder<Model> builder) {

        builder.HasIndex(m => m.Name).IsUnique();

        builder.HasData(new Model[] {
            new() { Id = 1, BrandId = 1, Name = "X445" },
            new() { Id = 2, BrandId = 1, Name = "X5" },
            new() { Id = 3, BrandId = 2, Name = "Q3" },
            new() { Id = 4, BrandId = 3, Name = "C34" },
            new() { Id = 5, BrandId = 4, Name = "Octavia" },
            new() { Id = 6, BrandId = 5, Name = "Jetta" },
            new() { Id = 7, BrandId = 6, Name = "XC90" },
        });
    }

}