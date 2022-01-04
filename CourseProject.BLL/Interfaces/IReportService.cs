using CourseProject.BLL.ReportDtos;
using CourseProject.Domain;

namespace CourseProject.BLL.Interfaces; 

public interface IReportService {

    Task<PurchaseOrdersReportDto> GetPurchaseOrdersReport(DateRangeSettings dateSettings, int showroomId);

    Task<SupplyOrdersReportDto> GetSupplyOrdersReport(DateRangeSettings dateSettings, int showroomId, int supplierId);
}