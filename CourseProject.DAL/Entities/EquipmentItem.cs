namespace CourseProject.DAL.Entities; 

public class EquipmentItem : BaseEntity {

    public int CarId { get; set; }

    public virtual Car Car { get; set; }

    public string Name { get; set; }

    public string UnitsOfMeasure { get; set; }

    public bool Optional { get; set; }

    public virtual ICollection<EquipmentItem> EquipmentItems { get; set; }
}