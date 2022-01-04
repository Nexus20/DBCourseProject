using CourseProject.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CourseProject.DAL.Repositories; 

public class EquipmentItemCategoryRepository : Repository<EquipmentItemCategory> {

    public EquipmentItemCategoryRepository(ApplicationDbContext context) : base(context) {
    }

    protected override IQueryable<EquipmentItemCategory> FindAllWithDetailsWithoutFilter() {

        return Context.EquipmentItemCategories
            .Include(eic => eic.EquipmentItems)
            .AsNoTracking();
    }
}