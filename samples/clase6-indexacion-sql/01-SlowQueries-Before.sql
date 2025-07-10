-- QUERIES PROBLEMÁTICOS SIN ÍNDICES APROPIADOS
-- Sistema de E-Commerce con performance issues severos

-- ================================
-- SCHEMA SETUP - Tablas sin índices optimizados
-- ================================

-- Tabla Customers (100,000 registros)
CREATE TABLE Customers (
    CustomerID int IDENTITY(1,1) PRIMARY KEY, -- Solo clustered index en PK
    FirstName nvarchar(50) NOT NULL,
    LastName nvarchar(50) NOT NULL,
    Email nvarchar(100) NOT NULL, -- ❌ Sin índice - búsquedas por email lentas
    Phone nvarchar(20),
    Country nvarchar(50), -- ❌ Sin índice - filtros por país lentos
    State nvarchar(50),
    City nvarchar(50),
    PostalCode nvarchar(10),
    DateRegistered datetime2 DEFAULT GETUTCDATE(), -- ❌ Sin índice - reportes por fecha lentos
    IsActive bit DEFAULT 1, -- ❌ Sin índice - filtros lentos
    CustomerType nvarchar(20) DEFAULT 'Regular' -- ❌ Sin índice
);

-- Tabla Products (50,000 registros)
CREATE TABLE Products (
    ProductID int IDENTITY(1,1) PRIMARY KEY,
    ProductName nvarchar(200) NOT NULL, -- ❌ Sin índice - búsquedas por nombre lentas
    CategoryID int NOT NULL, -- ❌ Sin índice en FK - joins lentos
    SupplierID int NOT NULL, -- ❌ Sin índice en FK - joins lentos  
    UnitPrice decimal(10,2) NOT NULL, -- ❌ Sin índice - filtros por precio lentos
    UnitsInStock int NOT NULL, -- ❌ Sin índice - reportes de inventario lentos
    Discontinued bit DEFAULT 0, -- ❌ Sin índice - filtros lentos
    CreatedDate datetime2 DEFAULT GETUTCDATE() -- ❌ Sin índice
);

-- Tabla Orders (500,000 registros)
CREATE TABLE Orders (
    OrderID int IDENTITY(1,1) PRIMARY KEY,
    CustomerID int NOT NULL, -- ❌ Sin índice en FK - joins customer lentos
    OrderDate datetime2 DEFAULT GETUTCDATE(), -- ❌ Sin índice - reportes por fecha lentos
    ShippedDate datetime2 NULL, -- ❌ Sin índice
    ShipCountry nvarchar(50), -- ❌ Sin índice - reportes por país lentos
    ShipCity nvarchar(50), -- ❌ Sin índice
    TotalAmount decimal(10,2) NOT NULL, -- ❌ Sin índice - reportes de ventas lentos
    OrderStatus nvarchar(20) DEFAULT 'Pending' -- ❌ Sin índice - filtros por status lentos
);

-- Tabla OrderDetails (2,000,000 registros)
CREATE TABLE OrderDetails (
    OrderDetailID int IDENTITY(1,1) PRIMARY KEY,
    OrderID int NOT NULL, -- ❌ Sin índice en FK - joins lentos
    ProductID int NOT NULL, -- ❌ Sin índice en FK - joins lentos
    UnitPrice decimal(10,2) NOT NULL,
    Quantity int NOT NULL,
    Discount decimal(4,2) DEFAULT 0
);

-- ================================
-- QUERY 1: Búsqueda de cliente por email - EXTREMADAMENTE LENTO
-- ================================
-- ❌ PROBLEMA: Table scan en 100,000 registros
-- Tiempo estimado: 500-1000ms
-- Ejecuciones: 1000+ veces por día
SELECT CustomerID, FirstName, LastName, Country 
FROM Customers 
WHERE Email = 'john.smith@email.com';

-- Execution Plan: Clustered Index Scan (COST: 100%)
-- Logical Reads: ~800 pages
-- Duration: 600ms+

-- ================================
-- QUERY 2: Productos por categoría y precio - MUY LENTO
-- ================================
-- ❌ PROBLEMA: Table scan + sort costoso
-- Tiempo estimado: 800-1500ms
SELECT ProductID, ProductName, UnitPrice, UnitsInStock
FROM Products 
WHERE CategoryID = 5 
    AND UnitPrice BETWEEN 10.00 AND 50.00
    AND Discontinued = 0
ORDER BY UnitPrice ASC;

-- Execution Plan: Clustered Index Scan + Sort (COST: 100%)
-- Logical Reads: ~400 pages
-- Duration: 900ms+

-- ================================
-- QUERY 3: Órdenes de cliente con detalles - EXTREMADAMENTE LENTO
-- ================================
-- ❌ PROBLEMA: Nested loop joins sin índices en FKs
-- Tiempo estimado: 2000-5000ms
SELECT 
    o.OrderID,
    o.OrderDate,
    o.TotalAmount,
    c.FirstName,
    c.LastName,
    COUNT(od.OrderDetailID) as ItemCount
FROM Orders o
INNER JOIN Customers c ON o.CustomerID = c.CustomerID
INNER JOIN OrderDetails od ON o.OrderID = od.OrderID
WHERE o.OrderDate >= '2023-01-01'
    AND o.OrderStatus = 'Completed'
    AND c.Country = 'USA'
GROUP BY o.OrderID, o.OrderDate, o.TotalAmount, c.FirstName, c.LastName
ORDER BY o.OrderDate DESC;

-- Execution Plan: Multiple table scans + hash matches
-- Logical Reads: 10,000+ pages
-- Duration: 3000ms+

-- ================================
-- QUERY 4: Top productos vendidos - LENTÍSIMO
-- ================================
-- ❌ PROBLEMA: Join de tablas grandes sin índices optimizados
-- Tiempo estimado: 3000-8000ms
SELECT TOP 10
    p.ProductName,
    SUM(od.Quantity) as TotalQuantitySold,
    SUM(od.UnitPrice * od.Quantity * (1 - od.Discount)) as TotalRevenue,
    COUNT(DISTINCT od.OrderID) as OrderCount
FROM Products p
INNER JOIN OrderDetails od ON p.ProductID = od.ProductID
INNER JOIN Orders o ON od.OrderID = o.OrderID
WHERE o.OrderDate >= DATEADD(month, -6, GETDATE())
    AND o.OrderStatus = 'Completed'
GROUP BY p.ProductID, p.ProductName
ORDER BY TotalRevenue DESC;

-- Execution Plan: Hash joins + expensive sort
-- Logical Reads: 15,000+ pages
-- Duration: 5000ms+

-- ================================
-- QUERY 5: Reporte de ventas mensuales por país - HORRIBLE
-- ================================
-- ❌ PROBLEMA: Agregación sin índices + sorts múltiples
-- Tiempo estimado: 5000-10000ms
SELECT 
    c.Country,
    YEAR(o.OrderDate) as Year,
    MONTH(o.OrderDate) as Month,
    COUNT(o.OrderID) as OrderCount,
    SUM(o.TotalAmount) as MonthlyRevenue,
    AVG(o.TotalAmount) as AvgOrderValue
FROM Orders o
INNER JOIN Customers c ON o.CustomerID = c.CustomerID
WHERE o.OrderDate >= '2022-01-01'
    AND o.OrderStatus IN ('Completed', 'Shipped')
GROUP BY c.Country, YEAR(o.OrderDate), MONTH(o.OrderDate)
HAVING COUNT(o.OrderID) >= 10
ORDER BY c.Country, Year DESC, Month DESC;

-- Execution Plan: Table scans + expensive grouping
-- Logical Reads: 20,000+ pages
-- Duration: 8000ms+

-- ================================
-- QUERY 6: Customers activos sin órdenes recientes - MUY LENTO
-- ================================
-- ❌ PROBLEMA: NOT EXISTS con table scans
-- Tiempo estimado: 2000-4000ms
SELECT c.CustomerID, c.FirstName, c.LastName, c.Email, c.DateRegistered
FROM Customers c
WHERE c.IsActive = 1
    AND c.DateRegistered >= '2020-01-01'
    AND NOT EXISTS (
        SELECT 1 
        FROM Orders o 
        WHERE o.CustomerID = c.CustomerID 
            AND o.OrderDate >= DATEADD(month, -12, GETDATE())
    );

-- Execution Plan: Nested loops + table scans
-- Logical Reads: 25,000+ pages  
-- Duration: 3500ms+

-- ================================
-- QUERY 7: Inventario bajo por supplier - LENTO
-- ================================
-- ❌ PROBLEMA: No hay índices en campos calculados
-- Tiempo estimado: 1000-2000ms
SELECT 
    p.SupplierID,
    COUNT(*) as LowStockProducts,
    SUM(p.UnitPrice * p.UnitsInStock) as InventoryValue
FROM Products p
WHERE p.UnitsInStock < 20
    AND p.Discontinued = 0
GROUP BY p.SupplierID
HAVING COUNT(*) > 5
ORDER BY LowStockProducts DESC;

-- Execution Plan: Table scan + grouping
-- Duration: 1200ms+

-- ================================
-- QUERY 8: Análisis de patrones de compra - EXTREMADAMENTE LENTO
-- ================================
-- ❌ PROBLEMA: Window functions sin partitioning índices
-- Tiempo estimado: 8000-15000ms
WITH CustomerOrderAnalysis AS (
    SELECT 
        c.CustomerID,
        c.CustomerType,
        o.OrderDate,
        o.TotalAmount,
        ROW_NUMBER() OVER (PARTITION BY c.CustomerID ORDER BY o.OrderDate DESC) as OrderRank,
        LAG(o.OrderDate) OVER (PARTITION BY c.CustomerID ORDER BY o.OrderDate) as PreviousOrderDate,
        SUM(o.TotalAmount) OVER (PARTITION BY c.CustomerID) as CustomerLifetimeValue
    FROM Customers c
    INNER JOIN Orders o ON c.CustomerID = o.CustomerID
    WHERE o.OrderStatus = 'Completed'
        AND o.OrderDate >= '2022-01-01'
)
SELECT 
    CustomerType,
    AVG(DATEDIFF(day, PreviousOrderDate, OrderDate)) as AvgDaysBetweenOrders,
    AVG(CustomerLifetimeValue) as AvgLifetimeValue,
    COUNT(DISTINCT CustomerID) as CustomerCount
FROM CustomerOrderAnalysis
WHERE OrderRank <= 10
    AND PreviousOrderDate IS NOT NULL
GROUP BY CustomerType
ORDER BY AvgLifetimeValue DESC;

-- Execution Plan: Multiple scans + expensive windowing
-- Duration: 12000ms+

-- ================================
-- PROBLEMAS DE PERFORMANCE IDENTIFICADOS:
-- ================================

/*
❌ ISSUES CRÍTICOS:

1. MISSING FOREIGN KEY INDEXES:
   - Orders.CustomerID (500K joins)
   - OrderDetails.OrderID (2M joins)  
   - OrderDetails.ProductID (2M joins)
   - Products.CategoryID (50K joins)
   - Products.SupplierID (50K joins)

2. MISSING FILTERING INDEXES:
   - Customers.Email (login queries)
   - Customers.Country (reporting)
   - Customers.IsActive (active customer queries)
   - Products.UnitPrice (price range filters)
   - Orders.OrderDate (date range queries)
   - Orders.OrderStatus (status filters)
   - Products.Discontinued (active product queries)

3. MISSING COVERING INDEXES:
   - Queries que requieren muchas columnas
   - No hay INCLUDE columns para evitar key lookups
   - Sorts costosos por falta de ordered indexes

4. NO COMPUTED COLUMN INDEXES:
   - Funciones en WHERE clauses
   - YEAR/MONTH calculations repetitivos
   - Calculated totals

5. POOR QUERY PATTERNS:
   - SELECT * usage
   - No WHERE clause optimization
   - Expensive OR conditions
   - NOT EXISTS sin índices

RESULTADO:
- Query times: 500ms - 15,000ms
- CPU utilization: 90%+
- Disk I/O: Extremadamente alto
- Blocking: Frecuente
- Timeouts: Comunes en horas pico

OBJETIVO DEL EJERCICIO:
Optimizar todos estos queries con índices apropiados para lograr:
✅ Query times < 100ms
✅ CPU < 30%
✅ Logical reads reducidos 90%+
✅ Sin timeouts
✅ Concurrent users soportados

TIEMPO ESTIMADO: 90 minutos
DIFICULTAD: Intermedio-Avanzado
*/