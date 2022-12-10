
	Select 
	--ID PODUCTO
	P.PrdId AS IdProducto,
	--Existencias
	(SELECT SUM( M.MvmCantidad )-

	--MAN-> MOVIMIENTO AJUSTE NEGATIVO
	--MMAN-> MOTIVOS MOVIMIENTO AJUSTE NEGATIVO
	 ISNULL((select SUM(MvmCantidad) from Movimientos MAN inner join  MotivosMovimientos MMAN ON MAN.MvmIdMotivoMovimiento = MMAN.MtmId WHERE MMAN.MtmIdTipoMovimiento=2 AND MAN.MvmIdProducto = P.PrdId),0)+
	 ISNULL((SELECT SUM(DF.DtfCantidad) FROM DetalleFactura DF INNER JOIN Factura F ON F.FtrId=DF.DtfIdFactura WHERE DF.DtfIdProducto = P.PrdId AND F.FtrEsFacturaNula = 0),0) WHERE MM.MtmIdTipoMovimiento = 1 AND M.MvmIdProducto= P.PrdId) 

	AS existencia

	from Productos P

	INNER JOIN Movimientos M ON M.MvmIdProducto=P.PrdId
	INNER JOIN MotivosMovimientos MM ON M.MvmIdMotivoMovimiento = MM.MtmId

	WHERE M.MvmIdBodega = 1 and MM.MtmIdTipoMovimiento=1
	GROUP BY P.PrdId,MM.MtmIdTipoMovimiento,M.MvmIdProducto

	--select * from MotivosMovimientos 
	--where MtmIdTipoMovimiento = 2 