using System.Globalization;
using AutoMapper;
using ClosedXML.Excel;
using CourseProject.BLL.DTO;
using CourseProject.BLL.FilterModels;
using CourseProject.BLL.Interfaces;
using CourseProject.BLL.Pipeline;
using CourseProject.BLL.PipelineBuilders;
using CourseProject.BLL.Validation;
using CourseProject.DAL.Entities;
using CourseProject.DAL.Interfaces;
using Microsoft.AspNetCore.Http;

namespace CourseProject.BLL.Services; 

public class CarService : ICarService {

    private readonly IUnitOfWork _unitOfWork;

    private readonly IMapper _mapper;

    private readonly IPipelineBuilderDirector<Car, CarFilterModel> _builderDirector;

    public CarService(IUnitOfWork unitOfWork, IMapper mapper, IPipelineBuilderDirector<Car, CarFilterModel> builderDirector) {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _builderDirector = builderDirector;
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

    public async Task<OperationResult> EditCarAsync(CarDto carDto, IFormFileCollection formFileCollection = null,
        string directoryPath = null) {

        var operationResult = new OperationResult();

        await using var transaction = _unitOfWork.BeginTransaction();

        try {
            var car = _mapper.Map<CarDto, Car>(carDto);
            _unitOfWork.GetRepository<IRepository<Car>, Car>().Update(car);

            await _unitOfWork.SaveChangesAsync();

            if (formFileCollection.Any() && !string.IsNullOrWhiteSpace(directoryPath)) {

                directoryPath = Path.Combine(directoryPath, car.Id.ToString());

                if (!Directory.Exists(directoryPath)) {
                    var dirInfo = new DirectoryInfo(directoryPath);
                    dirInfo.Create();
                }

                foreach (var uploadedImage in formFileCollection) {

                    var path = $"/img/cars/{car.Id}/{uploadedImage.FileName}";

                    await using (var fileStream = new FileStream(Path.Combine(directoryPath, uploadedImage.FileName), FileMode.Create)) {
                        await uploadedImage.CopyToAsync(fileStream);
                    }

                    await _unitOfWork.GetRepository<IRepository<CarPhoto>, CarPhoto>().CreateAsync(new CarPhoto() { CarId = car.Id, Path = path });
                }

                await _unitOfWork.SaveChangesAsync();
            }

            await transaction.CommitAsync();
        }
        catch (Exception ex) {
            await transaction.RollbackAsync();
            operationResult.AddError("Unexpected", $"There is an error: {ex.Message}");
        }

        return operationResult;
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

    public async Task<DtoListWithPossibleEntitiesCount<CarDto>> GetAllCarsAsync(CarFilterModel filterModel) {

        var pipeline = new SelectionPipeline<Car, CarFilterModel>(filterModel, _builderDirector);

        var expressions = pipeline.Process();

        var source = _unitOfWork.GetRepository<ICarRepository, Car>().FindAllWithDetails(expressions);

        var possibleCarsCount = await _unitOfWork.GetRepository<ICarRepository, Car>()
            .CountAsync(expressions.FilterExpressions);

        //logger.LogInformation($"All games were returned. Returned games count: {source.Count()}");

        return new DtoListWithPossibleEntitiesCount<CarDto>() {
            Dtos = _mapper.Map<IEnumerable<Car>, IEnumerable<CarDto>>(source),
            PossibleDtosCount = possibleCarsCount
        };
    }

    public async Task<OperationResult<CarDto>> GetCarByIdAsync(int id) {

        var operationResult = new OperationResult<CarDto>();

        var car = await _unitOfWork.GetRepository<ICarRepository, Car>().FirstOrDefaultWithDetailsAsync(c => c.Id == id);

        operationResult.Result = _mapper.Map<Car, CarDto>(car);

        return operationResult;
    }

    public async Task<OperationResult> CreateCarAsync(IFormFile carFile, string directoryPath) {

        var operationResult = new OperationResult<CarDto>();

        await using var transaction = _unitOfWork.BeginTransaction();

        if (!Directory.Exists(directoryPath)) {
            var dirInfo = new DirectoryInfo(directoryPath);
            dirInfo.Create();
        }

        try {

            var excelFilePath = Path.Combine(directoryPath, carFile.FileName);

            await using (var fileStream = new FileStream(excelFilePath, FileMode.Create)) {
                await carFile.CopyToAsync(fileStream);
            }

            var workbook = new XLWorkbook(excelFilePath);
            var worksheet = workbook.Worksheet(1);

            var brandRow = worksheet.FirstRowUsed();

            var brandName = brandRow.Cell(2).GetString();

            var textInfo = CultureInfo.InvariantCulture.TextInfo;

            var brand = await _unitOfWork.GetRepository<IRepository<Brand>, Brand>()
                .FirstOrDefaultAsync(b => b.Name.ToLower() == brandName.ToLower());

            if (brand == null) {
                brand = new Brand() {
                    Name = textInfo.ToTitleCase(brandName),
                };

                await _unitOfWork.GetRepository<IRepository<Brand>, Brand>().CreateAsync(brand);
                await _unitOfWork.SaveChangesAsync();
            }

            var modelRow = brandRow.RowBelow();

            var modelName = modelRow.Cell(2).GetString();

            var model = await _unitOfWork.GetRepository<IRepository<Model>, Model>()
                .FirstOrDefaultAsync(m => m.Name.ToLower() == modelName.ToLower());

            if (model == null) {

                model = new Model() {
                    BrandId = brand.Id,
                    Name = modelName
                };

                await _unitOfWork.GetRepository<IRepository<Model>, Model>().CreateAsync(model);
                await _unitOfWork.SaveChangesAsync();
            }

            var subModelRow = modelRow.RowBelow();

            var subModelName = subModelRow.Cell(2).GetString();

            var car = new Car() {
                ModelId = model.Id,
                Submodel = textInfo.ToTitleCase(subModelName),
            };

            await _unitOfWork.GetRepository<IRepository<Car>, Car>().CreateAsync(car);
            await _unitOfWork.SaveChangesAsync();

            var equipmentItemRow = subModelRow.RowBelow(4);

            while (!equipmentItemRow.Cell(1).IsEmpty()) {
                var categoryName = equipmentItemRow.Cell(1).GetString();

                var category = await _unitOfWork
                    .GetRepository<IRepository<EquipmentItemCategory>, EquipmentItemCategory>()
                    .FirstOrDefaultAsync(c => c.Name.ToLower() == categoryName.ToLower());

                var unitsOfMeasure = equipmentItemRow.Cell(3).GetString();

                if (category == null) {
                    category = new EquipmentItemCategory() {
                        Name = textInfo.ToTitleCase(categoryName),
                        UnitsOfMeasure = unitsOfMeasure
                    };

                    await _unitOfWork.GetRepository<IRepository<EquipmentItemCategory>, EquipmentItemCategory>()
                        .CreateAsync(category);
                }

                var optional = equipmentItemRow.Cell(5).GetString().ToLower() switch {
                    "yes" => true,
                    "no" => false,
                    _ => throw new ArgumentException()
                };

                var equipmentItem = await _unitOfWork.GetRepository<IRepository<EquipmentItem>, EquipmentItem>()
                    .FirstOrDefaultAsync(e => e.EquipmentItemCategoryId == category.Id && e.CarId == car.Id);

                if (equipmentItem == null) {

                    equipmentItem = new EquipmentItem() {
                        EquipmentItemCategoryId = category.Id,
                        CarId = car.Id,
                        Optional = optional
                    };

                    await _unitOfWork.GetRepository<IRepository<EquipmentItem>, EquipmentItem>()
                        .CreateAsync(equipmentItem);
                    await _unitOfWork.SaveChangesAsync();
                }
                
                var value = equipmentItemRow.Cell(2).GetString();
                var price = (decimal)equipmentItemRow.Cell(4).GetDouble();

                var equipmentItemValue = new EquipmentItemValue() {
                    EquipmentItemId = equipmentItem.Id,
                    Price = price,
                    Value = textInfo.ToTitleCase(value)
                };

                await _unitOfWork.GetRepository<IRepository<EquipmentItemValue>, EquipmentItemValue>()
                    .CreateAsync(equipmentItemValue);
                await _unitOfWork.SaveChangesAsync();

                equipmentItemRow = equipmentItemRow.RowBelow();
            }

            await transaction.CommitAsync();
        }
        catch (Exception ex) {
            await transaction.RollbackAsync();
            operationResult.AddError("Unexpected", "There is unexpected error");
        }

        return operationResult;
    }
}