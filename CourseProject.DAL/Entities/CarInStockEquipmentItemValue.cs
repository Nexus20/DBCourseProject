namespace CourseProject.DAL.Entities; 

public class CarInStockEquipmentItemValue {

    public int EquipmentItemValueId { get; set; }

    public virtual EquipmentItemValue EquipmentItemValue { get; set; }

    public int CarInStockId { get; set; }

    public virtual CarInStock CarInStock { get; set; }
}