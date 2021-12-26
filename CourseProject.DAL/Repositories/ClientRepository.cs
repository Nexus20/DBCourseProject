using System.Linq.Expressions;
using CourseProject.DAL.Entities;
using CourseProject.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CourseProject.DAL.Repositories; 

public class ClientRepository : Repository<Client>, IClientRepository {
    public ClientRepository(ApplicationDbContext context) : base(context) {
    }

    public IEnumerable<Client> FindAllWithDetails(Expression<Func<Client, bool>> filter = null) {

        var query = Context.Clients
            .Include(c => c.PurchaseOrders)
            .ThenInclude(p => p.PurchaseOrderEquipmentItemsValues)
            .ThenInclude(pe => pe.EquipmentItemValue)
            .ThenInclude(ev => ev.EquipmentItem)
            .ThenInclude(ei => ei.Car)
            .ThenInclude(c => c.Model)
            .ThenInclude(m => m.Brand)
            .Include(c => c.PurchaseOrders)
            .ThenInclude(p => p.PurchaseOrderEquipmentItemsValues)
            .ThenInclude(pe => pe.EquipmentItemValue)
            .ThenInclude(ev => ev.EquipmentItem)
            .ThenInclude(ei => ei.Category)
            .AsQueryable();

        if (filter != null) {
            query = query.Where(filter);
        }

        return query.AsNoTracking();
    }

    public Task<Client> FirstOrDefaultWithDetailsAsync(Expression<Func<Client, bool>> filter) {

        return Context.Clients
            .Include(c => c.PurchaseOrders)
                .ThenInclude(p => p.PurchaseOrderEquipmentItemsValues)
                .ThenInclude(pe => pe.EquipmentItemValue)
                .ThenInclude(ev => ev.EquipmentItem)
                .ThenInclude(ei => ei.Car)
                .ThenInclude(c => c.Model)
                .ThenInclude(m => m.Brand)
            .Include(c => c.PurchaseOrders)
                .ThenInclude(p => p.PurchaseOrderEquipmentItemsValues)
                .ThenInclude(pe => pe.EquipmentItemValue)
                .ThenInclude(ev => ev.EquipmentItem)
                .ThenInclude(ei => ei.Category)
            .AsNoTracking()
            .FirstOrDefaultAsync(filter);
    }
}