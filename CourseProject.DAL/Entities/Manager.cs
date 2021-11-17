namespace CourseProject.DAL.Entities; 

public class Manager {

    public Guid Id { get; set; }
    
    public string UserId { get; set; }

    public virtual User User { get; set; }

    public int ShowroomId { get; set; }

    public virtual Showroom Showroom { get; set; }

    public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; }

    public virtual ICollection<SupplyOrder> SupplyOrders { get; set; }
}