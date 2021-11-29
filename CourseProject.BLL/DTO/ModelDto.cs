namespace CourseProject.BLL.DTO; 

public class ModelDto : BaseDto {
    public string Name { get; set; }

    public int BrandId { get; set; }

    public BrandDto Brand { get; set; }

    public ICollection<CarDto> Cars { get; set; }
}