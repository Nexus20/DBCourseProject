using CourseProject.BLL.DTO;

namespace CourseProject.BLL.Interfaces; 

public interface IStatisticsService {
    
    IEnumerable<ClientDto> GetTopClientsWhoMadeMoreOrders();

    IEnumerable<ModelDto> GetTopMostFrequentlyPurchasedCarModels();
}