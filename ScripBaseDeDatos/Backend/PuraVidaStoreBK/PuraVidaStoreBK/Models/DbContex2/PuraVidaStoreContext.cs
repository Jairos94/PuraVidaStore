using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PuraVidaStoreBK.Models.DbContex2
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
        public virtual DbSet<FacturaResuman> FacturaResumen { get; set; } = null!;
        public virtual DbSet<FormaPago> FormaPagos { get; set; } = null!;
        public virtual DbSet<HistorialClienteMayoristum> HistorialClienteMayorista { get; set; } = null!;
        public virtual DbSet<HistorialFacturasAnulada> HistorialFacturasAnuladas { get; set; } = null!;
        public virtual DbSet<HistorialPrecio> HistorialPrecios { get; set; } = null!;
        public virtual DbSet<Monedum> Moneda { get; set; } = null!;
        public virtual DbSet<MotivosMovimiento> MotivosMovimientos { get; set; } = null!;
        public virtual DbSet<Movimiento> Movimientos { get; set; } = null!;
        public virtual DbSet<OtrosCargo> OtrosCargos { get; set; } = null!;
        public virtual DbSet<Pedido> Pedidos { get; set; } = null!;
        public virtual DbSet<Persona> Personas { get; set; } = null!;
        public virtual DbSet<Producto> Productos { get; set; } = null!;
        public virtual DbSet<Proveedore> Proveedores { get; set; } = null!;
        public virtual DbSet<RolUsiario> RolUsiarios { get; set; } = null!;
        public virtual DbSet<TipoMovimiento> TipoMovimientos { get; set; } = null!;
        public virtual DbSet<TipoProducto> TipoProductos { get; set; } = null!;
        public virtual DbSet<Trackin> Trackins { get; set; } = null!;
        public virtual DbSet<TrackinsAsociado> TrackinsAsociados { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-4FJOI9V;Database=PuraVidaStore;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bodega>(entity =>
            {
                entity.HasKey(e => e.BdgId)
                    .HasName("PK__Bodegas__522D12A51FBC787E");

                entity.Property(e => e.BdgDescripción)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("bdgDescripción");
            });

            modelBuilder.Entity<ClientesMayorista>(entity =>
            {
                entity.HasKey(e => e.ClmId)
                    .HasName("PK__Clientes__FD21CFD25D2C9F01");

                entity.Property(e => e.ClmCorreo)
                    .HasMaxLength(20)
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
                    .HasConstraintName("FK__ClientesM__ClmId__34C8D9D1");
            });

            modelBuilder.Entity<DetalleFactura>(entity =>
            {
                entity.HasKey(e => e.DtfId)
                    .HasName("PK__DetalleF__08270EDEE7FD8D53");

                entity.ToTable("DetalleFactura");

                entity.HasOne(d => d.DtfIdProductoNavigation)
                    .WithMany(p => p.DetalleFacturas)
                    .HasForeignKey(d => d.DtfIdProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DetalleFa__DtfId__4BAC3F29");

                entity.HasOne(d => d.DtfIdProducto1)
                    .WithMany(p => p.DetalleFacturas)
                    .HasForeignKey(d => d.DtfIdProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DetalleFa__DtfId__4AB81AF0");
            });

            modelBuilder.Entity<DetalleProductoPedido>(entity =>
            {
                entity.HasKey(e => e.DppId)
                    .HasName("PK__DetalleP__45FE6F5AA8E08345");

                entity.ToTable("DetalleProductoPedido");

                entity.HasOne(d => d.DppIdMonedaNavigation)
                    .WithMany(p => p.DetalleProductoPedidos)
                    .HasForeignKey(d => d.DppIdMoneda)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DetallePr__DppId__619B8048");

                entity.HasOne(d => d.DppIdPedidoNavigation)
                    .WithMany(p => p.DetalleProductoPedidos)
                    .HasForeignKey(d => d.DppIdPedido)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DetallePr__DppId__60A75C0F");

                entity.HasOne(d => d.DppIdProductoNavigation)
                    .WithMany(p => p.DetalleProductoPedidos)
                    .HasForeignKey(d => d.DppIdProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DetallePr__DppId__5FB337D6");
            });

            modelBuilder.Entity<EstadoPedido>(entity =>
            {
                entity.HasKey(e => e.EtpId)
                    .HasName("PK__EstadoPe__CD1FF58C20C845BB");

                entity.ToTable("EstadoPedido");

                entity.Property(e => e.EtpDescripcion)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EstatusFactura>(entity =>
            {
                entity.HasKey(e => e.EtfId)
                    .HasName("PK__EstatusF__CA83E8166F310484");

                entity.ToTable("EstatusFactura");

                entity.Property(e => e.EsfDescripcion)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Factura>(entity =>
            {
                entity.HasKey(e => e.FtrId)
                    .HasName("PK__Factura__2B314E1F3C0F686F");

                entity.ToTable("Factura");

                entity.Property(e => e.FtrFecha).HasColumnType("datetime");

                entity.HasOne(d => d.FtrBodegaNavigation)
                    .WithMany(p => p.Facturas)
                    .HasForeignKey(d => d.FtrBodega)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Factura__FtrBode__6FE99F9F");

                entity.HasOne(d => d.FtrEstatus)
                    .WithMany(p => p.Facturas)
                    .HasForeignKey(d => d.FtrEstatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Factura__FtrEsta__47DBAE45");

                entity.HasOne(d => d.FtrFormaPagoNavigation)
                    .WithMany(p => p.Facturas)
                    .HasForeignKey(d => d.FtrFormaPago)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Factura__FtrForm__05D8E0BE");

                entity.HasOne(d => d.FtrIdUsuarioNavigation)
                    .WithMany(p => p.Facturas)
                    .HasForeignKey(d => d.FtrIdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Factura__FtrIdUs__4316F928");

                entity.HasOne(d => d.FtrMayoristaNavigation)
                    .WithMany(p => p.Facturas)
                    .HasForeignKey(d => d.FtrMayorista)
                    .HasConstraintName("FK__Factura__FtrMayo__440B1D61");
            });

            modelBuilder.Entity<FacturaResuman>(entity =>
            {
                entity.HasKey(e => e.FtrId)
                    .HasName("PK__FacturaR__2B314E1F154DA4F6");

                entity.HasOne(d => d.FtrFacturaNavigation)
                    .WithMany(p => p.FacturaResumen)
                    .HasForeignKey(d => d.FtrFactura)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FacturaRe__FtrFa__02FC7413");
            });

            modelBuilder.Entity<FormaPago>(entity =>
            {
                entity.HasKey(e => e.FrpId)
                    .HasName("PK__FormaPag__C30F48C3B82E704C");

                entity.ToTable("FormaPago");

                entity.Property(e => e.FrpDescripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<HistorialClienteMayoristum>(entity =>
            {
                entity.HasKey(e => e.HcmId)
                    .HasName("PK__Historia__3B25B7B84D9CEA32");

                entity.Property(e => e.HcmFechaActualizacion).HasColumnType("datetime");

                entity.Property(e => e.HcmFechaVencimiento).HasColumnType("datetime");

                entity.HasOne(d => d.HcmIdClienteNavigation)
                    .WithMany(p => p.HistorialClienteMayorista)
                    .HasForeignKey(d => d.HcmIdCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Historial__HcmId__37A5467C");
            });

            modelBuilder.Entity<HistorialFacturasAnulada>(entity =>
            {
                entity.HasKey(e => e.HlfId)
                    .HasName("PK__Historia__A894EAB3986E6961");

                entity.Property(e => e.HlfRazon)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.HasOne(d => d.HlfIdFcturaNavigation)
                    .WithMany(p => p.HistorialFacturasAnulada)
                    .HasForeignKey(d => d.HlfIdFctura)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Historial__HlfId__4F7CD00D");

                entity.HasOne(d => d.HlfIdUsuarioNavigation)
                    .WithMany(p => p.HistorialFacturasAnulada)
                    .HasForeignKey(d => d.HlfIdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Historial__HlfId__4E88ABD4");
            });

            modelBuilder.Entity<HistorialPrecio>(entity =>
            {
                entity.HasKey(e => e.HlpId)
                    .HasName("PK__Historia__AAE48D2CCFEE5D99");

                entity.Property(e => e.HlpFecha).HasColumnType("datetime");

                entity.HasOne(d => d.HlpIdProductoNavigation)
                    .WithMany(p => p.HistorialPrecios)
                    .HasForeignKey(d => d.HlpIdProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Historial__HlpId__30F848ED");

                entity.HasOne(d => d.HlpIdUsuarioNavigation)
                    .WithMany(p => p.HistorialPrecios)
                    .HasForeignKey(d => d.HlpIdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Historial__HlpId__31EC6D26");
            });

            modelBuilder.Entity<Monedum>(entity =>
            {
                entity.HasKey(e => e.MndId)
                    .HasName("PK__Moneda__D2906950D8649454");

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
                    .HasName("PK__MotivosM__62D5204F84870B46");

                entity.Property(e => e.MtmDescripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.MtmIdTipoMovimientoNavigation)
                    .WithMany(p => p.MotivosMovimientos)
                    .HasForeignKey(d => d.MtmIdTipoMovimiento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MotivosMo__MtmId__3F466844");
            });

            modelBuilder.Entity<Movimiento>(entity =>
            {
                entity.HasKey(e => e.MvmId)
                    .HasName("PK__Movimien__D46E0D011FFE982D");

                entity.Property(e => e.MvmFecha).HasColumnType("datetime");

                entity.HasOne(d => d.MvmIdBodegaNavigation)
                    .WithMany(p => p.Movimientos)
                    .HasForeignKey(d => d.MvmIdBodega)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Movimient__MvmId__52593CB8");

                entity.HasOne(d => d.MvmIdMotivoMovimientoNavigation)
                    .WithMany(p => p.Movimientos)
                    .HasForeignKey(d => d.MvmIdMotivoMovimiento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Movimient__MvmId__403A8C7D");

                entity.HasOne(d => d.MvmIdProductoNavigation)
                    .WithMany(p => p.Movimientos)
                    .HasForeignKey(d => d.MvmIdProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Movimient__MvmId__3A81B327");

                entity.HasOne(d => d.MvmIdUsuarioNavigation)
                    .WithMany(p => p.Movimientos)
                    .HasForeignKey(d => d.MvmIdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Movimient__MvmId__44FF419A");
            });

            modelBuilder.Entity<OtrosCargo>(entity =>
            {
                entity.HasKey(e => e.OtrId)
                    .HasName("PK__OtrosCar__91381F33CE238665");

                entity.Property(e => e.OtrRazon).HasColumnType("text");

                entity.HasOne(d => d.OtrIdMonedaNavigation)
                    .WithMany(p => p.OtrosCargos)
                    .HasForeignKey(d => d.OtrIdMoneda)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OtrosCarg__OtrId__6E01572D");

                entity.HasOne(d => d.OtrIdPedidoNavigation)
                    .WithMany(p => p.OtrosCargos)
                    .HasForeignKey(d => d.OtrIdPedido)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OtrosCarg__OtrId__6EF57B66");
            });

            modelBuilder.Entity<Pedido>(entity =>
            {
                entity.HasKey(e => e.PddId)
                    .HasName("PK__Pedido__98F0BE93936BA3F6");

                entity.ToTable("Pedido");

                entity.Property(e => e.PddFecha).HasColumnType("datetime");

                entity.Property(e => e.PddRazonCancelada).HasColumnType("text");

                entity.HasOne(d => d.PddEstadoNavigation)
                    .WithMany(p => p.Pedidos)
                    .HasForeignKey(d => d.PddEstado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Pedido__PddEstad__5CD6CB2B");

                entity.HasOne(d => d.PddIdUsuarioNavigation)
                    .WithMany(p => p.Pedidos)
                    .HasForeignKey(d => d.PddIdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Pedido__PddIdUsu__5535A963");

                entity.HasOne(d => d.PddProveedorNavigation)
                    .WithMany(p => p.Pedidos)
                    .HasForeignKey(d => d.PddProveedor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Pedido__PddProve__59FA5E80");
            });

            modelBuilder.Entity<Persona>(entity =>
            {
                entity.HasKey(e => e.PsrId)
                    .HasName("PK__Persona__4F16F4C87567EE03");

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
                    .HasName("PK__Producto__7168B164F60755CE");

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
                    .HasConstraintName("FK__Productos__PrdId__2E1BDC42");
            });

            modelBuilder.Entity<Proveedore>(entity =>
            {
                entity.HasKey(e => e.PvdId)
                    .HasName("PK__Proveedo__E82C8553DF048A19");

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
                    .HasName("PK__RolUsiar__812CEDA6983A7D63");

                entity.ToTable("RolUsiario");

                entity.Property(e => e.RluId).HasColumnName("RluID");

                entity.Property(e => e.RluDescripcion)
                    .HasMaxLength(16)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TipoMovimiento>(entity =>
            {
                entity.HasKey(e => e.TpmId)
                    .HasName("PK__TipoMovi__0637E7B019D7DE63");

                entity.ToTable("TipoMovimiento");

                entity.Property(e => e.TpmDescripcion)
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TipoProducto>(entity =>
            {
                entity.HasKey(e => e.TppId)
                    .HasName("PK__TipoProd__028B0F70D32FC76B");

                entity.ToTable("TipoProducto");

                entity.Property(e => e.TppDescripción)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Trackin>(entity =>
            {
                entity.HasKey(e => e.TrkId)
                    .HasName("PK__Trackins__B83DA4B293853C15");

                entity.Property(e => e.TrKtrackin)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("TrKTrackin");

                entity.Property(e => e.TrkFecha).HasColumnType("datetime");

                entity.HasOne(d => d.TrkEstadoNavigation)
                    .WithMany(p => p.Trackins)
                    .HasForeignKey(d => d.TrkEstado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Trackins__TrkEst__66603565");

                entity.HasOne(d => d.TrkIdPedidoNavigation)
                    .WithMany(p => p.Trackins)
                    .HasForeignKey(d => d.TrkIdPedido)
                    .HasConstraintName("FK__Trackins__TrkIdP__656C112C");

                entity.HasOne(d => d.TrkMonedaNavigation)
                    .WithMany(p => p.Trackins)
                    .HasForeignKey(d => d.TrkMoneda)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Trackins__TrkMon__6477ECF3");

                entity.HasOne(d => d.TrkProveedorNavigation)
                    .WithMany(p => p.Trackins)
                    .HasForeignKey(d => d.TrkProveedor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Trackins__TrkPro__6754599E");
            });

            modelBuilder.Entity<TrackinsAsociado>(entity =>
            {
                entity.HasKey(e => e.TraId)
                    .HasName("PK__Trackins__E6FDEF50059C5F97");

                entity.HasOne(d => d.TraIdTrackinNavigation)
                    .WithMany(p => p.TrackinsAsociados)
                    .HasForeignKey(d => d.TraIdTrackin)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TrackinsA__TraId__6A30C649");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.UsrId)
                    .HasName("PK__Usuarios__6A1E3D48CEFF182B");

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
                    .HasConstraintName("FK__Usuarios__UsrIdP__29572725");

                entity.HasOne(d => d.UsrIdRolNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.UsrIdRol)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Usuarios__UsrIdR__267ABA7A");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
