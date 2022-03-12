/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (1000) [UsrID]
      ,[UsrUser]
      ,ENCRYPTBYPASSPHRASE('PassSecrete',[UsrPass]) As Password
      ,[UsrEmail]
      ,[UsrIdRol]
      ,[UsrIdPersona]
  FROM [PuraVidaStore].[dbo].[Usuarios]