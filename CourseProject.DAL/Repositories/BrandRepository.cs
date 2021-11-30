using System.Linq.Expressions;
using CourseProject.DAL.Entities;
using CourseProject.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CourseProject.DAL.Repositories; 

public class BrandRepository : Repository<Brand>, IBrandRepository {
    public BrandRepository(ApplicationDbContext context) : base(context) {
    }

    public Brand FirstOrDefaultWithDetails(Expression<Func<Brand, bool>> filter) {
        return FindAllWithDetails(filter).FirstOrDefault();
    }

    public IEnumerable<Brand> FindAllWithDetails(Expression<Func<Brand, bool>> filter = null) {

        var query = Context.Brands
            .Include(b => b.Suppliers)
            .Include(b => b.Models)
            .ThenInclude(m => m.Cars)
            .AsQueryable();

        if (filter != null) {
            query = query.Where(filter);
        }

        return query.AsNoTracking();

    }
}