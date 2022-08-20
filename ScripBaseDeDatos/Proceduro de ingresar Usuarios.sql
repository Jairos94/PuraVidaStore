
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

EXEC IngresarUsuario 'Admin','GzPfpnFb7HWhQeP0wnV0+g==','jairo.ri.ce@gmail.com',1,1

EXEC IngresarUsuario 'Vendedor','GzPfpnFb7HWhQeP0wnV0+g==',NULL,2,2



CREATE PROCEDURE EditarUsuario

	@UsrUser varchar(16),
	@UsrPass varchar(256),
	@Email  varchar(100),
	@Rol int,
	@idPersona int,
	@idUsuario int
	


AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
		UPDATE [dbo].[Usuarios]
	   SET [UsrUser] = @UsrUser
		  ,[UsrPass] = EncryptByPassPhrase('password',@UsrPass)
		  ,[UsrEmail] = @Email
		  ,[UsrIdRol] = @Rol
		  ,[UsrIdPersona] = @idPersona
	 WHERE [UsrID] = @idUsuario
END
GO



  Select U.UsrID,U.UsrUser,U.UsrEmail,U.UsrIdRol,U.UsrIdPersona,P.PsrNombre,P.PsrApellido1,P.PsrApellido2 from Usuarios U
  Inner Join Persona P on p.PsrId=u.UsrIdPersona