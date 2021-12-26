using System.Linq.Expressions;
using CourseProject.DAL.Entities;
using CourseProject.DAL.SelectionPipelineExpressions;

namespace CourseProject.DAL.Interfaces; 

public interface ICarRepository : IRepository<Car> {

    Task<Car> FirstOrDefaultWithDetailsAsync(Expression<Func<Car, bool>> filter);

    IEnumerable<Car> FindAllWithDetails(SelectionPipelineExpressions<Car> expressions);

    IEnumerable<Car> FindAllWithDetails(Expression<Func<Car, bool>> filter = null);
}