using System.ComponentModel.DataAnnotations;

namespace CourseProject.WEB.Models; 

public class BrandViewModel : BaseViewModel {
    
    [Required]
    [Display(Name = "Brand")]
    public string Name { get; set; }

    public ICollection<ModelViewModel> Models { get; set; }

    public ICollection<SupplierViewModel> Suppliers { get; set; }

    [Display(Name = "Models count")]
    public int ModelsCount => Models == null ? 0 : Models.Count;

    [Display(Name = "Cars count")]
    public int CarsCount => Models == null ? 0 : Models.Sum(m => m.Cars.Count);

    [Display(Name = "Suppliers count")]
    public int SuppliersCount => Suppliers == null ? 0 : Suppliers.Count;
}