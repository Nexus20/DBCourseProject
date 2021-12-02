using AutoMapper;
using CourseProject.BLL.DTO;
using CourseProject.BLL.Interfaces;
using CourseProject.BLL.Validation;
using CourseProject.DAL.Entities;
using CourseProject.DAL.Interfaces;

namespace CourseProject.BLL.Services; 

public class EquipmentItemService : IEquipmentItemService {

    private readonly IUnitOfWork _unitOfWork;

    private readonly IMapper _mapper;

    public EquipmentItemService(IUnitOfWork unitOfWork, IMapper mapper) {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<OperationResult<EquipmentItemDto>> CreateItemAsync(EquipmentItemDto modelDto) {

        var operationResult = new OperationResult<EquipmentItemDto>();

        var item = _mapper.Map<EquipmentItemDto, EquipmentItem>(modelDto);

        await _unitOfWork.GetRepository<IRepository<EquipmentItem>, EquipmentItem>().CreateAsync(item);

        await _unitOfWork.SaveChangesAsync();

        operationResult.Result = _mapper.Map<EquipmentItem, EquipmentItemDto>(item);

        return operationResult;
    }

    public async Task<OperationResult> EditItemAsync(EquipmentItemDto modelDto) {

        var operationResult = new OperationResult();

        var model = _mapper.Map<EquipmentItemDto, EquipmentItem>(modelDto);

        _unitOfWork.GetRepository<IRepository<EquipmentItem>, EquipmentItem>().Update(model);

        await _unitOfWork.SaveChangesAsync();

        return operationResult;
    }

    public async Task<OperationResult> DeleteItemAsync(int id) {

        var operationResult = new OperationResult();

            _unitOfWork.GetRepository<IRepository<EquipmentItem>, EquipmentItem>().Delete(m => m.Id == id);
        await _unitOfWork.SaveChangesAsync();

        return operationResult;
    }

    public IEnumerable<EquipmentItemDto> GetAllItems() {

        var source = _unitOfWork.GetRepository<IRepository<EquipmentItem>, EquipmentItem>().FindAll();

        return _mapper.Map<IEnumerable<EquipmentItem>, IEnumerable<EquipmentItemDto>>(source);
    }

    public async Task<OperationResult<EquipmentItemDto>> GetItemById(int id) {

        var operationResult = new OperationResult<EquipmentItemDto>();

        var model = await _unitOfWork.GetRepository<IRepository<EquipmentItem>, EquipmentItem>().FirstOrDefaultAsync(m => m.Id == id);

        operationResult.Result = _mapper.Map<EquipmentItem, EquipmentItemDto>(model);

        return operationResult;
    }
}