using CourseProject.BLL.DTO;
using CourseProject.DAL.StatisticsModels;

namespace CourseProject.BLL.Interfaces; 

public interface IStatisticsService {
    
    Task<IEnumerable<MaxOrdersClientDto>> GetTopClientsWhoMadeMoreOrdersAsync();

    Task<IEnumerable<MostPurchasedModelDto>> GetTopMostPurchasedCarModelsAsync();
}