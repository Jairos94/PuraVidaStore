using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PuraVidaStoreBK.Models.DbContex;

public partial class PuraVidaStoreContext : DbContext
{
    public PuraVidaStoreContext()
    {
    }

    public PuraVidaStoreContext(DbContextOptions<PuraVidaStoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Bodega> Bodegas { get; set; }

    public virtual DbSet<ClientesMayorista> ClientesMayoristas { get; set; }

    public virtual DbSet<DetalleFactura> DetalleFacturas { get; set; }

    public virtual DbSet<DetalleProductoPedido> DetalleProductoPedidos { get; set; }

    public virtual DbSet<EstadoPedido> EstadoPedidos { get; set; }

    public virtual DbSet<EstatusFactura> EstatusFacturas { get; set; }

    public virtual DbSet<Factura> Facturas { get; set; }

    public virtual DbSet<FacturaResumen> FacturaResumen { get; set; }

    public virtual DbSet<FormaPago> FormaPagos { get; set; }

    public virtual DbSet<HistorialClienteMayorista> HistorialClienteMayorista { get; set; }

    public virtual DbSet<HistorialFacturasAnulada> HistorialFacturasAnuladas { get; set; }

    public virtual DbSet<HistorialPrecio> HistorialPrecios { get; set; }

    public virtual DbSet<Impuesto> Impuestos { get; set; }

    public virtual DbSet<ImpuestosPorFactura> ImpuestosPorFacturas { get; set; }

    public virtual DbSet<ImpuestosPorParametro> ImpuestosPorParametros { get; set; }

    public virtual DbSet<Moneda> Moneda { get; set; }

    public virtual DbSet<MotivosMovimiento> MotivosMovimientos { get; set; }

    public virtual DbSet<Movimiento> Movimientos { get; set; }

    public virtual DbSet<OtrosCargo> OtrosCargos { get; set; }

    public virtual DbSet<ParametrosEmail> ParametrosEmails { get; set; }

    public virtual DbSet<ParametrosGlobales> ParametrosGlobales { get; set; }

    public virtual DbSet<Pedido> Pedidos { get; set; }

    public virtual DbSet<Persona> Personas { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Proveedores> Proveedores { get; set; }

    public virtual DbSet<RolUsiario> RolUsiarios { get; set; }

    public virtual DbSet<TiempoParaRenovar> TiempoParaRenovar { get; set; }

    public virtual DbSet<TipoMovimiento> TipoMovimientos { get; set; }

    public virtual DbSet<TipoProducto> TipoProductos { get; set; }

    public virtual DbSet<Tracking> Trackings { get; set; }

    public virtual DbSet<TrackingsAsociado> TrackingsAsociados { get; set; }

    public virtual DbSet<UsuaiosEnvioCorreo> UsuaiosEnvioCorreos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-4FJOI9V;Initial Catalog=PuraVidaStore;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bodega>(entity =>
        {
            entity.HasKey(e => e.BdgId).HasName("PK__Bodegas__522D12A5C804A2AD");

            entity.Property(e => e.BdgDescripcion)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("bdgDescripcion");
            entity.Property(e => e.BdgVisible).HasColumnName("bdgVisible");
        });

        modelBuilder.Entity<ClientesMayorista>(entity =>
        {
            entity.HasKey(e => e.ClmId).HasName("PK__Clientes__FD21CFD23095AB57");

            entity.Property(e => e.ClmCorreo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ClmFechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.ClmFechaVencimiento).HasColumnType("datetime");
            entity.Property(e => e.ClmTelefono)
                .HasMaxLength(12)
                .IsUnicode(false);

            entity.HasOne(d => d.ClmIdPersonaNavigation).WithMany(p => p.ClientesMayorista)
                .HasForeignKey(d => d.ClmIdPersona)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ClientesM__ClmId__619B8048");
        });

        modelBuilder.Entity<DetalleFactura>(entity =>
        {
            entity.HasKey(e => e.DtfId).HasName("PK__DetalleF__08270EDE5A494768");

            entity.ToTable("DetalleFactura");

            entity.HasOne(d => d.DtfIdProductoNavigation).WithMany(p => p.DetalleFacturas)
                .HasForeignKey(d => d.DtfIdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetalleFa__DtfId__6383C8BA");

            entity.HasOne(d => d.DtfIdProducto1).WithMany(p => p.DetalleFacturas)
                .HasForeignKey(d => d.DtfIdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetalleFa__DtfId__628FA481");
        });

        modelBuilder.Entity<DetalleProductoPedido>(entity =>
        {
            entity.HasKey(e => e.DppId).HasName("PK__DetalleP__45FE6F5AC06DA845");

            entity.ToTable("DetalleProductoPedido");

            entity.HasOne(d => d.DppIdMonedaNavigation).WithMany(p => p.DetalleProductoPedidos)
                .HasForeignKey(d => d.DppIdMoneda)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetallePr__DppId__66603565");

            entity.HasOne(d => d.DppIdPedidoNavigation).WithMany(p => p.DetalleProductoPedidos)
                .HasForeignKey(d => d.DppIdPedido)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetallePr__DppId__656C112C");

            entity.HasOne(d => d.DppIdProductoNavigation).WithMany(p => p.DetalleProductoPedidos)
                .HasForeignKey(d => d.DppIdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetallePr__DppId__6477ECF3");
        });

        modelBuilder.Entity<EstadoPedido>(entity =>
        {
            entity.HasKey(e => e.EtpId).HasName("PK__EstadoPe__CD1FF58CDFD087A5");

            entity.ToTable("EstadoPedido");

            entity.Property(e => e.EtpDescripcion)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<EstatusFactura>(entity =>
        {
            entity.HasKey(e => e.EtfId).HasName("PK__EstatusF__CA83E8168A46A0AC");

            entity.ToTable("EstatusFactura");

            entity.Property(e => e.EsfDescripcion)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Factura>(entity =>
        {
            entity.HasKey(e => e.FtrId).HasName("PK__Factura__2B314E1FA10BC290");

            entity.ToTable("Factura");

            entity.Property(e => e.FtrCodigoFactura).HasMaxLength(350);
            entity.Property(e => e.FtrFecha).HasColumnType("datetime");

            entity.HasOne(d => d.FtrBodegaNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.FtrBodega)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Factura__FtrBode__6754599E");

            entity.HasOne(d => d.FtrEstatus).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.FtrEstatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Factura__FtrEsta__68487DD7");

            entity.HasOne(d => d.FtrFormaPagoNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.FtrFormaPago)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Factura__FtrForm__693CA210");

            entity.HasOne(d => d.FtrIdUsuarioNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.FtrIdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Factura__FtrIdUs__6A30C649");

            entity.HasOne(d => d.FtrMayoristaNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.FtrMayorista)
                .HasConstraintName("FK__Factura__FtrMayo__6B24EA82");
        });

        modelBuilder.Entity<FacturaResumen>(entity =>
        {
            entity.HasKey(e => e.FtrId).HasName("PK__FacturaR__2B314E1F0C1F3D9D");

            entity.HasOne(d => d.FtrFacturaNavigation).WithMany(p => p.FacturaResumen)
                .HasForeignKey(d => d.FtrFactura)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FacturaRe__FtrFa__6C190EBB");
        });

        modelBuilder.Entity<FormaPago>(entity =>
        {
            entity.HasKey(e => e.FrpId).HasName("PK__FormaPag__C30F48C3DFE13E07");

            entity.ToTable("FormaPago");

            entity.Property(e => e.FrpDescripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<HistorialClienteMayorista>(entity =>
        {
            entity.HasKey(e => e.HcmId).HasName("PK__Historia__3B25B7B86843122C");

            entity.Property(e => e.HcmFechaActualizacion).HasColumnType("datetime");
            entity.Property(e => e.HcmFechaVencimiento).HasColumnType("datetime");

            entity.HasOne(d => d.HcmIdClienteNavigation).WithMany(p => p.HistorialClienteMayorista)
                .HasForeignKey(d => d.HcmIdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Historial__HcmId__6D0D32F4");
        });

        modelBuilder.Entity<HistorialFacturasAnulada>(entity =>
        {
            entity.HasKey(e => e.HlfId).HasName("PK__Historia__A894EAB3D6E3C961");

            entity.Property(e => e.HlfRazon)
                .HasMaxLength(250)
                .IsUnicode(false);

            entity.HasOne(d => d.HlfIdFcturaNavigation).WithMany(p => p.HistorialFacturasAnulada)
                .HasForeignKey(d => d.HlfIdFctura)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Historial__HlfId__6EF57B66");

            entity.HasOne(d => d.HlfIdUsuarioNavigation).WithMany(p => p.HistorialFacturasAnulada)
                .HasForeignKey(d => d.HlfIdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Historial__HlfId__6E01572D");
        });

        modelBuilder.Entity<HistorialPrecio>(entity =>
        {
            entity.HasKey(e => e.HlpId).HasName("PK__Historia__AAE48D2C20410B31");

            entity.Property(e => e.HlpFecha).HasColumnType("datetime");

            entity.HasOne(d => d.HlpIdProductoNavigation).WithMany(p => p.HistorialPrecios)
                .HasForeignKey(d => d.HlpIdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Historial__HlpId__6FE99F9F");

            entity.HasOne(d => d.HlpIdUsuarioNavigation).WithMany(p => p.HistorialPrecios)
                .HasForeignKey(d => d.HlpIdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Historial__HlpId__70DDC3D8");
        });

        modelBuilder.Entity<Impuesto>(entity =>
        {
            entity.HasKey(e => e.ImpId).HasName("PK__Impuesto__B6CB82B86BDE0665");

            entity.Property(e => e.ImpDescripcion)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ImpuestosPorFactura>(entity =>
        {
            entity.HasKey(e => e.IpfId).HasName("PK__Impuesto__C85CA5DAFFFA2841");

            entity.ToTable("ImpuestosPorFactura");

            entity.Property(e => e.IpfId).ValueGeneratedNever();

            entity.HasOne(d => d.IpfIdFacturaNavigation).WithMany(p => p.ImpuestosPorFacturas)
                .HasForeignKey(d => d.IpfIdFactura)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Impuestos__IpfId__71D1E811");

            entity.HasOne(d => d.IpfIdImpuestoNavigation).WithMany(p => p.ImpuestosPorFacturas)
                .HasForeignKey(d => d.IpfIdImpuesto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Impuestos__IpfId__72C60C4A");
        });

        modelBuilder.Entity<ImpuestosPorParametro>(entity =>
        {
            entity.HasKey(e => new { e.ImpPid, e.ImpPidParametroGlobal, e.ImpPidImpuesto }).HasName("PK__Impuesto__D0483333AC4F9899");

            entity.ToTable("ImpuestosPorParametro");

            entity.Property(e => e.ImpPid)
                .ValueGeneratedOnAdd()
                .HasColumnName("ImpPId");
            entity.Property(e => e.ImpPidParametroGlobal).HasColumnName("ImpPIdParametroGlobal");
            entity.Property(e => e.ImpPidImpuesto).HasColumnName("ImpPIdImpuesto");

            entity.HasOne(d => d.ImpPidImpuestoNavigation).WithMany(p => p.ImpuestosPorParametros)
                .HasForeignKey(d => d.ImpPidImpuesto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Impuestos__ImpPI__74AE54BC");

            entity.HasOne(d => d.ImpPidParametroGlobalNavigation).WithMany(p => p.ImpuestosPorParametros)
                .HasForeignKey(d => d.ImpPidParametroGlobal)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Impuestos__ImpPI__73BA3083");
        });

        modelBuilder.Entity<Moneda>(entity =>
        {
            entity.HasKey(e => e.MndId).HasName("PK__Moneda__D29069509129DF0B");

            entity.Property(e => e.MndCodigo)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.MndDescripcion)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MotivosMovimiento>(entity =>
        {
            entity.HasKey(e => e.MtmId).HasName("PK__MotivosM__62D5204F3DE51784");

            entity.Property(e => e.MtmDescripcion)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.MtmIdTipoMovimientoNavigation).WithMany(p => p.MotivosMovimientos)
                .HasForeignKey(d => d.MtmIdTipoMovimiento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MotivosMo__MtmId__75A278F5");
        });

        modelBuilder.Entity<Movimiento>(entity =>
        {
            entity.HasKey(e => e.MvmId).HasName("PK__Movimien__D46E0D01DA218E0F");

            entity.Property(e => e.MvmFecha).HasColumnType("datetime");

            entity.HasOne(d => d.MvmIdBodegaNavigation).WithMany(p => p.Movimientos)
                .HasForeignKey(d => d.MvmIdBodega)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Movimient__MvmId__778AC167");

            entity.HasOne(d => d.MvmIdMotivoMovimientoNavigation).WithMany(p => p.Movimientos)
                .HasForeignKey(d => d.MvmIdMotivoMovimiento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Movimient__MvmId__797309D9");

            entity.HasOne(d => d.MvmIdProductoNavigation).WithMany(p => p.Movimientos)
                .HasForeignKey(d => d.MvmIdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Movimient__MvmId__787EE5A0");

            entity.HasOne(d => d.MvmIdUsuarioNavigation).WithMany(p => p.Movimientos)
                .HasForeignKey(d => d.MvmIdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Movimient__MvmId__76969D2E");
        });

        modelBuilder.Entity<OtrosCargo>(entity =>
        {
            entity.HasKey(e => e.OtrId).HasName("PK__OtrosCar__91381F33DB58D2A5");

            entity.Property(e => e.OtrRazon).HasColumnType("text");

            entity.HasOne(d => d.OtrIdMonedaNavigation).WithMany(p => p.OtrosCargos)
                .HasForeignKey(d => d.OtrIdMoneda)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OtrosCarg__OtrId__7A672E12");

            entity.HasOne(d => d.OtrIdPedidoNavigation).WithMany(p => p.OtrosCargos)
                .HasForeignKey(d => d.OtrIdPedido)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OtrosCarg__OtrId__7B5B524B");
        });

        modelBuilder.Entity<ParametrosEmail>(entity =>
        {
            entity.HasKey(e => e.PreId).HasName("PK__Parametr__7024CEC94BD1B810");

            entity.ToTable("ParametrosEmail");

            entity.Property(e => e.PreId).ValueGeneratedOnAdd();
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

            entity.HasOne(d => d.Pre).WithOne(p => p.ParametrosEmail)
                .HasForeignKey<ParametrosEmail>(d => d.PreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Parametro__PreId__7C4F7684");
        });

        modelBuilder.Entity<ParametrosGlobales>(entity =>
        {
            entity.HasKey(e => e.PrgId).HasName("PK__Parametr__76A0837B5CB7C961");

            entity.HasOne(d => d.PrgIdBodegaNavigation).WithMany(p => p.ParametrosGlobales)
                .HasForeignKey(d => d.PrgIdBodega)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Parametro__PrgId__7E37BEF6");

            entity.HasOne(d => d.PrgIdTiempoNavigation).WithMany(p => p.ParametrosGlobales)
                .HasForeignKey(d => d.PrgIdTiempo)
                .HasConstraintName("FK__Parametro__PrgId__7D439ABD");
        });

        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.HasKey(e => e.PddId).HasName("PK__Pedido__98F0BE93C40A29F8");

            entity.ToTable("Pedido");

            entity.Property(e => e.PddFecha).HasColumnType("datetime");
            entity.Property(e => e.PddRazonCancelada).HasColumnType("text");

            entity.HasOne(d => d.PddEstadoNavigation).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.PddEstado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Pedido__PddEstad__7F2BE32F");

            entity.HasOne(d => d.PddIdUsuarioNavigation).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.PddIdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Pedido__PddIdUsu__00200768");

            entity.HasOne(d => d.PddProveedorNavigation).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.PddProveedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Pedido__PddProve__01142BA1");
        });

        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.PsrId).HasName("PK__Persona__4F16F4C8C78D655E");

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
            entity.HasKey(e => e.PrdId).HasName("PK__Producto__7168B16462DE9164");

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

            entity.HasOne(d => d.PrdIdTipoProductoNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.PrdIdTipoProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Productos__PrdId__02084FDA");
        });

        modelBuilder.Entity<Proveedores>(entity =>
        {
            entity.HasKey(e => e.PvdId).HasName("PK__Proveedo__E82C855367DEC512");

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
            entity.HasKey(e => e.RluId).HasName("PK__RolUsiar__812CEDA628162752");

            entity.ToTable("RolUsiario");

            entity.Property(e => e.RluId).HasColumnName("RluID");
            entity.Property(e => e.RluDescripcion)
                .HasMaxLength(16)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TiempoParaRenovar>(entity =>
        {
            entity.HasKey(e => e.TrpId).HasName("PK__TiempoPa__BDDC11D48A278FFD");

            entity.ToTable("TiempoParaRenovar");

            entity.Property(e => e.TrrDescricpcion)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TipoMovimiento>(entity =>
        {
            entity.HasKey(e => e.TpmId).HasName("PK__TipoMovi__0637E7B007C019CE");

            entity.ToTable("TipoMovimiento");

            entity.Property(e => e.TpmDescripcion)
                .HasMaxLength(15)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TipoProducto>(entity =>
        {
            entity.HasKey(e => e.TppId).HasName("PK__TipoProd__028B0F70F1C1E8DE");

            entity.ToTable("TipoProducto");

            entity.Property(e => e.TppDescripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Tracking>(entity =>
        {
            entity.HasKey(e => e.TrkId).HasName("PK__Trackins__B83DA4B21E9F565C");

            entity.Property(e => e.TrKtrackin)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("TrKTrackin");
            entity.Property(e => e.TrkFecha).HasColumnType("datetime");

            entity.HasOne(d => d.TrkEstadoNavigation).WithMany(p => p.Trackings)
                .HasForeignKey(d => d.TrkEstado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Trackins__TrkEst__02FC7413");

            entity.HasOne(d => d.TrkIdPedidoNavigation).WithMany(p => p.Trackings)
                .HasForeignKey(d => d.TrkIdPedido)
                .HasConstraintName("FK__Trackins__TrkIdP__03F0984C");

            entity.HasOne(d => d.TrkMonedaNavigation).WithMany(p => p.Trackings)
                .HasForeignKey(d => d.TrkMoneda)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Trackins__TrkMon__04E4BC85");

            entity.HasOne(d => d.TrkProveedorNavigation).WithMany(p => p.Trackings)
                .HasForeignKey(d => d.TrkProveedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Trackins__TrkPro__05D8E0BE");
        });

        modelBuilder.Entity<TrackingsAsociado>(entity =>
        {
            entity.HasKey(e => e.TraId).HasName("PK__Trackins__E6FDEF509682187E");

            entity.HasOne(d => d.TraIdTrackinNavigation).WithMany(p => p.TrackingsAsociados)
                .HasForeignKey(d => d.TraIdTrackin)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TrackinsA__TraId__06CD04F7");
        });

        modelBuilder.Entity<UsuaiosEnvioCorreo>(entity =>
        {
            entity.HasKey(e => e.UecId).HasName("PK__UsuaiosE__2A7A0348B41B8016");

            entity.Property(e => e.UecId).ValueGeneratedOnAdd();

            entity.HasOne(d => d.Uec).WithOne(p => p.UsuaiosEnvioCorreo)
                .HasForeignKey<UsuaiosEnvioCorreo>(d => d.UecId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UsuaiosEn__UecId__07C12930");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsrId).HasName("PK__Usuarios__6A1E3D48E7A6746A");

            entity.Property(e => e.UsrId).HasColumnName("UsrID");
            entity.Property(e => e.UsrEmail)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UsrPass).HasMaxLength(8000);
            entity.Property(e => e.UsrUser)
                .HasMaxLength(16)
                .IsUnicode(false);

            entity.HasOne(d => d.UsrIdPersonaNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.UsrIdPersona)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Usuarios__UsrIdP__08B54D69");

            entity.HasOne(d => d.UsrIdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.UsrIdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Usuarios__UsrIdR__09A971A2");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
