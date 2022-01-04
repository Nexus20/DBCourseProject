using CourseProject.BLL.DTO;
using CourseProject.BLL.FilterModels;
using CourseProject.BLL.Validation;

namespace CourseProject.BLL.Interfaces; 

public interface ISupplyOrderService {

    Task<OperationResult<int>> CreateOrderAsync(SupplyOrderDto dto);

    Task<OperationResult<SupplyOrderDto>> GetOrderByIdAsync(int id);

    IEnumerable<SupplyOrderDto> GetAllOrders();

    Task<DtoListWithPossibleEntitiesCount<SupplyOrderDto>> GetAllSupplyOrdersAsync(
        SupplyOrderFilterModel filterModel);

    Task<OperationResult> AddCarToSupplyOrderAsync(int supplyOrderId, int[] equipment, int carsCount, Guid managerId);

    Task<OperationResult> SendSupplyOrderAsync(int supplyOrderId);

    Task<OperationResult<int>> TakeCarsToShowroom(CloseSupplyOrderDto dto);

    Task<OperationResult> CloseSupplyOrderAsync(int supplyOrderId);

    Task<OperationResult> CancelSupplyOrderAsync(int supplyOrderId);

    Task<OperationResult<int>> CreateSupplyOrderByPurchaseOrderIdAsync(int purchaseOrderId, string managerId, int supplierId);
}