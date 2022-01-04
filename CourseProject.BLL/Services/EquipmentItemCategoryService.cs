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

public class EquipmentItemCategoryService : IEquipmentItemCategoryService {

    private readonly IUnitOfWork _unitOfWork;

    private readonly IMapper _mapper;

    private readonly IPipelineBuilderDirector<EquipmentItemCategory, EquipmentItemCategoryFilterModel> _builderDirector;

    public EquipmentItemCategoryService(IUnitOfWork unitOfWork, IMapper mapper, IPipelineBuilderDirector<EquipmentItemCategory, EquipmentItemCategoryFilterModel> builderDirector) {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _builderDirector = builderDirector;
    }

    public async Task<OperationResult> CreateCategoryAsync(EquipmentItemCategoryDto modelDto) {

        var operationResult = new OperationResult();

        var model = _mapper.Map<EquipmentItemCategoryDto, EquipmentItemCategory>(modelDto);

        await _unitOfWork.GetRepository<IRepository<EquipmentItemCategory>, EquipmentItemCategory>().CreateAsync(model);

        await _unitOfWork.SaveChangesAsync();

        return operationResult;
    }

    public async Task<OperationResult> EditCategoryAsync(EquipmentItemCategoryDto modelDto) {

        var operationResult = new OperationResult();

        var model = _mapper.Map<EquipmentItemCategoryDto, EquipmentItemCategory>(modelDto);

        _unitOfWork.GetRepository<IRepository<EquipmentItemCategory>, EquipmentItemCategory>().Update(model);

        await _unitOfWork.SaveChangesAsync();

        return operationResult;
    }

    public async Task<OperationResult> DeleteCategoryAsync(int id) {

        var operationResult = new OperationResult();

        _unitOfWork.GetRepository<IRepository<EquipmentItemCategory>, EquipmentItemCategory>().Delete(m => m.Id == id);
        await _unitOfWork.SaveChangesAsync();

        return operationResult;
    }

    public IEnumerable<EquipmentItemCategoryDto> GetAllCategories() {

        var source = _unitOfWork.GetRepository<IRepository<EquipmentItemCategory>, EquipmentItemCategory>().Find(includeExpressions: ec => ec.EquipmentItems);

        return _mapper.Map<IEnumerable<EquipmentItemCategory>, IEnumerable<EquipmentItemCategoryDto>>(source);
    }

    public async Task<OperationResult<EquipmentItemCategoryDto>> GetCategoryById(int id) {

        var operationResult = new OperationResult<EquipmentItemCategoryDto>();

        var model = await _unitOfWork.GetRepository<IRepository<EquipmentItemCategory>, EquipmentItemCategory>().FirstOrDefaultAsync(m => m.Id == id);

        operationResult.Result = _mapper.Map<EquipmentItemCategory, EquipmentItemCategoryDto>(model);

        return operationResult;
    }

    public async Task<DtoListWithPossibleEntitiesCount<EquipmentItemCategoryDto>> GetAllCategoriesAsync(EquipmentItemCategoryFilterModel filterModel) {

        var pipeline = new SelectionPipeline<EquipmentItemCategory, EquipmentItemCategoryFilterModel>(filterModel, _builderDirector);

        var expressions = pipeline.Process();

        var source = _unitOfWork.GetRepository<IRepository<EquipmentItemCategory>, EquipmentItemCategory>().FindAllWithDetails(expressions);

        var possibleCarsCount = await _unitOfWork.GetRepository<IRepository<EquipmentItemCategory>, EquipmentItemCategory>()
            .CountAsync(expressions.FilterExpressions);

        //logger.LogInformation($"All games were returned. Returned games count: {source.Count()}");

        return new DtoListWithPossibleEntitiesCount<EquipmentItemCategoryDto>() {
            Dtos = _mapper.Map<IEnumerable<EquipmentItemCategory>, IEnumerable<EquipmentItemCategoryDto>>(source),
            PossibleDtosCount = possibleCarsCount
        };
    }
}