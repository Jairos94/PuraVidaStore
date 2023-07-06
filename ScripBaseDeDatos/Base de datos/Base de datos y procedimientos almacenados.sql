CREATE DATABASE PuraVidaStore
GO

USE PuraVidaStore
GO
/****** Object:  Table [dbo].[Bodegas]    Script Date: 28/06/2023 22:25:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bodegas](
	[BdgId] [int] IDENTITY(1,1) NOT NULL,
	[bdgDescripcion] [varchar](30) NOT NULL,
	[bdgVisible] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[BdgId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ClientesMayoristas]    Script Date: 28/06/2023 22:25:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClientesMayoristas](
	[ClmId] [bigint] IDENTITY(1,1) NOT NULL,
	[ClmIdPersona] [bigint] NOT NULL,
	[ClmFechaCreacion] [datetime] NOT NULL,
	[ClmFechaVencimiento] [datetime] NOT NULL,
	[ClmCorreo] [varchar](100) NULL,
	[ClmTelefono] [varchar](12) NULL,
PRIMARY KEY CLUSTERED 
(
	[ClmId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DetalleFactura]    Script Date: 28/06/2023 22:25:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DetalleFactura](
	[DtfId] [bigint] IDENTITY(1,1) NOT NULL,
	[DtfIdProducto] [bigint] NOT NULL,
	[DtfIdFactura] [bigint] NOT NULL,
	[DtfPrecio] [float] NOT NULL,
	[DtfDescuento] [int] NULL,
	[DtfCantidad] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[DtfId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DetalleProductoPedido]    Script Date: 28/06/2023 22:25:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DetalleProductoPedido](
	[DppId] [bigint] IDENTITY(1,1) NOT NULL,
	[DppIdProducto] [bigint] NOT NULL,
	[DppIdPedido] [bigint] NOT NULL,
	[DppIdMoneda] [int] NOT NULL,
	[DppPesoUnitario] [float] NULL,
	[DppValorMoneda] [float] NULL,
	[DppCostoMoneda] [float] NULL,
	[DppCostoColones] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[DppId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EstadoPedido]    Script Date: 28/06/2023 22:25:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EstadoPedido](
	[EtpId] [int] IDENTITY(1,1) NOT NULL,
	[EtpDescripcion] [varchar](30) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[EtpId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EstatusFactura]    Script Date: 28/06/2023 22:25:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EstatusFactura](
	[EtfId] [int] IDENTITY(1,1) NOT NULL,
	[EsfDescripcion] [varchar](10) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[EtfId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Factura]    Script Date: 28/06/2023 22:25:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Factura](
	[FtrId] [bigint] IDENTITY(1,1) NOT NULL,
	[FtrFecha] [datetime] NOT NULL,
	[FtrIdUsuario] [int] NOT NULL,
	[FtrMayorista] [bigint] NULL,
	[FtrEstatusId] [int] NOT NULL,
	[FtrBodega] [int] NOT NULL,
	[FtrFormaPago] [int] NOT NULL,
	[FtrEsFacturaNula] [bit] NULL,
	[FtrCodigoFactura] [nvarchar](350) NULL,
PRIMARY KEY CLUSTERED 
(
	[FtrId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FacturaResumen]    Script Date: 28/06/2023 22:25:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FacturaResumen](
	[FtrId] [bigint] IDENTITY(1,1) NOT NULL,
	[FtrFactura] [bigint] NOT NULL,
	[FtrMontoTotal] [float] NOT NULL,
	[FtrMontoPagado] [float] NULL,
	[FtrCambio] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[FtrId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FormaPago]    Script Date: 28/06/2023 22:25:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FormaPago](
	[FrpId] [int] IDENTITY(1,1) NOT NULL,
	[FrpDescripcion] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[FrpId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HistorialClienteMayorista]    Script Date: 28/06/2023 22:25:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HistorialClienteMayorista](
	[HcmId] [bigint] IDENTITY(1,1) NOT NULL,
	[HcmIdCliente] [bigint] NOT NULL,
	[HcmFechaVencimiento] [datetime] NOT NULL,
	[HcmFechaActualizacion] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[HcmId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HistorialFacturasAnuladas]    Script Date: 28/06/2023 22:25:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HistorialFacturasAnuladas](
	[HlfId] [bigint] IDENTITY(1,1) NOT NULL,
	[HlfIdUsuario] [int] NOT NULL,
	[HlfIdFctura] [bigint] NOT NULL,
	[HlfRazon] [varchar](250) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[HlfId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HistorialPrecios]    Script Date: 28/06/2023 22:25:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HistorialPrecios](
	[HlpId] [bigint] IDENTITY(1,1) NOT NULL,
	[HlpIdProducto] [bigint] NOT NULL,
	[HlpFecha] [datetime] NOT NULL,
	[HlpIdUsuario] [int] NOT NULL,
	[HlpPrecioMayorista] [float] NULL,
	[HlpPrecioMinorista] [float] NULL,
	[HlpPrecioMayoristaAnterior] [float] NULL,
	[HlpPrecioMinoristaAnterior] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[HlpId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Impuestos]    Script Date: 28/06/2023 22:25:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Impuestos](
	[ImpId] [int] IDENTITY(1,1) NOT NULL,
	[ImpDescripcion] [varchar](100) NULL,
	[ImpPorcentaje] [float] NULL,
	[ImpActivo] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[ImpId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ImpuestosPorFactura]    Script Date: 28/06/2023 22:25:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ImpuestosPorFactura](
	[IpfId] [bigint] NOT NULL,
	[IpfIdFactura] [bigint] NOT NULL,
	[IpfIdImpuesto] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IpfId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ImpuestosPorParametro]    Script Date: 28/06/2023 22:25:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ImpuestosPorParametro](
	[ImpPId] [int] IDENTITY(1,1) NOT NULL,
	[ImpPIdParametroGlobal] [int] NOT NULL,
	[ImpPIdImpuesto] [int] NOT NULL,
 CONSTRAINT [PK__Impuesto__D0483333AC4F9899] PRIMARY KEY CLUSTERED 
(
	[ImpPId] ASC,
	[ImpPIdParametroGlobal] ASC,
	[ImpPIdImpuesto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Moneda]    Script Date: 28/06/2023 22:25:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Moneda](
	[MndId] [int] IDENTITY(1,1) NOT NULL,
	[MndCodigo] [varchar](10) NOT NULL,
	[MndDescripcion] [varchar](30) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MndId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MotivosMovimientos]    Script Date: 28/06/2023 22:25:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MotivosMovimientos](
	[MtmId] [int] IDENTITY(1,1) NOT NULL,
	[MtmDescripcion] [varchar](250) NULL,
	[MtmIdTipoMovimiento] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MtmId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Movimientos]    Script Date: 28/06/2023 22:25:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Movimientos](
	[MvmId] [bigint] IDENTITY(1,1) NOT NULL,
	[MvmIdProducto] [bigint] NOT NULL,
	[MvmCantidad] [int] NOT NULL,
	[MvmFecha] [datetime] NOT NULL,
	[MvmIdMotivoMovimiento] [int] NOT NULL,
	[MvmIdUsuario] [int] NOT NULL,
	[MvmIdBodega] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MvmId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OtrosCargos]    Script Date: 28/06/2023 22:25:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OtrosCargos](
	[OtrId] [int] IDENTITY(1,1) NOT NULL,
	[OtrIdMoneda] [int] NOT NULL,
	[OtrIdPedido] [bigint] NOT NULL,
	[OtrValorMoneda] [float] NULL,
	[OtrCostoMoneda] [float] NULL,
	[OtrRazon] [text] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[OtrId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ParametrosEmail]    Script Date: 28/06/2023 22:25:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ParametrosEmail](
	[PreId] [int] IDENTITY(1,1) NOT NULL,
	[PreHost] [varchar](250) NOT NULL,
	[PrePuerto] [int] NOT NULL,
	[preUser] [varchar](250) NOT NULL,
	[PreClave] [varchar](250) NOT NULL,
	[PreSsl] [bit] NOT NULL,
	[PreIdParametroGlobal] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[PreId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ParametrosGlobales]    Script Date: 28/06/2023 22:25:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ParametrosGlobales](
	[PrgId] [int] IDENTITY(1,1) NOT NULL,
	[PrgUndsHabilitarMayorista] [int] NOT NULL,
	[PrgUndsAgregarMayorista] [int] NOT NULL,
	[PrgHabilitarImpuestos] [bit] NOT NULL,
	[PrgImpustosIncluidos] [bit] NOT NULL,
	[PrgIdBodega] [int] NOT NULL,
	[PrgIdTiempo] [int] NULL,
	[PrgCantidadTiempo] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[PrgId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pedido]    Script Date: 28/06/2023 22:25:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pedido](
	[PddId] [bigint] IDENTITY(1,1) NOT NULL,
	[PddFecha] [datetime] NOT NULL,
	[PddIdUsuario] [int] NOT NULL,
	[PddRazonCancelada] [text] NULL,
	[PddProveedor] [bigint] NOT NULL,
	[PddEstado] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PddId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Persona]    Script Date: 28/06/2023 22:25:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Persona](
	[PsrId] [bigint] IDENTITY(1,1) NOT NULL,
	[PsrIdentificacion] [varchar](30) NOT NULL,
	[PsrNombre] [varchar](50) NOT NULL,
	[PsrApellido1] [varchar](50) NOT NULL,
	[PsrApellido2] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PsrId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Productos]    Script Date: 28/06/2023 22:25:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Productos](
	[PrdId] [bigint] IDENTITY(1,1) NOT NULL,
	[PrdNombre] [varchar](50) NOT NULL,
	[PrdPrecioVentaMayorista] [float] NOT NULL,
	[PrdPrecioVentaMinorista] [float] NOT NULL,
	[PrdCodigo] [varchar](100) NULL,
	[PrdUnidadesMinimas] [int] NULL,
	[PrdIdTipoProducto] [int] NOT NULL,
	[PrdCodigoProvedor] [varchar](50) NULL,
	[PdrVisible] [bit] NULL,
	[PdrFoto] [varchar](max) NULL,
	[PdrTieneExistencias] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[PrdId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Proveedores]    Script Date: 28/06/2023 22:25:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Proveedores](
	[PvdId] [bigint] IDENTITY(1,1) NOT NULL,
	[PvdProveedorNmbre] [varchar](100) NOT NULL,
	[PvdProveedorCorreo] [varchar](100) NULL,
	[PvdProveedorNumeroTelefono] [varchar](30) NULL,
	[PvdCodigoPais] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[PvdId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RolUsiario]    Script Date: 28/06/2023 22:25:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RolUsiario](
	[RluID] [int] IDENTITY(1,1) NOT NULL,
	[RluDescripcion] [varchar](16) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[RluID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TiempoParaRenovar]    Script Date: 28/06/2023 22:25:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TiempoParaRenovar](
	[TrpId] [int] IDENTITY(1,1) NOT NULL,
	[TrrDescricpcion] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[TrpId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TipoMovimiento]    Script Date: 28/06/2023 22:25:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoMovimiento](
	[TpmId] [int] IDENTITY(1,1) NOT NULL,
	[TpmDescripcion] [varchar](15) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[TpmId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TipoProducto]    Script Date: 28/06/2023 22:25:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoProducto](
	[TppId] [int] IDENTITY(1,1) NOT NULL,
	[TppDescripcion] [varchar](50) NOT NULL,
	[TppVisible] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[TppId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Trackings]    Script Date: 28/06/2023 22:25:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Trackings](
	[TrkId] [bigint] IDENTITY(1,1) NOT NULL,
	[TrkFecha] [datetime] NOT NULL,
	[TrKTrackin] [varchar](300) NOT NULL,
	[TrkMoneda] [int] NOT NULL,
	[TrkCostoMoneda] [float] NULL,
	[TrkValorMoneda] [float] NULL,
	[TrkIdPedido] [bigint] NULL,
	[TrkPesoProveedor] [float] NULL,
	[TrkPesoReal] [float] NULL,
	[TrkMedidaLargoCm] [float] NULL,
	[TrkMedidaAnchoCm] [float] NULL,
	[TrkMedidaAlturaCm] [float] NULL,
	[TrkEstado] [int] NOT NULL,
	[TrkProveedor] [bigint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[TrkId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TrackingsAsociados]    Script Date: 28/06/2023 22:25:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TrackingsAsociados](
	[TraId] [bigint] IDENTITY(1,1) NOT NULL,
	[TraIdTrackin] [bigint] NOT NULL,
	[TraIdTrackinPrincial] [bigint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[TraId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UsuaiosEnvioCorreos]    Script Date: 28/06/2023 22:25:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsuaiosEnvioCorreos](
	[UecId] [int] IDENTITY(1,1) NOT NULL,
	[UecIdUsuario] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UecId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 28/06/2023 22:25:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[UsrID] [int] IDENTITY(1,1) NOT NULL,
	[UsrUser] [varchar](16) NOT NULL,
	[UsrPass] [varbinary](8000) NOT NULL,
	[UsrEmail] [varchar](100) NULL,
	[UsrIdRol] [int] NOT NULL,
	[UsrIdPersona] [bigint] NOT NULL,
	[UsrActivo] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[UsrID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ClientesMayoristas]  WITH CHECK ADD FOREIGN KEY([ClmIdPersona])
REFERENCES [dbo].[Persona] ([PsrId])
GO
ALTER TABLE [dbo].[DetalleFactura]  WITH CHECK ADD FOREIGN KEY([DtfIdProducto])
REFERENCES [dbo].[Productos] ([PrdId])
GO
ALTER TABLE [dbo].[DetalleFactura]  WITH CHECK ADD FOREIGN KEY([DtfIdProducto])
REFERENCES [dbo].[Factura] ([FtrId])
GO
ALTER TABLE [dbo].[DetalleProductoPedido]  WITH CHECK ADD FOREIGN KEY([DppIdProducto])
REFERENCES [dbo].[Productos] ([PrdId])
GO
ALTER TABLE [dbo].[DetalleProductoPedido]  WITH CHECK ADD FOREIGN KEY([DppIdPedido])
REFERENCES [dbo].[Pedido] ([PddId])
GO
ALTER TABLE [dbo].[DetalleProductoPedido]  WITH CHECK ADD FOREIGN KEY([DppIdMoneda])
REFERENCES [dbo].[Moneda] ([MndId])
GO
ALTER TABLE [dbo].[Factura]  WITH CHECK ADD FOREIGN KEY([FtrBodega])
REFERENCES [dbo].[Bodegas] ([BdgId])
GO
ALTER TABLE [dbo].[Factura]  WITH CHECK ADD FOREIGN KEY([FtrEstatusId])
REFERENCES [dbo].[EstatusFactura] ([EtfId])
GO
ALTER TABLE [dbo].[Factura]  WITH CHECK ADD FOREIGN KEY([FtrFormaPago])
REFERENCES [dbo].[FormaPago] ([FrpId])
GO
ALTER TABLE [dbo].[Factura]  WITH CHECK ADD FOREIGN KEY([FtrIdUsuario])
REFERENCES [dbo].[Usuarios] ([UsrID])
GO
ALTER TABLE [dbo].[Factura]  WITH CHECK ADD FOREIGN KEY([FtrMayorista])
REFERENCES [dbo].[ClientesMayoristas] ([ClmId])
GO
ALTER TABLE [dbo].[FacturaResumen]  WITH CHECK ADD FOREIGN KEY([FtrFactura])
REFERENCES [dbo].[Factura] ([FtrId])
GO
ALTER TABLE [dbo].[HistorialClienteMayorista]  WITH CHECK ADD FOREIGN KEY([HcmIdCliente])
REFERENCES [dbo].[ClientesMayoristas] ([ClmId])
GO
ALTER TABLE [dbo].[HistorialFacturasAnuladas]  WITH CHECK ADD FOREIGN KEY([HlfIdUsuario])
REFERENCES [dbo].[Usuarios] ([UsrID])
GO
ALTER TABLE [dbo].[HistorialFacturasAnuladas]  WITH CHECK ADD FOREIGN KEY([HlfIdFctura])
REFERENCES [dbo].[Factura] ([FtrId])
GO
ALTER TABLE [dbo].[HistorialPrecios]  WITH CHECK ADD FOREIGN KEY([HlpIdProducto])
REFERENCES [dbo].[Productos] ([PrdId])
GO
ALTER TABLE [dbo].[HistorialPrecios]  WITH CHECK ADD FOREIGN KEY([HlpIdUsuario])
REFERENCES [dbo].[Usuarios] ([UsrID])
GO
ALTER TABLE [dbo].[ImpuestosPorFactura]  WITH CHECK ADD FOREIGN KEY([IpfIdFactura])
REFERENCES [dbo].[Factura] ([FtrId])
GO
ALTER TABLE [dbo].[ImpuestosPorFactura]  WITH CHECK ADD FOREIGN KEY([IpfIdImpuesto])
REFERENCES [dbo].[Impuestos] ([ImpId])
GO
ALTER TABLE [dbo].[ImpuestosPorParametro]  WITH CHECK ADD FOREIGN KEY([ImpPIdParametroGlobal])
REFERENCES [dbo].[ParametrosGlobales] ([PrgId])
GO
ALTER TABLE [dbo].[ImpuestosPorParametro]  WITH CHECK ADD FOREIGN KEY([ImpPIdImpuesto])
REFERENCES [dbo].[Impuestos] ([ImpId])
GO
ALTER TABLE [dbo].[MotivosMovimientos]  WITH CHECK ADD FOREIGN KEY([MtmIdTipoMovimiento])
REFERENCES [dbo].[TipoMovimiento] ([TpmId])
GO
ALTER TABLE [dbo].[Movimientos]  WITH CHECK ADD FOREIGN KEY([MvmIdUsuario])
REFERENCES [dbo].[Usuarios] ([UsrID])
GO
ALTER TABLE [dbo].[Movimientos]  WITH CHECK ADD FOREIGN KEY([MvmIdBodega])
REFERENCES [dbo].[Bodegas] ([BdgId])
GO
ALTER TABLE [dbo].[Movimientos]  WITH CHECK ADD FOREIGN KEY([MvmIdProducto])
REFERENCES [dbo].[Productos] ([PrdId])
GO
ALTER TABLE [dbo].[Movimientos]  WITH CHECK ADD FOREIGN KEY([MvmIdMotivoMovimiento])
REFERENCES [dbo].[MotivosMovimientos] ([MtmId])
GO
ALTER TABLE [dbo].[OtrosCargos]  WITH CHECK ADD FOREIGN KEY([OtrIdMoneda])
REFERENCES [dbo].[Moneda] ([MndId])
GO
ALTER TABLE [dbo].[OtrosCargos]  WITH CHECK ADD FOREIGN KEY([OtrIdPedido])
REFERENCES [dbo].[Pedido] ([PddId])
GO
ALTER TABLE [dbo].[ParametrosEmail]  WITH CHECK ADD FOREIGN KEY([PreId])
REFERENCES [dbo].[ParametrosGlobales] ([PrgId])
GO
ALTER TABLE [dbo].[ParametrosGlobales]  WITH CHECK ADD FOREIGN KEY([PrgIdTiempo])
REFERENCES [dbo].[TiempoParaRenovar] ([TrpId])
GO
ALTER TABLE [dbo].[ParametrosGlobales]  WITH CHECK ADD FOREIGN KEY([PrgIdBodega])
REFERENCES [dbo].[Bodegas] ([BdgId])
GO
ALTER TABLE [dbo].[Pedido]  WITH CHECK ADD FOREIGN KEY([PddEstado])
REFERENCES [dbo].[EstadoPedido] ([EtpId])
GO
ALTER TABLE [dbo].[Pedido]  WITH CHECK ADD FOREIGN KEY([PddIdUsuario])
REFERENCES [dbo].[Usuarios] ([UsrID])
GO
ALTER TABLE [dbo].[Pedido]  WITH CHECK ADD FOREIGN KEY([PddProveedor])
REFERENCES [dbo].[Proveedores] ([PvdId])
GO
ALTER TABLE [dbo].[Productos]  WITH CHECK ADD FOREIGN KEY([PrdIdTipoProducto])
REFERENCES [dbo].[TipoProducto] ([TppId])
GO
ALTER TABLE [dbo].[Trackings]  WITH CHECK ADD FOREIGN KEY([TrkEstado])
REFERENCES [dbo].[EstadoPedido] ([EtpId])
GO
ALTER TABLE [dbo].[Trackings]  WITH CHECK ADD FOREIGN KEY([TrkIdPedido])
REFERENCES [dbo].[Pedido] ([PddId])
GO
ALTER TABLE [dbo].[Trackings]  WITH CHECK ADD FOREIGN KEY([TrkMoneda])
REFERENCES [dbo].[Moneda] ([MndId])
GO
ALTER TABLE [dbo].[Trackings]  WITH CHECK ADD FOREIGN KEY([TrkProveedor])
REFERENCES [dbo].[Proveedores] ([PvdId])
GO
ALTER TABLE [dbo].[TrackingsAsociados]  WITH CHECK ADD FOREIGN KEY([TraIdTrackin])
REFERENCES [dbo].[Trackings] ([TrkId])
GO
ALTER TABLE [dbo].[UsuaiosEnvioCorreos]  WITH CHECK ADD FOREIGN KEY([UecId])
REFERENCES [dbo].[Usuarios] ([UsrID])
GO
ALTER TABLE [dbo].[Usuarios]  WITH CHECK ADD FOREIGN KEY([UsrIdPersona])
REFERENCES [dbo].[Persona] ([PsrId])
GO
ALTER TABLE [dbo].[Usuarios]  WITH CHECK ADD FOREIGN KEY([UsrIdRol])
REFERENCES [dbo].[RolUsiario] ([RluID])
GO
/****** Object:  StoredProcedure [dbo].[EditarUsuario]    Script Date: 28/06/2023 22:25:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[EditarUsuario]

	@UsrUser varchar(16),
	@UsrPass varchar(256),
	@Email  varchar(100) =null,
	@Rol int,
	@idPersona bigint,
	@idUsuario int,
	@activo bit
	


AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
		UPDATE [dbo].[Usuarios]
	   SET [UsrUser] = @UsrUser
		  ,[UsrPass] = EncryptByPassPhrase('password',@UsrPass)
		  ,[UsrEmail] = @Email
		  ,[UsrIdRol] = @Rol
		  ,[UsrIdPersona] = @idPersona
		  ,[UsrActivo] = @activo
	 WHERE [UsrID] = @idUsuario
END
GO
/****** Object:  StoredProcedure [dbo].[GetUsuario]    Script Date: 28/06/2023 22:25:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[GetUsuario]
	@Usuario varchar(50),
	@Pass varchar(300)
AS

BEGIN

Declare 
@User varchar(50),
@Pass1 varchar (250),
@Activo bit
SELECT @User = UsrUser,@Pass1=UsrPass, @Activo=UsrActivo FROM Usuarios Where UsrUser=@Usuario

	IF @User = @Usuario and @Activo =1
	
			IF DecryptByPassPhrase('password',@Pass1) = @Pass
			SELECT 
		   u.UsrID
		  ,u.UsrUser
		  ,u.UsrEmail
		  ,u.UsrIdRol
		  ,u.UsrActivo
		  ,p.*
		  ,r.*
		  ,u.UsrIdPersona FROM Usuarios u 
		  inner join Persona p on u.UsrIdPersona =p.PsrId
		  inner join RolUsiario r on u.UsrIdRol=r.RluID

		  WHERE u.UsrUser =@Usuario 
			else 
			begin
	
				SELECT 'La contraseña ingresada no coencide'
		end
	ELSE 
		Begin
		
			SELECT 'El usuario ingresado no existe'
		END
	END

GO
/****** Object:  StoredProcedure [dbo].[IngresarUsuario]    Script Date: 28/06/2023 22:25:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  CREATE PROCEDURE [dbo].[IngresarUsuario]
  @Usuario varchar(15),
  @Pass VARCHAR(256),
  @Email Varchar(100),
  @IdRol int,
  @IdPersona bigint
  AS
 

  INSERT INTO [dbo].[Usuarios]
           ([UsrUser]
           ,[UsrPass]
           ,[UsrEmail]
           ,[UsrIdRol]
           ,[UsrIdPersona],
		   [UsrActivo])
     VALUES
           (@Usuario
           ,EncryptByPassPhrase('password',@Pass )
           ,@Email
           ,@IdRol
           ,@IdPersona
		   ,1
		   )
GO
/****** Object:  StoredProcedure [dbo].[ObtenerUsuarios]    Script Date: 28/06/2023 22:25:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ObtenerUsuarios]
AS
SELECT U.UsrID,U.UsrUser,U.UsrEmail,U.UsrIdRol,U.UsrIdPersona,U.UsrActivo,R.*,P.* FROM Usuarios U
INNER JOIN Persona P on U.UsrIdPersona=P.PsrId
INNER JOIN RolUsiario R ON U.UsrIdRol= R.RluID
Where U.UsrActivo=1
GO
/****** Object:  StoredProcedure [dbo].[ocpv]    Script Date: 28/06/2023 22:25:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[ocpv]
@IdUsuario int
as
Begin
	SET NOCOUNT ON;
	Select CONVERT(varchar,DecryptByPassPhrase('password',U.UsrPass))  from Usuarios U where U.UsrID=@IdUsuario
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Inventarios]    Script Date: 28/06/2023 22:25:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_Inventarios]
@IdBodega INT
AS
BEGIN


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
WHERE F.FtrEsFacturaNula = 0 and DF.DtfIdProducto = MPrincipal.MvmIdProducto
),0)

AS existencia
FROM [dbo].[Movimientos] MPrincipal
INNER JOIN [dbo].[MotivosMovimientos] MMPrincipal on MPrincipal.MvmIdMotivoMovimiento=MMPrincipal.MtmId
WHERE MMPrincipal.MtmIdTipoMovimiento=1 AND MPrincipal.MvmIdBodega=@IdBodega
GROUP BY  MPrincipal.MvmIdProducto,MMPrincipal.MtmIdTipoMovimiento


END
GO
