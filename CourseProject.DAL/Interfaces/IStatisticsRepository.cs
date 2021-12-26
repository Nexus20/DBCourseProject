using CourseProject.DAL.StatisticsModels;

namespace CourseProject.DAL.Interfaces; 

public interface IStatisticsRepository {

    Task<IEnumerable<MaxOrdersClient>> GetTopClientsWhoMadeMoreOrders();
}