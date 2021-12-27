using CourseProject.DAL.StatisticsModels;

namespace CourseProject.DAL.Interfaces; 

public interface IStatisticsRepository {

    Task<IEnumerable<MaxOrdersClient>> GetTopClientsWhoMadeMoreOrdersAsync();

    Task<IEnumerable<MostPurchasedModel>> GetTopMostPurchasedCarModelsAsync();
}