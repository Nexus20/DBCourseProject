namespace CourseProject.WEB.Models; 

public class EquipmentItemCategoryViewModel : BaseViewModel {

    public string Name { get; set; }

    public string UnitsOfMeasure { get; set; }

    public virtual ICollection<EquipmentItemViewModel> EquipmentItems { get; set; }
}