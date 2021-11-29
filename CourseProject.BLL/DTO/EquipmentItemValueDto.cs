namespace CourseProject.BLL.DTO; 

public class EquipmentItemValueDto : BaseDto {

    public int EquipmentItemId { get; set; }

    public EquipmentItemDto EquipmentItem { get; set; }

    public string Value { get; set; }

    public decimal Price { get; set; }

    public ICollection<CarInStockDto> CarsInStock { get; set; }

    public virtual ICollection<PurchaseOrderDto> PurchaseOrders { get; set; }

    public virtual ICollection<SupplyOrderPartDto> SupplyOrderParts { get; set; }
}