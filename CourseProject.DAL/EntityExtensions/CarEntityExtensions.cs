using CourseProject.DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseProject.DAL.EntityExtensions; 

public static class CarEntityExtensions {

    public static void Configure(this EntityTypeBuilder<Car> builder) {

        builder.HasIndex(c => c.Submodel).IsUnique();

        builder.HasData(new Car[] {
            new() { Id = 1, ModelId = 3, Submodel = "Sportback 40 TFSI quattro S line" },
            new() { Id = 2, ModelId = 3, Submodel = "Sportback 35 TFSI" },
            new() { Id = 3, ModelId = 4, Submodel = "Aircross" },
            new() { Id = 4, ModelId = 4, Submodel = "Aircross New" },
            new() { Id = 5, ModelId = 5, Submodel = "A5" },
            new() { Id = 6, ModelId = 5, Submodel = "A7" },
            new() { Id = 7, ModelId = 6, Submodel = "III" },
            new() { Id = 8, ModelId = 6, Submodel = "IV" },
            new() { Id = 9, ModelId = 7, Submodel = "B5 (D) Momentum Pro AWD" },
            new() { Id = 10, ModelId = 2, Submodel = "xDrive40i" },
        });
    }

}