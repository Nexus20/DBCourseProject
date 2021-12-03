using CourseProject.DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseProject.DAL.EntityExtensions; 

public static class UserEntityExtensions {

    public static void Configure(this EntityTypeBuilder<User> builder) {

        builder.Property(u => u.Id).ValueGeneratedOnAdd();
    }

}