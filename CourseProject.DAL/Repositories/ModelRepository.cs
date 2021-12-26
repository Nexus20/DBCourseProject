using System.Linq.Expressions;
using CourseProject.DAL.Entities;
using CourseProject.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CourseProject.DAL.Repositories; 

public class ModelRepository : Repository<Model>, IModelRepository {
    public ModelRepository(ApplicationDbContext context) : base(context) {
    }

    public Model FirstOrDefaultWithDetails(Expression<Func<Model, bool>> filter) {
        return FindAllWithDetails(filter).FirstOrDefault();
    }

    public IEnumerable<Model> FindAllWithDetails(Expression<Func<Model, bool>> filter = null) {

        var query = Context.Models
            .Include(m => m.Brand)
            .Include(m => m.Cars)
            .ThenInclude(c => c.EquipmentItems)
            .ThenInclude(ei => ei.EquipmentItemValues)
            .ThenInclude(ev => ev.PurchaseOrderEquipmentItemsValues)
            .ThenInclude(pv => pv.PurchaseOrder)
            .AsQueryable();

        if (filter != null) {
            query = query.Where(filter);
        }

        return query.AsNoTracking();

    }
}