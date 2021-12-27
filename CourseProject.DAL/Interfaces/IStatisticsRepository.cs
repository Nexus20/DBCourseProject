using CourseProject.DAL.StatisticsModels;

namespace CourseProject.DAL.Interfaces; 

public interface IStatisticsRepository {

    Task<IEnumerable<MaxOrdersClient>> GetTopClientsWhoMadeMoreOrdersAsync();

    Task<OrdersProfit> GetProfitAsync();

    Task<IEnumerable<MostPurchasedModel>> GetTopMostPurchasedCarModelsAsync();

    Task<IEnumerable<MaxPurchaseOrdersManager>> GetTopManagersWhoCompletedMorePurchaseOrdersAsync();
}