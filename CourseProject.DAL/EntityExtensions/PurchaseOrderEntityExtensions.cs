using CourseProject.DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseProject.DAL.EntityExtensions; 

public static class PurchaseOrderEntityExtensions {

    public static void Configure(this EntityTypeBuilder<PurchaseOrder> builder) {

        builder.HasIndex(x => x.VinCode).IsUnique();

        builder.Property(x => x.VinCode).HasMaxLength(17);
    }

}