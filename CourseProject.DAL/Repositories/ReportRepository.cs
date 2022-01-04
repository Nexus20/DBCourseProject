using CourseProject.DAL.Interfaces;
using CourseProject.DAL.ReportModels;
using CourseProject.Domain;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CourseProject.DAL.Repositories;

public class ReportRepository : IReportRepository {

    protected readonly ApplicationDbContext Context;

    public ReportRepository(ApplicationDbContext context) {
        Context = context;
    }
    public async Task<PurchaseOrdersReport> GetPurchaseOrdersReport(DateRangeSettings settings, int showroomId) {

        var result = new PurchaseOrdersReport();

        await using var command = Context.Database.GetDbConnection().CreateCommand();

        command.CommandText =
            "SELECT po.Id, po.CreationDate, po.LastUpdateDate, po.VinCode, CONCAT(s.City, ', ', s.Street, ', ', s.House) AS Showroom, CONCAT(b.[Name], ' ', m.[Name], ' ', c.Submodel) AS Car, CONCAT(u.Surname, ' ', u.[Name], ' ', u.Patronymic) AS Client, u.Email AS ClientEmail, u.PhoneNumber AS ClientPhone, CONCAT(u2.Surname, ' ', u2.[Name], ' ', u2.Patronymic) AS Manager, u2.Email AS ManagerEmail, u2.PhoneNumber AS ManagerPhone, sum(eiv.Price) AS Profit FROM PurchaseOrders po " +
            "JOIN Showrooms s ON s.Id = po.ShowroomId " +
            "JOIN PurchaseOrderEquipmentItemsValues poeiv ON poeiv.PurchaseOrderId = po.Id " +
            "JOIN EquipmentItemValues eiv ON eiv.Id = poeiv.EquipmentItemValueId " +
            "JOIN EquipmentItems ei ON ei.Id = eiv.EquipmentItemId " +
            "JOIN Cars c ON c.Id = ei.CarId " +
            "JOIN Models m ON m.Id = c.ModelId " +
            "JOIN Brands b ON b.Id = m.BrandId " +
            "JOIN AspNetUsers u ON u.Id = po.ClientId " +
            "JOIN Managers mn ON mn.Id = po.ManagerId " +
            "JOIN AspNetUsers u2 ON u2.Id = mn.UserId " +
            "WHERE po.[State] = 2";

        if (showroomId > 0) {
            command.CommandText += " AND po.ShowroomId = @showroomId";
            var parameter = new SqlParameter("@showroomId", showroomId);
            command.Parameters.Add(parameter);
        }

        switch (settings) {
            case DateRangeSettings.Month:
                command.CommandText += " AND MONTH(po.LastUpdateDate) = MONTH(GETDATE()) - 1 ";
                break;
            case DateRangeSettings.Quarter:
                command.CommandText +=
                    " AND MONTH(po.LastUpdateDate) IN (MONTH(GETDATE()) - 1, MONTH(GETDATE()) - 2, MONTH(GETDATE()) - 3, MONTH(GETDATE()) - 4) ";
                break;
            case DateRangeSettings.Year:
                command.CommandText += " AND YEAR(po.LastUpdateDate) = YEAR(GETDATE()) - 1 ";
                break;
        }

        command.CommandText += " GROUP BY po.Id, po.CreationDate, po.LastUpdateDate, po.VinCode, s.City, s.House, s.Street, b.[Name], m.[Name], c.Submodel, u.[Name], u.Surname, u.Patronymic, u.Email, u.PhoneNumber, u2.[Name], u2.Surname, u2.Patronymic, u2.Email, u2.PhoneNumber";

        await Context.Database.OpenConnectionAsync();
        await using var reader = await command.ExecuteReaderAsync();

        if (reader.HasRows) {

            while (await reader.ReadAsync()) {

                result.Parts.Add(new PurchaseOrdersReportPart() {
                    OrderId = reader.GetInt32(0),
                    Client = reader.GetString(6),
                    ClientEmail = reader.GetString(7),
                    ClientPhone = reader.GetString(8),
                    CreationDate = reader.GetDateTime(1),
                    LastUpdateDate = reader.GetDateTime(2),
                    Manager = reader.GetString(9),
                    ManagerEmail = reader.GetString(10),
                    ManagerPhone = reader.GetString(11),
                    Car = reader.GetString(5),
                    VinCode = reader.GetString(3),
                    Profit = reader.GetDecimal(12),
                    Showroom = reader.GetString(4)
                });

            }
        }

        await reader.CloseAsync();

        return result;
    }

    public async Task<SupplyOrdersReport> GetSupplyOrdersReport(DateRangeSettings settings, int showroomId, int supplierId) {

        var result = new SupplyOrdersReport();

        await using var command = Context.Database.GetDbConnection().CreateCommand();

        command.CommandText =
            "SELECT so.Id, sop.[Count], so.CreationDate, so.LastUpdateDate, CONCAT(sr.City, ', ', sr.Street, ', ', sr.House) AS Showroom, s.[Name], s.Email, s.Phone, CONCAT(b.[Name], ' ', m.[Name], ' ', c.Submodel) AS Car, CONCAT(u.Surname, ' ', u.[Name], ' ', u.Patronymic) AS Manager, u.Email, u.PhoneNumber, sum(eiv.Price) AS Price FROM SupplyOrders so " +
            "JOIN Suppliers s ON s.Id = so.SupplierId " +
            "JOIN SupplyOrderParts sop ON sop.SupplyOrderId = so.Id " +
            "JOIN SupplyOrderPartEquipmentItemsValues sopeiv ON sopeiv.SupplyOrderPartId = sop.Id " +
            "JOIN EquipmentItemValues eiv ON eiv.Id = sopeiv.EquipmentItemValueId " +
            "JOIN EquipmentItems ei ON ei.Id = eiv.EquipmentItemId " +
            "JOIN Cars c ON c.Id = ei.CarId " +
            "JOIN Models m ON m.Id = c.ModelId " +
            "JOIN Brands b ON b.Id = m.BrandId " +
            "JOIN Managers mn ON mn.Id = so.ManagerId " +
            "JOIN AspNetUsers u ON u.Id = mn.UserId " +
            "JOIN Showrooms sr ON sr.Id = mn.ShowroomId " +
            "WHERE so.[State] = 2";

        if (showroomId > 0) {
            command.CommandText += " AND po.ShowroomId = @showroomId";
            var parameter = new SqlParameter("@showroomId", showroomId);
            command.Parameters.Add(parameter);
        }

        if (supplierId > 0) {
            command.CommandText += " AND s.Id = @supplierId";
            var parameter = new SqlParameter("@supplierId", supplierId);
            command.Parameters.Add(parameter);
        }

        switch (settings) {
            case DateRangeSettings.Month:
                command.CommandText += " AND MONTH(so.LastUpdateDate) = MONTH(GETDATE()) - 1 ";
                break;
            case DateRangeSettings.Quarter:
                command.CommandText +=
                    " AND MONTH(so.LastUpdateDate) IN (MONTH(GETDATE()) - 1, MONTH(GETDATE()) - 2, MONTH(GETDATE()) - 3, MONTH(GETDATE()) - 4) ";
                break;
            case DateRangeSettings.Year:
                command.CommandText += " AND YEAR(so.LastUpdateDate) = YEAR(GETDATE()) - 1 ";
                break;
        }

        command.CommandText += " GROUP BY so.Id, sop.[Count], so.CreationDate, so.LastUpdateDate, sr.City, sr.Street, sr.House, s.[Name], s.Email, s.Phone, b.[Name], m.[Name], c.Submodel, u.Surname, u.[Name], u.Patronymic, u.Email, u.PhoneNumber";

        await Context.Database.OpenConnectionAsync();
        await using var reader = await command.ExecuteReaderAsync();

        if (reader.HasRows) {

            while (await reader.ReadAsync()) {

                result.Parts.Add(new SupplyOrdersReportPart() {
                    OrderId = reader.GetInt32(0),
                    CarsCount = reader.GetInt32(1),
                    CreationDate = reader.GetDateTime(2),
                    LastUpdateDate = reader.GetDateTime(3),
                    Showroom = reader.GetString(4),
                    SupplierName = reader.GetString(5),
                    SupplierEmail = reader.GetString(6),
                    SupplierPhone = reader.GetString(7),
                    Car = reader.GetString(8),
                    Manager = reader.GetString(9),
                    ManagerEmail = reader.GetString(10),
                    ManagerPhone = reader.GetString(11),
                    Price = reader.GetDecimal(12),
                });

            }
        }

        await reader.CloseAsync();

        return result;
    }
}