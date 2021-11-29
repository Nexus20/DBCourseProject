using System.Linq.Expressions;
using CourseProject.DAL.Entities;
using CourseProject.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CourseProject.DAL.Repositories; 

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class {

    protected readonly ApplicationDbContext Context;

    private readonly DbSet<TEntity> _dbSet;

    public Repository(ApplicationDbContext context) {
        Context = context;
        _dbSet = context.Set<TEntity>();
    }


    public IEnumerable<TEntity> FindAll() {
        return _dbSet.AsNoTracking();
    }

    public Task CreateAsync(TEntity entity) {
        return _dbSet.AddAsync(entity).AsTask();
    }

    public void Delete(Expression<Func<TEntity, bool>>? filter) {
        var entitiesToDelete = Find(filter);

        foreach (var entityToDelete in entitiesToDelete) {
            if (Context.Entry(entityToDelete).State == EntityState.Detached) {
                _dbSet.Attach(entityToDelete);
            }
            _dbSet.Remove(entityToDelete);
        }

    }

    public void Update(TEntity entity) {
        _dbSet.Attach(entity);
        Context.Entry(entity).State = EntityState.Modified;

    }

    public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>>? filter = null, Expression<Func<TEntity, object>>? orderByExpr = null, int? skipCount = null, int? takeCount = null,
        params Expression<Func<TEntity, object>>[] includeExpressions) {
        IQueryable<TEntity> query = _dbSet;

        if (skipCount > 0) {
            query = query.Skip(skipCount.Value);
        }

        if (takeCount > 0) {
            query = query.Take(takeCount.Value);
        }

        query = includeExpressions.Aggregate(query, (current, includeExpression) => current.Include(includeExpression));

        if (filter != null) {
            query = query.Where(filter);
        }

        if (orderByExpr != null) {
            query = query.OrderBy(orderByExpr);
        }

        return query.AsNoTracking();

    }

    public Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter,
        params Expression<Func<TEntity, object>>[] includeExpressions) {
        IQueryable<TEntity> query = _dbSet;

        foreach (var includeExpression in includeExpressions) {
            query = query.Include(includeExpression);
        }

        return query.AsNoTracking().FirstOrDefaultAsync(filter);
    }

    public async Task<bool> ContainsAsync(Expression<Func<TEntity, bool>> filter) {
        return await _dbSet.FirstOrDefaultAsync(filter) != null;
    }
}