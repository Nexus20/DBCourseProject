using CourseProject.BLL.DTO;
using CourseProject.BLL.Validation;
using Microsoft.AspNetCore.Http;

namespace CourseProject.BLL.Interfaces; 

public interface ICarService {

    Task<OperationResult> CreateCarAsync(CarDto carDto, IFormFileCollection formFileCollection = null,
        string directoryPath = null);

    Task<OperationResult> EditCarAsync(CarDto carDto);

    Task<OperationResult> DeleteCarAsync(CarDto carDto);

    IEnumerable<CarDto> GetAllCars();

    OperationResult<CarDto> GetCarById(int id);
}