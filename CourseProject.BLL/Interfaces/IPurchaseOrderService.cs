using CourseProject.BLL.DTO;
using CourseProject.BLL.Validation;

namespace CourseProject.BLL.Interfaces; 

public interface IPurchaseOrderService {

    Task<OperationResult> CreateOrderAsync(string clientId, int[] equipment);

    OperationResult<PurchaseOrderDto> GetOrderById(int id);

    IEnumerable<PurchaseOrderDto> GetAllOrders();
}