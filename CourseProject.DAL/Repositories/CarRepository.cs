using System.Linq.Expressions;
using CourseProject.DAL.Entities;
using CourseProject.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CourseProject.DAL.Repositories; 

public class CarRepository : Repository<Car>, ICarRepository {
    public CarRepository(ApplicationDbContext context) : base(context) {
    }

    public Car FirstOrDefaultWithDetails(Expression<Func<Car, bool>> filter) {
        return FindAllWithDetails(filter).FirstOrDefault();
    }

    public IEnumerable<Car> FindAllWithDetails(Expression<Func<Car, bool>> filter = null) {

        var query = Context.Cars
            .Include(c => c.Model)
            .ThenInclude(m => m.Brand)
            .Include(c => c.Photos)
            .Include(c => c.EquipmentItems)
            .ThenInclude(e => e.EquipmentItemValues)
            .Include(c => c.EquipmentItems)
            .ThenInclude(e => e.Category)
            .AsQueryable();

        if (filter != null) {
            query = query.Where(filter);
        }

        return query.AsNoTracking();

    }
}