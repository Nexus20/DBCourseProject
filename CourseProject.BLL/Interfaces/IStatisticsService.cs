using CourseProject.BLL.DTO;

namespace CourseProject.BLL.Interfaces; 

public interface IStatisticsService {
    
    Task<IEnumerable<MaxOrdersClientDto>> GetTopClientsWhoMadeMoreOrders();

    IEnumerable<ModelDto> GetTopMostFrequentlyPurchasedCarModels();
}