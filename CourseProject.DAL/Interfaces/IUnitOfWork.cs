using CourseProject.DAL.Entities;
using CourseProject.DAL.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage;

namespace CourseProject.DAL.Interfaces; 

public interface IUnitOfWork {

    UserManager<User> UserManager { get; }

    TRepository GetRepository<TRepository, TEntity>()
        where TRepository : IRepository<TEntity> where TEntity : class;

    Task<int> SaveChangesAsync();

    IDbContextTransaction BeginTransaction();
}