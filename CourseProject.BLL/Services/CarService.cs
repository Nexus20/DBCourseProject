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

    public IEnumerable<CarDto> GetAllCars() {

        var source = _unitOfWork.GetRepository<ICarRepository, Car>().FindAllWithDetails();

        //var source = includeDeleted
        //    ? _unitOfWork.GetRepository<IGameRepository, Game>().FindAllWithDetails()
        //    : _unitOfWork.GetRepository<IGameRepository, Game>().FindAllWithDetails(g => g.IsDeleted == false);

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