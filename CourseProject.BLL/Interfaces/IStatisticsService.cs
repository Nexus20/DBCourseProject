using CourseProject.BLL.DTO;
using CourseProject.BLL.DTO.StatisticsDtos;
using CourseProject.Domain;

namespace CourseProject.BLL.Interfaces; 

public interface IStatisticsService {
    
    Task<IEnumerable<MaxOrdersClientDto>> GetTopClientsWhoMadeMoreOrdersAsync(int top);

    Task<OrdersProfitDto> GetProfitAsync(DateRangeSettings settings);

    Task<IEnumerable<MostPurchasedModelDto>> GetTopMostPurchasedCarModelsAsync(int top);

    Task<IEnumerable<MaxPurchaseOrdersManagerDto>> GetTopManagersWhoCompletedMorePurchaseOrdersAsync(TopManagersWhoHandleMoreOrdersSettingsDto settingsDto);
}