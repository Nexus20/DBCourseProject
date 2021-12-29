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

public class BrandService : IBrandService {

    private readonly IUnitOfWork _unitOfWork;

    private readonly IMapper _mapper;

    private readonly IPipelineBuilderDirector<Brand, BrandFilterModel> _builderDirector;

    public BrandService(IUnitOfWork unitOfWork, IMapper mapper, IPipelineBuilderDirector<Brand, BrandFilterModel> builderDirector) {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _builderDirector = builderDirector;
    }

    public async Task<OperationResult> CreateBrandAsync(BrandDto brandDto) {

        var operationResult = new OperationResult();

        var brand = _mapper.Map<BrandDto, Brand>(brandDto);

        await _unitOfWork.GetRepository<IRepository<Brand>, Brand>().CreateAsync(brand);

        await _unitOfWork.SaveChangesAsync();

        return operationResult;
    }

    public async Task<OperationResult> EditBrandAsync(BrandDto brandDto) {

        var operationResult = new OperationResult();

        var brand = _mapper.Map<BrandDto, Brand>(brandDto);

        _unitOfWork.GetRepository<IRepository<Brand>, Brand>().Update(brand);

        await _unitOfWork.SaveChangesAsync();

        return operationResult;
    }

    public async Task<OperationResult> DeleteBrandAsync(int id) {

        var operationResult = new OperationResult();

        _unitOfWork.GetRepository<IBrandRepository, Brand>().Delete(b => b.Id == id);
        await _unitOfWork.SaveChangesAsync();

        return operationResult;
    }

    public IEnumerable<BrandDto> GetAllBrands() {

        var source = _unitOfWork.GetRepository<IBrandRepository, Brand>().FindAllWithDetails();

        return _mapper.Map<IEnumerable<Brand>, IEnumerable<BrandDto>>(source);
    }

    public async Task<OperationResult<BrandDto>> GetBrandById(int id) {

        var operationResult = new OperationResult<BrandDto>();

        var brand = await _unitOfWork.GetRepository<IRepository<Brand>, Brand>().FirstOrDefaultAsync(b => b.Id == id);

        operationResult.Result = _mapper.Map<Brand, BrandDto>(brand);

        return operationResult;
    }

    public async Task<DtoListWithPossibleEntitiesCount<BrandDto>> GetAllBrandsAsync(BrandFilterModel filterModel) {

        var pipeline = new SelectionPipeline<Brand, BrandFilterModel>(filterModel, _builderDirector);

        var expressions = pipeline.Process();

        var source = _unitOfWork.GetRepository<IBrandRepository, Brand>().FindAllWithDetails(expressions);

        var possibleCarsCount = await _unitOfWork.GetRepository<IBrandRepository, Brand>()
            .CountAsync(expressions.FilterExpressions);

        //logger.LogInformation($"All games were returned. Returned games count: {source.Count()}");

        return new DtoListWithPossibleEntitiesCount<BrandDto>() {
            Dtos = _mapper.Map<IEnumerable<Brand>, IEnumerable<BrandDto>>(source),
            PossibleDtosCount = possibleCarsCount
        };
    }
}