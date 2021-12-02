using CourseProject.BLL.DTO;
using CourseProject.BLL.Validation;

namespace CourseProject.BLL.Interfaces; 

public interface IEquipmentItemValueService {

    Task<OperationResult<EquipmentItemValueDto>> CreateItemValueAsync(EquipmentItemValueDto categoryDto);

    Task<OperationResult> EditItemValueAsync(EquipmentItemValueDto categoryDto);

    Task<OperationResult> DeleteItemValueAsync(int id);

    IEnumerable<EquipmentItemValueDto> GetAllValues();

    Task<OperationResult<EquipmentItemValueDto>> GetItemValueById(int id);
}