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

IF NOT EXISTS (
    SELECT * FROM INFORMATION_SCHEMA.COLUMNS 
    WHERE TABLE_NAME = 'ParametrosGlobales' 
    AND COLUMN_NAME = 'PrgLeyenda'
)
BEGIN
    ALTER TABLE ParametrosGlobales
    ADD PrgLeyenda VARCHAR(800);
END

GO

ALTER PROCEDURE [dbo].[SP_Inventarios]
@IdBodega INT
AS
BEGIN

-- Asegúrate de eliminar la tabla temporal si ya existe
IF OBJECT_ID('tempdb..#TempInventarios') IS NOT NULL
BEGIN
    DROP TABLE #TempInventarios;
END

-- Crear una nueva tabla temporal
CREATE TABLE #TempInventarios (
    IdProducto BIGINT,
    existencia INT
);

-- Insertar datos en la tabla temporal
INSERT INTO #TempInventarios (IdProducto, existencia)
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



UPDATE [dbo].[Productos]
SET PdrVisible = 1, PdrTieneExistencias = 1
WHERE 
	 PrdId IN (SELECT IdProducto FROM #TempInventarios) AND
	(
		PdrTieneExistencias = 0 
		OR PdrTieneExistencias IS NULL
		OR PdrVisible = 0 OR PdrVisible IS NULL
	)


--UPDATE P
--SET 
--    P.PdrVisible = 0, 
--    P.PdrTieneExistencias = 0
--FROM 
--    [dbo].[Productos] P
--INNER JOIN 
--    #TempInventarios TI ON P.PrdId = TI.IdProducto
--WHERE 
--    TI.Existencia = 0


SELECT * FROM #TempInventarios
WHERE existencia <> 0


DROP TABLE #TempInventarios;
END

GO
-- Declarar variable para el SQL dinámico
DECLARE @sql NVARCHAR(MAX) = N'';

-- Construir los GRANT para los procedimientos almacenados del esquema dbo
SELECT @sql += 'IF NOT EXISTS (SELECT * FROM sys.database_permissions WHERE grantee_principal_id = USER_ID(''Tienda'') AND major_id = OBJECT_ID(''[' + ROUTINE_SCHEMA + '].[' + ROUTINE_NAME + ']'') AND permission_name = ''EXECUTE'') '
               + 'BEGIN '
               + 'GRANT EXECUTE ON OBJECT::[' + ROUTINE_SCHEMA + '].[' + ROUTINE_NAME + '] TO Tienda; '
               + 'END; '
FROM INFORMATION_SCHEMA.ROUTINES
WHERE ROUTINE_TYPE = 'PROCEDURE' 
AND ROUTINE_SCHEMA = 'dbo';

-- Ejecutar el SQL dinámico
EXEC sp_executesql @sql;
