using CourseProject.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CourseProject.DAL.Repositories; 

public class ShowroomRepository : Repository<Showroom> {

    public ShowroomRepository(ApplicationDbContext context) : base(context) {
    }

    protected override IQueryable<Showroom> FindAllWithDetailsWithoutFilter() {

        return Context.Showrooms
            .Include(s => s.Managers)
            .AsNoTracking()
            .Include(s => s.CarsInStock)
                .ThenInclude(cis => cis.Car)
                    .ThenInclude(c => c.Model)
                        .ThenInclude(m => m.Brand)
                        .AsNoTracking()
            .Include(s => s.CarsInStock)
                .ThenInclude(cis => cis.CarInStockEquipmentItemValues)
                    .ThenInclude(pv => pv.EquipmentItemValue)
                        .ThenInclude(ev => ev.EquipmentItem)
                            .ThenInclude(ei => ei.Category)
                            .AsNoTracking()
            .Include(s => s.CarsInStock)
                .ThenInclude(cis => cis.CarInStockEquipmentItemValues)
                    .ThenInclude(pv => pv.EquipmentItemValue)
                        .ThenInclude(ev => ev.EquipmentItem)
                            .ThenInclude(ei => ei.Car)
                                .ThenInclude(c => c.Photos)
                                .AsNoTracking();
    }
}