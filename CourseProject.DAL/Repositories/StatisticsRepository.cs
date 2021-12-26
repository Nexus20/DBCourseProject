using CourseProject.DAL.Interfaces;
using CourseProject.DAL.StatisticsModels;
using Microsoft.EntityFrameworkCore;

namespace CourseProject.DAL.Repositories; 

public class StatisticsRepository : IStatisticsRepository {

    protected readonly ApplicationDbContext Context;

    public StatisticsRepository(ApplicationDbContext context) {
        Context = context;
    }

    public async Task<IEnumerable<MaxOrdersClient>> GetTopClientsWhoMadeMoreOrders() {

        var result = new List<MaxOrdersClient>();

        await using var command = Context.Database.GetDbConnection().CreateCommand();

        command.CommandText = "SELECT TOP 3 c.Id, c.[Name], c.Surname, c.Patronymic, c.Email, count(*) AS OrdersCount FROM dbo.AspNetUsers c " +
                              "JOIN dbo.PurchaseOrders po ON c.Id = po.ClientId " +
                              //"WHERE po.[State] = 0 " +
                              "GROUP BY c.Id, c.[Name], c.Surname, c.Patronymic, c.Email " +
                              "ORDER BY OrdersCount DESC";
        await Context.Database.OpenConnectionAsync();
        await using var reader = await command.ExecuteReaderAsync();

        if (reader.HasRows) {

            while (await reader.ReadAsync()) {

                result.Add(new MaxOrdersClient() {
                    Id = reader.GetString(0),
                    Name = reader.GetString(1),
                    Surname = reader.GetString(2),
                    Patronymic = reader.GetString(3),
                    Email = reader.GetString(4),
                    OrdersCount = reader.GetInt32(5),
                });

            }
        }

        await reader.CloseAsync();

        return result;
    }
}