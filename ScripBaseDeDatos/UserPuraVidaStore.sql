USE [master]
GO

/* For security reasons the login is created disabled and with a random password. */
/****** Object:  Login [PuraVidaStore]    Script Date: 17/02/2022 19:35:02 ******/
CREATE LOGIN [PuraVidaStore] WITH PASSWORD=N'B+IkWPua7MlM53O6SXyaQBzWZsD57msH4GQRJ9D+28s=', DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[Español], CHECK_EXPIRATION=ON, CHECK_POLICY=ON
GO

ALTER LOGIN [PuraVidaStore] DISABLE
GO

ALTER SERVER ROLE [serveradmin] ADD MEMBER [PuraVidaStore]
GO

ALTER SERVER ROLE [setupadmin] ADD MEMBER [PuraVidaStore]
GO

ALTER SERVER ROLE [diskadmin] ADD MEMBER [PuraVidaStore]
GO

ALTER SERVER ROLE [dbcreator] ADD MEMBER [PuraVidaStore]
GO

ALTER SERVER ROLE [bulkadmin] ADD MEMBER [PuraVidaStore]
GO


