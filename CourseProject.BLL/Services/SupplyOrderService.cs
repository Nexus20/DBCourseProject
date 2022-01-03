using AutoMapper;
using CourseProject.BLL.DTO;
using CourseProject.BLL.FilterModels;
using CourseProject.BLL.Interfaces;
using CourseProject.BLL.Pipeline;
using CourseProject.BLL.PipelineBuilders;
using CourseProject.BLL.Validation;
using CourseProject.DAL.Entities;
using CourseProject.DAL.Interfaces;
using CourseProject.Domain;

namespace CourseProject.BLL.Services; 

public class SupplyOrderService : ISupplyOrderService {

    private readonly IUnitOfWork _unitOfWork;

    private readonly IMapper _mapper;

    private readonly IPipelineBuilderDirector<SupplyOrder, SupplyOrderFilterModel> _builderDirector;

    public SupplyOrderService(IUnitOfWork unitOfWork, IMapper mapper, IPipelineBuilderDirector<SupplyOrder, SupplyOrderFilterModel> builderDirector) {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _builderDirector = builderDirector;
    }

    public async Task<OperationResult<int>> CreateOrderAsync(SupplyOrderDto dto) {

        var operationResult = new OperationResult<int>();

        var model = _mapper.Map<SupplyOrderDto, SupplyOrder>(dto);
        model.CreationDate = model.LastUpdateDate = DateTime.Now;

        await _unitOfWork.GetRepository<IRepository<SupplyOrder>, SupplyOrder>().CreateAsync(model);

        await _unitOfWork.SaveChangesAsync();

        operationResult.Result = model.Id;
        return operationResult;
    }

    public async Task<OperationResult<SupplyOrderDto>> GetOrderByIdAsync(int id) {

        var operationResult = new OperationResult<SupplyOrderDto>();

        var model = await _unitOfWork.GetRepository<IRepository<SupplyOrder>, SupplyOrder>().FirstOrDefaultWithDetailsAsync(s => s.Id == id);

        operationResult.Result = _mapper.Map<SupplyOrder, SupplyOrderDto>(model);

        return operationResult;
    }

    public IEnumerable<SupplyOrderDto> GetAllOrders() {
        throw new NotImplementedException();
    }

    public async Task<DtoListWithPossibleEntitiesCount<SupplyOrderDto>> GetAllSupplyOrdersAsync(SupplyOrderFilterModel filterModel) {

        var pipeline = new SelectionPipeline<SupplyOrder, SupplyOrderFilterModel>(filterModel, _builderDirector);

        var expressions = pipeline.Process();

        var source = _unitOfWork.GetRepository<IRepository<SupplyOrder>, SupplyOrder>().FindAllWithDetails(expressions);

        var possibleCarsCount = await _unitOfWork.GetRepository<IRepository<SupplyOrder>, SupplyOrder>()
            .CountAsync(expressions.FilterExpressions);

        //logger.LogInformation($"All games were returned. Returned games count: {source.Count()}");

        return new DtoListWithPossibleEntitiesCount<SupplyOrderDto>() {
            Dtos = _mapper.Map<IEnumerable<SupplyOrder>, IEnumerable<SupplyOrderDto>>(source),
            PossibleDtosCount = possibleCarsCount
        };
    }

    public async Task<OperationResult> AddCarToSupplyOrderAsync(int supplyOrderId, int[] equipment, int carsCount, Guid managerId) {

        var operationResult = new OperationResult();

        var manager = await _unitOfWork.GetRepository<IRepository<Manager>, Manager>()
            .FirstOrDefaultAsync(m => m.Id == managerId);
        
        if (manager == null) {
            operationResult.AddError(nameof(managerId), "Such manager not found");
            return operationResult;
        }

        var supplyOrder = await _unitOfWork.GetRepository<IRepository<SupplyOrder>, SupplyOrder>().FirstOrDefaultAsync(so => so.Id == supplyOrderId);

        if (supplyOrder == null) {
            operationResult.AddError(nameof(supplyOrderId), "Such supply order not found");
            return operationResult;
        }

        foreach (var equipmentItemValueId in equipment) {
            if (!await _unitOfWork.GetRepository<IRepository<EquipmentItemValue>, EquipmentItemValue>().ContainsAsync(e => e.Id == equipmentItemValueId)) {
                operationResult.AddError(nameof(equipmentItemValueId), "Such equipment not found");
                return operationResult;
            }
        }

        await using var transaction = _unitOfWork.BeginTransaction();

        try {
            var supplyOrderPart = new SupplyOrderPart() {
                Count = carsCount,
                SupplyOrderId  = supplyOrderId
            };

            await _unitOfWork.GetRepository<IRepository<SupplyOrderPart>, SupplyOrderPart>().CreateAsync(supplyOrderPart);
            await _unitOfWork.SaveChangesAsync();

            foreach (var equipmentItemValueId in equipment) {

                var supplyOrderPartEquipmentItemValue = new SupplyOrderPartEquipmentItemValue() {
                    EquipmentItemValueId = equipmentItemValueId,
                    SupplyOrderPartId = supplyOrderPart.Id,
                };

                await _unitOfWork
                    .GetRepository<IRepository<SupplyOrderPartEquipmentItemValue>, SupplyOrderPartEquipmentItemValue>()
                    .CreateAsync(supplyOrderPartEquipmentItemValue);
            }
            await _unitOfWork.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception ex) {
            await transaction.RollbackAsync();
            operationResult.AddError("Unexpected", "There is an unexpected error");
        }

        return operationResult;
    }

    public async Task<OperationResult> SendSupplyOrderAsync(int supplyOrderId) {

        var operationResult = new OperationResult();

        var supplyOrder = await _unitOfWork.GetRepository<IRepository<SupplyOrder>, SupplyOrder>()
            .FirstOrDefaultAsync(so => so.Id == supplyOrderId);

        if (supplyOrder == null) {
            operationResult.AddError(nameof(supplyOrderId), "Such supply order not found");
            return operationResult;
        }

        supplyOrder.State = SupplyOrderState.Processing;

        _unitOfWork.GetRepository<IRepository<SupplyOrder>, SupplyOrder>().Update(supplyOrder);
        await _unitOfWork.SaveChangesAsync();

        return operationResult;
    }

    public async Task<OperationResult> CloseSupplyOrderAsync(int supplyOrderId) {

        var operationResult = new OperationResult();

        var supplyOrder = await _unitOfWork.GetRepository<IRepository<SupplyOrder>, SupplyOrder>()
            .FirstOrDefaultAsync(so => so.Id == supplyOrderId);

        if (supplyOrder == null) {
            operationResult.AddError(nameof(supplyOrderId), "Such supply order not found");
            return operationResult;
        }

        supplyOrder.State = SupplyOrderState.Closed;

        _unitOfWork.GetRepository<IRepository<SupplyOrder>, SupplyOrder>().Update(supplyOrder);
        await _unitOfWork.SaveChangesAsync();

        return operationResult;
    }

    public async Task<OperationResult<int>> TakeCarsToShowroom(CloseSupplyOrderDto dto) {

        var operationResult = new OperationResult<int>();

        var supplyOrder = await _unitOfWork.GetRepository<IRepository<SupplyOrder>, SupplyOrder>()
            .FirstOrDefaultWithDetailsAsync(so => so.Id == dto.SupplyOrderId);

        if (supplyOrder == null) {
            operationResult.AddError(nameof(dto.SupplyOrderId), "Such supply order not found");
            return operationResult;
        }

        var manager = await _unitOfWork.GetRepository<IRepository<Manager>, Manager>()
            .FirstOrDefaultAsync(m => m.Id.ToString() == dto.ManagerId);

        if (manager == null) {
            operationResult.AddError(nameof(dto.ManagerId), "Such manager not found");
            return operationResult;
        }

        foreach (var part in dto.Parts) {

            var car = await _unitOfWork.GetRepository<ICarRepository, Car>()
                .FirstOrDefaultAsync(c => c.Id == part.CarId);

            if (car == null) {
                operationResult.AddError(nameof(part.CarId), "Such car not found");
                return operationResult;
            }
        }

        await using var transaction = _unitOfWork.BeginTransaction();

        try {

            foreach (var part in dto.Parts) {

                foreach (var code in part.VinCodes) {

                    var carInStock = new CarInStock() {
                        CarId = part.CarId,
                        ShowroomId = manager.ShowroomId,
                        VinCode = code
                    };

                    await _unitOfWork.GetRepository<IRepository<CarInStock>, CarInStock>().CreateAsync(carInStock);
                    await _unitOfWork.SaveChangesAsync();


                    foreach (var supplyOrderPart in supplyOrder.Parts) {

                        var equipment = supplyOrderPart.SupplyOrderPartEquipmentItemsValues.Where(x =>
                            x.EquipmentItemValue.EquipmentItem.CarId == carInStock.CarId).Select(y => y.EquipmentItemValue);

                        foreach (var equipmentItemValue in equipment) {

                            var carInStockEquipmentItemValue = new CarInStockEquipmentItemValue() {
                                EquipmentItemValueId = equipmentItemValue.Id,
                                CarInStockId = carInStock.Id
                            };

                            await _unitOfWork
                                .GetRepository<IRepository<CarInStockEquipmentItemValue>, CarInStockEquipmentItemValue>()
                                .CreateAsync(carInStockEquipmentItemValue);
                            await _unitOfWork.SaveChangesAsync();
                        }
                    }
                }
            }

            await transaction.CommitAsync();
        }
        catch (Exception ex) {
            await transaction.RollbackAsync();
            operationResult.AddError("Unexpected", $"There is an error: {ex.Message}");
        }
        return operationResult;
    }
}