using AutoMapper;
using CourseProject.BLL.DTO;
using CourseProject.BLL.DTO.StatisticsDtos;
using CourseProject.BLL.Interfaces;
using CourseProject.DAL.Interfaces;
using CourseProject.DAL.StatisticsModels;
using CourseProject.Domain;

namespace CourseProject.BLL.Services; 

public class StatisticsService : IStatisticsService {

    private readonly IUnitOfWork _unitOfWork;

    private readonly IMapper _mapper;

    public StatisticsService(IUnitOfWork unitOfWork, IMapper mapper) {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<MaxOrdersClientDto>> GetTopClientsWhoMadeMoreOrdersAsync(int top) {

        var source = await _unitOfWork.StatisticsRepository.GetTopClientsWhoMadeMoreOrdersAsync(top);

        return _mapper.Map<IEnumerable<MaxOrdersClient>, IEnumerable<MaxOrdersClientDto>>(source);
    }

    public async Task<IEnumerable<MaxPurchaseOrdersManagerDto>> GetTopManagersWhoCompletedMorePurchaseOrdersAsync(TopManagersWhoHandleMoreOrdersSettingsDto settingsDto) {

        var source = await _unitOfWork.StatisticsRepository.GetTopManagersWhoCompletedMorePurchaseOrdersAsync(_mapper.Map<TopManagersWhoHandleMoreOrdersSettingsDto, TopManagersWhoHandleMoreOrdersSettings>(settingsDto));

        return _mapper.Map<IEnumerable<MaxPurchaseOrdersManager>, IEnumerable<MaxPurchaseOrdersManagerDto>>(source);
    }

    public async Task<IEnumerable<MostPurchasedModelDto>> GetTopMostPurchasedCarModelsAsync(int top) {

        var source = await _unitOfWork.StatisticsRepository.GetTopMostPurchasedCarModelsAsync(top);

        return _mapper.Map<IEnumerable<MostPurchasedModel>, IEnumerable<MostPurchasedModelDto>>(source);
    }

    public async Task<OrdersProfitDto> GetProfitAsync(DateRangeSettings settings) {

        var source = await _unitOfWork.StatisticsRepository.GetProfitAsync(settings);

        return _mapper.Map<OrdersProfit, OrdersProfitDto>(source);
    }
}