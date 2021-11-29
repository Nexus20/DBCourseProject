namespace CourseProject.DAL.Entities; 

public class EquipmentItem : BaseEntity {

    public int CarId { get; set; }

    public virtual Car Car { get; set; }

    public bool Optional { get; set; }

    public virtual ICollection<EquipmentItemValue> EquipmentItemValues { get; set; }

    public int EquipmentItemCategoryId { get; set; }

    public virtual EquipmentItemCategory Category { get; set; }
}