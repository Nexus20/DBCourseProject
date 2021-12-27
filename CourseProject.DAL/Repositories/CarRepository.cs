using System.Linq.Expressions;
using CourseProject.DAL.Entities;
using CourseProject.DAL.Interfaces;
using CourseProject.DAL.SelectionPipelineExpressions;
using Microsoft.EntityFrameworkCore;

namespace CourseProject.DAL.Repositories; 

public class CarRepository : Repository<Car>, ICarRepository {

    public CarRepository(ApplicationDbContext context) : base(context) {
    }

    protected override IQueryable<Car> FindAllWithDetailsWithoutFilter() {

        return Context.Cars.Include(c => c.Model)
            .ThenInclude(m => m.Brand)
            .Include(c => c.Photos)
            .Include(c => c.EquipmentItems)
            .ThenInclude(e => e.EquipmentItemValues)
            .Include(c => c.EquipmentItems)
            .ThenInclude(e => e.Category);
    }
}