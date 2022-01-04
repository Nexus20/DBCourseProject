using CourseProject.BLL.DTO;
using CourseProject.BLL.FilterModels;
using CourseProject.BLL.Validation;
using Microsoft.AspNetCore.Http;

namespace CourseProject.BLL.Interfaces; 

public interface ICarService {

    Task<OperationResult> CreateCarAsync(CarDto carDto, IFormFileCollection formFileCollection = null,
        string directoryPath = null);

    Task<OperationResult> EditCarAsync(CarDto carDto, IFormFileCollection formFileCollection = null,
        string directoryPath = null);

    Task<OperationResult> DeleteCarAsync(int id, string directoryPath = null);

    Task<DtoListWithPossibleEntitiesCount<CarDto>> GetAllCarsAsync(CarFilterModel carFilterModel = null);

    Task<OperationResult<CarDto>> GetCarByIdAsync(int id);

    Task<OperationResult> CreateCarAsync(IFormFile carFile, string directoryPath);
}