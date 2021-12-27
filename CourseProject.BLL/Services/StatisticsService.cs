using AutoMapper;
using CourseProject.BLL.DTO;
using CourseProject.BLL.Interfaces;
using CourseProject.DAL.Interfaces;
using CourseProject.DAL.StatisticsModels;

namespace CourseProject.BLL.Services; 

public class StatisticsService : IStatisticsService {

    private readonly IUnitOfWork _unitOfWork;

    private readonly IMapper _mapper;

    public StatisticsService(IUnitOfWork unitOfWork, IMapper mapper) {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<MaxOrdersClientDto>> GetTopClientsWhoMadeMoreOrdersAsync() {

        var source = await _unitOfWork.StatisticsRepository.GetTopClientsWhoMadeMoreOrdersAsync();

        return _mapper.Map<IEnumerable<MaxOrdersClient>, IEnumerable<MaxOrdersClientDto>>(source);
    }

    public async Task<IEnumerable<MaxPurchaseOrdersManagerDto>> GetTopManagersWhoCompletedMorePurchaseOrdersAsync() {

        var source = await _unitOfWork.StatisticsRepository.GetTopManagersWhoCompletedMorePurchaseOrdersAsync();

        return _mapper.Map<IEnumerable<MaxPurchaseOrdersManager>, IEnumerable<MaxPurchaseOrdersManagerDto>>(source);
    }

    public async Task<IEnumerable<MostPurchasedModelDto>> GetTopMostPurchasedCarModelsAsync() {

        var source = await _unitOfWork.StatisticsRepository.GetTopMostPurchasedCarModelsAsync();

        return _mapper.Map<IEnumerable<MostPurchasedModel>, IEnumerable<MostPurchasedModelDto>>(source);
    }

    public async Task<OrdersProfitDto> GetProfitAsync() {

        var source = await _unitOfWork.StatisticsRepository.GetProfitAsync();

        return _mapper.Map<OrdersProfit, OrdersProfitDto>(source);
    }
}