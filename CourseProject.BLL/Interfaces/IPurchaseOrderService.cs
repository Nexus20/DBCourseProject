using System.Security.Claims;
using CourseProject.BLL.DTO;
using CourseProject.BLL.FilterModels;
using CourseProject.BLL.Validation;

namespace CourseProject.BLL.Interfaces; 

public interface IPurchaseOrderService {

    Task<OperationResult<int>> CreateOrderAsync(string clientId, int[] equipment, int showroomId, ClientPersonalDataDto clientPersonalData);

    Task<OperationResult<PurchaseOrderDto>> GetOrderByIdAsync(int id);

    IEnumerable<PurchaseOrderDto> GetAllOrders();

    Task<DtoListWithPossibleEntitiesCount<PurchaseOrderDto>> GetAllPurchaseOrdersAsync(
        PurchaseOrderFilterModel filterModel);

    Task<OperationResult> AcceptOrderAsync(int purchaseOrderId, ClaimsPrincipal user);

    Task<OperationResult> CloseOrderAsync(int purchaseOrderId);

    Task<OperationResult> CancelOrderAsync(int purchaseOrderId);
}