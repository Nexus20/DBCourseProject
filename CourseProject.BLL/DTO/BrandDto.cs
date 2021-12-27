namespace CourseProject.BLL.DTO; 

public class BrandDto : BaseDto {
    public string Name { get; set; }

    public ICollection<ModelDto> Models { get; set; }

    public ICollection<SupplierDto> Suppliers { get; set; }
}