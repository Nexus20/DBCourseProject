using System.Linq.Expressions;
using CourseProject.DAL.Entities;

namespace CourseProject.DAL.Interfaces; 

public interface IClientRepository : IRepository<Client> {
    Task<Client> FirstOrDefaultWithDetailsAsync(Expression<Func<Client, bool>> filter);

    IEnumerable<Client> FindAllWithDetails(Expression<Func<Client, bool>> filter = null);
}