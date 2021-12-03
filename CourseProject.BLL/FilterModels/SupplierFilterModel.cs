using System.Linq.Expressions;
using CourseProject.DAL.Entities;

namespace CourseProject.BLL.FilterModels; 

public class SupplierFilterModel {
    public uint BrandId { get; set; }

    public Expression<Func<Supplier, bool>> FilterExpression => s => s.BrandId == BrandId;

    public bool IsReset => BrandId == 0;
}