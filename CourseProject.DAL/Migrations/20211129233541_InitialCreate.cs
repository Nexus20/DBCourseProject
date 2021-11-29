using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseProject.DAL.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Patronymic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EquipmentItemCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitsOfMeasure = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentItemCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Showrooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    House = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Showrooms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Models",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BrandId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Models", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Models_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrandId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Suppliers_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Managers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ShowroomId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Managers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Managers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Managers_Showrooms_ShowroomId",
                        column: x => x.ShowroomId,
                        principalTable: "Showrooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Submodel = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ModelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cars_Models_ModelId",
                        column: x => x.ModelId,
                        principalTable: "Models",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ManagerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_AspNetUsers_ClientId",
                        column: x => x.ClientId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_Managers_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "Managers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SupplyOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplierId = table.Column<int>(type: "int", nullable: false),
                    ManagerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplyOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupplyOrders_Managers_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "Managers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SupplyOrders_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CarPhotos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CarId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarPhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarPhotos_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CarsInStock",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VinCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CarId = table.Column<int>(type: "int", nullable: false),
                    ShowroomId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarsInStock", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarsInStock_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarsInStock_Showrooms_ShowroomId",
                        column: x => x.ShowroomId,
                        principalTable: "Showrooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EquipmentItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarId = table.Column<int>(type: "int", nullable: false),
                    Optional = table.Column<bool>(type: "bit", nullable: false),
                    EquipmentItemCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EquipmentItems_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EquipmentItems_EquipmentItemCategories_EquipmentItemCategoryId",
                        column: x => x.EquipmentItemCategoryId,
                        principalTable: "EquipmentItemCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SupplyOrderParts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplyOrderId = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplyOrderParts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupplyOrderParts_SupplyOrders_SupplyOrderId",
                        column: x => x.SupplyOrderId,
                        principalTable: "SupplyOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EquipmentItemValues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EquipmentItemId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentItemValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EquipmentItemValues_EquipmentItems_EquipmentItemId",
                        column: x => x.EquipmentItemId,
                        principalTable: "EquipmentItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CarInStockEquipmentItemsValues",
                columns: table => new
                {
                    EquipmentItemValueId = table.Column<int>(type: "int", nullable: false),
                    CarInStockId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarInStockEquipmentItemsValues", x => new { x.CarInStockId, x.EquipmentItemValueId });
                    table.ForeignKey(
                        name: "FK_CarInStockEquipmentItemsValues_CarsInStock_CarInStockId",
                        column: x => x.CarInStockId,
                        principalTable: "CarsInStock",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarInStockEquipmentItemsValues_EquipmentItemValues_EquipmentItemValueId",
                        column: x => x.EquipmentItemValueId,
                        principalTable: "EquipmentItemValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrderEquipmentItemsValues",
                columns: table => new
                {
                    PurchaseOrderId = table.Column<int>(type: "int", nullable: false),
                    EquipmentItemValueId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrderEquipmentItemsValues", x => new { x.EquipmentItemValueId, x.PurchaseOrderId });
                    table.ForeignKey(
                        name: "FK_PurchaseOrderEquipmentItemsValues_EquipmentItemValues_EquipmentItemValueId",
                        column: x => x.EquipmentItemValueId,
                        principalTable: "EquipmentItemValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderEquipmentItemsValues_PurchaseOrders_PurchaseOrderId",
                        column: x => x.PurchaseOrderId,
                        principalTable: "PurchaseOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SupplyOrderPartEquipmentItemsValues",
                columns: table => new
                {
                    SupplyOrderPartId = table.Column<int>(type: "int", nullable: false),
                    EquipmentItemValueId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplyOrderPartEquipmentItemsValues", x => new { x.EquipmentItemValueId, x.SupplyOrderPartId });
                    table.ForeignKey(
                        name: "FK_SupplyOrderPartEquipmentItemsValues_EquipmentItemValues_EquipmentItemValueId",
                        column: x => x.EquipmentItemValueId,
                        principalTable: "EquipmentItemValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SupplyOrderPartEquipmentItemsValues_SupplyOrderParts_SupplyOrderPartId",
                        column: x => x.SupplyOrderPartId,
                        principalTable: "SupplyOrderParts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "BMW" },
                    { 2, "Audi" },
                    { 3, "Citroen" },
                    { 4, "Skoda" },
                    { 5, "Volkswagen" },
                    { 6, "Volvo" }
                });

            migrationBuilder.InsertData(
                table: "EquipmentItemCategories",
                columns: new[] { "Id", "Name", "UnitsOfMeasure" },
                values: new object[,]
                {
                    { 1, "Engine", "" },
                    { 2, "Color", "" },
                    { 3, "Transmission", "" }
                });

            migrationBuilder.InsertData(
                table: "Showrooms",
                columns: new[] { "Id", "City", "House", "Phone", "Street" },
                values: new object[,]
                {
                    { 1, "City 1", "11", "0987654321", "Street 1" },
                    { 2, "City 1", "11", "0987654322", "Street 2" },
                    { 3, "City 2", "1", "0997654322", "Street 12" }
                });

            migrationBuilder.InsertData(
                table: "Models",
                columns: new[] { "Id", "BrandId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "X445" },
                    { 2, 1, "X5" },
                    { 3, 2, "Q3" },
                    { 4, 3, "C34" },
                    { 5, 4, "Octavia" },
                    { 6, 5, "Jetta" },
                    { 7, 6, "XC90" }
                });

            migrationBuilder.InsertData(
                table: "Suppliers",
                columns: new[] { "Id", "BrandId", "Email", "Name", "Phone" },
                values: new object[,]
                {
                    { 1, 1, "BMWsupplier1@gmail.com", "BMW supplier 1", "0998765431" },
                    { 2, 1, "BMWsupplier2@gmail.com", "BMW supplier 2", "0998765432" },
                    { 3, 2, "AudiSupplier1@gmail.com", "Audi supplier 1", "0998765433" },
                    { 4, 3, "CitroenSupplier@gmail.com", "Citroen supplier 2", "0998765434" },
                    { 5, 4, "SkodaSupplier@gmail.com", "Skoda supplier 1", "0998765435" },
                    { 6, 5, "VolkswagenSupplier@gmail.com", "Volkswagen supplier 1", "0998765436" },
                    { 7, 6, "VolvoSupplier@gmail.com", "Volvo supplier 1", "0998765437" }
                });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "ModelId", "Submodel" },
                values: new object[,]
                {
                    { 1, 3, "Sportback 40 TFSI quattro S line" },
                    { 2, 3, "Sportback 35 TFSI" },
                    { 3, 4, "Aircross" },
                    { 4, 4, "Aircross New" },
                    { 5, 5, "A5" },
                    { 6, 5, "A7" },
                    { 7, 6, "III" },
                    { 8, 6, "IV" },
                    { 9, 7, "B5 (D) Momentum Pro AWD" },
                    { 10, 2, "xDrive40i" }
                });

            migrationBuilder.InsertData(
                table: "CarsInStock",
                columns: new[] { "Id", "CarId", "ShowroomId", "VinCode" },
                values: new object[,]
                {
                    { 1, 1, 1, "12345678912345671" },
                    { 2, 3, 1, "12345678912345672" },
                    { 3, 5, 1, "12345678912345673" },
                    { 4, 2, 2, "12345678912345674" },
                    { 5, 4, 2, "12345678912345675" },
                    { 6, 6, 2, "12345678912345676" },
                    { 7, 3, 3, "12345678912345677" },
                    { 8, 5, 3, "12345678912345678" },
                    { 9, 9, 3, "12345678912345679" },
                    { 10, 9, 3, "12345678912345670" }
                });

            migrationBuilder.InsertData(
                table: "EquipmentItems",
                columns: new[] { "Id", "CarId", "EquipmentItemCategoryId", "Optional" },
                values: new object[,]
                {
                    { 1, 1, 1, false },
                    { 2, 1, 2, false },
                    { 3, 1, 3, false },
                    { 4, 2, 1, false },
                    { 5, 2, 2, false },
                    { 6, 2, 3, false },
                    { 7, 3, 1, false },
                    { 8, 3, 2, false },
                    { 9, 3, 3, false },
                    { 10, 4, 1, false },
                    { 11, 4, 2, false },
                    { 12, 4, 3, false },
                    { 13, 5, 1, false },
                    { 14, 5, 2, false },
                    { 15, 5, 3, false },
                    { 16, 6, 1, false },
                    { 17, 6, 2, false },
                    { 18, 6, 3, false },
                    { 19, 7, 1, false },
                    { 20, 7, 2, false },
                    { 21, 7, 3, false },
                    { 22, 8, 1, false },
                    { 23, 8, 2, false },
                    { 24, 8, 3, false },
                    { 25, 9, 1, false },
                    { 26, 9, 2, false },
                    { 27, 9, 3, false },
                    { 28, 10, 1, false },
                    { 29, 10, 2, false },
                    { 30, 10, 3, false }
                });

            migrationBuilder.InsertData(
                table: "EquipmentItemValues",
                columns: new[] { "Id", "EquipmentItemId", "Price", "Value" },
                values: new object[,]
                {
                    { 1, 1, 10000m, "Engine 1" },
                    { 2, 1, 15000m, "Engine 2" },
                    { 3, 1, 8000m, "Engine 3" },
                    { 4, 2, 1000m, "Color 1" },
                    { 5, 2, 1500m, "Color 2" },
                    { 6, 2, 800m, "Color 3" },
                    { 7, 3, 15000m, "Transmission 1" },
                    { 8, 3, 12000m, "Transmission 2" },
                    { 9, 3, 9000m, "Transmission 3" },
                    { 10, 4, 10000m, "Engine 1" },
                    { 11, 4, 15000m, "Engine 2" },
                    { 12, 4, 8000m, "Engine 3" },
                    { 13, 5, 1000m, "Color 1" },
                    { 14, 5, 1500m, "Color 2" },
                    { 15, 5, 800m, "Color 3" },
                    { 16, 6, 15000m, "Transmission 1" },
                    { 17, 6, 12000m, "Transmission 2" },
                    { 18, 6, 9000m, "Transmission 3" },
                    { 19, 7, 10000m, "Engine 1" },
                    { 20, 7, 15000m, "Engine 2" },
                    { 21, 7, 8000m, "Engine 3" },
                    { 22, 8, 1000m, "Color 1" },
                    { 23, 8, 1500m, "Color 2" },
                    { 24, 8, 800m, "Color 3" },
                    { 25, 9, 15000m, "Transmission 1" },
                    { 26, 9, 12000m, "Transmission 2" },
                    { 27, 9, 9000m, "Transmission 3" },
                    { 28, 10, 10000m, "Engine 1" },
                    { 29, 10, 15000m, "Engine 2" },
                    { 30, 10, 8000m, "Engine 3" },
                    { 31, 11, 1000m, "Color 1" },
                    { 32, 11, 1500m, "Color 2" },
                    { 33, 11, 800m, "Color 3" },
                    { 34, 12, 15000m, "Transmission 1" },
                    { 35, 12, 12000m, "Transmission 2" },
                    { 36, 12, 9000m, "Transmission 3" },
                    { 37, 13, 10000m, "Engine 1" },
                    { 38, 13, 15000m, "Engine 2" },
                    { 39, 13, 8000m, "Engine 3" },
                    { 40, 14, 1000m, "Color 1" },
                    { 41, 14, 1500m, "Color 2" },
                    { 42, 14, 800m, "Color 3" }
                });

            migrationBuilder.InsertData(
                table: "EquipmentItemValues",
                columns: new[] { "Id", "EquipmentItemId", "Price", "Value" },
                values: new object[,]
                {
                    { 43, 15, 15000m, "Transmission 1" },
                    { 44, 15, 12000m, "Transmission 2" },
                    { 45, 15, 9000m, "Transmission 3" },
                    { 46, 16, 10000m, "Engine 1" },
                    { 47, 16, 15000m, "Engine 2" },
                    { 48, 16, 8000m, "Engine 3" },
                    { 49, 17, 1000m, "Color 1" },
                    { 50, 17, 1500m, "Color 2" },
                    { 51, 17, 800m, "Color 3" },
                    { 52, 18, 15000m, "Transmission 1" },
                    { 53, 18, 12000m, "Transmission 2" },
                    { 54, 18, 9000m, "Transmission 3" },
                    { 55, 19, 10000m, "Engine 1" },
                    { 56, 19, 15000m, "Engine 2" },
                    { 57, 19, 8000m, "Engine 3" },
                    { 58, 20, 1000m, "Color 1" },
                    { 59, 20, 1500m, "Color 2" },
                    { 60, 20, 800m, "Color 3" },
                    { 61, 21, 15000m, "Transmission 1" },
                    { 62, 21, 12000m, "Transmission 2" },
                    { 63, 21, 9000m, "Transmission 3" },
                    { 64, 22, 10000m, "Engine 1" },
                    { 65, 22, 15000m, "Engine 2" },
                    { 66, 22, 8000m, "Engine 3" },
                    { 67, 23, 1000m, "Color 1" },
                    { 68, 23, 1500m, "Color 2" },
                    { 69, 23, 800m, "Color 3" },
                    { 70, 24, 15000m, "Transmission 1" },
                    { 71, 24, 12000m, "Transmission 2" },
                    { 72, 24, 9000m, "Transmission 3" },
                    { 73, 25, 10000m, "Engine 1" },
                    { 74, 25, 15000m, "Engine 2" },
                    { 75, 25, 8000m, "Engine 3" },
                    { 76, 26, 1000m, "Color 1" },
                    { 77, 26, 1500m, "Color 2" },
                    { 78, 26, 800m, "Color 3" },
                    { 79, 27, 15000m, "Transmission 1" },
                    { 80, 27, 12000m, "Transmission 2" },
                    { 81, 27, 9000m, "Transmission 3" },
                    { 82, 28, 10000m, "Engine 1" },
                    { 83, 28, 15000m, "Engine 2" },
                    { 84, 28, 8000m, "Engine 3" }
                });

            migrationBuilder.InsertData(
                table: "EquipmentItemValues",
                columns: new[] { "Id", "EquipmentItemId", "Price", "Value" },
                values: new object[,]
                {
                    { 85, 29, 1000m, "Color 1" },
                    { 86, 29, 1500m, "Color 2" },
                    { 87, 29, 800m, "Color 3" },
                    { 88, 30, 15000m, "Transmission 1" },
                    { 89, 30, 12000m, "Transmission 2" },
                    { 90, 30, 9000m, "Transmission 3" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Brands_Name",
                table: "Brands",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CarInStockEquipmentItemsValues_EquipmentItemValueId",
                table: "CarInStockEquipmentItemsValues",
                column: "EquipmentItemValueId");

            migrationBuilder.CreateIndex(
                name: "IX_CarPhotos_CarId",
                table: "CarPhotos",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_ModelId",
                table: "Cars",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_Submodel",
                table: "Cars",
                column: "Submodel",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CarsInStock_CarId",
                table: "CarsInStock",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_CarsInStock_ShowroomId",
                table: "CarsInStock",
                column: "ShowroomId");

            migrationBuilder.CreateIndex(
                name: "IX_CarsInStock_VinCode",
                table: "CarsInStock",
                column: "VinCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentItems_CarId",
                table: "EquipmentItems",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentItems_EquipmentItemCategoryId",
                table: "EquipmentItems",
                column: "EquipmentItemCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentItemValues_EquipmentItemId",
                table: "EquipmentItemValues",
                column: "EquipmentItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Managers_ShowroomId",
                table: "Managers",
                column: "ShowroomId");

            migrationBuilder.CreateIndex(
                name: "IX_Managers_UserId",
                table: "Managers",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Models_BrandId",
                table: "Models",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Models_Name",
                table: "Models",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderEquipmentItemsValues_PurchaseOrderId",
                table: "PurchaseOrderEquipmentItemsValues",
                column: "PurchaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_ClientId",
                table: "PurchaseOrders",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_ManagerId",
                table: "PurchaseOrders",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Showrooms_City_Street_House",
                table: "Showrooms",
                columns: new[] { "City", "Street", "House" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_BrandId",
                table: "Suppliers",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_Name",
                table: "Suppliers",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SupplyOrderPartEquipmentItemsValues_SupplyOrderPartId",
                table: "SupplyOrderPartEquipmentItemsValues",
                column: "SupplyOrderPartId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplyOrderParts_SupplyOrderId",
                table: "SupplyOrderParts",
                column: "SupplyOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplyOrders_ManagerId",
                table: "SupplyOrders",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplyOrders_SupplierId",
                table: "SupplyOrders",
                column: "SupplierId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CarInStockEquipmentItemsValues");

            migrationBuilder.DropTable(
                name: "CarPhotos");

            migrationBuilder.DropTable(
                name: "PurchaseOrderEquipmentItemsValues");

            migrationBuilder.DropTable(
                name: "SupplyOrderPartEquipmentItemsValues");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "CarsInStock");

            migrationBuilder.DropTable(
                name: "PurchaseOrders");

            migrationBuilder.DropTable(
                name: "EquipmentItemValues");

            migrationBuilder.DropTable(
                name: "SupplyOrderParts");

            migrationBuilder.DropTable(
                name: "EquipmentItems");

            migrationBuilder.DropTable(
                name: "SupplyOrders");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "EquipmentItemCategories");

            migrationBuilder.DropTable(
                name: "Managers");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "Models");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Showrooms");

            migrationBuilder.DropTable(
                name: "Brands");
        }
    }
}
