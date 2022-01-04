using CourseProject.DAL.ReportModels;
using CourseProject.Domain;

namespace CourseProject.DAL.Interfaces; 

public interface IReportRepository {

    Task<PurchaseOrdersReport> GetPurchaseOrdersReport(DateRangeSettings settings, int showroomId);

    Task<SupplyOrdersReport> GetSupplyOrdersReport(DateRangeSettings settings, int showroomId, int supplierId);
}