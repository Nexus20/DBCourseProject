using AutoMapper;
using CourseProject.BLL.DTO;
using CourseProject.BLL.FilterModels;
using CourseProject.BLL.Interfaces;
using CourseProject.BLL.Validation;
using CourseProject.DAL.Entities;
using CourseProject.DAL.Interfaces;
using Microsoft.AspNetCore.Http;

namespace CourseProject.BLL.Services; 

public class CarService : ICarService {

    private readonly IUnitOfWork _unitOfWork;

    private readonly IMapper _mapper;

    public CarService(IUnitOfWork unitOfWork, IMapper mapper) {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<OperationResult> CreateCarAsync(CarDto carDto, IFormFileCollection formFileCollection = null,
        string directoryPath = null) {

        var operationResult = new OperationResult();

        var car = _mapper.Map<CarDto, Car>(carDto);

        await _unitOfWork.GetRepository<IRepository<Car>, Car>().CreateAsync(car);

        await _unitOfWork.SaveChangesAsync();

        if (formFileCollection != null && formFileCollection.Any() && !string.IsNullOrWhiteSpace(directoryPath)) {
            directoryPath = Path.Combine(directoryPath, car.Id.ToString());

            if (!Directory.Exists(directoryPath)) {
                var dirInfo = new DirectoryInfo(directoryPath);
                dirInfo.Create();
            }

            foreach (var uploadedImage in formFileCollection) {

                var path = $"/img/cars/{car.Id}/{uploadedImage.FileName}";

                using (var fileStream = new FileStream(Path.Combine(directoryPath, uploadedImage.FileName), FileMode.Create)) {
                    await uploadedImage.CopyToAsync(fileStream);
                }

                await _unitOfWork.GetRepository<IRepository<CarPhoto>, CarPhoto>().CreateAsync(new CarPhoto() { CarId = car.Id, Path = path });
            }

            await _unitOfWork.SaveChangesAsync();

        }

        return operationResult;
    }

    public Task<OperationResult> EditCarAsync(CarDto carDto) {
        throw new NotImplementedException();
    }

    public async Task<OperationResult> DeleteCarAsync(int id, string directoryPath = null) {
        var operationResult = new OperationResult();

        if (!string.IsNullOrWhiteSpace(directoryPath)) {
            if (Directory.Exists(directoryPath)) {
                var dirInfo = new DirectoryInfo(directoryPath);
                dirInfo.Delete(true);
            }

            _unitOfWork.GetRepository<IRepository<CarPhoto>, CarPhoto>().Delete(c => c.CarId == id);
        }

        _unitOfWork.GetRepository<IRepository<Car>, Car>().Delete(c => c.Id == id);
        await _unitOfWork.SaveChangesAsync();

        return operationResult;
    }

    public IEnumerable<CarDto> GetAllCars(CarFilterModel carFilterModel = null) {

        var source = carFilterModel != null
            ? _unitOfWork.GetRepository<ICarRepository, Car>().FindAllWithDetails(carFilterModel.FilterExpression)
            : _unitOfWork.GetRepository<ICarRepository, Car>().FindAllWithDetails();

        //logger.LogInformation($"All games were returned. Returned games count: {source.Count()}");

        return _mapper.Map<IEnumerable<Car>, IEnumerable<CarDto>>(source);
    }

    public OperationResult<CarDto> GetCarById(int id) {

        var operationResult = new OperationResult<CarDto>();

        var car = _unitOfWork.GetRepository<ICarRepository, Car>().FirstOrDefaultWithDetails(c => c.Id == id);

        operationResult.Result = _mapper.Map<Car, CarDto>(car);

        return operationResult;
    }
}