using System.Linq.Expressions;
using CourseProject.DAL.Entities;
using CourseProject.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CourseProject.DAL.Repositories; 

public class PurchaseOrderRepository : Repository<PurchaseOrder>, IPurchaseOrderRepository {
    public PurchaseOrderRepository(ApplicationDbContext context) : base(context) {
    }

    public PurchaseOrder FirstOrDefaultWithDetails(Expression<Func<PurchaseOrder, bool>> filter) {
        return FindAllWithDetails(filter).FirstOrDefault();
    }

    public IEnumerable<PurchaseOrder> FindAllWithDetails(Expression<Func<PurchaseOrder, bool>> filter = null) {

        var query = Context.PurchaseOrders
            .Include(p => p.Client)
            .Include(p => p.Manager)
            .Include(p => p.PurchaseOrderEquipmentItemsValues)
                .ThenInclude(pv => pv.EquipmentItemValue)
                .ThenInclude(ev => ev.EquipmentItem)
                .ThenInclude(ei => ei.Car)
                .ThenInclude(c => c.Model)
                .ThenInclude(m => m.Brand)
            .Include(p => p.PurchaseOrderEquipmentItemsValues)
                .ThenInclude(pv => pv.EquipmentItemValue)
                .ThenInclude(ev => ev.EquipmentItem)
                .ThenInclude(ei => ei.Category)
            .AsQueryable();

        if (filter != null) {
            query = query.Where(filter);
        }

        return query.AsNoTracking();

    }
}