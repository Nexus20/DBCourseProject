using CourseProject.BLL.DTO;
using CourseProject.BLL.FilterModels;
using CourseProject.BLL.Validation;

namespace CourseProject.BLL.Interfaces; 

public interface IEquipmentItemCategoryService {

    Task<OperationResult> CreateCategoryAsync(EquipmentItemCategoryDto categoryDto);

    Task<OperationResult> EditCategoryAsync(EquipmentItemCategoryDto categoryDto);

    Task<OperationResult> DeleteCategoryAsync(int id);

    IEnumerable<EquipmentItemCategoryDto> GetAllCategories();

    Task<OperationResult<EquipmentItemCategoryDto>> GetCategoryById(int id);

    Task<DtoListWithPossibleEntitiesCount<EquipmentItemCategoryDto>> GetAllCategoriesAsync(EquipmentItemCategoryFilterModel filterModel);
}