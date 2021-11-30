using System.Linq.Expressions;
using CourseProject.DAL.Entities;

namespace CourseProject.DAL.Interfaces; 

public interface ICarRepository : IRepository<Car> {

    Car FirstOrDefaultWithDetails(Expression<Func<Car, bool>> filter);

    IEnumerable<Car> FindAllWithDetails(Expression<Func<Car, bool>> filter = null);
}