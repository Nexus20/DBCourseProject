namespace CourseProject.WEB.Models; 

public class CarViewModel : BaseViewModel {
    public string Submodel { get; set; }

    public int ModelId { get; set; }

    public ModelViewModel Model { get; set; }

    public ICollection<CarPhotoViewModel> Photos { get; set; }

    public virtual ICollection<EquipmentItemViewModel> EquipmentItems { get; set; }
}