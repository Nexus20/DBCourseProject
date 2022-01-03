using AutoMapper;
using CourseProject.BLL.DTO;
using CourseProject.BLL.FilterModels;
using CourseProject.BLL.Interfaces;
using CourseProject.BLL.Pipeline;
using CourseProject.BLL.PipelineBuilders;
using CourseProject.BLL.Validation;
using CourseProject.DAL.Entities;
using CourseProject.DAL.Interfaces;

namespace CourseProject.BLL.Services; 

public class CarInStockService : ICarInStockService {

    private readonly IUnitOfWork _unitOfWork;

    private readonly IMapper _mapper;

    private readonly IPipelineBuilderDirector<CarInStock, CarInStockFilterModel> _builderDirector;


    public CarInStockService(IUnitOfWork unitOfWork, IMapper mapper, IPipelineBuilderDirector<CarInStock, CarInStockFilterModel> builderDirector) {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _builderDirector = builderDirector;
    }

    public async Task<DtoListWithPossibleEntitiesCount<CarInStockDto>> GetAllCarsInStockAsync(CarInStockFilterModel filterModel) {

        var pipeline = new SelectionPipeline<CarInStock, CarInStockFilterModel>(filterModel, _builderDirector);

        var expressions = pipeline.Process();

        var source = _unitOfWork.GetRepository<IRepository<CarInStock>, CarInStock>().FindAllWithDetails(expressions);

        var possibleCarsCount = await _unitOfWork.GetRepository<IRepository<CarInStock>, CarInStock>()
            .CountAsync(expressions.FilterExpressions);

        //logger.LogInformation($"All games were returned. Returned games count: {source.Count()}");

        return new DtoListWithPossibleEntitiesCount<CarInStockDto>() {
            Dtos = _mapper.Map<IEnumerable<CarInStock>, IEnumerable<CarInStockDto>>(source),
            PossibleDtosCount = possibleCarsCount
        };
    }

    public async Task<OperationResult<CarInStockDto>> GetCarInStockByIdAsync(int id) {

        var operationResult = new OperationResult<CarInStockDto>();

        var carInStock = await _unitOfWork.GetRepository<IRepository<CarInStock>, CarInStock>().FirstOrDefaultWithDetailsAsync(c => c.Id == id);

        if (carInStock == null) {
            operationResult.AddError(nameof(id), "Such car in stock not found");
        }

        operationResult.Result = _mapper.Map<CarInStock, CarInStockDto>(carInStock);

        return operationResult;
    }
}