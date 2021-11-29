namespace CourseProject.DAL.Entities; 

public class SupplyOrderPartEquipmentItemValue {
    public int SupplyOrderPartId { get; set; }

    public virtual SupplyOrderPart SupplyOrderPart { get; set; }

    public int EquipmentItemValueId { get; set; }

    public virtual EquipmentItemValue EquipmentItemValue { get; set; }
}