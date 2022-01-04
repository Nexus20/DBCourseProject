using CourseProject.DAL.StatisticsModels;
using CourseProject.Domain;

namespace CourseProject.DAL.Interfaces; 

public interface IStatisticsRepository {

    Task<IEnumerable<MaxOrdersClient>> GetTopClientsWhoMadeMoreOrdersAsync(int top);

    Task<OrdersProfit> GetProfitAsync(DateRangeSettings settings);

    Task<IEnumerable<MostPurchasedModel>> GetTopMostPurchasedCarModelsAsync(int top);

    Task<IEnumerable<MaxPurchaseOrdersManager>> GetTopManagersWhoCompletedMorePurchaseOrdersAsync(TopManagersWhoHandleMoreOrdersSettings settings);
}