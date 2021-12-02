using CourseProject.BLL.DTO;
using CourseProject.BLL.Validation;

namespace CourseProject.BLL.Interfaces; 

public interface IEquipmentItemService {

    Task<OperationResult<EquipmentItemDto>> CreateItemAsync(EquipmentItemDto categoryDto);

    Task<OperationResult> EditItemAsync(EquipmentItemDto categoryDto);

    Task<OperationResult> DeleteItemAsync(int id);

    IEnumerable<EquipmentItemDto> GetAllItems();

    Task<OperationResult<EquipmentItemDto>> GetItemById(int id);
}