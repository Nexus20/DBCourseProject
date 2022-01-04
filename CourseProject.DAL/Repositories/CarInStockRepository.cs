using CourseProject.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CourseProject.DAL.Repositories; 

public class CarInStockRepository : Repository<CarInStock> {

    public CarInStockRepository(ApplicationDbContext context) : base(context) {
    }

    protected override IQueryable<CarInStock> FindAllWithDetailsWithoutFilter() {

        return Context.CarsInStock
            .Include(cis => cis.Car)
                .ThenInclude(c => c.Photos)
                .AsNoTracking()
            .Include(cis => cis.Car)
                .ThenInclude(c => c.Model)
                    .ThenInclude(m => m.Brand)
                    .AsNoTracking()
            .Include(cis => cis.CarInStockEquipmentItemValues)
                .ThenInclude(pv => pv.EquipmentItemValue)
                    .ThenInclude(ev => ev.EquipmentItem)
                        .ThenInclude(ei => ei.Category)
                        .AsNoTracking();
    }
}