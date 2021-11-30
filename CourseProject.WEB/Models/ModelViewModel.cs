namespace CourseProject.WEB.Models; 

public class ModelViewModel : BaseViewModel {
    public string Name { get; set; }

    public int BrandId { get; set; }

    public BrandViewModel Brand { get; set; }

    public ICollection<CarViewModel> Cars { get; set; }

    public int CarsCount => Cars == null ? 0 : Cars.Count();
}