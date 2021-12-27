using System.Linq.Expressions;
using CourseProject.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using CourseProject.DAL.SelectionPipelineExpressions;

namespace CourseProject.DAL.Repositories; 

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class {

    protected readonly ApplicationDbContext Context;

    private readonly DbSet<TEntity> _dbSet;

    public Repository(ApplicationDbContext context) {
        Context = context;
        _dbSet = context.Set<TEntity>();
    }

    public Task<int> CountAsync(List<Expression<Func<TEntity, bool>>> expressions, int? skipCount = null) {

        IQueryable<TEntity> query = _dbSet;

        if (expressions.Any()) {
            query = expressions.Aggregate(query, (current, expression) => current.Where(expression));
        }

        if (skipCount is > 0) {
            query = query.Skip(skipCount.Value);
        }

        return query.CountAsync();
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

    public IEnumerable<TEntity> FindAllWithDetails(SelectionPipelineExpressions<TEntity> expressions) {

        var query = FindAllWithDetailsWithoutFilter();

        if (expressions.FilterExpressions.Any()) {
            query = expressions.FilterExpressions.Aggregate(query, (current, expression) => current.Where(expression));
        }

        if (expressions.AscendingOrderExpressions.Any()) {
            query = query.OrderBy(expressions.AscendingOrderExpressions[0]);

            if (expressions.AscendingOrderExpressions.Count > 1) {

                for (var i = 1; i < expressions.AscendingOrderExpressions.Count; i++) {
                    query = (query as IOrderedQueryable<TEntity>).ThenBy(expressions.AscendingOrderExpressions[i]);
                }
            }
        }

        if (expressions.DescendingOrderExpressions.Any()) {
            query = query.OrderByDescending(expressions.DescendingOrderExpressions[0]);

            if (expressions.DescendingOrderExpressions.Count > 1) {

                for (var i = 1; i < expressions.DescendingOrderExpressions.Count; i++) {
                    query = (query as IOrderedQueryable<TEntity>).ThenByDescending(expressions.DescendingOrderExpressions[i]);
                }
            }
        }

        if (expressions.SkipCount > 0) {
            query = query.Skip(expressions.SkipCount);
        }

        if (expressions.TakeCount > 0) {
            query = query.Take(expressions.TakeCount);
        }

        return query.AsNoTracking().ToList();
    }

    public IEnumerable<TEntity> FindAllWithDetails(Expression<Func<TEntity, bool>> filter = null) {

        var query = FindAllWithDetailsWithoutFilter();

        if (filter != null) {
            query = query.Where(filter);
        }

        return query.AsNoTracking().ToList();
    }

    public Task<TEntity> FirstOrDefaultWithDetailsAsync(Expression<Func<TEntity, bool>> filter) {
        return FindAllWithDetailsWithoutFilter().AsNoTracking().FirstOrDefaultAsync(filter);
    }

    protected virtual IQueryable<TEntity> FindAllWithDetailsWithoutFilter() {
        return _dbSet;
    }
}