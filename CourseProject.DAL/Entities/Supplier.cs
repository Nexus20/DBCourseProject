namespace CourseProject.DAL.Entities; 

public class Supplier : BaseEntity {
    public string Name { get; set; }

    public string Phone { get; set; }

    public string Email { get; set; }

    public int BrandId { get; set; }

    public virtual Brand Brand { get; set; }

    public virtual ICollection<SupplyOrder> SupplyOrders { get; set; }
}