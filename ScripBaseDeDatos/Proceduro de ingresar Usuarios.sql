
  CREATE PROCEDURE IngresarUsuario
  @Usuario varchar(15),
  @Pass VARCHAR(256),
  @Email Varchar(50),
  @IdRol int,
  @IdPersona int
  AS
 

  INSERT INTO [dbo].[Usuarios]
           ([UsrUser]
           ,[UsrPass]
           ,[UsrEmail]
           ,[UsrIdRol]
           ,[UsrIdPersona])
     VALUES
           (@Usuario
           ,EncryptByPassPhrase('password',@Pass )
           ,@Email
           ,@IdRol
           ,@IdPersona)
GO

EXEC IngresarUsuario 'Admin','123','jairo.ri.ce@gmail.com',1,1

EXEC IngresarUsuario 'Vendedor','123',NULL,2,2