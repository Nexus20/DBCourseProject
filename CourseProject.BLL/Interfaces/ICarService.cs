using CourseProject.BLL.DTO;
using CourseProject.BLL.FilterModels;
using CourseProject.BLL.Validation;
using Microsoft.AspNetCore.Http;

namespace CourseProject.BLL.Interfaces; 

public interface ICarService {

    Task<OperationResult> CreateCarAsync(CarDto carDto, IFormFileCollection formFileCollection = null,
        string directoryPath = null);

    Task<OperationResult> EditCarAsync(CarDto carDto);

    Task<OperationResult> DeleteCarAsync(int id, string directoryPath = null);

    IEnumerable<CarDto> GetAllCars(CarFilterModel carFilterModel = null);

    OperationResult<CarDto> GetCarById(int id);

    Task<OperationResult> CreateCarAsync(IFormFile carFile, string directoryPath);
}