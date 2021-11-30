namespace CourseProject.WEB.Models; 

public class CreateEditCarViewModel : BaseViewModel {
    public string Submodel { get; set; }

    public int ModelId { get; set; }

    public IFormFileCollection Images { get; set; }
}