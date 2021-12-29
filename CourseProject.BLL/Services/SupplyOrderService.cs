using AutoMapper;
using CourseProject.BLL.DTO;
using CourseProject.BLL.FilterModels;
using CourseProject.BLL.Interfaces;
using CourseProject.BLL.PipelineBuilders;
using CourseProject.BLL.Validation;
using CourseProject.DAL.Entities;
using CourseProject.DAL.Interfaces;

namespace CourseProject.BLL.Services; 

public class SupplyOrderService : ISupplyOrderService {

    private readonly IUnitOfWork _unitOfWork;

    private readonly IMapper _mapper;

    private readonly IPipelineBuilderDirector<SupplyOrder, SupplyOrderFilterModel> _builderDirector;

    public SupplyOrderService(IUnitOfWork unitOfWork, IMapper mapper, IPipelineBuilderDirector<SupplyOrder, SupplyOrderFilterModel> builderDirector) {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _builderDirector = builderDirector;
    }

    public Task<OperationResult> CreateOrderAsync(string clientId, int[] equipment) {
        throw new NotImplementedException();
    }

    public Task<OperationResult<SupplyOrderDto>> GetOrderById(int id) {
        throw new NotImplementedException();
    }

    public IEnumerable<SupplyOrderDto> GetAllOrders() {
        throw new NotImplementedException();
    }

    public Task<DtoListWithPossibleEntitiesCount<SupplyOrderDto>> GetAllSupplyOrdersAsync(SupplyOrderFilterModel filterModel) {
        throw new NotImplementedException();
    }
}