using CourseProject.BLL.DTO;
using CourseProject.BLL.FilterModels;
using CourseProject.BLL.Validation;

namespace CourseProject.BLL.Interfaces; 

public interface ICarInStockService {

    Task<DtoListWithPossibleEntitiesCount<CarInStockDto>> GetAllCarsInStockAsync(CarInStockFilterModel carFilterModel);

    Task<OperationResult<CarInStockDto>> GetCarInStockByIdAsync(int id);
}