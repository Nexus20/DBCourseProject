namespace CourseProject.WEB.Models; 

public class CarViewModel : BaseViewModel {
    public string Submodel { get; set; }

    public int ModelId { get; set; }

    public ModelViewModel Model { get; set; }

    public string FullModel => $"{Model.Brand.Name} {Model.Name} {Submodel}";

    public ICollection<CarPhotoViewModel> Photos { get; set; }

    public ICollection<EquipmentItemViewModel> EquipmentItems { get; set; }
}