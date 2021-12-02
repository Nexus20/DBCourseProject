using AutoMapper;
using CourseProject.BLL.DTO;
using CourseProject.BLL.Interfaces;
using CourseProject.BLL.Validation;
using CourseProject.DAL.Entities;
using CourseProject.DAL.Interfaces;

namespace CourseProject.BLL.Services; 

public class EquipmentItemValueService : IEquipmentItemValueService {

    private readonly IUnitOfWork _unitOfWork;

    private readonly IMapper _mapper;

    public EquipmentItemValueService(IUnitOfWork unitOfWork, IMapper mapper) {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<OperationResult<EquipmentItemValueDto>> CreateItemValueAsync(EquipmentItemValueDto modelDto) {

        var operationResult = new OperationResult<EquipmentItemValueDto>();

        var item = _mapper.Map<EquipmentItemValueDto, EquipmentItemValue>(modelDto);

        await _unitOfWork.GetRepository<IRepository<EquipmentItemValue>, EquipmentItemValue>().CreateAsync(item);

        await _unitOfWork.SaveChangesAsync();

        operationResult.Result = _mapper.Map<EquipmentItemValue, EquipmentItemValueDto>(item);

        return operationResult;
    }

    public async Task<OperationResult> EditItemValueAsync(EquipmentItemValueDto modelDto) {

        var operationResult = new OperationResult();

        var model = _mapper.Map<EquipmentItemValueDto, EquipmentItemValue>(modelDto);

        _unitOfWork.GetRepository<IRepository<EquipmentItemValue>, EquipmentItemValue>().Update(model);

        await _unitOfWork.SaveChangesAsync();

        return operationResult;
    }

    public async Task<OperationResult> DeleteItemValueAsync(int id) {

        var operationResult = new OperationResult();

            _unitOfWork.GetRepository<IRepository<EquipmentItemValue>, EquipmentItemValue>().Delete(m => m.Id == id);
        await _unitOfWork.SaveChangesAsync();

        return operationResult;
    }

    public IEnumerable<EquipmentItemValueDto> GetAllValues() {

        var source = _unitOfWork.GetRepository<IRepository<EquipmentItemValue>, EquipmentItemValue>().FindAll();

        return _mapper.Map<IEnumerable<EquipmentItemValue>, IEnumerable<EquipmentItemValueDto>>(source);
    }

    public async Task<OperationResult<EquipmentItemValueDto>> GetItemValueById(int id) {

        var operationResult = new OperationResult<EquipmentItemValueDto>();

        var model = await _unitOfWork.GetRepository<IRepository<EquipmentItemValue>, EquipmentItemValue>().FirstOrDefaultAsync(m => m.Id == id);

        operationResult.Result = _mapper.Map<EquipmentItemValue, EquipmentItemValueDto>(model);

        return operationResult;
    }
}