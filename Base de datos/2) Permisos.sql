-- Cambiar a la base de datos master
USE master;
GO

-- Crear login si no existe
IF NOT EXISTS (SELECT * FROM sys.server_principals WHERE name = 'Tienda')
BEGIN
    CREATE LOGIN Tienda WITH PASSWORD = 'Store$2024$', CHECK_POLICY = OFF;
END
GO

-- Cambiar a la base de datos PuraVidaStore
USE PuraVidaStore;
GO

-- Crear usuario si no existe
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = 'Tienda')
BEGIN
    CREATE USER Tienda FOR LOGIN Tienda;
END
GO

-- Conceder permisos de conexión
GRANT CONNECT TO Tienda;
GO

-- Asignar roles de lectura y escritura si no están ya asignados
IF NOT EXISTS (SELECT * FROM sys.database_role_members WHERE member_principal_id = USER_ID('Tienda') AND role_principal_id = USER_ID('db_datareader'))
BEGIN
    ALTER ROLE db_datareader ADD MEMBER Tienda;
END
GO

IF NOT EXISTS (SELECT * FROM sys.database_role_members WHERE member_principal_id = USER_ID('Tienda') AND role_principal_id = USER_ID('db_datawriter'))
BEGIN
    ALTER ROLE db_datawriter ADD MEMBER Tienda;
END
GO

-- Lista de procedimientos almacenados
DECLARE @UserName NVARCHAR(128) = 'Tienda';
DECLARE @Procedures TABLE (ProcedureName NVARCHAR(128));
INSERT INTO @Procedures VALUES 
    ('dbo.EditarUsuario'),
    ('dbo.GetUsuario'),
    ('dbo.IngresarUsuario'),
    ('dbo.ObtenerUsuarios'),
    ('dbo.ocpv'),
    ('dbo.ReporteMovimientos'),
    ('dbo.SP_Inventarios');

-- Cursor para recorrer los procedimientos
DECLARE cur CURSOR FOR
SELECT ProcedureName FROM @Procedures;

DECLARE @Procedure NVARCHAR(128);

OPEN cur;

FETCH NEXT FROM cur INTO @Procedure;
WHILE @@FETCH_STATUS = 0
BEGIN
    -- Verificar si el usuario ya tiene permisos
    IF NOT EXISTS (
        SELECT *
        FROM sys.database_permissions
        WHERE grantee_principal_id = USER_ID(@UserName)
          AND major_id = OBJECT_ID(@Procedure)
          AND permission_name = 'EXECUTE'
    )
    BEGIN
        -- Otorgar permiso de ejecución
        DECLARE @SQL NVARCHAR(MAX);
        SET @SQL = 'GRANT EXECUTE ON ' + QUOTENAME(@Procedure) + ' TO ' + QUOTENAME(@UserName);
        EXEC sp_executesql @SQL;
    END

    FETCH NEXT FROM cur INTO @Procedure;
END

CLOSE cur;
DEALLOCATE cur;

GO
