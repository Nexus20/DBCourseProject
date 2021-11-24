using CourseProject.DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseProject.DAL.EntityExtensions; 

public static class CarInStockEntityExtensions {

    public static void Configure(this EntityTypeBuilder<CarInStock> builder) {

        builder.HasIndex(c => c.VinCode).IsUnique();

        builder.HasData(new CarInStock[] {
            new() { Id = 1, ShowroomId = 1, CarId = 1, VinCode = "12345678912345671"},
            new() { Id = 2, ShowroomId = 1, CarId = 3, VinCode = "12345678912345672"},
            new() { Id = 3, ShowroomId = 1, CarId = 5, VinCode = "12345678912345673"},
            new() { Id = 4, ShowroomId = 2, CarId = 2, VinCode = "12345678912345674"},
            new() { Id = 5, ShowroomId = 2, CarId = 4, VinCode = "12345678912345675"},
            new() { Id = 6, ShowroomId = 2, CarId = 6, VinCode = "12345678912345676"},
            new() { Id = 7, ShowroomId = 3, CarId = 3, VinCode = "12345678912345677"},
            new() { Id = 8, ShowroomId = 3, CarId = 5, VinCode = "12345678912345678"},
            new() { Id = 9, ShowroomId = 3, CarId = 9, VinCode = "12345678912345679"},
            new() { Id = 10, ShowroomId = 3, CarId = 9, VinCode = "12345678912345670"},
        });
    }

}