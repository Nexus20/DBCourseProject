using AutoMapper;
using CourseProject.BLL.DTO;
using CourseProject.BLL.Interfaces;
using CourseProject.BLL.Validation;
using CourseProject.DAL.Entities;
using CourseProject.DAL.Interfaces;
using CourseProject.Domain;

namespace CourseProject.BLL.Services; 

public class PurchaseOrderService : IPurchaseOrderService {

    private readonly IUnitOfWork _unitOfWork;

    private readonly IMapper _mapper;

    public PurchaseOrderService(IUnitOfWork unitOfWork, IMapper mapper) {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }


    public async Task<OperationResult> CreateOrderAsync(string clientId, int[] equipment) {

        var operationResult = new OperationResult();

        var user = await _unitOfWork.UserManager.FindByIdAsync(clientId);

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

        await using var transaction = _unitOfWork.BeginTransaction();

        try {
            var purchaseOrder = new PurchaseOrder() {
                ClientId = clientId,
                State = PurchaseOrderState.New,
            };

            await _unitOfWork.GetRepository<IRepository<PurchaseOrder>, PurchaseOrder>().CreateAsync(purchaseOrder);
            await _unitOfWork.SaveChangesAsync();

            foreach (var equipmentItemValueId in equipment) {

                var pur = new PurchaseOrderEquipmentItemValue() {
                    EquipmentItemValueId = equipmentItemValueId,
                    PurchaseOrderId = purchaseOrder.Id,
                };

                await _unitOfWork
                    .GetRepository<IRepository<PurchaseOrderEquipmentItemValue>, PurchaseOrderEquipmentItemValue>()
                    .CreateAsync(pur);
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

    public Task<OperationResult<PurchaseOrderDto>> GetOrderById(int id) {
        throw new NotImplementedException();
    }
}