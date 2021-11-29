namespace CourseProject.WEB.Models; 

public class EquipmentItemViewModel : BaseViewModel {

    public int CarId { get; set; }

    public virtual CarViewModel Car { get; set; }

    public bool Optional { get; set; }

    public ICollection<EquipmentItemValueViewModel> EquipmentItemValues { get; set; }

    public int EquipmentItemCategoryId { get; set; }

    public EquipmentItemCategoryViewModel Category { get; set; }
}