namespace CourseProject.DAL.Entities; 

public class PurchaseOrderEquipmentItemValue {
    public int PurchaseOrderId { get; set; }

    public virtual PurchaseOrder PurchaseOrder { get; set; }

    public int EquipmentItemValueId { get; set; }

    public virtual EquipmentItemValue EquipmentItemValue { get; set; }
}