namespace CourseProject.DAL.Entities; 

public class Client : User {

    public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; }
}