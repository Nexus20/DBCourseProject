using System.Linq.Expressions;
using CourseProject.DAL.Entities;
using CourseProject.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CourseProject.DAL.Repositories; 

public class BrandRepository : Repository<Brand>, IBrandRepository {
    public BrandRepository(ApplicationDbContext context) : base(context) {
    }

    protected override IQueryable<Brand> FindAllWithDetailsWithoutFilter() {

        return Context.Brands
            .Include(b => b.Suppliers)
            .AsNoTracking()
            .Include(b => b.Models)
                .ThenInclude(m => m.Cars)
                .AsNoTracking();
    }
}