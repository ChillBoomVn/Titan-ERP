using Microsoft.EntityFrameworkCore;
using Nexus.API.Models;

namespace Nexus.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // Khai báo các bảng (DbSet)
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Partner> Partners { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductDetail> ProductDetails { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<InventoryStock> InventoryStocks { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<PurchaseOrderItem> PurchaseOrderItems { get; set; }
        public DbSet<SalesOrder> SalesOrders { get; set; }
        public DbSet<SalesOrderItem> SalesOrderItems { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<StockTransaction> StockTransactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // =========================================================
            // FIX LỖI WARNING DECIMAL (QUAN TRỌNG)
            // Cấu hình tiền tệ: 18 số, 2 số lẻ (decimal(18,2))
            // =========================================================

            // 1. Partner
            modelBuilder.Entity<Partner>()
                .Property(p => p.CurrentBalance)
                .HasColumnType("decimal(18,2)");

            // 2. ProductDetail
            modelBuilder.Entity<ProductDetail>()
                .Property(p => p.ImportPrice)
                .HasColumnType("decimal(18,2)");
            modelBuilder.Entity<ProductDetail>()
                .Property(p => p.SalePrice)
                .HasColumnType("decimal(18,2)");

            // 3. PurchaseOrder
            modelBuilder.Entity<PurchaseOrder>()
                .Property(p => p.TotalAmount)
                .HasColumnType("decimal(18,2)");
            modelBuilder.Entity<PurchaseOrderItem>()
                .Property(p => p.UnitPrice)
                .HasColumnType("decimal(18,2)");

            // 4. SalesOrder
            modelBuilder.Entity<SalesOrder>()
                .Property(p => p.TotalAmount)
                .HasColumnType("decimal(18,2)");
            modelBuilder.Entity<SalesOrderItem>()
                .Property(p => p.UnitPrice)
                .HasColumnType("decimal(18,2)");
            modelBuilder.Entity<SalesOrderItem>()
                .Property(p => p.Discount)
                .HasColumnType("decimal(18,2)");

            // 5. Payment
            modelBuilder.Entity<Payment>()
                .Property(p => p.Amount)
                .HasColumnType("decimal(18,2)");

            // =========================================================
            // CÁC CẤU HÌNH KHÁC (GIỮ NGUYÊN)
            // =========================================================

            // Unique Constraints
            modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();
            modelBuilder.Entity<Role>().HasIndex(r => r.RoleName).IsUnique();
            modelBuilder.Entity<ProductDetail>().HasIndex(p => p.Barcode).IsUnique();
            modelBuilder.Entity<PurchaseOrder>().HasIndex(p => p.Code).IsUnique();
            modelBuilder.Entity<SalesOrder>().HasIndex(p => p.Code).IsUnique();

            // --- SEEDING DATA --- (Code giữ nguyên như cũ, mình rút gọn để bạn dễ copy)
            // Bạn nhớ giữ lại phần Seed Data bên dưới nhé!

            // 1. Roles
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, RoleName = "Admin", Description = "Cửa hàng trưởng" },
                new Role { Id = 2, RoleName = "Cashier", Description = "Thu ngân" },
                new Role { Id = 3, RoleName = "InventoryStaff", Description = "Nhân viên kho" },
                new Role { Id = 4, RoleName = "Accountant", Description = "Kế toán" }
            );

            // ... (Phần Seed Data còn lại giữ nguyên, không thay đổi gì) ...
            // Nếu bạn lỡ xóa thì copy lại từ câu trả lời trước nhé.

            // CHÚ Ý: ĐỂ TIỆN CHO BẠN, MÌNH SẼ PASTE LẠI FULL CODE SEED DATA BÊN DƯỚI CHO CHẮC ĂN

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Username = "admin", PasswordHash = "123", FullName = "Quản Lý Cửa Hàng", RoleId = 1 },
                new User { Id = 2, Username = "thungan1", PasswordHash = "123", FullName = "Em Thu Ngân Sáng", RoleId = 2 },
                new User { Id = 3, Username = "thungan2", PasswordHash = "123", FullName = "Em Thu Ngân Chiều", RoleId = 2 },
                new User { Id = 4, Username = "kho1", PasswordHash = "123", FullName = "Anh Kho", RoleId = 3 }
            );

            modelBuilder.Entity<Partner>().HasData(
                new Partner { Id = 1, Name = "Công Ty Vinamilk", Type = "Supplier", Address = "Hà Nội" },
                new Partner { Id = 2, Name = "Masan Consumer", Type = "Supplier", Address = "Hồ Chí Minh" },
                new Partner { Id = 3, Name = "Unilever Việt Nam", Type = "Supplier", Address = "Củ Chi" },
                new Partner { Id = 4, Name = "Acecook Việt Nam", Type = "Supplier", Address = "Bình Dương" },
                new Partner { Id = 5, Name = "Coca-Cola VN", Type = "Supplier", Address = "Hà Nội" },
                new Partner { Id = 6, Name = "Khách Lẻ Vãng Lai", Type = "Customer", Address = "N/A" },
                new Partner { Id = 7, Name = "Chị Lan (Thành viên Vàng)", Type = "Customer", Address = "Khu Tập Thể A1" }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Mì Ăn Liền", Category = "Thực Phẩm Khô" },
                new Product { Id = 2, Name = "Sữa Tươi Tiệt Trùng", Category = "Đồ Uống" },
                new Product { Id = 3, Name = "Nước Ngọt Có Gas", Category = "Đồ Uống" },
                new Product { Id = 4, Name = "Dầu Gội Đầu", Category = "Hóa Phẩm" },
                new Product { Id = 5, Name = "Nước Mắm", Category = "Gia Vị" },
                new Product { Id = 6, Name = "Bánh Snack", Category = "Bánh Kẹo" }
            );

            modelBuilder.Entity<ProductDetail>().HasData(
                new ProductDetail { Id = 1, ProductId = 1, Barcode = "8934561", ProductName = "Mì Hảo Hảo Tôm Chua Cay", Unit = "Gói", ImportPrice = 3500, SalePrice = 4500, Thumbnail = "https://images.unsplash.com/photo-1612929633738-8fe44f7ec841" },
                new ProductDetail { Id = 2, ProductId = 1, Barcode = "8934562", ProductName = "Mì Omachi Sườn Hầm Ngũ Quả", Unit = "Gói", ImportPrice = 7000, SalePrice = 8500, Thumbnail = "https://images.unsplash.com/photo-1591814468924-caf88d1232e1" },
                new ProductDetail { Id = 3, ProductId = 1, Barcode = "8934563", ProductName = "Thùng Mì Hảo Hảo (30 Gói)", Unit = "Thùng", ImportPrice = 100000, SalePrice = 125000 },
                new ProductDetail { Id = 4, ProductId = 2, Barcode = "8931111", ProductName = "Sữa Tươi Vinamilk Có Đường 180ml", Unit = "Hộp", ImportPrice = 7000, SalePrice = 8500, Thumbnail = "https://images.unsplash.com/photo-1550583724-b2692b85b150" },
                new ProductDetail { Id = 5, ProductId = 2, Barcode = "8931112", ProductName = "Lốc 4 Hộp Sữa Vinamilk 180ml", Unit = "Lốc", ImportPrice = 27000, SalePrice = 32000 },
                new ProductDetail { Id = 6, ProductId = 2, Barcode = "8931113", ProductName = "Sữa TH True Milk 1L", Unit = "Hộp", ImportPrice = 30000, SalePrice = 38000, Thumbnail = "https://images.unsplash.com/photo-1563636619-e9143da7973b" },
                new ProductDetail { Id = 7, ProductId = 3, Barcode = "8932221", ProductName = "Coca Cola Lon 330ml", Unit = "Lon", ImportPrice = 9000, SalePrice = 11000, Thumbnail = "https://images.unsplash.com/photo-1622483767028-3f66f32aef97" },
                new ProductDetail { Id = 8, ProductId = 3, Barcode = "8932222", ProductName = "Pepsi Chai 1.5L", Unit = "Chai", ImportPrice = 18000, SalePrice = 22000, Thumbnail = "https://images.unsplash.com/photo-1629203851122-3726ecdf080e" },
                new ProductDetail { Id = 9, ProductId = 4, Barcode = "8933331", ProductName = "Dầu Gội Clear Men Bạc Hà 650g", Unit = "Chai", ImportPrice = 140000, SalePrice = 165000, Thumbnail = "https://images.unsplash.com/photo-1631729371254-42c2a89ddf0d" },
                new ProductDetail { Id = 10, ProductId = 5, Barcode = "8934441", ProductName = "Nước Mắm Nam Ngư 750ml", Unit = "Chai", ImportPrice = 35000, SalePrice = 42000 }
            );

            modelBuilder.Entity<Warehouse>().HasData(
                new Warehouse { Id = 1, Name = "Siêu Thị Mini Tầng 1", Location = "Kệ Trưng Bày" },
                new Warehouse { Id = 2, Name = "Kho Chứa Hàng Sau", Location = "Phòng Kho" }
            );

            modelBuilder.Entity<InventoryStock>().HasData(
                new InventoryStock { Id = 1, WarehouseId = 1, ProductDetailId = 1, Quantity = 100 },
                new InventoryStock { Id = 2, WarehouseId = 2, ProductDetailId = 1, Quantity = 900 },
                new InventoryStock { Id = 3, WarehouseId = 1, ProductDetailId = 3, Quantity = 5 },
                new InventoryStock { Id = 4, WarehouseId = 2, ProductDetailId = 3, Quantity = 45 },
                new InventoryStock { Id = 5, WarehouseId = 1, ProductDetailId = 7, Quantity = 200 },
                new InventoryStock { Id = 6, WarehouseId = 2, ProductDetailId = 7, Quantity = 300 }
            );

            modelBuilder.Entity<PurchaseOrder>().HasData(
                new PurchaseOrder { Id = 1, Code = "PO-001", SupplierId = 4, TotalAmount = 5000000, Status = "Received", CreatedById = 1 },
                new PurchaseOrder { Id = 2, Code = "PO-002", SupplierId = 5, TotalAmount = 8000000, Status = "Received", CreatedById = 1 }
            );

            modelBuilder.Entity<PurchaseOrderItem>().HasData(
                new PurchaseOrderItem { Id = 1, PurchaseOrderId = 1, ProductDetailId = 1, Quantity = 1000, UnitPrice = 3500, ExpiryDate = new DateTime(2024, 12, 31) },
                new PurchaseOrderItem { Id = 2, PurchaseOrderId = 1, ProductDetailId = 3, Quantity = 50, UnitPrice = 100000, ExpiryDate = new DateTime(2024, 12, 31) },
                new PurchaseOrderItem { Id = 3, PurchaseOrderId = 2, ProductDetailId = 7, Quantity = 500, UnitPrice = 9000, ExpiryDate = new DateTime(2025, 06, 30) }
            );

            modelBuilder.Entity<SalesOrder>().HasData(
                new SalesOrder { Id = 1, Code = "BILL-001", CustomerId = 6, TotalAmount = 15500, Status = "Completed", CreatedById = 2 },
                new SalesOrder { Id = 2, Code = "BILL-002", CustomerId = 7, TotalAmount = 125000, Status = "Completed", CreatedById = 2 }
            );

            modelBuilder.Entity<SalesOrderItem>().HasData(
                new SalesOrderItem { Id = 1, SalesOrderId = 1, ProductDetailId = 1, Quantity = 1, UnitPrice = 4500 },
                new SalesOrderItem { Id = 2, SalesOrderId = 1, ProductDetailId = 7, Quantity = 1, UnitPrice = 11000 },
                new SalesOrderItem { Id = 3, SalesOrderId = 2, ProductDetailId = 3, Quantity = 1, UnitPrice = 125000 }
            );

            modelBuilder.Entity<StockTransaction>().HasData(
                new StockTransaction { Id = 1, WarehouseId = 1, ProductDetailId = 1, Type = "EXPORT", QuantityChanged = -1, ReferenceCode = "BILL-001", CreatedById = 2 },
                new StockTransaction { Id = 2, WarehouseId = 1, ProductDetailId = 7, Type = "EXPORT", QuantityChanged = -1, ReferenceCode = "BILL-001", CreatedById = 2 },
                new StockTransaction { Id = 3, WarehouseId = 1, ProductDetailId = 3, Type = "EXPORT", QuantityChanged = -1, ReferenceCode = "BILL-002", CreatedById = 2 }
            );
        }
    }
}