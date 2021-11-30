namespace CourseProject.WEB.Models; 

public class ModelViewModel : BaseViewModel {
    public string Name { get; set; }

    public string NameWithBrand => Brand == null ? string.Empty : $"{Brand.Name} {Name}";

    public int BrandId { get; set; }

    public BrandViewModel Brand { get; set; }

    public ICollection<CarViewModel> Cars { get; set; }

    public int CarsCount => Cars == null ? 0 : Cars.Count();
}