CREATE DATABASE <Nombre de la base de datos>
GO

USE <Nombre de la base de datos>
GO
/****** Object:  Table [dbo].[Bodegas]    Script Date: 18/05/2023 22:07:53 ******/
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
/****** Object:  Table [dbo].[ClientesMayoristas]    Script Date: 18/05/2023 22:07:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClientesMayoristas](
	[ClmId] [int] IDENTITY(1,1) NOT NULL,
	[ClmIdPersona] [int] NOT NULL,
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
/****** Object:  Table [dbo].[DetalleFactura]    Script Date: 18/05/2023 22:07:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DetalleFactura](
	[DtfId] [int] IDENTITY(1,1) NOT NULL,
	[DtfIdProducto] [int] NOT NULL,
	[DtfIdFactura] [int] NOT NULL,
	[DtfPrecio] [float] NOT NULL,
	[DtfDescuento] [int] NULL,
	[DtfCantidad] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[DtfId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DetalleProductoPedido]    Script Date: 18/05/2023 22:07:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DetalleProductoPedido](
	[DppId] [int] IDENTITY(1,1) NOT NULL,
	[DppIdProducto] [int] NOT NULL,
	[DppIdPedido] [int] NOT NULL,
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
/****** Object:  Table [dbo].[EstadoPedido]    Script Date: 18/05/2023 22:07:53 ******/
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
/****** Object:  Table [dbo].[EstatusFactura]    Script Date: 18/05/2023 22:07:53 ******/
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
/****** Object:  Table [dbo].[Factura]    Script Date: 18/05/2023 22:07:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Factura](
	[FtrId] [int] IDENTITY(1,1) NOT NULL,
	[FtrFecha] [datetime] NOT NULL,
	[FtrIdUsuario] [int] NOT NULL,
	[FtrMayorista] [int] NULL,
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
/****** Object:  Table [dbo].[FacturaResumen]    Script Date: 18/05/2023 22:07:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FacturaResumen](
	[FtrId] [int] IDENTITY(1,1) NOT NULL,
	[FtrFactura] [int] NOT NULL,
	[FtrMontoTotal] [float] NOT NULL,
	[FtrMontoPagado] [float] NULL,
	[FtrCambio] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[FtrId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FormaPago]    Script Date: 18/05/2023 22:07:53 ******/
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
/****** Object:  Table [dbo].[HistorialClienteMayorista]    Script Date: 18/05/2023 22:07:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HistorialClienteMayorista](
	[HcmId] [int] IDENTITY(1,1) NOT NULL,
	[HcmIdCliente] [int] NOT NULL,
	[HcmFechaVencimiento] [datetime] NOT NULL,
	[HcmFechaActualizacion] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[HcmId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HistorialFacturasAnuladas]    Script Date: 18/05/2023 22:07:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HistorialFacturasAnuladas](
	[HlfId] [int] IDENTITY(1,1) NOT NULL,
	[HlfIdUsuario] [int] NOT NULL,
	[HlfIdFctura] [int] NOT NULL,
	[HlfRazon] [varchar](250) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[HlfId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HistorialPrecios]    Script Date: 18/05/2023 22:07:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HistorialPrecios](
	[HlpId] [int] IDENTITY(1,1) NOT NULL,
	[HlpIdProducto] [int] NOT NULL,
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
/****** Object:  Table [dbo].[Impuestos]    Script Date: 18/05/2023 22:07:53 ******/
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
/****** Object:  Table [dbo].[ImpuestosPorFactura]    Script Date: 18/05/2023 22:07:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ImpuestosPorFactura](
	[IpfId] [int] IDENTITY(1,1) NOT NULL,
	[IpfIdFactura] [int] NOT NULL,
	[IpfIdImpuesto] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IpfId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ImpuestosPorParametro]    Script Date: 18/05/2023 22:07:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ImpuestosPorParametro](
	[ImpPId] [bigint] NOT NULL,
	[ImpPIdParametroGlobal] [int] NULL,
	[ImpPIdImpuesto] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ImpPId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Moneda]    Script Date: 18/05/2023 22:07:53 ******/
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
/****** Object:  Table [dbo].[MotivosMovimientos]    Script Date: 18/05/2023 22:07:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MotivosMovimientos](
	[MtmId] [int] IDENTITY(1,1) NOT NULL,
	[MtmDescripcion] [varchar](50) NOT NULL,
	[MtmIdTipoMovimiento] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MtmId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Movimientos]    Script Date: 18/05/2023 22:07:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Movimientos](
	[MvmId] [int] IDENTITY(1,1) NOT NULL,
	[MvmIdProducto] [int] NOT NULL,
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
/****** Object:  Table [dbo].[OtrosCargos]    Script Date: 18/05/2023 22:07:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OtrosCargos](
	[OtrId] [int] IDENTITY(1,1) NOT NULL,
	[OtrIdMoneda] [int] NOT NULL,
	[OtrIdPedido] [int] NOT NULL,
	[OtrValorMoneda] [float] NULL,
	[OtrCostoMoneda] [float] NULL,
	[OtrRazon] [text] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[OtrId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ParametrosEmail]    Script Date: 18/05/2023 22:07:53 ******/
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
PRIMARY KEY CLUSTERED 
(
	[PreId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ParametrosGlobales]    Script Date: 18/05/2023 22:07:53 ******/
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
PRIMARY KEY CLUSTERED 
(
	[PrgId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pedido]    Script Date: 18/05/2023 22:07:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pedido](
	[PddId] [int] IDENTITY(1,1) NOT NULL,
	[PddFecha] [datetime] NOT NULL,
	[PddIdUsuario] [int] NOT NULL,
	[PddRazonCancelada] [text] NULL,
	[PddProveedor] [int] NOT NULL,
	[PddEstado] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PddId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Persona]    Script Date: 18/05/2023 22:07:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Persona](
	[PsrId] [int] IDENTITY(1,1) NOT NULL,
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
/****** Object:  Table [dbo].[Productos]    Script Date: 18/05/2023 22:07:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Productos](
	[PrdId] [int] IDENTITY(1,1) NOT NULL,
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
/****** Object:  Table [dbo].[Proveedores]    Script Date: 18/05/2023 22:07:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Proveedores](
	[PvdId] [int] IDENTITY(1,1) NOT NULL,
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
/****** Object:  Table [dbo].[RolUsiario]    Script Date: 18/05/2023 22:07:53 ******/
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
/****** Object:  Table [dbo].[TipoMovimiento]    Script Date: 18/05/2023 22:07:53 ******/
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
/****** Object:  Table [dbo].[TipoProducto]    Script Date: 18/05/2023 22:07:53 ******/
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
/****** Object:  Table [dbo].[Trackins]    Script Date: 18/05/2023 22:07:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Trackins](
	[TrkId] [int] IDENTITY(1,1) NOT NULL,
	[TrkFecha] [datetime] NOT NULL,
	[TrKTrackin] [varchar](300) NOT NULL,
	[TrkMoneda] [int] NOT NULL,
	[TrkCostoMoneda] [float] NULL,
	[TrkValorMoneda] [float] NULL,
	[TrkIdPedido] [int] NULL,
	[TrkPesoProveedor] [float] NULL,
	[TrkPesoReal] [float] NULL,
	[TrkMedidaLargoCm] [float] NULL,
	[TrkMedidaAnchoCm] [float] NULL,
	[TrkMedidaAlturaCm] [float] NULL,
	[TrkEstado] [int] NOT NULL,
	[TrkProveedor] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[TrkId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TrackinsAsociados]    Script Date: 18/05/2023 22:07:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TrackinsAsociados](
	[TraId] [int] IDENTITY(1,1) NOT NULL,
	[TraIdTrackin] [int] NOT NULL,
	[TraIdTrackinPrincial] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[TraId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UsuaiosEnvioCorreos]    Script Date: 18/05/2023 22:07:53 ******/
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
/****** Object:  Table [dbo].[Usuarios]    Script Date: 18/05/2023 22:07:53 ******/
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
	[UsrIdPersona] [int] NOT NULL,
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
ALTER TABLE [dbo].[ClientesMayoristas]  WITH CHECK ADD FOREIGN KEY([ClmIdPersona])
REFERENCES [dbo].[Persona] ([PsrId])
GO
ALTER TABLE [dbo].[DetalleFactura]  WITH CHECK ADD FOREIGN KEY([DtfIdProducto])
REFERENCES [dbo].[Productos] ([PrdId])
GO
ALTER TABLE [dbo].[DetalleFactura]  WITH CHECK ADD FOREIGN KEY([DtfIdProducto])
REFERENCES [dbo].[Factura] ([FtrId])
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
ALTER TABLE [dbo].[Factura]  WITH CHECK ADD FOREIGN KEY([FtrBodega])
REFERENCES [dbo].[Bodegas] ([BdgId])
GO
ALTER TABLE [dbo].[Factura]  WITH CHECK ADD FOREIGN KEY([FtrEstatusId])
REFERENCES [dbo].[EstatusFactura] ([EtfId])
GO
ALTER TABLE [dbo].[Factura]  WITH CHECK ADD FOREIGN KEY([FtrEstatusId])
REFERENCES [dbo].[EstatusFactura] ([EtfId])
GO
ALTER TABLE [dbo].[Factura]  WITH CHECK ADD FOREIGN KEY([FtrFormaPago])
REFERENCES [dbo].[FormaPago] ([FrpId])
GO
ALTER TABLE [dbo].[Factura]  WITH CHECK ADD FOREIGN KEY([FtrFormaPago])
REFERENCES [dbo].[FormaPago] ([FrpId])
GO
ALTER TABLE [dbo].[Factura]  WITH CHECK ADD FOREIGN KEY([FtrIdUsuario])
REFERENCES [dbo].[Usuarios] ([UsrID])
GO
ALTER TABLE [dbo].[Factura]  WITH CHECK ADD FOREIGN KEY([FtrIdUsuario])
REFERENCES [dbo].[Usuarios] ([UsrID])
GO
ALTER TABLE [dbo].[Factura]  WITH CHECK ADD FOREIGN KEY([FtrMayorista])
REFERENCES [dbo].[ClientesMayoristas] ([ClmId])
GO
ALTER TABLE [dbo].[Factura]  WITH CHECK ADD FOREIGN KEY([FtrMayorista])
REFERENCES [dbo].[ClientesMayoristas] ([ClmId])
GO
ALTER TABLE [dbo].[FacturaResumen]  WITH CHECK ADD FOREIGN KEY([FtrFactura])
REFERENCES [dbo].[Factura] ([FtrId])
GO
ALTER TABLE [dbo].[FacturaResumen]  WITH CHECK ADD FOREIGN KEY([FtrFactura])
REFERENCES [dbo].[Factura] ([FtrId])
GO
ALTER TABLE [dbo].[HistorialClienteMayorista]  WITH CHECK ADD FOREIGN KEY([HcmIdCliente])
REFERENCES [dbo].[ClientesMayoristas] ([ClmId])
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
ALTER TABLE [dbo].[HistorialPrecios]  WITH CHECK ADD FOREIGN KEY([HlpIdProducto])
REFERENCES [dbo].[Productos] ([PrdId])
GO
ALTER TABLE [dbo].[HistorialPrecios]  WITH CHECK ADD FOREIGN KEY([HlpIdUsuario])
REFERENCES [dbo].[Usuarios] ([UsrID])
GO
ALTER TABLE [dbo].[ImpuestosPorFactura]  WITH CHECK ADD FOREIGN KEY([IpfIdFactura])
REFERENCES [dbo].[Factura] ([FtrId])
GO
ALTER TABLE [dbo].[ImpuestosPorFactura]  WITH CHECK ADD FOREIGN KEY([IpfIdFactura])
REFERENCES [dbo].[Impuestos] ([ImpId])
GO
ALTER TABLE [dbo].[ImpuestosPorParametro]  WITH CHECK ADD FOREIGN KEY([ImpPIdParametroGlobal])
REFERENCES [dbo].[ParametrosGlobales] ([PrgId])
GO
ALTER TABLE [dbo].[MotivosMovimientos]  WITH CHECK ADD FOREIGN KEY([MtmIdTipoMovimiento])
REFERENCES [dbo].[TipoMovimiento] ([TpmId])
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
ALTER TABLE [dbo].[Movimientos]  WITH CHECK ADD FOREIGN KEY([MvmIdUsuario])
REFERENCES [dbo].[Usuarios] ([UsrID])
GO
ALTER TABLE [dbo].[Movimientos]  WITH CHECK ADD FOREIGN KEY([MvmIdBodega])
REFERENCES [dbo].[Bodegas] ([BdgId])
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
ALTER TABLE [dbo].[OtrosCargos]  WITH CHECK ADD FOREIGN KEY([OtrIdMoneda])
REFERENCES [dbo].[Moneda] ([MndId])
GO
ALTER TABLE [dbo].[OtrosCargos]  WITH CHECK ADD FOREIGN KEY([OtrIdPedido])
REFERENCES [dbo].[Pedido] ([PddId])
GO
ALTER TABLE [dbo].[Pedido]  WITH CHECK ADD FOREIGN KEY([PddEstado])
REFERENCES [dbo].[EstadoPedido] ([EtpId])
GO
ALTER TABLE [dbo].[Pedido]  WITH CHECK ADD FOREIGN KEY([PddEstado])
REFERENCES [dbo].[EstadoPedido] ([EtpId])
GO
ALTER TABLE [dbo].[Pedido]  WITH CHECK ADD FOREIGN KEY([PddIdUsuario])
REFERENCES [dbo].[Usuarios] ([UsrID])
GO
ALTER TABLE [dbo].[Pedido]  WITH CHECK ADD FOREIGN KEY([PddIdUsuario])
REFERENCES [dbo].[Usuarios] ([UsrID])
GO
ALTER TABLE [dbo].[Pedido]  WITH CHECK ADD FOREIGN KEY([PddProveedor])
REFERENCES [dbo].[Proveedores] ([PvdId])
GO
ALTER TABLE [dbo].[Pedido]  WITH CHECK ADD FOREIGN KEY([PddProveedor])
REFERENCES [dbo].[Proveedores] ([PvdId])
GO
ALTER TABLE [dbo].[Productos]  WITH CHECK ADD FOREIGN KEY([PrdIdTipoProducto])
REFERENCES [dbo].[TipoProducto] ([TppId])
GO
ALTER TABLE [dbo].[Productos]  WITH CHECK ADD FOREIGN KEY([PrdIdTipoProducto])
REFERENCES [dbo].[TipoProducto] ([TppId])
GO
ALTER TABLE [dbo].[Productos]  WITH CHECK ADD FOREIGN KEY([PrdIdTipoProducto])
REFERENCES [dbo].[TipoProducto] ([TppId])
GO
ALTER TABLE [dbo].[Productos]  WITH CHECK ADD FOREIGN KEY([PrdIdTipoProducto])
REFERENCES [dbo].[TipoProducto] ([TppId])
GO
ALTER TABLE [dbo].[Productos]  WITH CHECK ADD FOREIGN KEY([PrdIdTipoProducto])
REFERENCES [dbo].[TipoProducto] ([TppId])
GO
ALTER TABLE [dbo].[Productos]  WITH CHECK ADD FOREIGN KEY([PrdIdTipoProducto])
REFERENCES [dbo].[TipoProducto] ([TppId])
GO
ALTER TABLE [dbo].[Trackins]  WITH CHECK ADD FOREIGN KEY([TrkEstado])
REFERENCES [dbo].[EstadoPedido] ([EtpId])
GO
ALTER TABLE [dbo].[Trackins]  WITH CHECK ADD FOREIGN KEY([TrkEstado])
REFERENCES [dbo].[EstadoPedido] ([EtpId])
GO
ALTER TABLE [dbo].[Trackins]  WITH CHECK ADD FOREIGN KEY([TrkIdPedido])
REFERENCES [dbo].[Pedido] ([PddId])
GO
ALTER TABLE [dbo].[Trackins]  WITH CHECK ADD FOREIGN KEY([TrkIdPedido])
REFERENCES [dbo].[Pedido] ([PddId])
GO
ALTER TABLE [dbo].[Trackins]  WITH CHECK ADD FOREIGN KEY([TrkMoneda])
REFERENCES [dbo].[Moneda] ([MndId])
GO
ALTER TABLE [dbo].[Trackins]  WITH CHECK ADD FOREIGN KEY([TrkMoneda])
REFERENCES [dbo].[Moneda] ([MndId])
GO
ALTER TABLE [dbo].[Trackins]  WITH CHECK ADD FOREIGN KEY([TrkProveedor])
REFERENCES [dbo].[Proveedores] ([PvdId])
GO
ALTER TABLE [dbo].[Trackins]  WITH CHECK ADD FOREIGN KEY([TrkProveedor])
REFERENCES [dbo].[Proveedores] ([PvdId])
GO
ALTER TABLE [dbo].[TrackinsAsociados]  WITH CHECK ADD FOREIGN KEY([TraIdTrackin])
REFERENCES [dbo].[Trackins] ([TrkId])
GO
ALTER TABLE [dbo].[TrackinsAsociados]  WITH CHECK ADD FOREIGN KEY([TraIdTrackin])
REFERENCES [dbo].[Trackins] ([TrkId])
GO
ALTER TABLE [dbo].[TrackinsAsociados]  WITH CHECK ADD FOREIGN KEY([TraIdTrackin])
REFERENCES [dbo].[Trackins] ([TrkId])
GO
ALTER TABLE [dbo].[TrackinsAsociados]  WITH CHECK ADD FOREIGN KEY([TraIdTrackin])
REFERENCES [dbo].[Trackins] ([TrkId])
GO
ALTER TABLE [dbo].[UsuaiosEnvioCorreos]  WITH CHECK ADD FOREIGN KEY([UecId])
REFERENCES [dbo].[Usuarios] ([UsrID])
GO
ALTER TABLE [dbo].[UsuaiosEnvioCorreos]  WITH CHECK ADD FOREIGN KEY([UecId])
REFERENCES [dbo].[Usuarios] ([UsrID])
GO
ALTER TABLE [dbo].[Usuarios]  WITH CHECK ADD FOREIGN KEY([UsrIdPersona])
REFERENCES [dbo].[Persona] ([PsrId])
GO
ALTER TABLE [dbo].[Usuarios]  WITH CHECK ADD FOREIGN KEY([UsrIdPersona])
REFERENCES [dbo].[Persona] ([PsrId])
GO
ALTER TABLE [dbo].[Usuarios]  WITH CHECK ADD FOREIGN KEY([UsrIdRol])
REFERENCES [dbo].[RolUsiario] ([RluID])
GO
ALTER TABLE [dbo].[Usuarios]  WITH CHECK ADD FOREIGN KEY([UsrIdRol])
REFERENCES [dbo].[RolUsiario] ([RluID])
GO
/****** Object:  StoredProcedure [dbo].[EditarUsuario]    Script Date: 18/05/2023 22:07:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[EditarUsuario]

	@UsrUser varchar(16),
	@UsrPass varchar(256),
	@Email  varchar(100) =null,
	@Rol int,
	@idPersona int,
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
/****** Object:  StoredProcedure [dbo].[GetUsuario]    Script Date: 18/05/2023 22:07:53 ******/
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
/****** Object:  StoredProcedure [dbo].[IngresarUsuario]    Script Date: 18/05/2023 22:07:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  CREATE PROCEDURE [dbo].[IngresarUsuario]
  @Usuario varchar(15),
  @Pass VARCHAR(256),
  @Email Varchar(100),
  @IdRol int,
  @IdPersona int
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
/****** Object:  StoredProcedure [dbo].[ObtenerUsuarios]    Script Date: 18/05/2023 22:07:53 ******/
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
/****** Object:  StoredProcedure [dbo].[ocpv]    Script Date: 18/05/2023 22:07:53 ******/
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
/****** Object:  StoredProcedure [dbo].[SP_Inventarios]    Script Date: 18/05/2023 22:07:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_Inventarios]
@IdBodega INT
AS
BEGIN



  Select M.MvmIdProducto AS IdProducto,
  --Se suman todos los productos
  SUM(M.MvmCantidad) 

  --Suma de otros ajustes positivos
  --MP => MOVIENTO POSITIVO
  +ISNULL((
  SELECT SUM(MP.MvmCantidad) FROM Movimientos MP 
  INNER JOIN MotivosMovimientos MM ON MP.MvmIdMotivoMovimiento = MM.MtmId
  INNER JOIN TipoMovimiento TM ON MM.MtmIdTipoMovimiento= TM.TpmId

  WHERE MP.MvmIdBodega = @IdBodega AND
		MP.MvmIdMotivoMovimiento<> 1 AND 
		TM.TpmId = 1 AND 
		MP.MvmIdProducto = M.MvmIdProducto
  ),0)

  --Movivientos negativos =>MN
  -ISNULL((
  select SUM(MN.MvmCantidad) from Movimientos MN
  inner join MotivosMovimientos MM ON MN.MvmIdMotivoMovimiento = MM.MtmIdTipoMovimiento 
  WHERE MM.MtmIdTipoMovimiento=2 AND MN.MvmIdProducto = M.MvmIdProducto
  ),0)

  -- Factunas no Nulas
  -ISNULL((
  select SUM(DF.DtfCantidad) from DetalleFactura DF 
  INNER JOIN Factura F ON F.FtrId = DF.DtfIdFactura
  WHERE F.FtrEsFacturaNula = 0 and DF.DtfIdProducto = M.MvmIdProducto
  ),0)

  AS existencia
  from Movimientos M

  where M.MvmIdBodega = @IdBodega and 
  M.MvmIdMotivoMovimiento = 1
  GROUP BY M.MvmIdProducto 



END
GO
