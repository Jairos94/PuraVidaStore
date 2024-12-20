CREATE DATABASE PuraVidaStore
GO

USE PuraVidaStore
GO
/****** Object:  UserDefinedTableType [dbo].[ImpuestosPorFactura]    Script Date: 28/9/2024 16:31:58 ******/
CREATE TYPE [dbo].[ImpuestosPorFactura] AS TABLE(
	[IdImpuesto] [int] NOT NULL,
	[Porcentaje] [decimal](18, 2) NOT NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[InDetalleFactura]    Script Date: 28/9/2024 16:31:58 ******/
CREATE TYPE [dbo].[InDetalleFactura] AS TABLE(
	[IdProducto] [bigint] NOT NULL,
	[Linea] [int] NULL,
	[Precio] [decimal](18, 2) NOT NULL,
	[MontoImpuesto] [decimal](18, 2) NOT NULL,
	[MontoDescuento] [decimal](18, 2) NULL,
	[Cantidad] [int] NOT NULL
)
GO
/****** Object:  Table [dbo].[Bodegas]    Script Date: 28/9/2024 16:31:58 ******/
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
/****** Object:  Table [dbo].[ClientesMayoristas]    Script Date: 28/9/2024 16:31:58 ******/
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
/****** Object:  Table [dbo].[DetalleFactura]    Script Date: 28/9/2024 16:31:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DetalleFactura](
	[DtfId] [bigint] IDENTITY(1,1) NOT NULL,
	[DtfIdProducto] [bigint] NOT NULL,
	[DtfIdFactura] [bigint] NOT NULL,
	[DtfLinea] [int] NULL,
	[DtfPrecio] [decimal](18, 2) NOT NULL,
	[DtfMontoImpuestos] [decimal](18, 2) NOT NULL,
	[DtfDescuento] [int] NULL,
	[DtfCantidad] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[DtfId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DetalleProductoPedido]    Script Date: 28/9/2024 16:31:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DetalleProductoPedido](
	[DppId] [bigint] IDENTITY(1,1) NOT NULL,
	[DppIdProducto] [bigint] NOT NULL,
	[DppIdPedido] [bigint] NOT NULL,
	[DppIdMoneda] [int] NOT NULL,
	[DppPesoUnitario] [decimal](18, 2) NULL,
	[DppValorMoneda] [decimal](18, 2) NULL,
	[DppCostoMoneda] [decimal](18, 2) NULL,
	[DppCostoColones] [decimal](18, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[DppId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EstadoPedido]    Script Date: 28/9/2024 16:31:58 ******/
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
/****** Object:  Table [dbo].[EstatusFactura]    Script Date: 28/9/2024 16:31:58 ******/
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
/****** Object:  Table [dbo].[Factura]    Script Date: 28/9/2024 16:31:58 ******/
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
	[FtrCorreoEnvio] [varchar](250) NULL,
PRIMARY KEY CLUSTERED 
(
	[FtrId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FacturaResumen]    Script Date: 28/9/2024 16:31:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FacturaResumen](
	[FtrId] [bigint] IDENTITY(1,1) NOT NULL,
	[FtrFactura] [bigint] NOT NULL,
	[FtrMontoTotal] [decimal](18, 2) NOT NULL,
	[FtrMontoImpuestos] [decimal](18, 2) NOT NULL,
	[FtrMontoPagado] [decimal](18, 2) NULL,
	[FtrCambio] [decimal](18, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[FtrId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FormaPago]    Script Date: 28/9/2024 16:31:58 ******/
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
/****** Object:  Table [dbo].[HistorialClienteMayorista]    Script Date: 28/9/2024 16:31:58 ******/
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
/****** Object:  Table [dbo].[HistorialFacturasAnuladas]    Script Date: 28/9/2024 16:31:58 ******/
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
/****** Object:  Table [dbo].[HistorialPrecios]    Script Date: 28/9/2024 16:31:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HistorialPrecios](
	[HlpId] [bigint] IDENTITY(1,1) NOT NULL,
	[HlpIdProducto] [bigint] NOT NULL,
	[HlpFecha] [datetime] NOT NULL,
	[HlpIdUsuario] [int] NOT NULL,
	[HlpPrecioMayorista] [decimal](18, 2) NULL,
	[HlpPrecioMinorista] [decimal](18, 2) NULL,
	[HlpPrecioMayoristaAnterior] [decimal](18, 2) NULL,
	[HlpPrecioMinoristaAnterior] [decimal](18, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[HlpId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Impuestos]    Script Date: 28/9/2024 16:31:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Impuestos](
	[ImpId] [int] IDENTITY(1,1) NOT NULL,
	[ImpDescripcion] [varchar](100) NULL,
	[ImpPorcentaje] [decimal](18, 2) NULL,
	[ImpActivo] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[ImpId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ImpuestosPorFactura]    Script Date: 28/9/2024 16:31:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ImpuestosPorFactura](
	[IpfId] [bigint] IDENTITY(1,1) NOT NULL,
	[IpfIdFactura] [bigint] NOT NULL,
	[IpfIdImpuesto] [int] NOT NULL,
	[IpfPorcentaje] [decimal](18, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[IpfId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ImpuestosPorParametro]    Script Date: 28/9/2024 16:31:58 ******/
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
/****** Object:  Table [dbo].[Moneda]    Script Date: 28/9/2024 16:31:58 ******/
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
/****** Object:  Table [dbo].[MotivosMovimientos]    Script Date: 28/9/2024 16:31:58 ******/
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
/****** Object:  Table [dbo].[Movimientos]    Script Date: 28/9/2024 16:31:58 ******/
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
/****** Object:  Table [dbo].[OtrosCargos]    Script Date: 28/9/2024 16:31:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OtrosCargos](
	[OtrId] [int] IDENTITY(1,1) NOT NULL,
	[OtrIdMoneda] [int] NOT NULL,
	[OtrIdPedido] [bigint] NOT NULL,
	[OtrValorMoneda] [decimal](18, 2) NULL,
	[OtrCostoMoneda] [decimal](18, 2) NULL,
	[OtrRazon] [text] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[OtrId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ParametrosEmail]    Script Date: 28/9/2024 16:31:58 ******/
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
/****** Object:  Table [dbo].[ParametrosGlobales]    Script Date: 28/9/2024 16:31:58 ******/
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
	[PrgImpresora] [varchar](100) NULL,
	[PrgNombreNegocio] [varchar](100) NULL,
	[PrgCedula] [varchar](50) NULL,
	[PrgLeyenda] [varchar](800) NULL,
PRIMARY KEY CLUSTERED 
(
	[PrgId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pedido]    Script Date: 28/9/2024 16:31:58 ******/
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
/****** Object:  Table [dbo].[Persona]    Script Date: 28/9/2024 16:31:58 ******/
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
/****** Object:  Table [dbo].[Productos]    Script Date: 28/9/2024 16:31:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Productos](
	[PrdId] [bigint] IDENTITY(1,1) NOT NULL,
	[PrdNombre] [varchar](50) NOT NULL,
	[PrdPrecioVentaMayorista] [decimal](18, 2) NOT NULL,
	[PrdPrecioVentaMinorista] [decimal](18, 2) NOT NULL,
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
/****** Object:  Table [dbo].[Proveedores]    Script Date: 28/9/2024 16:31:58 ******/
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
/****** Object:  Table [dbo].[RolUsiario]    Script Date: 28/9/2024 16:31:58 ******/
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
/****** Object:  Table [dbo].[TiempoParaRenovar]    Script Date: 28/9/2024 16:31:58 ******/
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
/****** Object:  Table [dbo].[TipoMovimiento]    Script Date: 28/9/2024 16:31:58 ******/
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
/****** Object:  Table [dbo].[TipoProducto]    Script Date: 28/9/2024 16:31:58 ******/
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
/****** Object:  Table [dbo].[Trackings]    Script Date: 28/9/2024 16:31:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Trackings](
	[TrkId] [bigint] IDENTITY(1,1) NOT NULL,
	[TrkFecha] [datetime] NOT NULL,
	[TrKTrackin] [varchar](300) NOT NULL,
	[TrkMoneda] [int] NOT NULL,
	[TrkCostoMoneda] [decimal](18, 2) NULL,
	[TrkValorMoneda] [decimal](18, 2) NULL,
	[TrkIdPedido] [bigint] NULL,
	[TrkPesoProveedor] [decimal](18, 2) NULL,
	[TrkPesoReal] [decimal](18, 2) NULL,
	[TrkMedidaLargoCm] [decimal](18, 2) NULL,
	[TrkMedidaAnchoCm] [decimal](18, 2) NULL,
	[TrkMedidaAlturaCm] [decimal](18, 2) NULL,
	[TrkEstado] [int] NOT NULL,
	[TrkProveedor] [bigint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[TrkId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TrackingsAsociados]    Script Date: 28/9/2024 16:31:58 ******/
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
/****** Object:  Table [dbo].[UsuaiosEnvioCorreos]    Script Date: 28/9/2024 16:31:58 ******/
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
/****** Object:  Table [dbo].[Usuarios]    Script Date: 28/9/2024 16:31:58 ******/
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
/****** Object:  StoredProcedure [dbo].[EditarUsuario]    Script Date: 28/9/2024 16:31:58 ******/
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
/****** Object:  StoredProcedure [dbo].[GetUsuario]    Script Date: 28/9/2024 16:31:58 ******/
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
/****** Object:  StoredProcedure [dbo].[IngresarFacturacion]    Script Date: 28/9/2024 16:31:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[IngresarFacturacion] 
    @IdUsuario INT,
    @IdBodega INT,
    @IdFormaPago INT,
    @IdClienteMayorista BIGINT = NULL,
    @CorreoEnvio VARCHAR(250) = NULL,
    @ResumenMontoTotal DECIMAL(18,2),
    @ResumemMontoImpuestos DECIMAL(18,2),
    @ResumenMontoPagado DECIMAL(18,2),
    @ResumenMontoCambio DECIMAL(18,2),
    @DetalleFactura InDetalleFactura READONLY,  -- Aquí especificamos el tipo de tabla con READONLY
    @Impuestos ImpuestosPorFactura READONLY -- También debe ser READONLY

AS
BEGIN
    DECLARE @FacturaId BIGINT;  -- Variable para almacenar el ID de la factura
    DECLARE @CodigoFactura NVARCHAR(20);  -- Variable para almacenar el código de la factura
    DECLARE @FechaActual DATETIME = GETDATE();  -- Obtener la fecha actual

    /* Inserta la factura */
    INSERT INTO [dbo].[Factura]
           (
               [FtrFecha],
               [FtrIdUsuario],
               [FtrMayorista],
               [FtrEstatusId],
               [FtrBodega],
               [FtrFormaPago],
               [FtrEsFacturaNula],
               [FtrCodigoFactura],
               [FtrCorreoEnvio]
           )
     VALUES
           (
               @FechaActual,
               @IdUsuario,
               @IdClienteMayorista,
               1,  -- Estatus de la factura
               @IdBodega,
               @IdFormaPago,
               0,  -- Indica que no es una factura nula
               NULL,  -- Código de factura será generado posteriormente
               @CorreoEnvio
           );

    -- Obtener el ID de la factura recién insertada
    SET @FacturaId = SCOPE_IDENTITY();

    -- Generar el código de la factura
    SET @CodigoFactura = CONCAT(
        CONVERT(VARCHAR(4), YEAR(@FechaActual)),  -- Año
        RIGHT('0' + CONVERT(VARCHAR(2), MONTH(@FechaActual)), 2), -- Mes con dos dígitos
        CONVERT(VARCHAR(100), @FacturaId)  -- ID de la factura
    );

    -- Actualizar el código de la factura en la base de datos
    UPDATE [dbo].[Factura]
    SET [FtrCodigoFactura] = @CodigoFactura
    WHERE [FtrId] = @FacturaId;

    /* Insertar Resumen */
    INSERT INTO [dbo].[FacturaResumen]
           (
               [FtrFactura],
               [FtrMontoTotal],
               [FtrMontoImpuestos],
               [FtrMontoPagado],
               [FtrCambio]
           )
     VALUES
           (
               @FacturaId,
               @ResumenMontoTotal,
               @ResumemMontoImpuestos,
               @ResumenMontoPagado,
               @ResumenMontoCambio
           );

    /* Insertar detalle */
    INSERT INTO [dbo].[DetalleFactura]
           (
               [DtfIdProducto],
               [DtfIdFactura],
               [DtfLinea],
               [DtfPrecio],
               [DtfMontoImpuestos],
               [DtfDescuento],
               [DtfCantidad]
           )
    SELECT 
        IdProducto,
        @FacturaId,
        Linea,
        Precio,
        MontoImpuesto,
        MontoDescuento,
        Cantidad
    FROM @DetalleFactura;

	IF EXISTS (SELECT 1 FROM @Impuestos)
    BEGIN
        -- 1) Si existe la tabla temporal, borrarla
        IF OBJECT_ID('tempdb..#TempImpuestos') IS NOT NULL
        BEGIN
            DROP TABLE #TempImpuestos;
        END

        -- 2) Crear la tabla temporal
        CREATE TABLE #TempImpuestos (
            IdImpuesto INT,
            Porcentaje DECIMAL(18, 2)
        );

        -- 3) Insertar los impuestos desde la tabla de parámetros a la tabla temporal
        INSERT INTO #TempImpuestos (IdImpuesto, Porcentaje)
        SELECT I.IdImpuesto, I.Porcentaje
        FROM @Impuestos I;

        -- 4) Verificar si la tabla temporal está vacía
        IF EXISTS (SELECT 1 FROM #TempImpuestos)
        BEGIN
            -- Si hay impuestos, realizar la inserción
            INSERT INTO [dbo].[ImpuestosPorFactura] 
                   ([IpfIdFactura], [IpfIdImpuesto], [IpfPorcentaje])
            SELECT 
                @FacturaId,  
                T.IdImpuesto,  
                T.Porcentaje   
            FROM #TempImpuestos T
            WHERE EXISTS (SELECT 1 FROM [dbo].[Impuestos] P WHERE T.IdImpuesto = P.ImpId);
        END

        -- (Opcional) Eliminar la tabla temporal si ya no es necesaria
        DROP TABLE #TempImpuestos;
	END



    SELECT @FacturaId, @CodigoFactura;
END
GO
/****** Object:  StoredProcedure [dbo].[IngresarUsuario]    Script Date: 28/9/2024 16:31:58 ******/
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
/****** Object:  StoredProcedure [dbo].[ObtenerUsuarios]    Script Date: 28/9/2024 16:31:58 ******/
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
/****** Object:  StoredProcedure [dbo].[ocpv]    Script Date: 28/9/2024 16:31:58 ******/
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
/****** Object:  StoredProcedure [dbo].[ReporteMovimientos]    Script Date: 28/9/2024 16:31:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ReporteMovimientos]
	-- Add the parameters for the stored procedure here
	@IdBodega INT,
	@FechaInicio date,
	@FechaFin date
AS
BEGIN
		Select 
			P.PrdCodigo AS Codigo,
			P.PrdNombre AS DescripcionProducto,
			M.MvmFecha AS fecha,
			--U.UsrUser AS Responsable,
			B.bdgDescripcion AS Bodega,
			MM.MtmDescripcion  AS Descripcion,
			CASE 
				MM.MtmIdTipoMovimiento
				WHEN 1 then M.MvmCantidad
				ELSE 0
			END
			AS Ingresos,
			CASE 
				MM.MtmIdTipoMovimiento
				WHEN 2 then M.MvmCantidad
				ELSE 0
			END
			AS Salidas
		
		from Movimientos M
		inner join Productos P ON M.MvmIdProducto = P.PrdId
		INNER JOIN MotivosMovimientos MM ON M.MvmIdMotivoMovimiento = MM.MtmId
		INNER JOIN Usuarios U ON M.MvmIdUsuario = U.UsrID
		INNER JOIN Bodegas B ON M.MvmIdBodega = B.BdgId
		WHERE 
		M.MvmIdBodega =  @IdBodega AND
		M.MvmFecha>=@FechaInicio AND
		M.MvmFecha<=@FechaFin 

		--union

		SELECT 
			P.PrdCodigo AS Codigo,
			P.PrdNombre AS DescripcionProducto,
			F.FtrFecha AS fecha,
			U.UsrUser AS Responsable,
			B.bdgDescripcion AS Bodega,
			CASE F.FtrEsFacturaNula  
				WHEN 0 
					THEN 'N°'+ F.FtrCodigoFactura 
				ELSE 'N°'+  F.FtrCodigoFactura +' FACTURA NULA' 
			END
			AS Descripcion,
			0 AS Ingresos,
			CASE F.FtrEsFacturaNula  
				WHEN 1 THEN 0
				ELSE DF.DtfCantidad 
			END
			AS Salidas
		FROM DetalleFactura DF
		
		
		inner join Factura F ON DF.DtfIdFactura = F.FtrId
		inner join Productos P ON DF.DtfIdFactura = P.PrdId
		INNER JOIN Usuarios U ON F.FtrIdUsuario = U.UsrID
		INNER JOIN Bodegas B ON F.FtrBodega= B.BdgId

		WHERE
		F.FtrBodega =  @IdBodega AND
		F.FtrFecha>=@FechaInicio AND
		F.FtrFecha<=@FechaFin
		
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Inventarios]    Script Date: 28/9/2024 16:31:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_Inventarios]
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
