using System.Linq.Expressions;
using CourseProject.DAL.Entities;

namespace CourseProject.DAL.Interfaces; 

public interface IPurchaseOrderRepository : IRepository<PurchaseOrder> {

    PurchaseOrder FirstOrDefaultWithDetails(Expression<Func<PurchaseOrder, bool>> filter);

    IEnumerable<PurchaseOrder> FindAllWithDetails(Expression<Func<PurchaseOrder, bool>> filter = null);
}