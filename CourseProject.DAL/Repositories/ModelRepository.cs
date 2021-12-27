using CourseProject.DAL.Entities;
using CourseProject.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CourseProject.DAL.Repositories; 

public class ModelRepository : Repository<Model>, IModelRepository {

    public ModelRepository(ApplicationDbContext context) : base(context) {
    }

    protected override IQueryable<Model> FindAllWithDetailsWithoutFilter() {

        return Context.Models
            .Include(m => m.Brand)
            .Include(m => m.Cars)
            .ThenInclude(c => c.EquipmentItems)
            .ThenInclude(ei => ei.EquipmentItemValues)
            .ThenInclude(ev => ev.PurchaseOrderEquipmentItemsValues)
            .ThenInclude(pv => pv.PurchaseOrder);
    }
}