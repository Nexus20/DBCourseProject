using CourseProject.BLL.DTO;
using CourseProject.BLL.Validation;

namespace CourseProject.BLL.Interfaces; 

public interface IBrandService {

    Task<OperationResult> CreateBrandAsync(BrandDto brandDto);

    Task<OperationResult> EditBrandAsync(BrandDto brandDto);

    Task<OperationResult> DeleteBrandAsync(BrandDto brandDto);

    IEnumerable<BrandDto> GetAllBrands();

    Task<OperationResult<BrandDto>> GetBrandById(int id);
}