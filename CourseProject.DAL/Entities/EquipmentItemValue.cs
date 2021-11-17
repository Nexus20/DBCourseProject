namespace CourseProject.DAL.Entities; 

public class EquipmentItemValue : BaseEntity {

    public int EquipmentItemId { get; set; }

    public virtual EquipmentItem EquipmentItem { get; set; }

    public string Values { get; set; }

    public decimal Price { get; set; }

    public virtual ICollection<CarInStockEquipmentItemValue> CarInStockEquipmentItemValues { get; set; }

    public virtual ICollection<PurchaseOrderEquipmentItemValue> PurchaseOrderEquipmentItemsValues { get; set; }

    public virtual ICollection<SupplyOrderPartEquipmentItemValue> SupplyOrderPartEquipmentItemsValues { get; set; }
}