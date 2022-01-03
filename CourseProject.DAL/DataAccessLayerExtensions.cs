﻿using CourseProject.DAL.Entities;
using CourseProject.DAL.Interfaces;
using CourseProject.DAL.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CourseProject.DAL {
    public static class DataAccessLayerExtensions {

        public static IServiceCollection AddDataAccessLayer(this IServiceCollection services, string connectionString) {
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<ICarRepository, CarRepository>();
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<IModelRepository, ModelRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IPurchaseOrderRepository, PurchaseOrderRepository>();
            services.AddScoped<IRepository<Supplier>, SupplierRepository>();
            services.AddScoped<IRepository<SupplyOrder>, SupplyOrderRepository>();
            services.AddScoped<IRepository<Showroom>, ShowroomRepository>();
            services.AddScoped<IRepository<CarInStock>, CarInStockRepository>();
            services.AddScoped<IStatisticsRepository, StatisticsRepository>();

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddSignInManager<SignInManager<User>>()
                .AddRoleManager<RoleManager<IdentityRole>>();

            return services;
        }

    }
}