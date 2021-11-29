namespace CourseProject.WEB.Models; 

public class EquipmentItemValueViewModel : BaseViewModel {

    public int EquipmentItemId { get; set; }

    public EquipmentItemViewModel EquipmentItem { get; set; }

    public string Value { get; set; }

    public decimal Price { get; set; }

    public ICollection<CarInStockViewModel> CarsInStock { get; set; }

    public virtual ICollection<PurchaseOrderViewModel> PurchaseOrders { get; set; }

    public virtual ICollection<SupplyOrderPartViewModel> SupplyOrderParts { get; set; }
}