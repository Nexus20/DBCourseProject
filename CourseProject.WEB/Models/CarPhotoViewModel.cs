namespace CourseProject.WEB.Models; 

public class CarPhotoViewModel : BaseViewModel {

    public string Path { get; set; }

    public int CarId { get; set; }

    public CarViewModel Car { get; set; }
}