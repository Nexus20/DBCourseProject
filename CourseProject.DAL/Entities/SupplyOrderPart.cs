namespace CourseProject.DAL.Entities; 

public class SupplyOrderPart : BaseEntity {

    public int SupplyOrderId { get; set; }

    public virtual SupplyOrder SupplyOrder { get; set; }

    public int Count { get; set; }

    public virtual ICollection<SupplyOrderPartEquipmentItemValue> SupplyOrderPartEquipmentItemsValues { get; set; }
}