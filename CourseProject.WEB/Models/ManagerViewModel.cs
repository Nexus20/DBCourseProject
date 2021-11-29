namespace CourseProject.WEB.Models; 

public class ManagerViewModel {

    public Guid Id { get; set; }
    
    public string UserId { get; set; }

    public UserViewModel User { get; set; }

    public int ShowroomId { get; set; }

    public ShowroomViewModel Showroom { get; set; }

    public ICollection<PurchaseOrderViewModel> PurchaseOrders { get; set; }

    public ICollection<SupplyOrderViewModel> SupplyOrders { get; set; }
}