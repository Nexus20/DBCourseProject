using AutoMapper;
using CourseProject.BLL.Interfaces;
using CourseProject.BLL.ReportDtos;
using CourseProject.DAL.Interfaces;
using CourseProject.DAL.ReportModels;
using CourseProject.Domain;

namespace CourseProject.BLL.Services; 

public class ReportService : IReportService {

    private readonly IUnitOfWork _unitOfWork;

    private readonly IMapper _mapper;

    public ReportService(IUnitOfWork unitOfWork, IMapper mapper) {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PurchaseOrdersReportDto> GetPurchaseOrdersReport(DateRangeSettings dateSettings, int showroomId) {

        var source = await _unitOfWork.ReportRepository.GetPurchaseOrdersReport(dateSettings, showroomId);

        return _mapper.Map<PurchaseOrdersReport, PurchaseOrdersReportDto>(source);
    }

    public async Task<SupplyOrdersReportDto> GetSupplyOrdersReport(DateRangeSettings dateSettings, int showroomId, int supplierId) {

        var source = await _unitOfWork.ReportRepository.GetSupplyOrdersReport(dateSettings, showroomId, supplierId);

        return _mapper.Map<SupplyOrdersReport, SupplyOrdersReportDto>(source);
    }
}