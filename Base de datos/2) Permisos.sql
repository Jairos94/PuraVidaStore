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

-- Asignar roles de lectura y escritura
ALTER ROLE db_datareader ADD MEMBER Tienda;
ALTER ROLE db_datawriter ADD MEMBER Tienda;
GO


-- Cambiar a la base de datos PuraVidaStore
USE PuraVidaStore;
GO

-- Otorgar permiso de ejecución a todos los procedimientos en el esquema dbo
GRANT EXECUTE ON SCHEMA::dbo TO Tienda;
GO
