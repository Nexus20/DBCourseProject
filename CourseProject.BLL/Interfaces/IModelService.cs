using CourseProject.BLL.DTO;
using CourseProject.BLL.FilterModels;
using CourseProject.BLL.Validation;

namespace CourseProject.BLL.Interfaces; 

public interface IModelService {

    Task<OperationResult> CreateModelAsync(ModelDto modelDto);

    Task<OperationResult> EditModelAsync(ModelDto modelDto);

    Task<OperationResult> DeleteModelAsync(int id);

    IEnumerable<ModelDto> GetAllModels();

    Task<DtoListWithPossibleEntitiesCount<ModelDto>> GetAllModelsAsync(ModelFilterModel filterModel);

    Task<OperationResult<ModelDto>> GetModelById(int id);
}