use PuraVidaStore
INSERT INTO RolUsiario(RluDescripcion) VALUES('Administrador'),('Vendedor')
go

INSERT INTO [dbo].[Persona]
           ([PsrIdentificacion]
           ,[PsrNombre]
           ,[PsrApellido1]
           ,[PsrApellido2])
     VALUES
           ('1'
           ,'Administrador'
           ,''
           ,''),
		   ('2'
           ,'Vendedor'
           ,''
           ,'')
GO

INSERT INTO [dbo].[Bodegas]
           ([bdgDescripcion]
           ,[bdgVisible])
     VALUES
           ('Central'
           ,1)
GO



INSERT INTO [dbo].[TipoMovimiento]
           ([TpmDescripcion])
     VALUES
           ('Positivo'),('Negativo')
GO


INSERT INTO [dbo].[MotivosMovimientos]
           ([MtmDescripcion]
           ,[MtmIdTipoMovimiento])
     VALUES
           ('Ingreso por compra'
           ,1)
GO

INSERT INTO [dbo].[FormaPago]
           ([FrpDescripcion])
     VALUES
           ('Efectivo'),('Sinpe Movil'),('Transferencia Bancaria')
go


INSERT INTO [dbo].[TiempoParaRenovar]
           ([TrrDescricpcion])
     VALUES
           ('Días'),('Meses'),('Años')
GO

INSERT INTO [dbo].[EstatusFactura]
           ([EsfDescripcion])
     VALUES
           ('Activa'),('Nula')
GO

INSERT INTO [dbo].[ParametrosGlobales]
           ([PrgUndsHabilitarMayorista]
           ,[PrgUndsAgregarMayorista]
           ,[PrgHabilitarImpuestos]
           ,[PrgImpustosIncluidos]
           ,[PrgIdBodega]
           ,[PrgIdTiempo]
           ,[PrgCantidadTiempo])
     VALUES
           (1
           ,1
           ,0
           ,0
           ,1
           ,3
           ,1)
GO

EXEC IngresarUsuario 'Admin','GzPfpnFb7HWhQeP0wnV0+g==','jairo.ri.ce@gmail.com',1,1
go

EXEC IngresarUsuario 'Vendedor','GzPfpnFb7HWhQeP0wnV0+g==',NULL,2,2
go
