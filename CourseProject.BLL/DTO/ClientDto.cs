namespace CourseProject.BLL.DTO; 

public class ClientDto  : UserDto {

    public virtual ICollection<PurchaseOrderDto> PurchaseOrders { get; set; }
}