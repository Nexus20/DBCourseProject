namespace CourseProject.DAL.Entities; 

public class CarInStock : BaseEntity {
    public string VinCode { get; set; }

    public int CarId { get; set; }

    public virtual Car Car { get; set; }

    public virtual ICollection<CarInStockEquipmentItemValue> CarInStockEquipmentItemValues { get; set; }

    public int ShowroomId { get; set; }

    public virtual Showroom Showroom { get; set; }
}