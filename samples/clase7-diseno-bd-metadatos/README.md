# Clase 7: Diseño de Base de Datos con Metadatos

## 📋 Descripción
Ejercicios avanzados para crear arquitecturas de base de datos evolutivas usando metadatos, patrones EAV (Entity-Attribute-Value), modelos híbridos JSON y sistemas que se adaptan dinámicamente a cambios de requerimientos.

## 🎯 Objetivos de Aprendizaje
- Diseñar bases de datos que evolucionen sin downtime
- Implementar patrón EAV de forma eficiente
- Combinar modelos relacionales con JSON/NoSQL
- Crear meta-modelos para configuración dinámica
- Desarrollar interfaces que se generen automáticamente
- Manejar versionado de esquemas y migración de datos

## 📁 Estructura de Archivos

### Ejercicio 1: EAV Pattern Implementation
- `01-EAV-Pattern.sql` - Implementación completa del patrón EAV
- `01-EAV-CSharp.cs` - Código C# para trabajar con EAV
- `01-EAV-Performance.md` - Optimizaciones para performance

### Ejercicio 2: JSON Hybrid Model
- `02-JSON-Columns.sql` - Uso de columnas JSON en SQL Server
- `02-JSON-CSharp.cs` - Manejo de JSON en Entity Framework
- `02-Schema-Evolution.md` - Evolución de esquemas con JSON

### Ejercicio 3: Meta-Model Implementation
- `03-MetaModel-Schema.sql` - Esquema de metadatos completo
- `03-MetaModel-CSharp.cs` - Motor de metadatos en C#
- `03-DynamicUI-Generator.cs` - Generador de interfaces dinámicas

### Ejercicio 4: Enterprise Example
- `04-Enterprise-Example.sql` - Ejemplo completo de sistema enterprise
- `04-Configuration-Engine.cs` - Motor de configuración avanzado
- `04-Migration-Scripts.sql` - Scripts de migración automática

## 🏗️ Conceptos Fundamentales

### Entity-Attribute-Value (EAV) Pattern
Permite almacenar atributos dinámicos sin modificar el esquema de BD.

**Ventajas:**
- Flexibilidad extrema para nuevos atributos
- Sin cambios de esquema
- Soporte para tipos de datos variados

**Desventajas:**
- Queries complejos
- Performance challenges
- Pérdida de integridad referencial

### Hybrid JSON Model
Combina estructura relacional con flexibilidad JSON.

**Ventajas:**
- Lo mejor de ambos mundos
- Fácil evolución de schema
- Queries nativos JSON

**Desventajas:**
- Complejidad en diseño
- Herramientas de BI limitadas
- Versionado de JSON schema

## 🗃️ Patrón EAV - Implementación

### Schema Básico
```sql
-- Entities table - tipos de entidades
CREATE TABLE EntityTypes (
    ID int IDENTITY(1,1) PRIMARY KEY,
    Name nvarchar(100) NOT NULL,
    Description nvarchar(500),
    IsActive bit DEFAULT 1
);

-- Attributes table - definición de atributos
CREATE TABLE AttributeDefinitions (
    ID int IDENTITY(1,1) PRIMARY KEY,
    EntityTypeID int FOREIGN KEY REFERENCES EntityTypes(ID),
    Name nvarchar(100) NOT NULL,
    DataType nvarchar(50) NOT NULL, -- varchar, int, decimal, datetime, bool
    IsRequired bit DEFAULT 0,
    DefaultValue nvarchar(500),
    ValidationRule nvarchar(1000),
    DisplayOrder int,
    IsActive bit DEFAULT 1
);

-- Entity instances - instancias reales
CREATE TABLE Entities (
    ID int IDENTITY(1,1) PRIMARY KEY,
    EntityTypeID int FOREIGN KEY REFERENCES EntityTypes(ID),
    CreatedDate datetime2 DEFAULT GETUTCDATE(),
    ModifiedDate datetime2 DEFAULT GETUTCDATE(),
    IsActive bit DEFAULT 1
);

-- Values table - valores de atributos
CREATE TABLE AttributeValues (
    ID bigint IDENTITY(1,1) PRIMARY KEY,
    EntityID int FOREIGN KEY REFERENCES Entities(ID),
    AttributeDefinitionID int FOREIGN KEY REFERENCES AttributeDefinitions(ID),
    StringValue nvarchar(max),
    IntValue int,
    DecimalValue decimal(18,4),
    DateTimeValue datetime2,
    BoolValue bit,
    CreatedDate datetime2 DEFAULT GETUTCDATE()
);
```

### Query Optimization
```sql
-- Índices críticos para performance
CREATE INDEX IX_AttributeValues_Entity_Attribute 
ON AttributeValues (EntityID, AttributeDefinitionID);

CREATE INDEX IX_AttributeValues_Attribute_String 
ON AttributeValues (AttributeDefinitionID, StringValue);

CREATE INDEX IX_AttributeValues_Attribute_Int 
ON AttributeValues (AttributeDefinitionID, IntValue);

-- Función para obtener valores tipados
CREATE FUNCTION GetTypedValue(
    @EntityID int,
    @AttributeName nvarchar(100)
)
RETURNS sql_variant
AS
BEGIN
    DECLARE @Value sql_variant;
    
    SELECT @Value = 
        CASE ad.DataType
            WHEN 'varchar' THEN av.StringValue
            WHEN 'int' THEN av.IntValue
            WHEN 'decimal' THEN av.DecimalValue
            WHEN 'datetime' THEN av.DateTimeValue
            WHEN 'bool' THEN av.BoolValue
        END
    FROM AttributeValues av
    JOIN AttributeDefinitions ad ON av.AttributeDefinitionID = ad.ID
    WHERE av.EntityID = @EntityID 
        AND ad.Name = @AttributeName;
    
    RETURN @Value;
END;
```

## 🔄 Hybrid JSON Model

### Schema con JSON
```sql
-- Tabla híbrida: estructura fija + JSON flexible
CREATE TABLE Products (
    ID int IDENTITY(1,1) PRIMARY KEY,
    Name nvarchar(200) NOT NULL,
    Category nvarchar(100) NOT NULL,
    BasePrice decimal(18,2) NOT NULL,
    
    -- Metadatos flexibles en JSON
    Attributes nvarchar(max) CHECK (ISJSON(Attributes) = 1),
    
    -- Configuración específica por categoría
    CategoryConfig nvarchar(max) CHECK (ISJSON(CategoryConfig) = 1),
    
    CreatedDate datetime2 DEFAULT GETUTCDATE(),
    ModifiedDate datetime2 DEFAULT GETUTCDATE()
);

-- Índices en propiedades JSON
CREATE INDEX IX_Products_Attributes_Brand 
ON Products (Name) INCLUDE (BasePrice)
WHERE JSON_VALUE(Attributes, '$.brand') IS NOT NULL;

-- Computed columns para queries frecuentes
ALTER TABLE Products 
ADD Brand AS JSON_VALUE(Attributes, '$.brand');

CREATE INDEX IX_Products_Brand ON Products (Brand);
```

### Queries JSON Avanzados
```sql
-- Buscar productos por atributos JSON
SELECT 
    p.ID,
    p.Name,
    p.BasePrice,
    JSON_VALUE(p.Attributes, '$.brand') AS Brand,
    JSON_VALUE(p.Attributes, '$.color') AS Color,
    JSON_QUERY(p.CategoryConfig, '$.specifications') AS Specs
FROM Products p
WHERE JSON_VALUE(p.Attributes, '$.brand') = 'Samsung'
    AND JSON_VALUE(p.Attributes, '$.color') = 'Black'
    AND JSON_VALUE(p.CategoryConfig, '$.warranty_years') = '2';

-- Actualizar propiedades JSON específicas
UPDATE Products 
SET Attributes = JSON_MODIFY(Attributes, '$.warranty_years', 3)
WHERE JSON_VALUE(Attributes, '$.brand') = 'Apple';

-- Agregar nueva propiedad JSON sin afectar existentes
UPDATE Products 
SET Attributes = JSON_MODIFY(
    ISNULL(Attributes, '{}'), 
    '$.eco_rating', 
    'A+'
)
WHERE Category = 'Electronics';
```

## 🧠 Meta-Model Engine

### Meta-Schema Definition
```sql
-- Definición de formularios dinámicos
CREATE TABLE FormDefinitions (
    ID int IDENTITY(1,1) PRIMARY KEY,
    Name nvarchar(100) NOT NULL,
    Title nvarchar(200) NOT NULL,
    Description nvarchar(500),
    Version int DEFAULT 1,
    IsActive bit DEFAULT 1,
    CreatedDate datetime2 DEFAULT GETUTCDATE()
);

-- Campos de formularios
CREATE TABLE FieldDefinitions (
    ID int IDENTITY(1,1) PRIMARY KEY,
    FormDefinitionID int FOREIGN KEY REFERENCES FormDefinitions(ID),
    Name nvarchar(100) NOT NULL,
    Label nvarchar(200) NOT NULL,
    FieldType nvarchar(50) NOT NULL, -- textbox, dropdown, checkbox, etc.
    DataType nvarchar(50) NOT NULL,
    IsRequired bit DEFAULT 0,
    ValidationRules nvarchar(max) CHECK (ISJSON(ValidationRules) = 1),
    DisplayOrder int,
    GroupName nvarchar(100),
    
    -- Configuración específica del campo
    FieldConfig nvarchar(max) CHECK (ISJSON(FieldConfig) = 1)
);

-- Datos dinámicos basados en formularios
CREATE TABLE DynamicFormData (
    ID bigint IDENTITY(1,1) PRIMARY KEY,
    FormDefinitionID int FOREIGN KEY REFERENCES FormDefinitions(ID),
    RecordID nvarchar(100) NOT NULL, -- ID del registro de negocio
    FieldData nvarchar(max) CHECK (ISJSON(FieldData) = 1),
    CreatedDate datetime2 DEFAULT GETUTCDATE(),
    ModifiedDate datetime2 DEFAULT GETUTCDATE(),
    
    UNIQUE(FormDefinitionID, RecordID)
);
```

## 🎨 Dynamic UI Generation

### C# Meta-Model Engine
```csharp
public class MetaModelEngine
{
    public class FormDefinition
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public List<FieldDefinition> Fields { get; set; } = new();
    }
    
    public class FieldDefinition
    {
        public string Name { get; set; }
        public string Label { get; set; }
        public FieldType Type { get; set; }
        public bool IsRequired { get; set; }
        public Dictionary<string, object> Config { get; set; } = new();
        public List<ValidationRule> Validations { get; set; } = new();
    }
    
    // Generar formulario HTML dinámicamente
    public string GenerateFormHtml(FormDefinition form)
    {
        var html = new StringBuilder();
        html.Append($"<form id='form_{form.Name}' class='dynamic-form'>");
        html.Append($"<h2>{form.Title}</h2>");
        
        foreach (var field in form.Fields.OrderBy(f => f.DisplayOrder))
        {
            html.Append(GenerateFieldHtml(field));
        }
        
        html.Append("<button type='submit'>Save</button>");
        html.Append("</form>");
        
        return html.ToString();
    }
    
    // Validar datos contra meta-modelo
    public ValidationResult ValidateData(
        FormDefinition form, 
        Dictionary<string, object> data)
    {
        var result = new ValidationResult();
        
        foreach (var field in form.Fields)
        {
            var value = data.ContainsKey(field.Name) ? data[field.Name] : null;
            
            // Validar requerido
            if (field.IsRequired && (value == null || value.ToString() == ""))
            {
                result.AddError(field.Name, $"{field.Label} is required");
                continue;
            }
            
            // Validar tipo de dato
            if (value != null && !ValidateDataType(value, field.DataType))
            {
                result.AddError(field.Name, $"{field.Label} has invalid data type");
                continue;
            }
            
            // Validaciones personalizadas
            foreach (var validation in field.Validations)
            {
                if (!validation.Validate(value))
                {
                    result.AddError(field.Name, validation.ErrorMessage);
                }
            }
        }
        
        return result;
    }
}
```

## 📊 Performance Considerations

### EAV Optimization
```sql
-- Vista materializada para queries frecuentes
CREATE VIEW ProductAttributesFlat AS
SELECT 
    e.ID AS ProductID,
    MAX(CASE WHEN ad.Name = 'Brand' THEN av.StringValue END) AS Brand,
    MAX(CASE WHEN ad.Name = 'Color' THEN av.StringValue END) AS Color,
    MAX(CASE WHEN ad.Name = 'Weight' THEN av.DecimalValue END) AS Weight,
    MAX(CASE WHEN ad.Name = 'LaunchDate' THEN av.DateTimeValue END) AS LaunchDate
FROM Entities e
JOIN EntityTypes et ON e.EntityTypeID = et.ID
LEFT JOIN AttributeValues av ON e.ID = av.EntityID
LEFT JOIN AttributeDefinitions ad ON av.AttributeDefinitionID = ad.ID
WHERE et.Name = 'Product'
GROUP BY e.ID;

-- Índice en vista materializada
CREATE UNIQUE CLUSTERED INDEX IX_ProductAttributesFlat_ProductID 
ON ProductAttributesFlat (ProductID);
```

### JSON Performance
```sql
-- Memory-optimized table para metadatos hot
CREATE TABLE MetadataCache (
    ID int IDENTITY(1,1) PRIMARY KEY NONCLUSTERED,
    EntityType nvarchar(100) NOT NULL,
    EntityID int NOT NULL,
    MetadataJSON nvarchar(max) NOT NULL,
    CacheExpiry datetime2 NOT NULL,
    
    INDEX IX_MetadataCache_Entity HASH (EntityType, EntityID) WITH (BUCKET_COUNT = 1000000)
) WITH (MEMORY_OPTIMIZED = ON, DURABILITY = SCHEMA_AND_DATA);
```

## 🎯 Best Practices

### Schema Evolution Strategy
1. **Versionado de metadatos** - Track schema changes
2. **Backward compatibility** - Mantener versiones anteriores
3. **Gradual migration** - Migrar datos incrementalmente
4. **Rollback plan** - Plan de contingencia
5. **Testing riguroso** - Validar en todos los entornos

### Performance Guidelines
1. **Cache metadatos** - En memoria o Redis
2. **Índices específicos** - Para queries frecuentes
3. **Partitioning** - Para tablas grandes
4. **Archival strategy** - Para datos históricos
5. **Monitoring** - Performance metrics continuos

## 🔄 Migration Patterns

### Schema Versioning
```sql
-- Tabla de versiones de schema
CREATE TABLE SchemaVersions (
    ID int IDENTITY(1,1) PRIMARY KEY,
    Version nvarchar(20) NOT NULL,
    Description nvarchar(500),
    UpgradeScript nvarchar(max),
    DowngradeScript nvarchar(max),
    AppliedDate datetime2,
    AppliedBy nvarchar(100)
);

-- Procedimiento de migración automática
CREATE PROCEDURE MigrateToVersion
    @TargetVersion nvarchar(20)
AS
BEGIN
    DECLARE @CurrentVersion nvarchar(20);
    SELECT TOP 1 @CurrentVersion = Version 
    FROM SchemaVersions 
    WHERE AppliedDate IS NOT NULL 
    ORDER BY AppliedDate DESC;
    
    -- Aplicar migraciones incrementales
    DECLARE migration_cursor CURSOR FOR
    SELECT UpgradeScript, Version
    FROM SchemaVersions 
    WHERE Version > @CurrentVersion 
        AND Version <= @TargetVersion
    ORDER BY Version;
    
    DECLARE @Script nvarchar(max), @Version nvarchar(20);
    
    OPEN migration_cursor;
    FETCH NEXT FROM migration_cursor INTO @Script, @Version;
    
    WHILE @@FETCH_STATUS = 0
    BEGIN
        EXEC sp_executesql @Script;
        
        UPDATE SchemaVersions 
        SET AppliedDate = GETUTCDATE(), 
            AppliedBy = SYSTEM_USER
        WHERE Version = @Version;
        
        FETCH NEXT FROM migration_cursor INTO @Script, @Version;
    END
    
    CLOSE migration_cursor;
    DEALLOCATE migration_cursor;
END;
```

## 🎓 Enterprise Implementation Example

### Complete Configuration System
```csharp
public class ConfigurationEngine
{
    // Configuración dinámica basada en metadatos
    public async Task<T> GetEntityConfigurationAsync<T>(
        string entityType, 
        int entityId) where T : class, new()
    {
        var metadata = await GetMetadataAsync(entityType, entityId);
        var config = new T();
        
        // Mapear JSON metadata a objeto tipado
        JsonConvert.PopulateObject(metadata.ConfigurationJson, config);
        
        return config;
    }
    
    // Actualización de configuración sin downtime
    public async Task UpdateConfigurationAsync<T>(
        string entityType, 
        int entityId, 
        T configuration)
    {
        var json = JsonConvert.SerializeObject(configuration);
        
        await _context.Database.ExecuteSqlRawAsync(
            "UPDATE MetadataCache SET MetadataJSON = {0} WHERE EntityType = {1} AND EntityID = {2}",
            json, entityType, entityId);
            
        // Invalidar cache
        await _cache.RemoveAsync($"config:{entityType}:{entityId}");
        
        // Notificar cambios a otros servicios
        await _eventBus.PublishAsync(new ConfigurationChangedEvent 
        { 
            EntityType = entityType, 
            EntityId = entityId 
        });
    }
}
```

**TIEMPO ESTIMADO:** 4-5 horas
**DIFICULTAD:** Avanzado
**PREREQUISITOS:** SQL avanzado, Entity Framework, JSON
**RESULTADO:** Sistema completamente configurable y evolutivo