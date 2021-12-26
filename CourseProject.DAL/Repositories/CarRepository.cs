using System.Linq.Expressions;
using CourseProject.DAL.Entities;
using CourseProject.DAL.Interfaces;
using CourseProject.DAL.SelectionPipelineExpressions;
using Microsoft.EntityFrameworkCore;

namespace CourseProject.DAL.Repositories; 

public class CarRepository : Repository<Car>, ICarRepository {

    public CarRepository(ApplicationDbContext context) : base(context) {
    }

    public Task<Car> FirstOrDefaultWithDetails(Expression<Func<Car, bool>> filter) {
        return FindAllWithDetailsWithoutFilter().AsNoTracking().FirstOrDefaultAsync(filter);
    }

    public IEnumerable<Car> FindAllWithDetails(Expression<Func<Car, bool>> filter = null) {

        var query = FindAllWithDetailsWithoutFilter();

        if (filter != null) {
            query = query.Where(filter);
        }

        return query.AsNoTracking().ToList();
    }

    public IEnumerable<Car> FindAllWithDetails(SelectionPipelineExpressions<Car> expressions) {

        var query = FindAllWithDetailsWithoutFilter();

        if (expressions.FilterExpressions.Any()) {
            query = expressions.FilterExpressions.Aggregate(query, (current, expression) => current.Where(expression));
        }

        if (expressions.AscendingOrderExpression != null) {
            query = query.OrderBy(expressions.AscendingOrderExpression);
        }

        if (expressions.DescendingOrderExpression != null) {
            query = query.OrderByDescending(expressions.DescendingOrderExpression);
        }

        if (expressions.SkipCount > 0) {
            query = query.Skip(expressions.SkipCount);
        }

        if (expressions.TakeCount > 0) {
            query = query.Take(expressions.TakeCount);
        }

        return query.AsNoTracking().ToList();
    }

    private IQueryable<Car> FindAllWithDetailsWithoutFilter() {

        return Context.Cars.Include(c => c.Model)
            .ThenInclude(m => m.Brand)
            .Include(c => c.Photos)
            .Include(c => c.EquipmentItems)
            .ThenInclude(e => e.EquipmentItemValues)
            .Include(c => c.EquipmentItems)
            .ThenInclude(e => e.Category);
    }
}