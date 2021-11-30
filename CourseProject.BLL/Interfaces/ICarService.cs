using CourseProject.BLL.DTO;
using CourseProject.BLL.Validation;

namespace CourseProject.BLL.Interfaces; 

public interface ICarService {

    Task<OperationResult> CreateCarAsync(CarDto carDto);

    Task<OperationResult> EditCarAsync(CarDto carDto);

    Task<OperationResult> DeleteCarAsync(CarDto carDto);

    IEnumerable<CarDto> GetAllCars();

    OperationResult<CarDto> GetCarById(int id);
}