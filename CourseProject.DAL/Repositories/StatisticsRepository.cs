using CourseProject.DAL.Interfaces;
using CourseProject.DAL.StatisticsModels;
using CourseProject.Domain;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CourseProject.DAL.Repositories;

public class StatisticsRepository : IStatisticsRepository {

    protected readonly ApplicationDbContext Context;

    public StatisticsRepository(ApplicationDbContext context) {
        Context = context;
    }

    public async Task<IEnumerable<MaxOrdersClient>> GetTopClientsWhoMadeMoreOrdersAsync(int top) {

        var result = new List<MaxOrdersClient>();

        await using var command = Context.Database.GetDbConnection().CreateCommand();

        command.CommandText = "WITH cte(Id, [Name], Surname, Patronymic, Email, OrdersCount, rn) AS ( " +
                              "SELECT c.Id, c.[Name], c.Surname, c.Patronymic, c.Email, count(*) AS OrdersCount, ROW_NUMBER() OVER(ORDER BY c.Id ASC) AS rn FROM dbo.AspNetUsers c " +
                              "JOIN dbo.PurchaseOrders po ON c.Id = po.ClientId " +
                              "WHERE po.[State] = 2 " +
                              "GROUP BY c.Id, c.[Name], c.Surname, c.Patronymic, c.Email) " +
                              "SELECT Id, [Name], Surname, Patronymic, Email, OrdersCount FROM cte WHERE rn <= @top ORDER BY OrdersCount DESC";

        var parameter = new SqlParameter("@top", top);
        command.Parameters.Add(parameter);

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

    public async Task<IEnumerable<MostPurchasedModel>> GetTopMostPurchasedCarModelsAsync(int top) {

        var result = new List<MostPurchasedModel>();

        await using var command = Context.Database.GetDbConnection().CreateCommand();

        command.CommandText = "WITH cte(Brand, Model, OrdersCount, rn) AS ( " +
                              "SELECT b.[Name], m.[Name], count(poeiv.PurchaseOrderId) AS OrdersCount, ROW_NUMBER() OVER(ORDER BY b.[Name] ASC) AS rn FROM[dbo].[Models] m " +
                              "JOIN dbo.Brands b ON m.BrandId = b.Id " +
                              "JOIN dbo.Cars c ON c.ModelId = m.Id " +
                              "JOIN dbo.EquipmentItems ei ON ei.CarId = c.Id " +
                              "JOIN dbo.EquipmentItemValues eiv ON eiv.Id = ei.Id " +
                              "JOIN dbo.PurchaseOrderEquipmentItemsValues poeiv ON poeiv.EquipmentItemValueId = eiv.Id " +
                              "JOIN dbo.PurchaseOrders po ON po.Id = poeiv.PurchaseOrderId " +
                              "WHERE po.[State] = 2 " +
                              "GROUP BY b.[Name], m.[Name]) " +
                              "SELECT Brand, Model, OrdersCount FROM cte WHERE rn <= @top ORDER BY OrdersCount DESC";

        var parameter = new SqlParameter("@top", top);
        command.Parameters.Add(parameter);

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

    public async Task<IEnumerable<MaxPurchaseOrdersManager>> GetTopManagersWhoCompletedMorePurchaseOrdersAsync(TopManagersWhoHandleMoreOrdersSettings settings) {

        var result = new List<MaxPurchaseOrdersManager>();

        await using var command = Context.Database.GetDbConnection().CreateCommand();

        command.CommandText = "WITH cte(Id, [Name], Surname, Patronymic, Email, OrdersCount, rn) AS ( " +
                              "SELECT m.Id, u.[Name], u.Surname, u.Patronymic, u.Email, count(po.Id) AS OrdersCount, ROW_NUMBER() OVER(ORDER BY m.Id ASC) AS rn FROM[dbo].[AspNetUsers] u " +
                              "JOIN dbo.Managers m ON m.UserId = u.Id " +
                              "JOIN dbo.PurchaseOrders po ON po.ManagerId = m.Id " +
                              "WHERE po.[State] = 2";

        switch (settings.DateRangeSettings) {
            case DateRangeSettings.Month:
                command.CommandText += " AND MONTH(po.LastUpdateDate) = MONTH(GETDATE()) - 1 ";
                break;
            case DateRangeSettings.Quarter:
                command.CommandText +=
                    " AND MONTH(po.LastUpdateDate) IN (MONTH(GETDATE()) - 1, MONTH(GETDATE()) - 2, MONTH(GETDATE()) - 3, MONTH(GETDATE()) - 4)";
                break;
            case DateRangeSettings.Year:
                command.CommandText += " AND YEAR(po.LastUpdateDate) = YEAR(GETDATE()) - 1 ";
                break;
        }

        command.CommandText += "GROUP BY m.Id, u.[Name], u.Surname, u.Patronymic, u.Email) " +
                               "SELECT Id, [Name], Surname, Patronymic, Email, OrdersCount FROM cte WHERE rn <= @top ORDER BY OrdersCount DESC";

        var parameter = new SqlParameter("@top", settings.Top);
        command.Parameters.Add(parameter);

        await Context.Database.OpenConnectionAsync();
        await using var reader = await command.ExecuteReaderAsync();

        if (reader.HasRows) {

            while (await reader.ReadAsync()) {

                result.Add(new MaxPurchaseOrdersManager() {
                    Id = reader.GetGuid(0).ToString(),
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

    public async Task<OrdersProfit> GetProfitAsync(DateRangeSettings settings) {

        var result = new OrdersProfit();

        await using var command = Context.Database.GetDbConnection().CreateCommand();

        command.CommandText = "SELECT coalesce(sum(eiv.Price), 0) AS Profit, (SELECT count(*) FROM dbo.PurchaseOrders WHERE dbo.PurchaseOrders.[State] = 2";

        switch (settings) {
            case DateRangeSettings.Month:
                command.CommandText += " AND MONTH(dbo.PurchaseOrders.LastUpdateDate) = MONTH(GETDATE()) - 1 ";
                break;
            case DateRangeSettings.Quarter:
                command.CommandText +=
                    " AND MONTH(dbo.PurchaseOrders.LastUpdateDate) IN (MONTH(GETDATE()) - 1, MONTH(GETDATE()) - 2, MONTH(GETDATE()) - 3, MONTH(GETDATE()) - 4)";
                break;
            case DateRangeSettings.Year:
                command.CommandText += " AND YEAR(dbo.PurchaseOrders.LastUpdateDate) = YEAR(GETDATE()) - 1 ";
                break;
        }

        command.CommandText += ") AS OrdersCount FROM dbo.PurchaseOrders po " +
                               "JOIN dbo.PurchaseOrderEquipmentItemsValues poeiv ON poeiv.PurchaseOrderId = po.Id " +
                               "JOIN dbo.EquipmentItemValues eiv ON eiv.Id = poeiv.EquipmentItemValueId " +
                               "WHERE po.[State] = 2";

        switch (settings) {
            case DateRangeSettings.Month:
                command.CommandText += " AND MONTH(po.LastUpdateDate) = MONTH(GETDATE()) - 1 ";
                break;
            case DateRangeSettings.Quarter:
                command.CommandText +=
                    " AND MONTH(po.LastUpdateDate) IN (MONTH(GETDATE()) - 1, MONTH(GETDATE()) - 2, MONTH(GETDATE()) - 3, MONTH(GETDATE()) - 4)";
                break;
            case DateRangeSettings.Year:
                command.CommandText += " AND YEAR(po.LastUpdateDate) = YEAR(GETDATE()) - 1 ";
                break;
        }

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