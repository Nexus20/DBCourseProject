using AutoMapper;
using CourseProject.BLL.DTO;
using CourseProject.BLL.Interfaces;
using CourseProject.BLL.Validation;
using CourseProject.DAL.Entities;
using CourseProject.DAL.Interfaces;

namespace CourseProject.BLL.Services; 

public class CarService : ICarService {

    private readonly IUnitOfWork _unitOfWork;

    private readonly IMapper _mapper;

    public CarService(IUnitOfWork unitOfWork, IMapper mapper) {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<OperationResult> CreateCarAsync(CarDto carDto) {

        var operationResult = new OperationResult();

        var car = _mapper.Map<CarDto, Car>(carDto);

        await _unitOfWork.GetRepository<IRepository<Car>, Car>().CreateAsync(car);

        await _unitOfWork.SaveChangesAsync();

        return operationResult;
    }

    public Task<OperationResult> EditCarAsync(CarDto carDto) {
        throw new NotImplementedException();
    }

    public Task<OperationResult> DeleteCarAsync(CarDto carDto) {
        throw new NotImplementedException();
    }
}