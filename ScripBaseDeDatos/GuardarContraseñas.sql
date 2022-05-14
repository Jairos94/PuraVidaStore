/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (1000) [UsrID]
      ,[UsrUser]
	  ,DecryptByPassPhrase('password',UsrPass) 
      ,ENCRYPTBYPASSPHRASE('password',[UsrPass]) As Password 
      ,[UsrEmail]
      ,[UsrIdRol]
      ,[UsrIdPersona]
  FROM [PuraVidaStore].[dbo].[Usuarios]


  SELECT CardNumber, CardNumber_EncryptedbyPassphrase   
    AS 'Encrypted card number', CONVERT(varchar,  
    DecryptByPassphrase('PassSecrete', UsrPass, 1   
    , CONVERT(varbinary, CreditCardID)))  