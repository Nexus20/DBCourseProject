namespace CourseProject.DAL.Entities; 

public class Showroom : BaseEntity {

    public string City { get; set; }

    public string Street { get; set; }

    public string House { get; set; }

    public string Phone { get; set; }

    public virtual ICollection<Manager> Managers { get; set; }

    public virtual ICollection<CarInStock> CarsInStock { get; set; }

    public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; }
}