USE [PuraVidaStore];
GO

IF NOT EXISTS (
    SELECT * FROM INFORMATION_SCHEMA.COLUMNS 
    WHERE TABLE_NAME = 'ParametrosGlobales' 
    AND COLUMN_NAME = 'PrgImpresora'
)
BEGIN
    ALTER TABLE ParametrosGlobales
    ADD PrgImpresora VARCHAR(100);
END

IF NOT EXISTS (
    SELECT * FROM INFORMATION_SCHEMA.COLUMNS 
    WHERE TABLE_NAME = 'ParametrosGlobales' 
    AND COLUMN_NAME = 'PrgNombreNegocio'
)
BEGIN
    ALTER TABLE ParametrosGlobales
    ADD PrgNombreNegocio VARCHAR(100);
END

IF NOT EXISTS (
    SELECT * FROM INFORMATION_SCHEMA.COLUMNS 
    WHERE TABLE_NAME = 'ParametrosGlobales' 
    AND COLUMN_NAME = 'PrgCedula'
)
BEGIN
    ALTER TABLE ParametrosGlobales
    ADD PrgCedula VARCHAR(50);
END
