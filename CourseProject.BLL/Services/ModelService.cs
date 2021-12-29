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

public class ModelService : IModelService {

    private readonly IUnitOfWork _unitOfWork;

    private readonly IMapper _mapper;

    private readonly IPipelineBuilderDirector<Model, ModelFilterModel> _builderDirector;

    public ModelService(IUnitOfWork unitOfWork, IMapper mapper, IPipelineBuilderDirector<Model, ModelFilterModel> builderDirector) {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _builderDirector = builderDirector;
    }

    public async Task<OperationResult> CreateModelAsync(ModelDto modelDto) {

        var operationResult = new OperationResult();

        var model = _mapper.Map<ModelDto, Model>(modelDto);

        await _unitOfWork.GetRepository<IRepository<Model>, Model>().CreateAsync(model);

        await _unitOfWork.SaveChangesAsync();

        return operationResult;
    }

    public async Task<OperationResult> EditModelAsync(ModelDto modelDto) {

        var operationResult = new OperationResult();

        var model = _mapper.Map<ModelDto, Model>(modelDto);

        _unitOfWork.GetRepository<IRepository<Model>, Model>().Update(model);

        await _unitOfWork.SaveChangesAsync();

        return operationResult;
    }

    public async Task<OperationResult> DeleteModelAsync(int id) {

        var operationResult = new OperationResult();

        _unitOfWork.GetRepository<IRepository<Model>, Model>().Delete(m => m.Id == id);
        await _unitOfWork.SaveChangesAsync();

        return operationResult;
    }

    public async Task<DtoListWithPossibleEntitiesCount<ModelDto>> GetAllModelsAsync(ModelFilterModel filterModel) {

        var pipeline = new SelectionPipeline<Model, ModelFilterModel>(filterModel, _builderDirector);

        var expressions = pipeline.Process();

        var source = _unitOfWork.GetRepository<IModelRepository, Model>().FindAllWithDetails(expressions);

        var possibleCarsCount = await _unitOfWork.GetRepository<IModelRepository, Model>()
            .CountAsync(expressions.FilterExpressions);

        //logger.LogInformation($"All games were returned. Returned games count: {source.Count()}");

        return new DtoListWithPossibleEntitiesCount<ModelDto>() {
            Dtos = _mapper.Map<IEnumerable<Model>, IEnumerable<ModelDto>>(source),
            PossibleDtosCount = possibleCarsCount
        };
    }

    public IEnumerable<ModelDto> GetAllModels() {

        var source = _unitOfWork.GetRepository<IModelRepository, Model>().FindAllWithDetails();

        return _mapper.Map<IEnumerable<Model>, IEnumerable<ModelDto>>(source);
    }

    public async Task<OperationResult<ModelDto>> GetModelById(int id) {

        var operationResult = new OperationResult<ModelDto>();

        var model = await _unitOfWork.GetRepository<IRepository<Model>, Model>().FirstOrDefaultAsync(m => m.Id == id);

        operationResult.Result = _mapper.Map<Model, ModelDto>(model);

        return operationResult;
    }
}