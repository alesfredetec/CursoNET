-- EJEMPLO COMPLETO: Implementación del patrón EAV (Entity-Attribute-Value)
-- Sistema de gestión de productos con atributos dinámicos

-- ================================
-- SCHEMA EAV COMPLETO
-- ================================

-- ✅ Tabla de tipos de entidades
CREATE TABLE EntityTypes (
    ID int IDENTITY(1,1) PRIMARY KEY,
    Name nvarchar(100) NOT NULL UNIQUE,
    Description nvarchar(500),
    TableName nvarchar(100), -- Tabla base si existe
    IsActive bit DEFAULT 1,
    CreatedDate datetime2 DEFAULT GETUTCDATE(),
    ModifiedDate datetime2 DEFAULT GETUTCDATE()
);

-- ✅ Definición de atributos dinámicos
CREATE TABLE AttributeDefinitions (
    ID int IDENTITY(1,1) PRIMARY KEY,
    EntityTypeID int NOT NULL FOREIGN KEY REFERENCES EntityTypes(ID),
    Name nvarchar(100) NOT NULL,
    DisplayName nvarchar(200) NOT NULL,
    DataType nvarchar(50) NOT NULL, -- varchar, int, decimal, datetime, bool, text, json
    MaxLength int NULL,
    IsRequired bit DEFAULT 0,
    DefaultValue nvarchar(max),
    ValidationRule nvarchar(max), -- JSON con reglas de validación
    DisplayOrder int DEFAULT 0,
    GroupName nvarchar(100),
    HelpText nvarchar(500),
    IsActive bit DEFAULT 1,
    CreatedDate datetime2 DEFAULT GETUTCDATE(),
    
    UNIQUE (EntityTypeID, Name)
);

-- ✅ Instancias de entidades
CREATE TABLE Entities (
    ID bigint IDENTITY(1,1) PRIMARY KEY,
    EntityTypeID int NOT NULL FOREIGN KEY REFERENCES EntityTypes(ID),
    ExternalID nvarchar(100), -- ID en sistema externo si aplica
    Status nvarchar(50) DEFAULT 'Active',
    CreatedDate datetime2 DEFAULT GETUTCDATE(),
    ModifiedDate datetime2 DEFAULT GETUTCDATE(),
    CreatedBy nvarchar(100),
    ModifiedBy nvarchar(100),
    Version rowversion, -- Para optimistic locking
    
    INDEX IX_Entities_EntityType (EntityTypeID),
    INDEX IX_Entities_ExternalID (ExternalID),
    INDEX IX_Entities_Status (Status)
);

-- ✅ Valores de atributos - Tabla principal EAV
CREATE TABLE AttributeValues (
    ID bigint IDENTITY(1,1) PRIMARY KEY,
    EntityID bigint NOT NULL FOREIGN KEY REFERENCES Entities(ID) ON DELETE CASCADE,
    AttributeDefinitionID int NOT NULL FOREIGN KEY REFERENCES AttributeDefinitions(ID),
    
    -- Columnas tipadas para performance
    StringValue nvarchar(max),
    IntValue int,
    DecimalValue decimal(18,6),
    DateTimeValue datetime2,
    BoolValue bit,
    TextValue nvarchar(max), -- Para textos largos
    JsonValue nvarchar(max) CHECK (JsonValue IS NULL OR ISJSON(JsonValue) = 1),
    
    CreatedDate datetime2 DEFAULT GETUTCDATE(),
    ModifiedDate datetime2 DEFAULT GETUTCDATE(),
    
    -- Constraint: solo un valor por atributo por entidad
    UNIQUE (EntityID, AttributeDefinitionID),
    
    -- Índices optimizados para queries frecuentes
    INDEX IX_AttributeValues_Entity_Attribute (EntityID, AttributeDefinitionID),
    INDEX IX_AttributeValues_Attribute_String (AttributeDefinitionID, StringValue),
    INDEX IX_AttributeValues_Attribute_Int (AttributeDefinitionID, IntValue),
    INDEX IX_AttributeValues_Attribute_Decimal (AttributeDefinitionID, DecimalValue),
    INDEX IX_AttributeValues_Attribute_DateTime (AttributeDefinitionID, DateTimeValue),
    INDEX IX_AttributeValues_Attribute_Bool (AttributeDefinitionID, BoolValue)
);

-- ✅ Tabla de auditoría para cambios de valores
CREATE TABLE AttributeValueHistory (
    ID bigint IDENTITY(1,1) PRIMARY KEY,
    AttributeValueID bigint NOT NULL,
    EntityID bigint NOT NULL,
    AttributeDefinitionID int NOT NULL,
    OldValue nvarchar(max),
    NewValue nvarchar(max),
    ChangeDate datetime2 DEFAULT GETUTCDATE(),
    ChangedBy nvarchar(100),
    ChangeReason nvarchar(500),
    
    INDEX IX_AttributeValueHistory_Entity (EntityID),
    INDEX IX_AttributeValueHistory_Attribute (AttributeDefinitionID),
    INDEX IX_AttributeValueHistory_Date (ChangeDate)
);

-- ================================
-- DATOS DE EJEMPLO
-- ================================

-- ✅ Insertar tipos de entidades
INSERT INTO EntityTypes (Name, Description) VALUES
('Product', 'Productos del catálogo'),
('Customer', 'Clientes del sistema'),
('Order', 'Órdenes de compra'),
('Supplier', 'Proveedores');

-- ✅ Definir atributos para Productos
DECLARE @ProductTypeID int = (SELECT ID FROM EntityTypes WHERE Name = 'Product');

INSERT INTO AttributeDefinitions (EntityTypeID, Name, DisplayName, DataType, IsRequired, DisplayOrder, GroupName) VALUES
(@ProductTypeID, 'Brand', 'Marca', 'varchar', 1, 1, 'Basic'),
(@ProductTypeID, 'Model', 'Modelo', 'varchar', 1, 2, 'Basic'),
(@ProductTypeID, 'Color', 'Color', 'varchar', 0, 3, 'Basic'),
(@ProductTypeID, 'Weight', 'Peso (kg)', 'decimal', 0, 4, 'Physical'),
(@ProductTypeID, 'Dimensions', 'Dimensiones', 'varchar', 0, 5, 'Physical'),
(@ProductTypeID, 'WarrantyYears', 'Años de Garantía', 'int', 0, 6, 'Warranty'),
(@ProductTypeID, 'LaunchDate', 'Fecha de Lanzamiento', 'datetime', 0, 7, 'Dates'),
(@ProductTypeID, 'IsEcoFriendly', 'Eco-Friendly', 'bool', 0, 8, 'Environmental'),
(@ProductTypeID, 'Specifications', 'Especificaciones Técnicas', 'json', 0, 9, 'Technical'),
(@ProductTypeID, 'Description', 'Descripción Detallada', 'text', 0, 10, 'Content');

-- ✅ Definir atributos para Clientes
DECLARE @CustomerTypeID int = (SELECT ID FROM EntityTypes WHERE Name = 'Customer');

INSERT INTO AttributeDefinitions (EntityTypeID, Name, DisplayName, DataType, IsRequired, DisplayOrder, GroupName) VALUES
(@CustomerTypeID, 'PreferredLanguage', 'Idioma Preferido', 'varchar', 0, 1, 'Preferences'),
(@CustomerTypeID, 'CreditLimit', 'Límite de Crédito', 'decimal', 0, 2, 'Financial'),
(@CustomerTypeID, 'LoyaltyPoints', 'Puntos de Lealtad', 'int', 0, 3, 'Loyalty'),
(@CustomerTypeID, 'LastPurchaseDate', 'Última Compra', 'datetime', 0, 4, 'Activity'),
(@CustomerTypeID, 'IsVIP', 'Cliente VIP', 'bool', 0, 5, 'Status'),
(@CustomerTypeID, 'Preferences', 'Preferencias', 'json', 0, 6, 'Preferences'),
(@CustomerTypeID, 'Notes', 'Notas del Cliente', 'text', 0, 7, 'Content');

-- ================================
-- FUNCIONES HELPER PARA EAV
-- ================================

-- ✅ Función para obtener valor tipado
CREATE FUNCTION GetAttributeValue(
    @EntityID bigint,
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
            WHEN 'text' THEN av.TextValue
            WHEN 'json' THEN av.JsonValue
        END
    FROM AttributeValues av
    INNER JOIN AttributeDefinitions ad ON av.AttributeDefinitionID = ad.ID
    INNER JOIN Entities e ON av.EntityID = e.ID
    WHERE av.EntityID = @EntityID 
        AND ad.Name = @AttributeName;
    
    RETURN @Value;
END;

-- ✅ Procedimiento para establecer valor de atributo
CREATE PROCEDURE SetAttributeValue
    @EntityID bigint,
    @AttributeName nvarchar(100),
    @Value nvarchar(max),
    @ModifiedBy nvarchar(100) = 'System'
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @AttributeDefID int;
    DECLARE @DataType nvarchar(50);
    DECLARE @OldValue nvarchar(max);
    
    -- Obtener definición del atributo
    SELECT 
        @AttributeDefID = ad.ID,
        @DataType = ad.DataType
    FROM AttributeDefinitions ad
    INNER JOIN Entities e ON ad.EntityTypeID = e.EntityTypeID
    WHERE e.ID = @EntityID AND ad.Name = @AttributeName;
    
    IF @AttributeDefID IS NULL
    BEGIN
        RAISERROR('Attribute %s not found for entity %d', 16, 1, @AttributeName, @EntityID);
        RETURN;
    END
    
    -- Obtener valor actual para auditoría
    SELECT @OldValue = 
        CASE @DataType
            WHEN 'varchar' THEN StringValue
            WHEN 'int' THEN CAST(IntValue AS nvarchar)
            WHEN 'decimal' THEN CAST(DecimalValue AS nvarchar)
            WHEN 'datetime' THEN CONVERT(nvarchar, DateTimeValue, 121)
            WHEN 'bool' THEN CASE WHEN BoolValue = 1 THEN 'true' ELSE 'false' END
            WHEN 'text' THEN TextValue
            WHEN 'json' THEN JsonValue
        END
    FROM AttributeValues
    WHERE EntityID = @EntityID AND AttributeDefinitionID = @AttributeDefID;
    
    -- Insertar o actualizar valor
    MERGE AttributeValues AS target
    USING (SELECT @EntityID as EntityID, @AttributeDefID as AttributeDefinitionID) AS source
    ON target.EntityID = source.EntityID AND target.AttributeDefinitionID = source.AttributeDefinitionID
    WHEN MATCHED THEN
        UPDATE SET
            StringValue = CASE WHEN @DataType = 'varchar' THEN @Value ELSE NULL END,
            IntValue = CASE WHEN @DataType = 'int' THEN TRY_CAST(@Value AS int) ELSE NULL END,
            DecimalValue = CASE WHEN @DataType = 'decimal' THEN TRY_CAST(@Value AS decimal(18,6)) ELSE NULL END,
            DateTimeValue = CASE WHEN @DataType = 'datetime' THEN TRY_CAST(@Value AS datetime2) ELSE NULL END,
            BoolValue = CASE WHEN @DataType = 'bool' THEN TRY_CAST(@Value AS bit) ELSE NULL END,
            TextValue = CASE WHEN @DataType = 'text' THEN @Value ELSE NULL END,
            JsonValue = CASE WHEN @DataType = 'json' THEN @Value ELSE NULL END,
            ModifiedDate = GETUTCDATE()
    WHEN NOT MATCHED THEN
        INSERT (EntityID, AttributeDefinitionID, StringValue, IntValue, DecimalValue, DateTimeValue, BoolValue, TextValue, JsonValue)
        VALUES (@EntityID, @AttributeDefID,
            CASE WHEN @DataType = 'varchar' THEN @Value ELSE NULL END,
            CASE WHEN @DataType = 'int' THEN TRY_CAST(@Value AS int) ELSE NULL END,
            CASE WHEN @DataType = 'decimal' THEN TRY_CAST(@Value AS decimal(18,6)) ELSE NULL END,
            CASE WHEN @DataType = 'datetime' THEN TRY_CAST(@Value AS datetime2) ELSE NULL END,
            CASE WHEN @DataType = 'bool' THEN TRY_CAST(@Value AS bit) ELSE NULL END,
            CASE WHEN @DataType = 'text' THEN @Value ELSE NULL END,
            CASE WHEN @DataType = 'json' THEN @Value ELSE NULL END
        );
    
    -- Guardar en auditoría si cambió el valor
    IF @OldValue != @Value OR (@OldValue IS NULL AND @Value IS NOT NULL) OR (@OldValue IS NOT NULL AND @Value IS NULL)
    BEGIN
        INSERT INTO AttributeValueHistory (AttributeValueID, EntityID, AttributeDefinitionID, OldValue, NewValue, ChangedBy)
        SELECT av.ID, @EntityID, @AttributeDefID, @OldValue, @Value, @ModifiedBy
        FROM AttributeValues av
        WHERE av.EntityID = @EntityID AND av.AttributeDefinitionID = @AttributeDefID;
    END
    
    -- Actualizar timestamp de entidad
    UPDATE Entities 
    SET ModifiedDate = GETUTCDATE(), ModifiedBy = @ModifiedBy
    WHERE ID = @EntityID;
END;

-- ================================
-- VISTA MATERIALIZADA PARA PERFORMANCE
-- ================================

-- ✅ Vista que "aplana" atributos de productos para queries frecuentes
CREATE VIEW ProductAttributesFlat AS
SELECT 
    e.ID AS ProductID,
    e.ExternalID,
    e.Status,
    MAX(CASE WHEN ad.Name = 'Brand' THEN av.StringValue END) AS Brand,
    MAX(CASE WHEN ad.Name = 'Model' THEN av.StringValue END) AS Model,
    MAX(CASE WHEN ad.Name = 'Color' THEN av.StringValue END) AS Color,
    MAX(CASE WHEN ad.Name = 'Weight' THEN av.DecimalValue END) AS Weight,
    MAX(CASE WHEN ad.Name = 'WarrantyYears' THEN av.IntValue END) AS WarrantyYears,
    MAX(CASE WHEN ad.Name = 'LaunchDate' THEN av.DateTimeValue END) AS LaunchDate,
    MAX(CASE WHEN ad.Name = 'IsEcoFriendly' THEN av.BoolValue END) AS IsEcoFriendly,
    MAX(CASE WHEN ad.Name = 'Specifications' THEN av.JsonValue END) AS Specifications,
    MAX(CASE WHEN ad.Name = 'Description' THEN av.TextValue END) AS Description,
    e.CreatedDate,
    e.ModifiedDate
FROM Entities e
INNER JOIN EntityTypes et ON e.EntityTypeID = et.ID
LEFT JOIN AttributeValues av ON e.ID = av.EntityID
LEFT JOIN AttributeDefinitions ad ON av.AttributeDefinitionID = ad.ID
WHERE et.Name = 'Product'
GROUP BY e.ID, e.ExternalID, e.Status, e.CreatedDate, e.ModifiedDate;

-- ✅ Índice en vista para performance
CREATE UNIQUE CLUSTERED INDEX IX_ProductAttributesFlat_ProductID 
ON ProductAttributesFlat (ProductID);

CREATE INDEX IX_ProductAttributesFlat_Brand 
ON ProductAttributesFlat (Brand);

CREATE INDEX IX_ProductAttributesFlat_LaunchDate 
ON ProductAttributesFlat (LaunchDate);

-- ================================
-- EJEMPLOS DE USO
-- ================================

-- ✅ Crear una entidad producto
DECLARE @ProductEntityID bigint;

INSERT INTO Entities (EntityTypeID, ExternalID, CreatedBy)
VALUES ((SELECT ID FROM EntityTypes WHERE Name = 'Product'), 'PROD-001', 'DataLoader');

SET @ProductEntityID = SCOPE_IDENTITY();

-- ✅ Establecer atributos del producto
EXEC SetAttributeValue @ProductEntityID, 'Brand', 'Samsung', 'DataLoader';
EXEC SetAttributeValue @ProductEntityID, 'Model', 'Galaxy S23', 'DataLoader';
EXEC SetAttributeValue @ProductEntityID, 'Color', 'Black', 'DataLoader';
EXEC SetAttributeValue @ProductEntityID, 'Weight', '168.0', 'DataLoader';
EXEC SetAttributeValue @ProductEntityID, 'WarrantyYears', '2', 'DataLoader';
EXEC SetAttributeValue @ProductEntityID, 'LaunchDate', '2023-02-17', 'DataLoader';
EXEC SetAttributeValue @ProductEntityID, 'IsEcoFriendly', 'true', 'DataLoader';
EXEC SetAttributeValue @ProductEntityID, 'Specifications', '{"screen": "6.1 inch", "storage": "128GB", "camera": "50MP"}', 'DataLoader';
EXEC SetAttributeValue @ProductEntityID, 'Description', 'Flagship smartphone with advanced camera system and all-day battery life.', 'DataLoader';

-- ✅ Consultar producto con todos sus atributos (método EAV puro)
SELECT 
    e.ID,
    e.ExternalID,
    ad.Name AS AttributeName,
    ad.DisplayName,
    ad.DataType,
    CASE ad.DataType
        WHEN 'varchar' THEN av.StringValue
        WHEN 'int' THEN CAST(av.IntValue AS nvarchar)
        WHEN 'decimal' THEN CAST(av.DecimalValue AS nvarchar)
        WHEN 'datetime' THEN CONVERT(nvarchar, av.DateTimeValue, 121)
        WHEN 'bool' THEN CASE WHEN av.BoolValue = 1 THEN 'true' ELSE 'false' END
        WHEN 'text' THEN av.TextValue
        WHEN 'json' THEN av.JsonValue
    END AS Value
FROM Entities e
INNER JOIN EntityTypes et ON e.EntityTypeID = et.ID
LEFT JOIN AttributeValues av ON e.ID = av.EntityID
LEFT JOIN AttributeDefinitions ad ON av.AttributeDefinitionID = ad.ID
WHERE et.Name = 'Product' 
    AND e.ExternalID = 'PROD-001'
ORDER BY ad.DisplayOrder;

-- ✅ Consultar usando vista materializada (mejor performance)
SELECT 
    ProductID,
    ExternalID,
    Brand,
    Model,
    Color,
    Weight,
    WarrantyYears,
    LaunchDate,
    IsEcoFriendly,
    JSON_VALUE(Specifications, '$.screen') AS ScreenSize,
    JSON_VALUE(Specifications, '$.storage') AS Storage,
    Description
FROM ProductAttributesFlat
WHERE ExternalID = 'PROD-001';

-- ✅ Buscar productos por atributos específicos
SELECT 
    ProductID,
    Brand,
    Model,
    Color,
    LaunchDate
FROM ProductAttributesFlat
WHERE Brand = 'Samsung'
    AND LaunchDate >= '2023-01-01'
    AND IsEcoFriendly = 1
ORDER BY LaunchDate DESC;

-- ✅ Query dinámico para buscar por cualquier atributo
SELECT DISTINCT
    e.ID AS ProductID,
    e.ExternalID,
    paf.Brand,
    paf.Model
FROM Entities e
INNER JOIN EntityTypes et ON e.EntityTypeID = et.ID
INNER JOIN AttributeValues av ON e.ID = av.EntityID
INNER JOIN AttributeDefinitions ad ON av.AttributeDefinitionID = ad.ID
LEFT JOIN ProductAttributesFlat paf ON e.ID = paf.ProductID
WHERE et.Name = 'Product'
    AND (
        (ad.Name = 'Brand' AND av.StringValue = 'Samsung') OR
        (ad.Name = 'Color' AND av.StringValue = 'Black') OR
        (ad.Name = 'WarrantyYears' AND av.IntValue >= 2)
    );

-- ✅ Reporte de auditoría - ver cambios en atributos
SELECT 
    h.ChangeDate,
    h.ChangedBy,
    e.ExternalID AS ProductID,
    ad.DisplayName AS Attribute,
    h.OldValue,
    h.NewValue,
    h.ChangeReason
FROM AttributeValueHistory h
INNER JOIN Entities e ON h.EntityID = e.ID
INNER JOIN AttributeDefinitions ad ON h.AttributeDefinitionID = ad.ID
WHERE e.ExternalID = 'PROD-001'
ORDER BY h.ChangeDate DESC;

-- ================================
-- PROCEDIMIENTOS DE MANTENIMIENTO
-- ================================

-- ✅ Limpiar valores huérfanos
CREATE PROCEDURE CleanupOrphanedValues
AS
BEGIN
    -- Eliminar valores de atributos sin entidad
    DELETE av
    FROM AttributeValues av
    LEFT JOIN Entities e ON av.EntityID = e.ID
    WHERE e.ID IS NULL;
    
    -- Eliminar valores de atributos desactivados
    DELETE av
    FROM AttributeValues av
    INNER JOIN AttributeDefinitions ad ON av.AttributeDefinitionID = ad.ID
    WHERE ad.IsActive = 0;
    
    PRINT 'Cleanup completed';
END;

-- ✅ Estadísticas de uso EAV
CREATE PROCEDURE GetEAVStatistics
AS
BEGIN
    SELECT 
        'Entity Types' AS Category,
        COUNT(*) AS Count,
        SUM(CASE WHEN IsActive = 1 THEN 1 ELSE 0 END) AS Active
    FROM EntityTypes
    
    UNION ALL
    
    SELECT 
        'Attribute Definitions' AS Category,
        COUNT(*) AS Count,
        SUM(CASE WHEN IsActive = 1 THEN 1 ELSE 0 END) AS Active
    FROM AttributeDefinitions
    
    UNION ALL
    
    SELECT 
        'Entities' AS Category,
        COUNT(*) AS Count,
        SUM(CASE WHEN Status = 'Active' THEN 1 ELSE 0 END) AS Active
    FROM Entities
    
    UNION ALL
    
    SELECT 
        'Attribute Values' AS Category,
        COUNT(*) AS Count,
        COUNT(*) AS Active
    FROM AttributeValues;
    
    -- Top entities por cantidad de atributos
    SELECT TOP 10
        et.Name AS EntityType,
        COUNT(av.ID) AS AttributeCount,
        AVG(CAST(COUNT(av.ID) AS float)) OVER (PARTITION BY et.Name) AS AvgAttributesPerEntity
    FROM Entities e
    INNER JOIN EntityTypes et ON e.EntityTypeID = et.ID
    LEFT JOIN AttributeValues av ON e.ID = av.EntityID
    GROUP BY et.Name, e.ID
    ORDER BY AttributeCount DESC;
END;

/*
RESUMEN DEL PATRÓN EAV:

✅ VENTAJAS:
- Flexibilidad extrema para agregar atributos
- No requiere cambios de schema
- Soporte para múltiples tipos de datos
- Auditoría completa de cambios
- Versionado de atributos

⚠️ DESVENTAJAS:
- Queries más complejos
- Performance challenges en grandes volúmenes
- Pérdida de foreign key constraints
- Herramientas de BI menos compatibles

🎯 CUÁNDO USAR EAV:
- Sistemas altamente configurables
- Catálogos de productos variables
- CRM con campos personalizados
- Formularios dinámicos
- Sistemas multi-tenant

🚀 OPTIMIZACIONES APLICADAS:
- Vistas materializadas para queries frecuentes
- Índices específicos por tipo de dato
- Procedimientos para operaciones comunes
- Auditoría automática
- Cleanup procedures

PRÓXIMO EJEMPLO:
02-JSON-Columns - Hybrid approach con JSON
*/