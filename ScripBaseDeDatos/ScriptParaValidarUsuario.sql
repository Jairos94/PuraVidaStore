USE [PuraVidaStore]
GO
/****** Object:  StoredProcedure [dbo].[GetUsuario]    Script Date: 22/07/2022 19:38:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER procedure [dbo].[GetUsuario]
	@Usuario varchar(50),
	@Pass varchar(300)
AS

BEGIN

Declare 
@User varchar(50),
@Pass1 varchar (250)

SELECT @User = UsrUser,@Pass1=UsrPass FROM Usuarios Where UsrUser=@Usuario

	IF @User = @Usuario
	
			IF DecryptByPassPhrase('password',@Pass1) = @Pass
			SELECT 
		   u.UsrID
		  ,u.UsrUser
		  ,u.UsrEmail
		  ,u.UsrIdRol,
		  p.*,
		  r.*
		  ,u.UsrIdPersona FROM Usuarios u 
		  inner join Persona p on u.UsrIdPersona =p.PsrId
		  inner join RolUsiario r on u.UsrIdRol=r.RluID

		  WHERE u.UsrUser =@Usuario 
			else 
			begin
	
				SELECT 'La contraseña ingresada no coencide'
		end
	ELSE 
		Begin
		
			SELECT 'El usuario ingresado no existe'
		END
	END

