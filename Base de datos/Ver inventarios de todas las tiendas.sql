DROP TABLE IF EXISTS #InventarioTotal;

CREATE TABLE #InventarioTotal (
    IdProducto BIGINT,
    existencia INT,
    IdBodega INT null
);

DECLARE @IdBodegaRecorrido INT;

DECLARE cursorInventarios CURSOR FOR
SELECT BdgId FROM [dbo].[Bodegas]; -- Solo seleccionamos IdBodega

OPEN cursorInventarios;

-- Obtener la primera fila
FETCH NEXT FROM cursorInventarios INTO @IdBodegaRecorrido;

-- Iterar mientras haya filas
WHILE @@FETCH_STATUS = 0
BEGIN
INSERT INTO #InventarioTotal (IdProducto, existencia, IdBodega)
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
	MNegativo.MvmIdBodega=@IdBodegaRecorrido)

	--se resta facturas no nulas
	-ISNULL((
	select SUM(DF.DtfCantidad) from DetalleFactura DF 
	INNER JOIN Factura F ON F.FtrId = DF.DtfIdFactura
	WHERE F.FtrEsFacturaNula = 0 and DF.DtfIdProducto = MPrincipal.MvmIdProducto AND F.FtrBodega =@IdBodegaRecorrido
	),0)

	AS existencia,
	@IdBodegaRecorrido as 'IdBodega'
	FROM [dbo].[Movimientos] MPrincipal
	INNER JOIN [dbo].[MotivosMovimientos] MMPrincipal on MPrincipal.MvmIdMotivoMovimiento=MMPrincipal.MtmId
	WHERE MMPrincipal.MtmIdTipoMovimiento=1 AND MPrincipal.MvmIdBodega=@IdBodegaRecorrido
	GROUP BY  MPrincipal.MvmIdProducto,MMPrincipal.MtmIdTipoMovimiento

	FETCH NEXT FROM cursorInventarios INTO @IdBodegaRecorrido;
END

CLOSE cursorInventarios;
DEALLOCATE cursorInventarios;



-- Aquí puedes consultar los datos insertados


SELECT 
    P.PrdCodigo,
    P.PrdNombre,
    TP.TppDescripcion,
    B.bdgDescripcion,
    SUM(I.existencia) AS TotalExistencia
FROM [dbo].[Productos] P
LEFT JOIN #InventarioTotal I ON P.PrdId = I.IdProducto  -- Asegúrate de que esta relación es correcta
INNER JOIN TipoProducto TP ON P.PrdIdTipoProducto = TP.TppId
INNER JOIN Bodegas B ON I.IdBodega = B.BdgId
GROUP BY 
    P.PrdCodigo,
    P.PrdNombre,
    TP.TppDescripcion,
    B.bdgDescripcion


DROP TABLE #InventarioTotal
