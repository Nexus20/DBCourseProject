namespace CourseProject.BLL.DTO; 

public class ManagerDto {

    public Guid Id { get; set; }
    
    public string UserId { get; set; }

    public UserDto User { get; set; }

    public int ShowroomId { get; set; }

    public ShowroomDto Showroom { get; set; }

    public ICollection<PurchaseOrderDto> PurchaseOrders { get; set; }

    public ICollection<SupplyOrderDto> SupplyOrders { get; set; }
}