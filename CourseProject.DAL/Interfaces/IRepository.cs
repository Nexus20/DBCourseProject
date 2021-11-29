using System.Linq.Expressions;
using CourseProject.DAL.Entities;

namespace CourseProject.DAL.Interfaces; 

public interface IRepository<TEntity> where TEntity : class {

    IEnumerable<TEntity> FindAll();

    Task CreateAsync(TEntity entity);

    void Delete(Expression<Func<TEntity, bool>>? filter);

    void Update(TEntity entity);

    IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>>? filter = null,
        Expression<Func<TEntity, object>>? orderByExpr = null,
        int? skipCount = null,
        int? takeCount = null,
        params Expression<Func<TEntity, object>>[] includeExpressions);

    Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter,
        params Expression<Func<TEntity, object>>[] includeExpressions);

    Task<bool> ContainsAsync(Expression<Func<TEntity, bool>> filter);
}