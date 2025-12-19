-- USE master;
-- GO

-- -- 1. Reset Database
-- IF EXISTS (SELECT name FROM sys.databases WHERE name = N'NexusERP')
-- BEGIN
--     ALTER DATABASE NexusERP SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
--     DROP DATABASE NexusERP;
-- END
-- GO

-- CREATE DATABASE NexusERP;
-- GO

-- USE NexusERP;
-- GO

-- -- =============================================
-- -- 1. CORE SYSTEM (Hệ thống)
-- -- =============================================
-- CREATE TABLE Roles (
--     Id INT IDENTITY(1,1) PRIMARY KEY,
--     RoleName NVARCHAR(50) NOT NULL UNIQUE,
--     Description NVARCHAR(255)
-- );

-- CREATE TABLE Users (
--     Id INT IDENTITY(1,1) PRIMARY KEY,
--     Username NVARCHAR(50) NOT NULL UNIQUE,
--     PasswordHash NVARCHAR(255) NOT NULL,
--     FullName NVARCHAR(100),
--     Email NVARCHAR(100),
--     IsActive BIT DEFAULT 1,
--     RoleId INT NOT NULL, 
--     FOREIGN KEY (RoleId) REFERENCES Roles(Id)
-- );

-- CREATE TABLE Partners (
--     Id INT IDENTITY(1,1) PRIMARY KEY,
--     Name NVARCHAR(100) NOT NULL,
--     Type NVARCHAR(20) NOT NULL, -- 'Customer' (Khách mua lẻ) / 'Supplier' (NCC hàng)
--     Phone NVARCHAR(20),
--     Email NVARCHAR(100),
--     Address NVARCHAR(255),
--     TaxCode NVARCHAR(50),       
--     CurrentBalance DECIMAL(18,2) DEFAULT 0, 
--     AssignedUserId INT,
--     FOREIGN KEY (AssignedUserId) REFERENCES Users(Id)
-- );

-- -- =============================================
-- -- 2. CATALOG (Hàng hóa tiêu dùng)
-- -- =============================================
-- CREATE TABLE Products (
--     Id INT IDENTITY(1,1) PRIMARY KEY,
--     Name NVARCHAR(100) NOT NULL, -- Ví dụ: Mì Hảo Hảo, Sữa Vinamilk
--     Category NVARCHAR(50),       -- Thực phẩm khô, Đồ uống, Hóa phẩm...
--     Description NVARCHAR(500)
-- );

-- CREATE TABLE ProductDetails (
--     Id INT IDENTITY(1,1) PRIMARY KEY,
--     ProductId INT NOT NULL,
    
--     -- Barcode để tít mã vạch tại quầy thu ngân
--     Barcode VARCHAR(50) NOT NULL UNIQUE, 
    
--     SKU VARCHAR(50), -- Mã nội bộ (nếu cần)
--     ProductName NVARCHAR(150) NOT NULL, -- Tên đầy đủ: Mì Hảo Hảo Tôm Chua Cay 75g
--     Unit NVARCHAR(20) NOT NULL,         -- Gói, Lon, Chai, Thùng
    
--     ImportPrice DECIMAL(18,2) DEFAULT 0,
--     SalePrice DECIMAL(18,2) DEFAULT 0,
    
--     Thumbnail NVARCHAR(500), 
    
--     FOREIGN KEY (ProductId) REFERENCES Products(Id) ON DELETE CASCADE
-- );

-- -- =============================================
-- -- 3. INVENTORY (Kho vận)
-- -- =============================================
-- CREATE TABLE Warehouses (
--     Id INT IDENTITY(1,1) PRIMARY KEY,
--     Name NVARCHAR(100) NOT NULL,
--     Location NVARCHAR(255)
-- );

-- CREATE TABLE InventoryStocks (
--     Id INT IDENTITY(1,1) PRIMARY KEY,
--     WarehouseId INT NOT NULL,
--     ProductDetailId INT NOT NULL,
--     Quantity INT DEFAULT 0,
--     FOREIGN KEY (WarehouseId) REFERENCES Warehouses(Id),
--     FOREIGN KEY (ProductDetailId) REFERENCES ProductDetails(Id)
-- );

-- -- =============================================
-- -- 4. PROCUREMENT (Nhập hàng NCC)
-- -- =============================================
-- CREATE TABLE PurchaseOrders (
--     Id INT IDENTITY(1,1) PRIMARY KEY,
--     Code VARCHAR(20) NOT NULL UNIQUE, 
--     SupplierId INT NOT NULL,          
--     OrderDate DATETIME DEFAULT GETDATE(),
--     TotalAmount DECIMAL(18,2) DEFAULT 0,
--     Status NVARCHAR(20) DEFAULT 'Draft', 
--     CreatedById INT NOT NULL,
--     FOREIGN KEY (SupplierId) REFERENCES Partners(Id),
--     FOREIGN KEY (CreatedById) REFERENCES Users(Id)
-- );

-- CREATE TABLE PurchaseOrderItems (
--     Id INT IDENTITY(1,1) PRIMARY KEY,
--     PurchaseOrderId INT NOT NULL,
--     ProductDetailId INT NOT NULL,
--     Quantity INT NOT NULL,
--     UnitPrice DECIMAL(18,2) NOT NULL, 
    
--     -- [QUAN TRỌNG VỚI SIÊU THỊ] Quản lý hạn sử dụng lô hàng nhập về
--     ExpiryDate DATETIME, 
    
--     FOREIGN KEY (PurchaseOrderId) REFERENCES PurchaseOrders(Id) ON DELETE CASCADE,
--     FOREIGN KEY (ProductDetailId) REFERENCES ProductDetails(Id)
-- );

-- -- =============================================
-- -- 5. POS / SALES (Bán lẻ tại quầy)
-- -- =============================================
-- CREATE TABLE SalesOrders (
--     Id INT IDENTITY(1,1) PRIMARY KEY,
--     Code VARCHAR(20) NOT NULL UNIQUE, 
--     CustomerId INT, -- Có thể NULL nếu khách vãng lai không cần lưu tên
--     OrderDate DATETIME DEFAULT GETDATE(),
--     TotalAmount DECIMAL(18,2) DEFAULT 0,
--     Status NVARCHAR(20) DEFAULT 'Completed', -- POS bán xong là Completed luôn
--     CreatedById INT NOT NULL, -- Thu ngân nào bán
--     FOREIGN KEY (CustomerId) REFERENCES Partners(Id),
--     FOREIGN KEY (CreatedById) REFERENCES Users(Id)
-- );

-- CREATE TABLE SalesOrderItems (
--     Id INT IDENTITY(1,1) PRIMARY KEY,
--     SalesOrderId INT NOT NULL,
--     ProductDetailId INT NOT NULL,
--     Quantity INT NOT NULL,
--     UnitPrice DECIMAL(18,2) NOT NULL,
--     Discount DECIMAL(18,2) DEFAULT 0, 
--     FOREIGN KEY (SalesOrderId) REFERENCES SalesOrders(Id) ON DELETE CASCADE,
--     FOREIGN KEY (ProductDetailId) REFERENCES ProductDetails(Id)
-- );

-- -- =============================================
-- -- 6. FINANCE (Tài chính)
-- -- =============================================
-- CREATE TABLE Payments (
--     Id INT IDENTITY(1,1) PRIMARY KEY,
--     TransactionDate DATETIME DEFAULT GETDATE(),
--     Code VARCHAR(20) UNIQUE, 
--     PartnerId INT, -- Có thể null nếu thu tiền khách lẻ
--     Amount DECIMAL(18,2) NOT NULL, 
--     PaymentMethod NVARCHAR(50) DEFAULT 'Cash', -- Cash, QR Code, Card
--     Type NVARCHAR(10) NOT NULL, 
--     ReferenceCode VARCHAR(50), 
--     Note NVARCHAR(255), 
--     CreatedById INT NOT NULL,
--     FOREIGN KEY (PartnerId) REFERENCES Partners(Id),
--     FOREIGN KEY (CreatedById) REFERENCES Users(Id)
-- );

-- -- =============================================
-- -- 7. LOGS (Lịch sử kho)
-- -- =============================================
-- CREATE TABLE StockTransactions (
--     Id INT IDENTITY(1,1) PRIMARY KEY,
--     TransactionDate DATETIME DEFAULT GETDATE(),
--     WarehouseId INT NOT NULL,
--     ProductDetailId INT NOT NULL,
--     Type VARCHAR(20) NOT NULL, 
--     QuantityChanged INT NOT NULL,
--     ReferenceCode VARCHAR(50), 
--     Note NVARCHAR(255), 
--     CreatedById INT NOT NULL,
--     FOREIGN KEY (WarehouseId) REFERENCES Warehouses(Id),
--     FOREIGN KEY (ProductDetailId) REFERENCES ProductDetails(Id),
--     FOREIGN KEY (CreatedById) REFERENCES Users(Id)
-- );

-- -- =============================================
-- -- =============================================
-- -- SEED DATA (SIÊU THỊ MINI)
-- -- =============================================
-- -- =============================================

-- -- 1. INSERT ROLES
-- INSERT INTO Roles (RoleName, Description) VALUES 
-- ('Admin', N'Cửa hàng trưởng'),
-- ('Cashier', N'Thu ngân'),
-- ('InventoryStaff', N'Nhân viên kho'),
-- ('Accountant', N'Kế toán');

-- -- 2. INSERT USERS
-- DECLARE @AdminRole INT = (SELECT Id FROM Roles WHERE RoleName = 'Admin');
-- DECLARE @CashierRole INT = (SELECT Id FROM Roles WHERE RoleName = 'Cashier');
-- DECLARE @InvRole INT = (SELECT Id FROM Roles WHERE RoleName = 'InventoryStaff');

-- INSERT INTO Users (Username, PasswordHash, FullName, RoleId) VALUES 
-- ('admin', '123', N'Quản Lý Cửa Hàng', @AdminRole),
-- ('thungan1', '123', N'Em Thu Ngân Sáng', @CashierRole),
-- ('thungan2', '123', N'Em Thu Ngân Chiều', @CashierRole),
-- ('kho1', '123', N'Anh Kho', @InvRole);

-- -- 3. INSERT PARTNERS (NCC Hàng Tiêu Dùng)
-- INSERT INTO Partners (Name, Type, Address) VALUES 
-- (N'Công Ty Vinamilk', 'Supplier', N'Hà Nội'),
-- (N'Masan Consumer', 'Supplier', N'Hồ Chí Minh'),
-- (N'Unilever Việt Nam', 'Supplier', N'Củ Chi'),
-- (N'Acecook Việt Nam', 'Supplier', N'Bình Dương'),
-- (N'Coca-Cola VN', 'Supplier', N'Hà Nội'),
-- (N'Khách Lẻ Vãng Lai', 'Customer', N'N/A'), -- ID 6: Khách không cần lưu tên
-- (N'Chị Lan (Thành viên Vàng)', 'Customer', N'Khu Tập Thể A1');

-- -- 4. INSERT PRODUCTS (Nhóm hàng)
-- INSERT INTO Products (Name, Category) VALUES 
-- (N'Mì Ăn Liền', N'Thực Phẩm Khô'),  -- 1
-- (N'Sữa Tươi Tiệt Trùng', N'Đồ Uống'), -- 2
-- (N'Nước Ngọt Có Gas', N'Đồ Uống'),    -- 3
-- (N'Dầu Gội Đầu', N'Hóa Phẩm'),         -- 4
-- (N'Nước Mắm', N'Gia Vị'),              -- 5
-- (N'Bánh Snack', N'Bánh Kẹo');          -- 6

-- -- 5. INSERT PRODUCT DETAILS (Sản phẩm cụ thể bán tại quầy - Có Barcode)
-- INSERT INTO ProductDetails (ProductId, Barcode, ProductName, Unit, ImportPrice, SalePrice, Thumbnail) VALUES 
-- -- Mì tôm (ID 1)
-- (1, '8934561', N'Mì Hảo Hảo Tôm Chua Cay', N'Gói', 3500, 4500, 'https://images.unsplash.com/photo-1612929633738-8fe44f7ec841?w=500'),
-- (1, '8934562', N'Mì Omachi Sườn Hầm Ngũ Quả', N'Gói', 7000, 8500, 'https://images.unsplash.com/photo-1591814468924-caf88d1232e1?w=500'),
-- (1, '8934563', N'Thùng Mì Hảo Hảo (30 Gói)', N'Thùng', 100000, 125000, NULL),

-- -- Sữa (ID 2)
-- (2, '8931111', N'Sữa Tươi Vinamilk Có Đường 180ml', N'Hộp', 7000, 8500, 'https://images.unsplash.com/photo-1550583724-b2692b85b150?w=500'),
-- (2, '8931112', N'Lốc 4 Hộp Sữa Vinamilk 180ml', N'Lốc', 27000, 32000, NULL),
-- (2, '8931113', N'Sữa TH True Milk 1L', N'Hộp', 30000, 38000, 'https://images.unsplash.com/photo-1563636619-e9143da7973b?w=500'),

-- -- Nước ngọt (ID 3)
-- (3, '8932221', N'Coca Cola Lon 330ml', N'Lon', 9000, 11000, 'https://images.unsplash.com/photo-1622483767028-3f66f32aef97?w=500'),
-- (3, '8932222', N'Pepsi Chai 1.5L', N'Chai', 18000, 22000, 'https://images.unsplash.com/photo-1629203851122-3726ecdf080e?w=500'),

-- -- Dầu gội (ID 4)
-- (4, '8933331', N'Dầu Gội Clear Men Bạc Hà 650g', N'Chai', 140000, 165000, 'https://images.unsplash.com/photo-1631729371254-42c2a89ddf0d?w=500'),

-- -- Gia vị (ID 5)
-- (5, '8934441', N'Nước Mắm Nam Ngư 750ml', N'Chai', 35000, 42000, NULL);

-- -- 6. INSERT WAREHOUSES
-- INSERT INTO Warehouses (Name, Location) VALUES 
-- (N'Siêu Thị Mini Tầng 1', N'Kệ Trưng Bày'),
-- (N'Kho Chứa Hàng Sau', N'Phòng Kho');

-- -- 7. INSERT PURCHASE ORDERS (Nhập hàng về kho)
-- -- Nhập Mì + Nước ngọt
-- INSERT INTO PurchaseOrders (Code, SupplierId, TotalAmount, Status, CreatedById) VALUES 
-- ('PO-001', 4, 5000000, 'Received', 1), -- Acecook
-- ('PO-002', 5, 8000000, 'Received', 1); -- CocaCola

-- INSERT INTO PurchaseOrderItems (PurchaseOrderId, ProductDetailId, Quantity, UnitPrice, ExpiryDate) VALUES 
-- (1, 1, 1000, 3500, '2024-12-31'), -- 1000 gói Hảo Hảo, hết hạn cuối năm
-- (1, 3, 50, 100000, '2024-12-31'), -- 50 Thùng Hảo Hảo
-- (2, 7, 500, 9000, '2025-06-30');  -- 500 Lon Coca

-- -- 8. INSERT INVENTORY STOCKS (Cập nhật tồn kho)
-- INSERT INTO InventoryStocks (WarehouseId, ProductDetailId, Quantity) VALUES 
-- (1, 1, 100), -- Kệ trưng bày có 100 gói mì
-- (2, 1, 900), -- Kho sau có 900 gói
-- (1, 3, 5),   -- Kệ có 5 thùng
-- (2, 3, 45),  -- Kho có 45 thùng
-- (1, 7, 200), -- Kệ có 200 lon Coca
-- (2, 7, 300); -- Kho có 300 lon

-- -- 9. INSERT SALES ORDERS (Khách mua tại quầy POS)
-- -- Khách vãng lai mua 1 gói mì, 1 lon coca
-- INSERT INTO SalesOrders (Code, CustomerId, TotalAmount, Status, CreatedById) VALUES 
-- ('BILL-001', 6, 15500, 'Completed', 2);

-- INSERT INTO SalesOrderItems (SalesOrderId, ProductDetailId, Quantity, UnitPrice) VALUES 
-- (1, 1, 1, 4500),  -- 1 Hảo Hảo
-- (1, 7, 1, 11000); -- 1 Coca Lon

-- -- Chị Lan mua thùng mì
-- INSERT INTO SalesOrders (Code, CustomerId, TotalAmount, Status, CreatedById) VALUES 
-- ('BILL-002', 7, 125000, 'Completed', 2);

-- INSERT INTO SalesOrderItems (SalesOrderId, ProductDetailId, Quantity, UnitPrice) VALUES 
-- (2, 3, 1, 125000); -- 1 Thùng Hảo Hảo

-- -- 10. INSERT TRANSACTIONS (Trừ kho)
-- INSERT INTO StockTransactions (WarehouseId, ProductDetailId, Type, QuantityChanged, ReferenceCode, CreatedById) VALUES 
-- (1, 1, 'EXPORT', -1, 'BILL-001', 2),
-- (1, 7, 'EXPORT', -1, 'BILL-001', 2),
-- (1, 3, 'EXPORT', -1, 'BILL-002', 2);

-- GO