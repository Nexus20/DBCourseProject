namespace CourseProject.WEB.Models; 

public class SupplyOrderPartViewModel : BaseViewModel {

    public int SupplyOrderId { get; set; }

    public SupplyOrderViewModel SupplyOrder { get; set; }

    public int Count { get; set; }

    public ICollection<EquipmentItemValueViewModel> EquipmentItemsValues { get; set; }
}