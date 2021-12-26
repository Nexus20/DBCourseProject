using AutoMapper;
using CourseProject.BLL.DTO;
using CourseProject.BLL.Interfaces;
using CourseProject.DAL.Entities;
using CourseProject.DAL.Interfaces;

namespace CourseProject.BLL.Services; 

public class StatisticsService : IStatisticsService {

    private readonly IUnitOfWork _unitOfWork;

    private readonly IMapper _mapper;

    public StatisticsService(IUnitOfWork unitOfWork, IMapper mapper) {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public IEnumerable<ClientDto> GetTopClientsWhoMadeMoreOrders() {

        var clients = _unitOfWork.GetRepository<IClientRepository, Client>().FindAllWithDetails();

        var topClients = clients.OrderByDescending(c => c.PurchaseOrders.Count).Take(3);

        return _mapper.Map<IEnumerable<Client>, IEnumerable<ClientDto>>(topClients);
    }

    public IEnumerable<ModelDto> GetTopMostFrequentlyPurchasedCarModels() {

        //var models = _unitOfWork.GetRepository<IModelRepository, Model>().FindAllWithDetails();
        var models = _unitOfWork.GetRepository<IModelRepository, Model>().FindAllWithDetails().OrderByDescending(m => m.Cars.Sum(c => c.EquipmentItems.Sum(ei => ei.EquipmentItemValues.Sum(ev => ev.PurchaseOrderEquipmentItemsValues.Count())))).Take(10);

        var dto = _mapper.Map<IEnumerable<Model>, IEnumerable<ModelDto>>(models);

        return dto;
    }
}