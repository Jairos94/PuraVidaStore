Create Procedure ocpv
@IdUsuario int
as
Begin
	SET NOCOUNT ON;
	Select CONVERT(varchar,DecryptByPassPhrase('password',U.UsrPass))  from Usuarios U
end