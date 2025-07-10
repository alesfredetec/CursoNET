# Clase 6: Indexación SQL y Optimización de Performance

## 📋 Descripción
Ejercicios prácticos para dominar la indexación en SQL Server, optimización de queries y configuración de Entity Framework Core para máximo performance.

## 🎯 Objetivos de Aprendizaje
- Entender tipos de índices y cuándo usar cada uno
- Identificar queries lentos y optimizarlos
- Configurar índices en Entity Framework Core
- Usar herramientas de profiling de SQL
- Aplicar best practices de performance en BD
- Diseñar esquemas optimizados para lectura/escritura

## 📁 Estructura de Archivos

### Ejercicio 1: Slow Queries Optimization
- `01-SlowQueries-Before.sql` - Queries problemáticos sin índices
- `01-SlowQueries-After.sql` - Queries optimizados con índices apropiados
- `01-ExecutionPlans-Analysis.md` - Análisis de planes de ejecución

### Ejercicio 2: EF Core Index Configuration
- `02-EFCore-Before.cs` - Configuración sin índices
- `02-EFCore-After.cs` - Configuración optimizada con índices
- `02-EFCore-Best-Practices.md` - Best practices para EF Core

### Ejercicio 3: Index Types Demonstration
- `03-IndexTypes-Demo.sql` - Ejemplos de todos los tipos de índices
- `03-Performance-Comparison.sql` - Comparativas de performance
- `03-Index-Maintenance.sql` - Scripts de mantenimiento

## 🗂️ Tipos de Índices en SQL Server

### Clustered Index
- **Un solo por tabla**
- Determina el orden físico de los datos
- Por defecto en Primary Key
- Ideal para ranges y ordenamiento

### Non-Clustered Index
- **Hasta 999 por tabla**
- Punteros a las filas de datos
- Ideal para búsquedas específicas
- Pueden incluir columnas adicionales

### Covering Index
- Non-clustered con columnas INCLUDE
- Evita key lookups
- Ideal para queries específicos frecuentes

### Filtered Index
- Índice con condición WHERE
- Para subconjuntos de datos
- Reduce tamaño y mejora performance

### Columnstore Index
- Para analytic workloads
- Compresión excelente
- Ideal para data warehousing

## 📊 Herramientas de Análisis

### SQL Server Management Studio
- **Execution Plans**: Análisis visual de queries
- **Index Usage Stats**: Estadísticas de uso
- **Missing Index DMVs**: Índices sugeridos
- **Activity Monitor**: Performance en tiempo real

### SQL Server Profiler / Extended Events
- Captura de queries ejecutados
- Análisis de performance patterns
- Identificación de queries problemáticos

### Database Engine Tuning Advisor
- Recomendaciones automáticas de índices
- Análisis de workload completo
- Estimación de mejoras

## 🔍 Query Performance Analysis

### Execution Plan Reading
```sql
-- Habilitar planes de ejecución
SET STATISTICS IO ON;
SET STATISTICS TIME ON;

-- Analizar query problemático
SELECT * FROM Orders o
JOIN Customers c ON o.CustomerID = c.ID
WHERE o.OrderDate > '2023-01-01'
AND c.Country = 'USA';

-- Ver plan de ejecución actual
```

### DMVs Útiles para Performance
```sql
-- Queries más costosos
SELECT TOP 10 
    total_worker_time/execution_count AS avg_cpu_time,
    total_elapsed_time/execution_count AS avg_elapsed_time,
    execution_count,
    SUBSTRING(st.text, (qs.statement_start_offset/2)+1, 
        CASE qs.statement_end_offset 
            WHEN -1 THEN DATALENGTH(st.text)
            ELSE qs.statement_end_offset 
        END - qs.statement_start_offset)/2 + 1) AS statement_text
FROM sys.dm_exec_query_stats qs
CROSS APPLY sys.dm_exec_sql_text(qs.sql_handle) st
ORDER BY total_worker_time/execution_count DESC;

-- Índices faltantes
SELECT 
    dm_mid.database_id,
    dm_migs.avg_total_user_cost * (dm_migs.avg_user_impact / 100.0) * 
        (dm_migs.user_seeks + dm_migs.user_scans) AS improvement_measure,
    'CREATE INDEX [missing_index_' + 
        CONVERT(varchar, dm_mig.index_group_handle) + '_' + 
        CONVERT(varchar, dm_mid.index_handle) + ']' + 
        ' ON ' + dm_mid.statement + 
        ' (' + ISNULL(dm_mid.equality_columns,'') + 
        CASE WHEN dm_mid.equality_columns IS NOT NULL 
            AND dm_mid.inequality_columns IS NOT NULL 
            THEN ',' ELSE '' END + 
        ISNULL(dm_mid.inequality_columns, '') + ')' + 
        ISNULL(' INCLUDE (' + dm_mid.included_columns + ')', '') AS create_index_statement
FROM sys.dm_db_missing_index_groups dm_mig
INNER JOIN sys.dm_db_missing_index_group_stats dm_migs
    ON dm_migs.group_handle = dm_mig.index_group_handle
INNER JOIN sys.dm_db_missing_index_details dm_mid
    ON dm_mig.index_handle = dm_mid.index_handle
WHERE dm_mid.database_id = DB_ID()
ORDER BY improvement_measure DESC;
```

## ⚡ Best Practices para Indexación

### Design Guidelines
1. **Primary Key siempre clustered** (excepto casos especiales)
2. **Foreign Keys necesitan índices** para JOINs eficientes
3. **Columnas WHERE frecuentes** deben estar indexadas
4. **Evitar over-indexing** - cada índice tiene costo en INSERT/UPDATE
5. **Usar covering indexes** para queries críticos

### Index Key Selection
```sql
-- ✅ CORRECTO: Más selectivo primero
CREATE INDEX IX_Orders_CustomerDate 
ON Orders (CustomerID, OrderDate);

-- ❌ INCORRECTO: Menos selectivo primero
CREATE INDEX IX_Orders_DateCustomer 
ON Orders (OrderDate, CustomerID);
```

### Include Columns Strategy
```sql
-- Para queries como: SELECT OrderID, Total FROM Orders WHERE CustomerID = @id
CREATE INDEX IX_Orders_Customer_Covering
ON Orders (CustomerID)
INCLUDE (OrderID, Total);
```

## 🏗️ Entity Framework Core Configuration

### Index Configuration
```csharp
// En DbContext.OnModelCreating()
modelBuilder.Entity<Order>()
    .HasIndex(o => o.CustomerID)
    .HasDatabaseName("IX_Orders_CustomerID");

// Covering index
modelBuilder.Entity<Order>()
    .HasIndex(o => o.CustomerID)
    .IncludeProperties(o => new { o.OrderDate, o.Total })
    .HasDatabaseName("IX_Orders_Customer_Covering");

// Filtered index
modelBuilder.Entity<Order>()
    .HasIndex(o => o.Status)
    .HasFilter("Status IN ('Pending', 'Processing')")
    .HasDatabaseName("IX_Orders_ActiveStatus");
```

### Query Optimization
```csharp
// ✅ CORRECTO: Usar Where antes de joins
var result = context.Orders
    .Where(o => o.OrderDate > DateTime.Today.AddDays(-30))
    .Include(o => o.Customer)
    .ToList();

// ❌ INCORRECTO: Include innecesario con toda la tabla
var result = context.Orders
    .Include(o => o.Customer)
    .Where(o => o.OrderDate > DateTime.Today.AddDays(-30))
    .ToList();
```

## 📈 Performance Monitoring

### Key Metrics
- **Query Duration**: Tiempo promedio de queries críticos
- **Index Usage**: Seeks vs Scans ratio
- **Blocking**: Queries que bloquean otros
- **Wait Stats**: Tipos de esperas más comunes

### Automated Monitoring
```sql
-- Query para identificar performance issues
WITH QueryStats AS (
    SELECT 
        qs.sql_handle,
        qs.statement_start_offset,
        qs.statement_end_offset,
        qs.creation_time,
        qs.execution_count,
        qs.total_worker_time,
        qs.total_elapsed_time,
        qs.total_logical_reads,
        qs.total_physical_reads
    FROM sys.dm_exec_query_stats qs
    WHERE qs.execution_count > 10 -- Queries ejecutados frecuentemente
)
SELECT 
    SUBSTRING(st.text, (qs.statement_start_offset/2)+1,
        CASE qs.statement_end_offset 
            WHEN -1 THEN DATALENGTH(st.text)
            ELSE qs.statement_end_offset 
        END - qs.statement_start_offset)/2 + 1) AS query_text,
    qs.execution_count,
    qs.total_worker_time / qs.execution_count AS avg_cpu_time,
    qs.total_elapsed_time / qs.execution_count AS avg_elapsed_time,
    qs.total_logical_reads / qs.execution_count AS avg_logical_reads
FROM QueryStats qs
CROSS APPLY sys.dm_exec_sql_text(qs.sql_handle) st
ORDER BY avg_elapsed_time DESC;
```

## 🎯 Common Performance Issues

### Table Scans
- **Causa**: Falta de índices apropiados
- **Solución**: Crear índices en columnas WHERE/JOIN
- **Identificación**: Execution plan muestra "Table Scan"

### Key Lookups
- **Causa**: Índice no cubre todas las columnas necesarias
- **Solución**: Usar INCLUDE columns en el índice
- **Identificación**: "Key Lookup" en execution plan

### Parameter Sniffing
- **Causa**: Plan optimizado para un valor específico
- **Solución**: OPTION(RECOMPILE) o variables locales
- **Identificación**: Performance varía con diferentes parámetros

### Index Fragmentation
- **Causa**: Muchos INSERT/UPDATE/DELETE
- **Solución**: REBUILD o REORGANIZE índices
- **Identificación**: DMV sys.dm_db_index_physical_stats

## 💡 Tips para Desarrolladores Jr

### Query Writing Best Practices
1. **WHERE clauses específicos** - Evitar OR complejos
2. **JOIN conditions correctos** - Siempre en columnas indexadas  
3. **SELECT específico** - Evitar SELECT *
4. **EXISTS vs IN** - EXISTS es generalmente más rápido
5. **SARGABLE conditions** - Evitar funciones en WHERE

### Index Maintenance
```sql
-- Script de mantenimiento semanal
DECLARE @sql NVARCHAR(MAX) = '';
SELECT @sql = @sql + 
    'ALTER INDEX ' + i.name + ' ON ' + s.name + '.' + t.name + 
    CASE 
        WHEN avg_fragmentation_in_percent > 30 THEN ' REBUILD;'
        WHEN avg_fragmentation_in_percent > 10 THEN ' REORGANIZE;'
        ELSE ''
    END + CHAR(13)
FROM sys.dm_db_index_physical_stats(DB_ID(), NULL, NULL, NULL, 'LIMITED') ps
JOIN sys.indexes i ON ps.object_id = i.object_id AND ps.index_id = i.index_id
JOIN sys.tables t ON i.object_id = t.object_id
JOIN sys.schemas s ON t.schema_id = s.schema_id
WHERE ps.avg_fragmentation_in_percent > 10
    AND i.index_id > 0;

EXEC sp_executesql @sql;
```

## 🎓 Métricas de Éxito
- **Query response time** < 100ms para queries frecuentes
- **Index seeks/scans ratio** > 80% seeks
- **CPU usage** < 70% promedio
- **IO latency** < 20ms promedio
- **Blocking incidents** = 0 en horas productivas

**TIEMPO ESTIMADO:** 3-4 horas
**DIFICULTAD:** Intermedio-Avanzado  
**HERRAMIENTAS:** SSMS, SQL Profiler, Entity Framework