using CourseProject.BLL.DTO;
using CourseProject.BLL.FilterModels;
using CourseProject.BLL.Validation;

namespace CourseProject.BLL.Interfaces; 

public interface ISupplyOrderService {

    Task<OperationResult> CreateOrderAsync(string clientId, int[] equipment);

    Task<OperationResult<SupplyOrderDto>> GetOrderById(int id);

    IEnumerable<SupplyOrderDto> GetAllOrders();

    Task<DtoListWithPossibleEntitiesCount<SupplyOrderDto>> GetAllSupplyOrdersAsync(
        SupplyOrderFilterModel filterModel);
}