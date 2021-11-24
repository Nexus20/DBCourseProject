using CourseProject.DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseProject.DAL.EntityExtensions; 

public static class CarEntityExtensions {

    public static void Configure(this EntityTypeBuilder<Car> builder) {

        builder.HasIndex(c => c.Submodel).IsUnique();

        builder.HasData(new Car[] {
            new() { ModelId = 3, Submodel = "Sportback 40 TFSI quattro S line" },
            new() { ModelId = 3, Submodel = "Sportback 35 TFSI" },
            new() { ModelId = 4, Submodel = "Aircross" },
            new() { ModelId = 4, Submodel = "Aircross New" },
            new() { ModelId = 5, Submodel = "A5" },
            new() { ModelId = 5, Submodel = "A7" },
            new() { ModelId = 6, Submodel = "III" },
            new() { ModelId = 6, Submodel = "IV" },
            new() { ModelId = 7, Submodel = "B5 (D) Momentum Pro AWD" },
            new() { ModelId = 2, Submodel = "xDrive40i" },
        });
    }

}