using CourseProject.DAL.Entities;
using CourseProject.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CourseProject.DAL.Repositories; 

public class SupplyOrderRepository : Repository<SupplyOrder> {

    public SupplyOrderRepository(ApplicationDbContext context) : base(context) {
    }

    protected override IQueryable<SupplyOrder> FindAllWithDetailsWithoutFilter() {
        
        return Context.SupplyOrders
            .Include(s => s.Supplier)
            .Include(s => s.Manager)
                .ThenInclude(m => m.User)
            .Include(s => s.Parts)
                .ThenInclude(p => p.SupplyOrderPartEquipmentItemsValues)
                    .ThenInclude(pv => pv.EquipmentItemValue)
                        .ThenInclude(ev => ev.EquipmentItem)
                            .ThenInclude(ei => ei.Car)
                                .ThenInclude(c => c.Model)
                                    .ThenInclude(m => m.Brand)
            .Include(s => s.Parts)
                .ThenInclude(p => p.SupplyOrderPartEquipmentItemsValues)
                    .ThenInclude(pv => pv.EquipmentItemValue)
                        .ThenInclude(ev => ev.EquipmentItem)
                            .ThenInclude(ei => ei.Category);
    }
}