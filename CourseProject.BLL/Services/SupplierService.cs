using AutoMapper;
using CourseProject.BLL.DTO;
using CourseProject.BLL.FilterModels;
using CourseProject.BLL.Interfaces;
using CourseProject.BLL.Validation;
using CourseProject.DAL.Entities;
using CourseProject.DAL.Interfaces;

namespace CourseProject.BLL.Services;

public class SupplierService : ISupplierService {

    private readonly IUnitOfWork _unitOfWork;

    private readonly IMapper _mapper;

    public SupplierService(IUnitOfWork unitOfWork, IMapper mapper) {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
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

    public IEnumerable<SupplierDto> GetAllSuppliers(SupplierFilterModel filterModel = null) {

        var source = filterModel == null
            ? _unitOfWork.GetRepository<IRepository<Supplier>, Supplier>()
                .Find(null, null, null, null, s => s.Brand, s => s.SupplyOrders)
            : _unitOfWork.GetRepository<IRepository<Supplier>, Supplier>()
                .Find(filterModel.FilterExpression, null, null, null, s => s.Brand, s => s.SupplyOrders);

        return _mapper.Map<IEnumerable<Supplier>, IEnumerable<SupplierDto>>(source);
    }

    public async Task<OperationResult<SupplierDto>> GetSupplierByIdAsync(int id) {

        var operationResult = new OperationResult<SupplierDto>();

        var entity = await _unitOfWork.GetRepository<IRepository<Supplier>, Supplier>()
            .FirstOrDefaultAsync(m => m.Id == id, s => s.Brand);

        operationResult.Result = _mapper.Map<Supplier, SupplierDto>(entity);

        return operationResult;
    }
}