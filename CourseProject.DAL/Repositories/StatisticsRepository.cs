using CourseProject.DAL.Interfaces;
using CourseProject.DAL.StatisticsModels;
using Microsoft.EntityFrameworkCore;

namespace CourseProject.DAL.Repositories;

public class StatisticsRepository : IStatisticsRepository {

    protected readonly ApplicationDbContext Context;

    public StatisticsRepository(ApplicationDbContext context) {
        Context = context;
    }

    public async Task<IEnumerable<MaxOrdersClient>> GetTopClientsWhoMadeMoreOrdersAsync() {

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

    public async Task<IEnumerable<MostPurchasedModel>> GetTopMostPurchasedCarModelsAsync() {

        var result = new List<MostPurchasedModel>();

        await using var command = Context.Database.GetDbConnection().CreateCommand();

        command.CommandText = "SELECT TOP 10 b.[Name], m.[Name], count(poeiv.PurchaseOrderId) AS OrdersCount FROM [dbo].[Models] m " +
                              "JOIN dbo.Brands b ON m.BrandId = b.Id " +
                              "JOIN dbo.Cars c ON c.ModelId = m.Id " +
                              "JOIN dbo.EquipmentItems ei ON ei.CarId = c.Id " +
                              "JOIN dbo.EquipmentItemValues eiv ON eiv.Id = ei.Id " +
                              "JOIN dbo.PurchaseOrderEquipmentItemsValues poeiv ON poeiv.EquipmentItemValueId = eiv.Id " +
                              "GROUP BY b.[Name], m.[Name] " +
                              "ORDER BY OrdersCount DESC";


        await Context.Database.OpenConnectionAsync();
        await using var reader = await command.ExecuteReaderAsync();

        if (reader.HasRows) {

            while (await reader.ReadAsync()) {

                result.Add(new MostPurchasedModel() {
                    Brand = reader.GetString(0),
                    Model = reader.GetString(1),
                    OrdersCount = reader.GetInt32(2),
                });
            }
        }

        await reader.CloseAsync();

        return result;
    }

    public async Task<IEnumerable<MaxPurchaseOrdersManager>> GetTopManagersWhoCompletedMorePurchaseOrdersAsync() {

        var result = new List<MaxPurchaseOrdersManager>();

        await using var command = Context.Database.GetDbConnection().CreateCommand();

        command.CommandText = "SELECT TOP 3 m.Id, u.[Name], u.Surname, u.Patronymic, u.Email, count(po.Id) AS OrdersCount FROM [dbo].[AspNetUsers] u " +
                              "JOIN dbo.Managers m ON m.UserId = u.Id " +
                              "JOIN dbo.PurchaseOrders po ON po.ManagerId = m.Id " +
                              "WHERE po.[State] = 2 AND MONTH(po.LastUpdateDate) = MONTH(GETDATE()) - 1 " +
                              "GROUP BY m.Id, u.[Name], u.Surname, u.Patronymic, u.Email " +
                              "ORDER BY OrdersCount DESC";

        await Context.Database.OpenConnectionAsync();
        await using var reader = await command.ExecuteReaderAsync();

        if (reader.HasRows) {

            while (await reader.ReadAsync()) {

                result.Add(new MaxPurchaseOrdersManager() {
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

    public async Task<OrdersProfit> GetProfitAsync() {

        var result = new OrdersProfit();

        await using var command = Context.Database.GetDbConnection().CreateCommand();

        command.CommandText = "SELECT coalesce(sum(eiv.Price), 0) AS Profit, (SELECT count(*) FROM dbo.PurchaseOrders) AS OrdersCount FROM dbo.PurchaseOrders po " +
                              "JOIN dbo.PurchaseOrderEquipmentItemsValues poeiv ON poeiv.PurchaseOrderId = po.Id " +
                              "JOIN dbo.EquipmentItemValues eiv ON eiv.Id = poeiv.EquipmentItemValueId " +
                              "WHERE po.[State] = 2 AND MONTH(po.LastUpdateDate) = MONTH(GETDATE()) - 1";

        await Context.Database.OpenConnectionAsync();
        await using var reader = await command.ExecuteReaderAsync();

        if (reader.HasRows) {

            if (await reader.ReadAsync()) {

                result.Profit = reader.GetDecimal(0);
                result.OrdersCount = reader.GetInt32(1);
            }
        }

        await reader.CloseAsync();

        return result;
    }
}