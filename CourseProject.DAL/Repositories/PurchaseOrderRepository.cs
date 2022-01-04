using CourseProject.DAL.Entities;
using CourseProject.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CourseProject.DAL.Repositories; 

public class PurchaseOrderRepository : Repository<PurchaseOrder>, IPurchaseOrderRepository {

    public PurchaseOrderRepository(ApplicationDbContext context) : base(context) {
    }

    protected override IQueryable<PurchaseOrder> FindAllWithDetailsWithoutFilter() {
        
        return Context.PurchaseOrders
            .Include(p => p.Showroom)
            .Include(p => p.Client)
            .Include(p => p.Manager)
            .ThenInclude(m => m.User)
            .Include(p => p.PurchaseOrderEquipmentItemsValues)
            .ThenInclude(pv => pv.EquipmentItemValue)
            .ThenInclude(ev => ev.EquipmentItem)
            .ThenInclude(ei => ei.Car)
            .ThenInclude(c => c.Photos)
            .Include(p => p.PurchaseOrderEquipmentItemsValues)
            .ThenInclude(pv => pv.EquipmentItemValue)
            .ThenInclude(ev => ev.EquipmentItem)
            .ThenInclude(ei => ei.Car)
            .ThenInclude(c => c.Model)
            .ThenInclude(m => m.Brand)
            .Include(p => p.PurchaseOrderEquipmentItemsValues)
            .ThenInclude(pv => pv.EquipmentItemValue)
            .ThenInclude(ev => ev.EquipmentItem)
            .ThenInclude(ei => ei.Category).AsNoTracking();
    }
}