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

GO
ALTER PROCEDURE [dbo].[SP_Inventarios]
@IdBodega INT
AS
BEGIN

UPDATE [dbo].[Productos]
SET PdrVisible = 1, PdrTieneExistencias = 1
WHERE PdrTieneExistencias = 0 OR PdrTieneExistencias IS NULL
	OR PdrVisible = 0 OR PdrVisible IS NULL;


SELECT MPrincipal.MvmIdProducto AS IdProducto,
--Suma todos los valores con 1 son los positivos
ISNULL(SUM([MvmCantidad]),0)

--Se resta todos los ajustes negativos
- 
(SELECT ISNULL(SUM([MvmCantidad]),0) 
FROM [dbo].[Movimientos] MNegativo
INNER JOIN [dbo].[MotivosMovimientos] MMNegativo on MNegativo.MvmIdMotivoMovimiento=MMNegativo.MtmId
WHERE MMNegativo.MtmIdTipoMovimiento=2 AND 
MNegativo.MvmIdProducto=MPrincipal.MvmIdProducto AND  
MNegativo.MvmIdBodega=@IdBodega)

--se resta facturas no nulas
-ISNULL((
select SUM(DF.DtfCantidad) from DetalleFactura DF 
INNER JOIN Factura F ON F.FtrId = DF.DtfIdFactura
WHERE F.FtrEsFacturaNula = 0 and DF.DtfIdProducto = MPrincipal.MvmIdProducto AND F.FtrBodega =@IdBodega
),0)

AS existencia
FROM [dbo].[Movimientos] MPrincipal
INNER JOIN [dbo].[MotivosMovimientos] MMPrincipal on MPrincipal.MvmIdMotivoMovimiento=MMPrincipal.MtmId
WHERE MMPrincipal.MtmIdTipoMovimiento=1 AND MPrincipal.MvmIdBodega=@IdBodega
GROUP BY  MPrincipal.MvmIdProducto,MMPrincipal.MtmIdTipoMovimiento


END