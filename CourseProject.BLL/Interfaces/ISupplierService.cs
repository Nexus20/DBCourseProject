using CourseProject.BLL.DTO;
using CourseProject.BLL.FilterModels;
using CourseProject.BLL.Validation;

namespace CourseProject.BLL.Interfaces; 

public interface ISupplierService {

    Task<OperationResult> CreateSupplierAsync(SupplierDto dto);

    Task<OperationResult> EditSupplierAsync(SupplierDto dto);

    Task<OperationResult> DeleteSupplierAsync(int id);

    Task<DtoListWithPossibleEntitiesCount<SupplierDto>> GetAllSuppliersAsync(SupplierFilterModel filterModel);

    Task<OperationResult<SupplierDto>> GetSupplierByIdAsync(int id);

    IEnumerable<SupplierDto> GetAllSuppliers();
}