using AutoMapper;
using CourseProject.BLL.DTO;
using CourseProject.BLL.Interfaces;
using CourseProject.BLL.Validation;
using CourseProject.DAL.Entities;
using CourseProject.DAL.Interfaces;

namespace CourseProject.BLL.Services; 

public class BrandService : IBrandService {

    private readonly IUnitOfWork _unitOfWork;

    private readonly IMapper _mapper;

    public BrandService(IUnitOfWork unitOfWork, IMapper mapper) {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
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

    public Task<OperationResult> DeleteBrandAsync(BrandDto brandDto) {
        throw new NotImplementedException();
    }

    public IEnumerable<BrandDto> GetAllBrands() {

        var source = _unitOfWork.GetRepository<IBrandRepository, Brand>().FindAllWithDetails();

        return _mapper.Map<IEnumerable<Brand>, IEnumerable<BrandDto>>(source);
    }

    public async Task<OperationResult<BrandDto>> GetBrandById(int id) {

        var operationResult = new OperationResult<BrandDto>();

        var brand = await _unitOfWork.GetRepository<IRepository<Brand>, Brand>().FirstOrDefaultAsync(c => c.Id == id);

        operationResult.Result = _mapper.Map<Brand, BrandDto>(brand);

        return operationResult;
    }
}