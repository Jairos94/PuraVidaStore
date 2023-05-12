using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PuraVidaStoreBK.Models.DbContex
{
    public partial class PuraVidaStoreContext : DbContext
    {
        public PuraVidaStoreContext()
        {
        }

        public PuraVidaStoreContext(DbContextOptions<PuraVidaStoreContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bodega> Bodegas { get; set; } = null!;
        public virtual DbSet<ClientesMayorista> ClientesMayoristas { get; set; } = null!;
        public virtual DbSet<DetalleFactura> DetalleFacturas { get; set; } = null!;
        public virtual DbSet<DetalleProductoPedido> DetalleProductoPedidos { get; set; } = null!;
        public virtual DbSet<EstadoPedido> EstadoPedidos { get; set; } = null!;
        public virtual DbSet<EstatusFactura> EstatusFacturas { get; set; } = null!;
        public virtual DbSet<Factura> Facturas { get; set; } = null!;
        public virtual DbSet<FacturaResumen> FacturaResumen { get; set; } = null!;
        public virtual DbSet<FormaPago> FormaPagos { get; set; } = null!;
        public virtual DbSet<HistorialClienteMayorista> HistorialClienteMayorista { get; set; } = null!;
        public virtual DbSet<HistorialFacturasAnulada> HistorialFacturasAnuladas { get; set; } = null!;
        public virtual DbSet<HistorialPrecio> HistorialPrecios { get; set; } = null!;
        public virtual DbSet<Impuesto> Impuestos { get; set; } = null!;
        public virtual DbSet<ImpuestosPorFactura> ImpuestosPorFacturas { get; set; } = null!;
        public virtual DbSet<ImpustosIncluido> ImpustosIncluidos { get; set; } = null!;
        public virtual DbSet<Moneda> Moneda { get; set; } = null!;
        public virtual DbSet<MotivosMovimiento> MotivosMovimientos { get; set; } = null!;
        public virtual DbSet<Movimiento> Movimientos { get; set; } = null!;
        public virtual DbSet<OtrosCargo> OtrosCargos { get; set; } = null!;
        public virtual DbSet<ParametrosEmail> ParametrosEmails { get; set; } = null!;
        public virtual DbSet<ParametrosGlobale> ParametrosGlobales { get; set; } = null!;
        public virtual DbSet<Pedido> Pedidos { get; set; } = null!;
        public virtual DbSet<Persona> Personas { get; set; } = null!;
        public virtual DbSet<Producto> Productos { get; set; } = null!;
        public virtual DbSet<Proveedore> Proveedores { get; set; } = null!;
        public virtual DbSet<RolUsiario> RolUsiarios { get; set; } = null!;
        public virtual DbSet<TipoMovimiento> TipoMovimientos { get; set; } = null!;
        public virtual DbSet<TipoProducto> TipoProductos { get; set; } = null!;
        public virtual DbSet<Trackin> Trackins { get; set; } = null!;
        public virtual DbSet<TrackinsAsociado> TrackinsAsociados { get; set; } = null!;
        public virtual DbSet<UsuaiosEnvioCorreo> UsuaiosEnvioCorreos { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-4FJOI9V;Initial Catalog=PuraVidaStore;Trusted_Connection=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bodega>(entity =>
            {
                entity.HasKey(e => e.BdgId)
                    .HasName("PK__Bodegas__522D12A5A5CBDCD6");

                entity.Property(e => e.BdgDescripcion)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("bdgDescripcion");

                entity.Property(e => e.BdgVisible).HasColumnName("bdgVisible");
            });

            modelBuilder.Entity<ClientesMayorista>(entity =>
            {
                entity.HasKey(e => e.ClmId)
                    .HasName("PK__Clientes__FD21CFD2DAF6B8A3");

                entity.Property(e => e.ClmCorreo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ClmFechaCreacion).HasColumnType("datetime");

                entity.Property(e => e.ClmFechaVencimiento).HasColumnType("datetime");

                entity.Property(e => e.ClmTelefono)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.HasOne(d => d.ClmIdPersonaNavigation)
                    .WithMany(p => p.ClientesMayorista)
                    .HasForeignKey(d => d.ClmIdPersona)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ClientesM__ClmId__5FB337D6");
            });

            modelBuilder.Entity<DetalleFactura>(entity =>
            {
                entity.HasKey(e => e.DtfId)
                    .HasName("PK__DetalleF__08270EDE28E3470A");

                entity.ToTable("DetalleFactura");

                entity.HasOne(d => d.DtfIdProductoNavigation)
                    .WithMany(p => p.DetalleFacturas)
                    .HasForeignKey(d => d.DtfIdProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DetalleFa__DtfId__628FA481");

                entity.HasOne(d => d.DtfIdProducto1)
                    .WithMany(p => p.DetalleFacturas)
                    .HasForeignKey(d => d.DtfIdProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DetalleFa__DtfId__619B8048");
            });

            modelBuilder.Entity<DetalleProductoPedido>(entity =>
            {
                entity.HasKey(e => e.DppId)
                    .HasName("PK__DetalleP__45FE6F5A2644F7D3");

                entity.ToTable("DetalleProductoPedido");

                entity.HasOne(d => d.DppIdMonedaNavigation)
                    .WithMany(p => p.DetalleProductoPedidos)
                    .HasForeignKey(d => d.DppIdMoneda)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DetallePr__DppId__6754599E");

                entity.HasOne(d => d.DppIdPedidoNavigation)
                    .WithMany(p => p.DetalleProductoPedidos)
                    .HasForeignKey(d => d.DppIdPedido)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DetallePr__DppId__66603565");

                entity.HasOne(d => d.DppIdProductoNavigation)
                    .WithMany(p => p.DetalleProductoPedidos)
                    .HasForeignKey(d => d.DppIdProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DetallePr__DppId__656C112C");
            });

            modelBuilder.Entity<EstadoPedido>(entity =>
            {
                entity.HasKey(e => e.EtpId)
                    .HasName("PK__EstadoPe__CD1FF58CA6A3288A");

                entity.ToTable("EstadoPedido");

                entity.Property(e => e.EtpDescripcion)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EstatusFactura>(entity =>
            {
                entity.HasKey(e => e.EtfId)
                    .HasName("PK__EstatusF__CA83E8167187D72F");

                entity.ToTable("EstatusFactura");

                entity.Property(e => e.EsfDescripcion)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Factura>(entity =>
            {
                entity.HasKey(e => e.FtrId)
                    .HasName("PK__Factura__2B314E1F820C52AF");

                entity.ToTable("Factura");

                entity.Property(e => e.FtrCodigoFactura).HasMaxLength(350);

                entity.Property(e => e.FtrFecha).HasColumnType("datetime");

                entity.HasOne(d => d.FtrBodegaNavigation)
                    .WithMany(p => p.Facturas)
                    .HasForeignKey(d => d.FtrBodega)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Factura__FtrBode__6B24EA82");

                entity.HasOne(d => d.FtrEstatus)
                    .WithMany(p => p.Facturas)
                    .HasForeignKey(d => d.FtrEstatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Factura__FtrEsta__6D0D32F4");

                entity.HasOne(d => d.FtrFormaPagoNavigation)
                    .WithMany(p => p.Facturas)
                    .HasForeignKey(d => d.FtrFormaPago)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Factura__FtrForm__6EF57B66");

                entity.HasOne(d => d.FtrIdUsuarioNavigation)
                    .WithMany(p => p.Facturas)
                    .HasForeignKey(d => d.FtrIdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Factura__FtrIdUs__70DDC3D8");

                entity.HasOne(d => d.FtrMayoristaNavigation)
                    .WithMany(p => p.Facturas)
                    .HasForeignKey(d => d.FtrMayorista)
                    .HasConstraintName("FK__Factura__FtrMayo__72C60C4A");
            });

            modelBuilder.Entity<FacturaResumen>(entity =>
            {
                entity.HasKey(e => e.FtrId)
                    .HasName("PK__FacturaR__2B314E1F2A7A2FBC");

                entity.HasOne(d => d.FtrFacturaNavigation)
                    .WithMany(p => p.FacturaResumen)
                    .HasForeignKey(d => d.FtrFactura)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FacturaRe__FtrFa__74AE54BC");
            });

            modelBuilder.Entity<FormaPago>(entity =>
            {
                entity.HasKey(e => e.FrpId)
                    .HasName("PK__FormaPag__C30F48C317637389");

                entity.ToTable("FormaPago");

                entity.Property(e => e.FrpDescripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<HistorialClienteMayorista>(entity =>
            {
                entity.HasKey(e => e.HcmId)
                    .HasName("PK__Historia__3B25B7B898377746");

                entity.Property(e => e.HcmFechaActualizacion).HasColumnType("datetime");

                entity.Property(e => e.HcmFechaVencimiento).HasColumnType("datetime");

                entity.HasOne(d => d.HcmIdClienteNavigation)
                    .WithMany(p => p.HistorialClienteMayorista)
                    .HasForeignKey(d => d.HcmIdCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Historial__HcmId__76969D2E");
            });

            modelBuilder.Entity<HistorialFacturasAnulada>(entity =>
            {
                entity.HasKey(e => e.HlfId)
                    .HasName("PK__Historia__A894EAB380BCB393");

                entity.Property(e => e.HlfRazon)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.HasOne(d => d.HlfIdFcturaNavigation)
                    .WithMany(p => p.HistorialFacturasAnulada)
                    .HasForeignKey(d => d.HlfIdFctura)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Historial__HlfId__797309D9");

                entity.HasOne(d => d.HlfIdUsuarioNavigation)
                    .WithMany(p => p.HistorialFacturasAnulada)
                    .HasForeignKey(d => d.HlfIdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Historial__HlfId__787EE5A0");
            });

            modelBuilder.Entity<HistorialPrecio>(entity =>
            {
                entity.HasKey(e => e.HlpId)
                    .HasName("PK__Historia__AAE48D2CF63D0E15");

                entity.Property(e => e.HlpFecha).HasColumnType("datetime");

                entity.HasOne(d => d.HlpIdProductoNavigation)
                    .WithMany(p => p.HistorialPrecios)
                    .HasForeignKey(d => d.HlpIdProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Historial__HlpId__7C4F7684");

                entity.HasOne(d => d.HlpIdUsuarioNavigation)
                    .WithMany(p => p.HistorialPrecios)
                    .HasForeignKey(d => d.HlpIdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Historial__HlpId__7D439ABD");
            });

            modelBuilder.Entity<Impuesto>(entity =>
            {
                entity.HasKey(e => e.ImpId)
                    .HasName("PK__Impuesto__B6CB82B8E5289F21");

                entity.Property(e => e.ImpDescripcion)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ImpuestosPorFactura>(entity =>
            {
                entity.HasKey(e => e.IpfId)
                    .HasName("PK__Impuesto__C85CA5DA625BF11A");

                entity.ToTable("ImpuestosPorFactura");

                entity.HasOne(d => d.IpfIdFacturaNavigation)
                    .WithMany(p => p.ImpuestosPorFacturas)
                    .HasForeignKey(d => d.IpfIdFactura)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Impuestos__IpfId__00200768");

                entity.HasOne(d => d.IpfIdFactura1)
                    .WithMany(p => p.ImpuestosPorFacturas)
                    .HasForeignKey(d => d.IpfIdFactura)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Impuestos__IpfId__01142BA1");
            });

            modelBuilder.Entity<ImpustosIncluido>(entity =>
            {
                entity.HasKey(e => e.IicId)
                    .HasName("PK__Impustos__46219D54C2358FB4");

                entity.HasOne(d => d.IicIdConfiguracionNavigation)
                    .WithMany(p => p.ImpustosIncluidos)
                    .HasForeignKey(d => d.IicIdConfiguracion)
                    .HasConstraintName("FK__ImpustosI__IicId__02FC7413");

                entity.HasOne(d => d.IicIdImpuestoNavigation)
                    .WithMany(p => p.ImpustosIncluidos)
                    .HasForeignKey(d => d.IicIdImpuesto)
                    .HasConstraintName("FK__ImpustosI__IicId__02084FDA");
            });

            modelBuilder.Entity<Moneda>(entity =>
            {
                entity.HasKey(e => e.MndId)
                    .HasName("PK__Moneda__D2906950C17BB354");

                entity.Property(e => e.MndCodigo)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.MndDescripcion)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MotivosMovimiento>(entity =>
            {
                entity.HasKey(e => e.MtmId)
                    .HasName("PK__MotivosM__62D5204F1E58C45F");

                entity.Property(e => e.MtmDescripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.MtmIdTipoMovimientoNavigation)
                    .WithMany(p => p.MotivosMovimientos)
                    .HasForeignKey(d => d.MtmIdTipoMovimiento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MotivosMo__MtmId__03F0984C");
            });

            modelBuilder.Entity<Movimiento>(entity =>
            {
                entity.HasKey(e => e.MvmId)
                    .HasName("PK__Movimien__D46E0D01AED8368F");

                entity.Property(e => e.MvmFecha).HasColumnType("datetime");

                entity.HasOne(d => d.MvmIdBodegaNavigation)
                    .WithMany(p => p.Movimientos)
                    .HasForeignKey(d => d.MvmIdBodega)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Movimient__MvmId__06CD04F7");

                entity.HasOne(d => d.MvmIdMotivoMovimientoNavigation)
                    .WithMany(p => p.Movimientos)
                    .HasForeignKey(d => d.MvmIdMotivoMovimiento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Movimient__MvmId__08B54D69");

                entity.HasOne(d => d.MvmIdProductoNavigation)
                    .WithMany(p => p.Movimientos)
                    .HasForeignKey(d => d.MvmIdProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Movimient__MvmId__07C12930");

                entity.HasOne(d => d.MvmIdUsuarioNavigation)
                    .WithMany(p => p.Movimientos)
                    .HasForeignKey(d => d.MvmIdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Movimient__MvmId__05D8E0BE");
            });

            modelBuilder.Entity<OtrosCargo>(entity =>
            {
                entity.HasKey(e => e.OtrId)
                    .HasName("PK__OtrosCar__91381F33045B4889");

                entity.Property(e => e.OtrRazon).HasColumnType("text");

                entity.HasOne(d => d.OtrIdMonedaNavigation)
                    .WithMany(p => p.OtrosCargos)
                    .HasForeignKey(d => d.OtrIdMoneda)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OtrosCarg__OtrId__0C85DE4D");

                entity.HasOne(d => d.OtrIdPedidoNavigation)
                    .WithMany(p => p.OtrosCargos)
                    .HasForeignKey(d => d.OtrIdPedido)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OtrosCarg__OtrId__0D7A0286");
            });

            modelBuilder.Entity<ParametrosEmail>(entity =>
            {
                entity.HasKey(e => e.PreId)
                    .HasName("PK__Parametr__7024CEC9A06BC43B");

                entity.ToTable("ParametrosEmail");

                entity.Property(e => e.PreClave)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.PreHost)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.PreUser)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("preUser");
            });

            modelBuilder.Entity<ParametrosGlobale>(entity =>
            {
                entity.HasKey(e => e.PrgId)
                    .HasName("PK__Parametr__76A0837B7D4DBD06");
            });

            modelBuilder.Entity<Pedido>(entity =>
            {
                entity.HasKey(e => e.PddId)
                    .HasName("PK__Pedido__98F0BE93695D3FA5");

                entity.ToTable("Pedido");

                entity.Property(e => e.PddFecha).HasColumnType("datetime");

                entity.Property(e => e.PddRazonCancelada).HasColumnType("text");

                entity.HasOne(d => d.PddEstadoNavigation)
                    .WithMany(p => p.Pedidos)
                    .HasForeignKey(d => d.PddEstado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Pedido__PddEstad__10566F31");

                entity.HasOne(d => d.PddIdUsuarioNavigation)
                    .WithMany(p => p.Pedidos)
                    .HasForeignKey(d => d.PddIdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Pedido__PddIdUsu__123EB7A3");

                entity.HasOne(d => d.PddProveedorNavigation)
                    .WithMany(p => p.Pedidos)
                    .HasForeignKey(d => d.PddProveedor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Pedido__PddProve__14270015");
            });

            modelBuilder.Entity<Persona>(entity =>
            {
                entity.HasKey(e => e.PsrId)
                    .HasName("PK__Persona__4F16F4C809AFE16A");

                entity.ToTable("Persona");

                entity.Property(e => e.PsrApellido1)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PsrApellido2)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PsrIdentificacion)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.PsrNombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.PrdId)
                    .HasName("PK__Producto__7168B164A01DDBF1");

                entity.Property(e => e.PdrFoto).IsUnicode(false);

                entity.Property(e => e.PrdCodigo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PrdCodigoProvedor)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PrdNombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.PrdIdTipoProductoNavigation)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.PrdIdTipoProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Productos__PrdId__160F4887");
            });

            modelBuilder.Entity<Proveedore>(entity =>
            {
                entity.HasKey(e => e.PvdId)
                    .HasName("PK__Proveedo__E82C85539B7A09B7");

                entity.Property(e => e.PvdProveedorCorreo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PvdProveedorNmbre)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PvdProveedorNumeroTelefono)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RolUsiario>(entity =>
            {
                entity.HasKey(e => e.RluId)
                    .HasName("PK__RolUsiar__812CEDA6872A4F05");

                entity.ToTable("RolUsiario");

                entity.Property(e => e.RluId).HasColumnName("RluID");

                entity.Property(e => e.RluDescripcion)
                    .HasMaxLength(16)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TipoMovimiento>(entity =>
            {
                entity.HasKey(e => e.TpmId)
                    .HasName("PK__TipoMovi__0637E7B04B9DEF71");

                entity.ToTable("TipoMovimiento");

                entity.Property(e => e.TpmDescripcion)
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TipoProducto>(entity =>
            {
                entity.HasKey(e => e.TppId)
                    .HasName("PK__TipoProd__028B0F70C1708004");

                entity.ToTable("TipoProducto");

                entity.Property(e => e.TppDescripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Trackin>(entity =>
            {
                entity.HasKey(e => e.TrkId)
                    .HasName("PK__Trackins__B83DA4B22C31B688");

                entity.Property(e => e.TrKtrackin)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("TrKTrackin");

                entity.Property(e => e.TrkFecha).HasColumnType("datetime");

                entity.HasOne(d => d.TrkEstadoNavigation)
                    .WithMany(p => p.Trackins)
                    .HasForeignKey(d => d.TrkEstado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Trackins__TrkEst__1BC821DD");

                entity.HasOne(d => d.TrkIdPedidoNavigation)
                    .WithMany(p => p.Trackins)
                    .HasForeignKey(d => d.TrkIdPedido)
                    .HasConstraintName("FK__Trackins__TrkIdP__1DB06A4F");

                entity.HasOne(d => d.TrkMonedaNavigation)
                    .WithMany(p => p.Trackins)
                    .HasForeignKey(d => d.TrkMoneda)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Trackins__TrkMon__1F98B2C1");

                entity.HasOne(d => d.TrkProveedorNavigation)
                    .WithMany(p => p.Trackins)
                    .HasForeignKey(d => d.TrkProveedor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Trackins__TrkPro__2180FB33");
            });

            modelBuilder.Entity<TrackinsAsociado>(entity =>
            {
                entity.HasKey(e => e.TraId)
                    .HasName("PK__Trackins__E6FDEF50D24343AF");

                entity.HasOne(d => d.TraIdTrackinNavigation)
                    .WithMany(p => p.TrackinsAsociados)
                    .HasForeignKey(d => d.TraIdTrackin)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TrackinsA__TraId__236943A5");
            });

            modelBuilder.Entity<UsuaiosEnvioCorreo>(entity =>
            {
                entity.HasKey(e => e.UecId)
                    .HasName("PK__UsuaiosE__2A7A0348334539F5");

                entity.Property(e => e.UecId).ValueGeneratedOnAdd();

                entity.HasOne(d => d.Uec)
                    .WithOne(p => p.UsuaiosEnvioCorreo)
                    .HasForeignKey<UsuaiosEnvioCorreo>(d => d.UecId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UsuaiosEn__UecId__2739D489");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.UsrId)
                    .HasName("PK__Usuarios__6A1E3D48E38B7EB1");

                entity.Property(e => e.UsrId).HasColumnName("UsrID");

                entity.Property(e => e.UsrEmail)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UsrPass).HasMaxLength(8000);

                entity.Property(e => e.UsrUser)
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.HasOne(d => d.UsrIdPersonaNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.UsrIdPersona)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Usuarios__UsrIdP__29221CFB");

                entity.HasOne(d => d.UsrIdRolNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.UsrIdRol)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Usuarios__UsrIdR__2B0A656D");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
