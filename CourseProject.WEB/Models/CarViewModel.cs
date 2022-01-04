using System.ComponentModel.DataAnnotations;

namespace CourseProject.WEB.Models; 

public class CarViewModel : BaseViewModel {
    public string Submodel { get; set; }

    public int ModelId { get; set; }

    public ModelViewModel Model { get; set; }

    [Display(Name = "Full model")]
    public string FullModel => $"{Model.Brand.Name} {Model.Name} {Submodel}";

    public ICollection<CarPhotoViewModel> Photos { get; set; }

    [Display(Name = "Available equipment")]
    public ICollection<EquipmentItemViewModel> EquipmentItems { get; set; }
}