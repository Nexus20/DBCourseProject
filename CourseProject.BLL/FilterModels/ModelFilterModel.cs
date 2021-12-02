using System.Linq.Expressions;
using CourseProject.DAL.Entities;

namespace CourseProject.BLL.FilterModels; 

public class ModelFilterModel {
    public uint BrandId { get; set; }

    public Expression<Func<Model, bool>> FilterExpression => m => m.BrandId == BrandId;

    public bool IsReset => BrandId == 0;
}