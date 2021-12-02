using System.Linq.Expressions;
using CourseProject.DAL.Entities;

namespace CourseProject.BLL.FilterModels; 

public class CarFilterModel {
    public uint BrandId { get; set; }

    public uint ModelId { get; set; }

    public Expression<Func<Car, bool>> FilterExpression {
        get {

            if (BrandId == 0) {
                return c => c.ModelId == ModelId;
            }

            if (ModelId == 0) {
                return c => c.Model.BrandId == BrandId;
            }
            
            return c => c.ModelId == ModelId && c.Model.BrandId == BrandId;
        }
    }

    public bool IsReset => BrandId == 0 && ModelId == 0;
}