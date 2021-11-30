using System.Linq.Expressions;
using CourseProject.DAL.Entities;

namespace CourseProject.DAL.Interfaces; 

public interface IBrandRepository : IRepository<Brand> {

    Brand FirstOrDefaultWithDetails(Expression<Func<Brand, bool>> filter);

    IEnumerable<Brand> FindAllWithDetails(Expression<Func<Brand, bool>> filter = null);
}