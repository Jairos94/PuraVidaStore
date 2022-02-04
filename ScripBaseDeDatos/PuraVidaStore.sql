CREATE DATABASE PuraVidaStore
GO

USE PuraVidaStore
GO

CREATE TABLE Usuarios
(
	UsrID INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	UsrUser VARCHAR(16) NOT NULL,
	UsrPass VARCHAR(250) NOT NULL,
	UsrEmail VARCHAR(20) NULL
)
Go

CREATE TABLE RolUsiario
(
	RluID INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	RluDescripcion VARCHAR(16) NOT NULL
)
GO

INSERT INTO RolUsiario(RluDescripcion) VALUES('Administrador'),('Vendedor')
GO


ALTER TABLE Usuarios
ADD UsrIdRol int FOREIGN KEY(UsrIdRol)  References RolUsiario(RluID) NOT NULL
GO

CREATE TABLE Persona
(
	PsrId INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	PsrIdentificacion VARCHAR(30) NOT NULL,
	PsrNombre VARCHAR(50) NOT NULL,
	PsrApellido1 VARCHAR(50) NOT NULL,
	PsrApellido2 VARCHAR(50) NOT NULL
)
GO

ALTER TABLE Usuarios
ADD UsrIdPersona int FOREIGN KEY(UsrIdPersona) References Persona(PsrId) NOT NULL
GO



CREATE TABLE Productos 
(
	PrdId INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	PrdNombre VARCHAR(50) NOT NULL,
	PrdPrecioVentaMayorista FLOAT NOT NULL,
	PrdPrecioVentaMinorista FLOAT NOT NULL,
	PrdCodigo VARCHAR(100) NULL,
	PrdUnidadesMinimas int NULL
)
GO


CREATE TABLE TipoProducto 
(
	TppId INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	TppDescripci�n VARCHAR(50) NOT NULL
)
GO

ALTER TABLE Productos
ADD PrdIdTipoProducto INT FOREIGN KEY(PrdIdTipoProducto) References TipoProducto(TppId) NOT NULL
GO



CREATE TABLE HistorialPrecios
(
	HlpId INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	HlpIdProducto INT FOREIGN KEY(HlpIdProducto) References Productos(PrdId) NOT NULL,
	HlpFecha DATETIME NOT NULL,
	HlpIdUsuario INT FOREIGN KEY(HlpIdUsuario) References Usuarios(UsrID) NOT NULL,
	HlpPrecioMayorista FLOAT NULL,
	HlpPrecioMinorista FLOAT NULL
)
GO


CREATE TABLE ClientesMayoristas
(
	ClmId INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	ClmIdPersona INT FOREIGN KEY(ClmIdPersona) References Persona(PsrId) NOT NULL,
	ClmFechaCreacion DATETIME NOT NULL,
	ClmFechaVencimiento DATETIME NOT NULL
)
GO


CREATE TABLE HistorialClienteMayorista
(
	HcmId INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	HcmIdCliente INT FOREIGN KEY(HcmIdCliente) References ClientesMayoristas(ClmId) NOT NULL,
	HcmFechaVencimiento DATETIME NOT NULL,
	HcmFechaActualizacion DATETIME NOT NULL
)
Go

--Los Motivos son como la descripci�n tales como 
--ingreso de mercader�a,
--ingreso por devoluci�n
--Ingreso por traslado 
--Salida por traslado 
/*
	Una vez identificado cada motivo se debe saber que los motivos pueden sumar o restar
	Recordar queel tipo de movimiento ser� un dropdawn
*/
CREATE TABLE Movimientos
(
	MvmId INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	MvmIdProducto INT FOREIGN KEY(MvmIdProducto) References Productos(PrdId) NOT NULL,
	MvmCantidad INT NOT NULL,
	MvmFecha DATETIME NOT NULL
)
GO

CREATE TABLE MotivosMovimientos
(
	MtmId INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	MtmDescripcion VARCHAR(50) NOT NULL
)
Go

CREATE TABLE TipoMovimiento
(
	TpmId INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	TpmDescripcion VARCHAR (15) NOT NULL,
)
GO

--Positivo suma y negativo resta
INSERT INTO TipoMovimiento(TpmDescripcion) VALUES('Positivo'),('Negativo')
GO

ALTER TABLE MotivosMovimientos
ADD MtmIdTipoMovimiento INT FOREIGN KEY(MtmIdTipoMovimiento) References TipoMovimiento(TpmId) NOT NULL
GO

ALTER TABLE Movimientos
ADD MvmIdMotivoMovimiento INT FOREIGN KEY(MvmIdMotivoMovimiento) References MotivosMovimientos(MtmId) NOT NULL
GO


ALTER TABLE ClientesMayoristas
ADD ClmCorreo VARCHAR(20) NULL,
    ClmTelefono VARCHAR(12) NULL




CREATE TABLE Factura
(
	FtrId INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	FtrFecha DATETIME NOT NULL,
	FtrIdUsuario INT FOREIGN KEY(FtrIdUsuario) References Usuarios(UsrID) NOT NULL,
	FtrMayorista INT FOREIGN KEY(FtrMayorista) References ClientesMayoristas(ClmId) NULL
)
GO

ALTER TABLE Movimientos
ADD MvmIdUsuario INT FOREIGN KEY(MvmIdUsuario) References Usuarios(UsrID) NOT NULL
GO

CREATE TABLE EstatusFactura
(
	EtfId INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	EsfDescripcion VARCHAR(10) NOT NULL
)
GO
INSERT INTO EstatusFactura(EsfDescripcion) VALUES('Aplicada'),('Anulada')
GO

ALTER TABLE Factura
ADD FtrEstatusId INT FOREIGN KEY(FtrEstatusId) References EstatusFactura(EtfId) NOT NULL
GO

CREATE TABLE DetalleFactura
(
	DtfId INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	DtfIdProducto INT FOREIGN KEY(DtfIdProducto) References Productos(PrdId) NOT NULL,
	DtfIdFactura INT FOREIGN KEY(DtfIdProducto) References Factura(FtrId) NOT NULL,
	DtfPrecio FLOAT NOT NULL,
	DtfDescuento INT NULL

)

CREATE TABLE HistorialFacturasAnuladas
(
	HlfId INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	HlfIdUsuario INT FOREIGN KEY(HlfIdUsuario) References Usuarios(UsrID) NOT NULL,
	HlfIdFctura INT FOREIGN KEY(HlfIdFctura) References Factura(FtrId) NOT NULL,
	HlfRazon VARCHAR(250) NOT NULL
)
GO

