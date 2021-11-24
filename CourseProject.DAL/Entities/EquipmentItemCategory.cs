namespace CourseProject.DAL.Entities; 

public class EquipmentItemCategory : BaseEntity {

    public string Name { get; set; }

    public string UnitsOfMeasure { get; set; }

    public virtual ICollection<EquipmentItem> EquipmentItems { get; set; }
}