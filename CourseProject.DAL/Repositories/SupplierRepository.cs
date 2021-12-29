using CourseProject.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CourseProject.DAL.Repositories; 

public class SupplierRepository : Repository<Supplier> {

    public SupplierRepository(ApplicationDbContext context) : base(context) {
    }

    protected override IQueryable<Supplier> FindAllWithDetailsWithoutFilter() {

        return Context.Suppliers
            .Include(s => s.Brand)
            .Include(s => s.SupplyOrders);
    }
}