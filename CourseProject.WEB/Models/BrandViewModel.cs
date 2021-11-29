namespace CourseProject.WEB.Models; 

public class BrandViewModel : BaseViewModel {
    public string Name { get; set; }

    public ICollection<ModelViewModel> Models { get; set; }

    public ICollection<SupplierViewModel> Suppliers { get; set; }
}