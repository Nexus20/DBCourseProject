using System.Linq.Expressions;
using CourseProject.DAL.Entities;

namespace CourseProject.DAL.Interfaces; 

public interface IModelRepository : IRepository<Model> {

    Model FirstOrDefaultWithDetails(Expression<Func<Model, bool>> filter);

    IEnumerable<Model> FindAllWithDetails(Expression<Func<Model, bool>> filter = null);
}