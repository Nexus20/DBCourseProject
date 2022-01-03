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

public class SupplierService : ISupplierService {

    private readonly IUnitOfWork _unitOfWork;

    private readonly IMapper _mapper;

    private readonly IPipelineBuilderDirector<Supplier, SupplierFilterModel> _builderDirector;

    public SupplierService(IUnitOfWork unitOfWork, IMapper mapper, IPipelineBuilderDirector<Supplier, SupplierFilterModel> builderDirector) {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _builderDirector = builderDirector;
    }

    public async Task<OperationResult> CreateSupplierAsync(SupplierDto dto) {

        var operationResult = new OperationResult();

        var entity = _mapper.Map<SupplierDto, Supplier>(dto);

        await _unitOfWork.GetRepository<IRepository<Supplier>, Supplier>().CreateAsync(entity);

        await _unitOfWork.SaveChangesAsync();

        return operationResult;
    }

    public async Task<OperationResult> EditSupplierAsync(SupplierDto dto) {

        var operationResult = new OperationResult();

        var entity = _mapper.Map<SupplierDto, Supplier>(dto);

        _unitOfWork.GetRepository<IRepository<Supplier>, Supplier>().Update(entity);

        await _unitOfWork.SaveChangesAsync();

        return operationResult;
    }

    public async Task<OperationResult> DeleteSupplierAsync(int id) {

        var operationResult = new OperationResult();

        _unitOfWork.GetRepository<IRepository<Supplier>, Supplier>().Delete(s => s.Id == id);
        await _unitOfWork.SaveChangesAsync();

        return operationResult;
    }

    public async Task<DtoListWithPossibleEntitiesCount<SupplierDto>> GetAllSuppliersAsync(SupplierFilterModel filterModel) {

        var pipeline = new SelectionPipeline<Supplier, SupplierFilterModel>(filterModel, _builderDirector);

        var expressions = pipeline.Process();

        var source = _unitOfWork.GetRepository<IRepository<Supplier>, Supplier>().FindAllWithDetails(expressions);

        var possibleCarsCount = await _unitOfWork.GetRepository<IRepository<Supplier>, Supplier>()
            .CountAsync(expressions.FilterExpressions);

        //logger.LogInformation($"All games were returned. Returned games count: {source.Count()}");

        return new DtoListWithPossibleEntitiesCount<SupplierDto>() {
            Dtos = _mapper.Map<IEnumerable<Supplier>, IEnumerable<SupplierDto>>(source),
            PossibleDtosCount = possibleCarsCount
        };
    }

    public async Task<OperationResult<SupplierDto>> GetSupplierByIdAsync(int id) {

        var operationResult = new OperationResult<SupplierDto>();

        var entity = await _unitOfWork.GetRepository<IRepository<Supplier>, Supplier>()
            .FirstOrDefaultAsync(m => m.Id == id, s => s.Brand);

        operationResult.Result = _mapper.Map<Supplier, SupplierDto>(entity);

        return operationResult;
    }

    public IEnumerable<SupplierDto> GetAllSuppliers() {

        var source = _unitOfWork.GetRepository<IRepository<Supplier>, Supplier>().FindAllWithDetails();

        return _mapper.Map<IEnumerable<Supplier>, IEnumerable<SupplierDto>>(source);
    }
}