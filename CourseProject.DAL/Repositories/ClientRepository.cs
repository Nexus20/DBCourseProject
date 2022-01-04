using System.Linq.Expressions;
using CourseProject.DAL.Entities;
using CourseProject.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CourseProject.DAL.Repositories; 

public class ClientRepository : Repository<Client>, IClientRepository {
    public ClientRepository(ApplicationDbContext context) : base(context) {
    }

    protected override IQueryable<Client> FindAllWithDetailsWithoutFilter() {

        return Context.Clients
            .Include(c => c.PurchaseOrders)
            .ThenInclude(p => p.PurchaseOrderEquipmentItemsValues)
            .ThenInclude(pe => pe.EquipmentItemValue)
            .ThenInclude(ev => ev.EquipmentItem)
            .ThenInclude(ei => ei.Car)
            .ThenInclude(c => c.Photos)
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
            .ThenInclude(ei => ei.Category).AsNoTracking();
    }
}