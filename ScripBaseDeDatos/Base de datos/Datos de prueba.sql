use <Base de datos Creada>
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


EXEC IngresarUsuario 'Admin','GzPfpnFb7HWhQeP0wnV0+g==','jairo.ri.ce@gmail.com',1,1
go

EXEC IngresarUsuario 'Vendedor','GzPfpnFb7HWhQeP0wnV0+g==',NULL,2,2
go
