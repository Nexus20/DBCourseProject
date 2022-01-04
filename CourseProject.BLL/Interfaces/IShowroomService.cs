using CourseProject.BLL.DTO;
using CourseProject.BLL.FilterModels;
using CourseProject.BLL.Validation;

namespace CourseProject.BLL.Interfaces; 

public interface IShowroomService {

    Task<OperationResult> CreateShowroomAsync(ShowroomDto dto);

    Task<OperationResult> EditShowroomAsync(ShowroomDto dto);

    Task<OperationResult> DeleteShowroomAsync(int id);

    IEnumerable<ShowroomDto> GetAllShowrooms();

    Task<OperationResult<ShowroomDto>> GetShowroomByIdAsync(int id);

    Task<DtoListWithPossibleEntitiesCount<ShowroomDto>> GetAllShowroomsAsync(ShowroomFilterModel filterModel);
}