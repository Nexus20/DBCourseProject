using CourseProject.DAL.Entities;
using CourseProject.DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace CourseProject.DAL; 

public class UnitOfWork : IUnitOfWork {

    private readonly ApplicationDbContext _context;

    private readonly IServiceProvider _services;

    public UnitOfWork(ApplicationDbContext context, IServiceProvider services) {
        _context = context;
        _services = services;
    }

    public UserManager<User> UserManager => _services.GetRequiredService<UserManager<User>>();

    public SignInManager<User> SignInManager => _services.GetRequiredService<SignInManager<User>>();

    public RoleManager<IdentityRole> RoleManager => _services.GetRequiredService<RoleManager<IdentityRole>>();

    public IStatisticsRepository StatisticsRepository => _services.GetRequiredService<IStatisticsRepository>();

    public TRepository GetRepository<TRepository, TEntity>()
        where TRepository : IRepository<TEntity> where TEntity : class {
        return _services.GetRequiredService<TRepository>();
    }

    public Task<int> SaveChangesAsync() {
        return _context.SaveChangesAsync();
    }

    public IDbContextTransaction BeginTransaction() {
        return _context.Database.BeginTransaction();
    }
}