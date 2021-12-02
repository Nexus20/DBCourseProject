using CourseProject.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage;

namespace CourseProject.DAL.Interfaces; 

public interface IUnitOfWork {

    UserManager<User> UserManager { get; }

    SignInManager<User> SignInManager { get; }

    RoleManager<IdentityRole> RoleManager { get; }

    TRepository GetRepository<TRepository, TEntity>()
        where TRepository : IRepository<TEntity> where TEntity : class;

    Task<int> SaveChangesAsync();

    IDbContextTransaction BeginTransaction();
}