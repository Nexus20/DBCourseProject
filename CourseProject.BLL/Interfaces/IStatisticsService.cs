using CourseProject.BLL.DTO;

namespace CourseProject.BLL.Interfaces; 

public interface IStatisticsService {
    
    Task<IEnumerable<MaxOrdersClientDto>> GetTopClientsWhoMadeMoreOrdersAsync();

    Task<OrdersProfitDto> GetProfitAsync();

    Task<IEnumerable<MostPurchasedModelDto>> GetTopMostPurchasedCarModelsAsync();

    Task<IEnumerable<MaxPurchaseOrdersManagerDto>> GetTopManagersWhoCompletedMorePurchaseOrdersAsync();
}