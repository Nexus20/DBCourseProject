using CourseProject.DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseProject.DAL.EntityExtensions; 

public static class SupplierEntityExtensions {

    public static void Configure(this EntityTypeBuilder<Supplier> builder) {

        builder.HasIndex(m => m.Name).IsUnique();

        builder.HasData(new Supplier[] {
            new() { Id = 1, BrandId = 1, Phone = "0998765431", Email = "BMWsupplier1@gmail.com", Name = "BMW supplier 1" },
            new() { Id = 2, BrandId = 1, Phone = "0998765432", Email = "BMWsupplier2@gmail.com", Name = "BMW supplier 2" },
            new() { Id = 3, BrandId = 2, Phone = "0998765433", Email = "AudiSupplier1@gmail.com", Name = "Audi supplier 1" },
            new() { Id = 4, BrandId = 3, Phone = "0998765434", Email = "CitroenSupplier@gmail.com", Name = "Citroen supplier 2" },
            new() { Id = 5, BrandId = 4, Phone = "0998765435", Email = "SkodaSupplier@gmail.com", Name = "Skoda supplier 1" },
            new() { Id = 6, BrandId = 5, Phone = "0998765436", Email = "VolkswagenSupplier@gmail.com", Name = "Volkswagen supplier 1" },
            new() { Id = 7, BrandId = 6, Phone = "0998765437", Email = "VolvoSupplier@gmail.com", Name = "Volvo supplier 1" },
        });
    }

}