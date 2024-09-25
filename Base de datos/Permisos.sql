USE master;
Go

CREATE LOGIN Tienda WITH PASSWORD = 'Store$2024$', CHECK_POLICY = OFF;
go


CREATE USER Tienda FOR LOGIN Tienda;
go

GRANT CONNECT SQL TO Tienda;
go

USE PuraVidaStore;
CREATE USER Tienda FOR LOGIN Tienda;
ALTER ROLE db_datareader ADD MEMBER Tienda;
ALTER ROLE db_datawriter ADD MEMBER Tienda;
