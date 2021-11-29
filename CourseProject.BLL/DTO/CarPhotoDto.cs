namespace CourseProject.BLL.DTO; 

public class CarPhotoDto : BaseDto {

    public string Path { get; set; }

    public int CarId { get; set; }

    public CarDto Car { get; set; }
}