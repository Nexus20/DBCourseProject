namespace CourseProject.WEB.Models; 

public class ClientViewModel  : UserViewModel {

    public ICollection<PurchaseOrderViewModel> PurchaseOrders { get; set; }
}