USE [PuraVidaStore]
GO
/****** Object:  StoredProcedure [dbo].[GetUsuario]    Script Date: 08/03/2022 18:59:58 ******/
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
		SELECT * FROM Usuarios WHERE UsrUser=@Usuario 
		else 
		return 1
	ELSE 
	Begin
		Return 0
	END
	END

