namespace CourseProject.WEB.Models; 

public class ClientViewModel  : UserViewModel {

    public virtual ICollection<PurchaseOrderViewModel> PurchaseOrders { get; set; }
}