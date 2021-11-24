using CourseProject.DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CourseProject.DAL.EntityExtensions;

namespace CourseProject.DAL; 

public class ApplicationDbContext : IdentityDbContext<User> {

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Brand> Brands { get; set; }
    public DbSet<Model> Models { get; set; }
    public DbSet<Car> Cars { get; set; }
    public DbSet<CarPhoto> CarPhotos { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Manager> Managers { get; set; }
    public DbSet<Showroom> Showrooms { get; set; }
    public DbSet<CarInStock> CarsInStock { get; set; }
    public DbSet<CarInStockEquipmentItemValue> CarInStockEquipmentItemsValues { get; set; }
    public DbSet<EquipmentItem> EquipmentItems { get; set; }
    public DbSet<EquipmentItemValue> EquipmentItemValues { get; set; }
    public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
    public DbSet<PurchaseOrderEquipmentItemValue> PurchaseOrderEquipmentItemsValues { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<SupplyOrder> SupplyOrders { get; set; }
    public DbSet<SupplyOrderPart> SupplyOrderParts { get; set; }
    public DbSet<SupplyOrderPartEquipmentItemValue> SupplyOrderPartEquipmentItemsValues { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {

        optionsBuilder
            .UseLazyLoadingProxies()
            .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=CourseProjectDB;Trusted_Connection=True;");

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder builder) {

        builder.Entity<Brand>().Configure();
        builder.Entity<Model>().Configure();
        builder.Entity<Car>().Configure();

        base.OnModelCreating(builder);
    }
}