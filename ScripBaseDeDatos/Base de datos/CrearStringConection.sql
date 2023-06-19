DECLARE @serverName VARCHAR(50) = 'localhost\\SQLEXPRESS'
DECLARE @databaseName VARCHAR(50) = 'PuraVidaStore'
DECLARE @connectionString VARCHAR(255)

SET @connectionString = 'Data Source=' + @serverName + ';Initial Cataloge=' + @databaseName + ';Trusted_Connection=True;'

SELECT @connectionString AS ConnectionString