use <nombre de la base de datos>
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


EXEC IngresarUsuario 'Admin','GzPfpnFb7HWhQeP0wnV0+g==','jairo.ri.ce@gmail.com',1,1
go

EXEC IngresarUsuario 'Vendedor','GzPfpnFb7HWhQeP0wnV0+g==',NULL,2,2
go
