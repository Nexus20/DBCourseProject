using System.Security.Claims;
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

public class PurchaseOrderService : IPurchaseOrderService {

    private readonly IUnitOfWork _unitOfWork;

    private readonly IMapper _mapper;

    private readonly IPipelineBuilderDirector<PurchaseOrder, PurchaseOrderFilterModel> _builderDirector;

    public PurchaseOrderService(IUnitOfWork unitOfWork, IMapper mapper, IPipelineBuilderDirector<PurchaseOrder, PurchaseOrderFilterModel> builderDirector) {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _builderDirector = builderDirector;
    }


    public async Task<OperationResult<int>> CreateOrderAsync(string clientId, int[] equipment, ClientPersonalDataDto clientPersonalData) {

        var operationResult = new OperationResult<int>();

        await using var transaction = _unitOfWork.BeginTransaction();

        try {

            var user = await _unitOfWork.UserManager.FindByIdAsync(clientId);

            if (user == null && !string.IsNullOrWhiteSpace(clientPersonalData.Surname) &&
                !string.IsNullOrWhiteSpace(clientPersonalData.Name) &&
                !string.IsNullOrWhiteSpace(clientPersonalData.Email) &&
                !string.IsNullOrWhiteSpace(clientPersonalData.Phone) &&
                !string.IsNullOrWhiteSpace(clientPersonalData.Patronymic)) {

                var client = new Client() {
                    Email = clientPersonalData.Email,
                    Name = clientPersonalData.Name,
                    Surname = clientPersonalData.Surname,
                    Patronymic = clientPersonalData.Patronymic,
                    PhoneNumber = clientPersonalData.Phone,
                };

                await _unitOfWork.GetRepository<IClientRepository, Client>().CreateAsync(client);
                await _unitOfWork.SaveChangesAsync();

                user = client;
                clientId = user.Id;
            }

            if (user == null) {
                operationResult.AddError(nameof(clientId), "Such user not found");
                return operationResult;
            }

            foreach (var equipmentItemValueId in equipment) {
                if (!await _unitOfWork.GetRepository<IRepository<EquipmentItemValue>, EquipmentItemValue>().ContainsAsync(e => e.Id == equipmentItemValueId)) {
                    operationResult.AddError(nameof(equipmentItemValueId), "Such equipment not found");
                    return operationResult;
                }
            }

            var purchaseOrder = new PurchaseOrder() {
                ClientId = clientId,
                State = PurchaseOrderState.New,
                CreationDate = DateTime.Now,
                LastUpdateDate = DateTime.Now
            };

            await _unitOfWork.GetRepository<IRepository<PurchaseOrder>, PurchaseOrder>().CreateAsync(purchaseOrder);
            await _unitOfWork.SaveChangesAsync();

            foreach (var equipmentItemValueId in equipment) {

                var purchaseOrderEquipmentItemValue = new PurchaseOrderEquipmentItemValue() {
                    EquipmentItemValueId = equipmentItemValueId,
                    PurchaseOrderId = purchaseOrder.Id,
                };

                await _unitOfWork
                    .GetRepository<IRepository<PurchaseOrderEquipmentItemValue>, PurchaseOrderEquipmentItemValue>()
                    .CreateAsync(purchaseOrderEquipmentItemValue);
            }
            await _unitOfWork.SaveChangesAsync();
            await transaction.CommitAsync();
            operationResult.Result = purchaseOrder.Id;
        }
        catch (Exception ex) {
            await transaction.RollbackAsync();
            operationResult.AddError("Unexpected", "There is an unexpected error");
        }

        return operationResult;
    }

    public async Task<OperationResult<PurchaseOrderDto>> GetOrderByIdAsync(int id) {

        var operationResult = new OperationResult<PurchaseOrderDto>();

        var order = await _unitOfWork.GetRepository<IPurchaseOrderRepository, PurchaseOrder>()
            .FirstOrDefaultWithDetailsAsync(p => p.Id == id);

        if (order == null) {
            operationResult.AddError(nameof(id), "Such order not found");
            return operationResult;
        }

        operationResult.Result = _mapper.Map<PurchaseOrder, PurchaseOrderDto>(order);

        return operationResult;
    }

    public IEnumerable<PurchaseOrderDto> GetAllOrders() {

        var source = _unitOfWork.GetRepository<IPurchaseOrderRepository, PurchaseOrder>().FindAllWithDetails();

        return _mapper.Map<IEnumerable<PurchaseOrder>, IEnumerable<PurchaseOrderDto>>(source);
    }

    public async Task<DtoListWithPossibleEntitiesCount<PurchaseOrderDto>> GetAllPurchaseOrdersAsync(PurchaseOrderFilterModel filterModel) {

        var pipeline = new SelectionPipeline<PurchaseOrder, PurchaseOrderFilterModel>(filterModel, _builderDirector);

        var expressions = pipeline.Process();

        var source = _unitOfWork.GetRepository<IPurchaseOrderRepository, PurchaseOrder>().FindAllWithDetails(expressions);

        var possibleCarsCount = await _unitOfWork.GetRepository<IPurchaseOrderRepository, PurchaseOrder>()
            .CountAsync(expressions.FilterExpressions);

        //logger.LogInformation($"All games were returned. Returned games count: {source.Count()}");

        return new DtoListWithPossibleEntitiesCount<PurchaseOrderDto>() {
            Dtos = _mapper.Map<IEnumerable<PurchaseOrder>, IEnumerable<PurchaseOrderDto>>(source),
            PossibleDtosCount = possibleCarsCount
        };
    }

    public async Task<OperationResult> AcceptOrderAsync(int purchaseOrderId, ClaimsPrincipal user) {

        var operationResult = new OperationResult();

        var userId = _unitOfWork.UserManager.GetUserId(user);

        if (string.IsNullOrWhiteSpace(userId)) {
            operationResult.AddError(nameof(user), "User with such claims not found");
            return operationResult;
        }

        var manager = await _unitOfWork.GetRepository<IRepository<Manager>, Manager>().FirstOrDefaultAsync(x => x.UserId == userId);

        if (manager == null) {
            operationResult.AddError(nameof(userId), "Manager with such user id not found");
            return operationResult;
        }

        var purchaseOrder = await _unitOfWork.GetRepository<IRepository<PurchaseOrder>, PurchaseOrder>().FirstOrDefaultAsync(x => x.Id == purchaseOrderId);

        if (purchaseOrder == null) {
            operationResult.AddError(nameof(purchaseOrderId), "Order with such id not found");
            return operationResult;
        }

        if (purchaseOrder.State != PurchaseOrderState.New) {
            operationResult.AddError(nameof(purchaseOrderId), "This order is already being processed. Please go back and refresh the page.");
            return operationResult;
        }

        purchaseOrder.State = PurchaseOrderState.Processing;
        purchaseOrder.ManagerId = manager.Id;
        purchaseOrder.LastUpdateDate = DateTime.Now;

        _unitOfWork.GetRepository<IRepository<PurchaseOrder>, PurchaseOrder>().Update(purchaseOrder);
        await _unitOfWork.SaveChangesAsync();

        return operationResult;
    }
}