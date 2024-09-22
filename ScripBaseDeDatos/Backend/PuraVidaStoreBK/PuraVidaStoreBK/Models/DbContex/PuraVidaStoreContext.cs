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

    public virtual DbSet<TiempoParaRenovar> TiempoParaRenovars { get; set; }

    public virtual DbSet<TipoMovimiento> TipoMovimientos { get; set; }

    public virtual DbSet<TipoProducto> TipoProductos { get; set; }

    public virtual DbSet<Tracking> Trackings { get; set; }

    public virtual DbSet<TrackingsAsociado> TrackingsAsociados { get; set; }

    public virtual DbSet<UsuaiosEnvioCorreo> UsuaiosEnvioCorreos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Data Source=JRIVERA;Initial Catalog=PuraVidaStore;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bodega>(entity =>
        {
            entity.HasKey(e => e.BdgId).HasName("PK__Bodegas__522D12A52AEBBC90");

            entity.Property(e => e.BdgDescripcion)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("bdgDescripcion");
            entity.Property(e => e.BdgVisible).HasColumnName("bdgVisible");
        });

        modelBuilder.Entity<ClientesMayorista>(entity =>
        {
            entity.HasKey(e => e.ClmId).HasName("PK__Clientes__FD21CFD2D3A10229");

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
                .HasConstraintName("FK__ClientesM__ClmId__74AE54BC");
        });

        modelBuilder.Entity<DetalleFactura>(entity =>
        {
            entity.HasKey(e => e.DtfId).HasName("PK__DetalleF__08270EDE76687E89");

            entity.ToTable("DetalleFactura");

            entity.Property(e => e.DtfMontoImpuestos).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.DtfPrecio).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.DtfIdProductoNavigation).WithMany(p => p.DetalleFacturas)
                .HasForeignKey(d => d.DtfIdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetalleFa__DtfId__76969D2E");

            entity.HasOne(d => d.DtfIdProducto1).WithMany(p => p.DetalleFacturas)
                .HasForeignKey(d => d.DtfIdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetalleFa__DtfId__75A278F5");
        });

        modelBuilder.Entity<DetalleProductoPedido>(entity =>
        {
            entity.HasKey(e => e.DppId).HasName("PK__DetalleP__45FE6F5AECCF292B");

            entity.ToTable("DetalleProductoPedido");

            entity.Property(e => e.DppCostoColones).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.DppCostoMoneda).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.DppPesoUnitario).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.DppValorMoneda).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.DppIdMonedaNavigation).WithMany(p => p.DetalleProductoPedidos)
                .HasForeignKey(d => d.DppIdMoneda)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetallePr__DppId__797309D9");

            entity.HasOne(d => d.DppIdPedidoNavigation).WithMany(p => p.DetalleProductoPedidos)
                .HasForeignKey(d => d.DppIdPedido)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetallePr__DppId__787EE5A0");

            entity.HasOne(d => d.DppIdProductoNavigation).WithMany(p => p.DetalleProductoPedidos)
                .HasForeignKey(d => d.DppIdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetallePr__DppId__778AC167");
        });

        modelBuilder.Entity<EstadoPedido>(entity =>
        {
            entity.HasKey(e => e.EtpId).HasName("PK__EstadoPe__CD1FF58C00D486AB");

            entity.ToTable("EstadoPedido");

            entity.Property(e => e.EtpDescripcion)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<EstatusFactura>(entity =>
        {
            entity.HasKey(e => e.EtfId).HasName("PK__EstatusF__CA83E816CA0172D5");

            entity.ToTable("EstatusFactura");

            entity.Property(e => e.EsfDescripcion)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Factura>(entity =>
        {
            entity.HasKey(e => e.FtrId).HasName("PK__Factura__2B314E1F2F72FB0A");

            entity.ToTable("Factura");

            entity.Property(e => e.FtrCodigoFactura).HasMaxLength(350);
            entity.Property(e => e.FtrCorreoEnvio)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.FtrFecha).HasColumnType("datetime");

            entity.HasOne(d => d.FtrBodegaNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.FtrBodega)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Factura__FtrBode__7A672E12");

            entity.HasOne(d => d.FtrEstatus).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.FtrEstatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Factura__FtrEsta__7B5B524B");

            entity.HasOne(d => d.FtrFormaPagoNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.FtrFormaPago)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Factura__FtrForm__7C4F7684");

            entity.HasOne(d => d.FtrIdUsuarioNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.FtrIdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Factura__FtrIdUs__7D439ABD");

            entity.HasOne(d => d.FtrMayoristaNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.FtrMayorista)
                .HasConstraintName("FK__Factura__FtrMayo__7E37BEF6");
        });

        modelBuilder.Entity<FacturaResumen>(entity =>
        {
            entity.HasKey(e => e.FtrId).HasName("PK__FacturaR__2B314E1F9C76FD54");

            entity.Property(e => e.FtrCambio).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.FtrMontoImpuestos).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.FtrMontoPagado).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.FtrMontoTotal).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.FtrFacturaNavigation).WithMany(p => p.FacturaResumen)
                .HasForeignKey(d => d.FtrFactura)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FacturaRe__FtrFa__7F2BE32F");
        });

        modelBuilder.Entity<FormaPago>(entity =>
        {
            entity.HasKey(e => e.FrpId).HasName("PK__FormaPag__C30F48C3B6E433AB");

            entity.ToTable("FormaPago");

            entity.Property(e => e.FrpDescripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<HistorialClienteMayorista>(entity =>
        {
            entity.HasKey(e => e.HcmId).HasName("PK__Historia__3B25B7B871861C88");

            entity.Property(e => e.HcmFechaActualizacion).HasColumnType("datetime");
            entity.Property(e => e.HcmFechaVencimiento).HasColumnType("datetime");

            entity.HasOne(d => d.HcmIdClienteNavigation).WithMany(p => p.HistorialClienteMayorista)
                .HasForeignKey(d => d.HcmIdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Historial__HcmId__00200768");
        });

        modelBuilder.Entity<HistorialFacturasAnulada>(entity =>
        {
            entity.HasKey(e => e.HlfId).HasName("PK__Historia__A894EAB37DDA5DED");

            entity.Property(e => e.HlfRazon)
                .HasMaxLength(250)
                .IsUnicode(false);

            entity.HasOne(d => d.HlfIdFcturaNavigation).WithMany(p => p.HistorialFacturasAnulada)
                .HasForeignKey(d => d.HlfIdFctura)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Historial__HlfId__02084FDA");

            entity.HasOne(d => d.HlfIdUsuarioNavigation).WithMany(p => p.HistorialFacturasAnulada)
                .HasForeignKey(d => d.HlfIdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Historial__HlfId__01142BA1");
        });

        modelBuilder.Entity<HistorialPrecio>(entity =>
        {
            entity.HasKey(e => e.HlpId).HasName("PK__Historia__AAE48D2C2ED85ACF");

            entity.Property(e => e.HlpFecha).HasColumnType("datetime");
            entity.Property(e => e.HlpPrecioMayorista).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.HlpPrecioMayoristaAnterior).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.HlpPrecioMinorista).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.HlpPrecioMinoristaAnterior).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.HlpIdProductoNavigation).WithMany(p => p.HistorialPrecios)
                .HasForeignKey(d => d.HlpIdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Historial__HlpId__02FC7413");

            entity.HasOne(d => d.HlpIdUsuarioNavigation).WithMany(p => p.HistorialPrecios)
                .HasForeignKey(d => d.HlpIdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Historial__HlpId__03F0984C");
        });

        modelBuilder.Entity<Impuesto>(entity =>
        {
            entity.HasKey(e => e.ImpId).HasName("PK__Impuesto__B6CB82B84814B374");

            entity.Property(e => e.ImpDescripcion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ImpPorcentaje).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<ImpuestosPorFactura>(entity =>
        {
            entity.HasKey(e => e.IpfId).HasName("PK__Impuesto__C85CA5DA1256514A");

            entity.ToTable("ImpuestosPorFactura");

            entity.Property(e => e.IpfPorcentaje).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.IpfIdFacturaNavigation).WithMany(p => p.ImpuestosPorFacturas)
                .HasForeignKey(d => d.IpfIdFactura)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Impuestos__IpfId__04E4BC85");

            entity.HasOne(d => d.IpfIdImpuestoNavigation).WithMany(p => p.ImpuestosPorFacturas)
                .HasForeignKey(d => d.IpfIdImpuesto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Impuestos__IpfId__05D8E0BE");
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
                .HasConstraintName("FK__Impuestos__ImpPI__07C12930");

            entity.HasOne(d => d.ImpPidParametroGlobalNavigation).WithMany(p => p.ImpuestosPorParametros)
                .HasForeignKey(d => d.ImpPidParametroGlobal)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Impuestos__ImpPI__06CD04F7");
        });

        modelBuilder.Entity<Moneda>(entity =>
        {
            entity.HasKey(e => e.MndId).HasName("PK__Moneda__D2906950766F34A9");

            entity.Property(e => e.MndCodigo)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.MndDescripcion)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MotivosMovimiento>(entity =>
        {
            entity.HasKey(e => e.MtmId).HasName("PK__MotivosM__62D5204F3A57B52A");

            entity.Property(e => e.MtmDescripcion)
                .HasMaxLength(250)
                .IsUnicode(false);

            entity.HasOne(d => d.MtmIdTipoMovimientoNavigation).WithMany(p => p.MotivosMovimientos)
                .HasForeignKey(d => d.MtmIdTipoMovimiento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MotivosMo__MtmId__08B54D69");
        });

        modelBuilder.Entity<Movimiento>(entity =>
        {
            entity.HasKey(e => e.MvmId).HasName("PK__Movimien__D46E0D0145E229C6");

            entity.Property(e => e.MvmFecha).HasColumnType("datetime");

            entity.HasOne(d => d.MvmIdBodegaNavigation).WithMany(p => p.Movimientos)
                .HasForeignKey(d => d.MvmIdBodega)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Movimient__MvmId__0A9D95DB");

            entity.HasOne(d => d.MvmIdMotivoMovimientoNavigation).WithMany(p => p.Movimientos)
                .HasForeignKey(d => d.MvmIdMotivoMovimiento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Movimient__MvmId__0C85DE4D");

            entity.HasOne(d => d.MvmIdProductoNavigation).WithMany(p => p.Movimientos)
                .HasForeignKey(d => d.MvmIdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Movimient__MvmId__0B91BA14");

            entity.HasOne(d => d.MvmIdUsuarioNavigation).WithMany(p => p.Movimientos)
                .HasForeignKey(d => d.MvmIdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Movimient__MvmId__09A971A2");
        });

        modelBuilder.Entity<OtrosCargo>(entity =>
        {
            entity.HasKey(e => e.OtrId).HasName("PK__OtrosCar__91381F33D31F5DB4");

            entity.Property(e => e.OtrCostoMoneda).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.OtrRazon).HasColumnType("text");
            entity.Property(e => e.OtrValorMoneda).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.OtrIdMonedaNavigation).WithMany(p => p.OtrosCargos)
                .HasForeignKey(d => d.OtrIdMoneda)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OtrosCarg__OtrId__0D7A0286");

            entity.HasOne(d => d.OtrIdPedidoNavigation).WithMany(p => p.OtrosCargos)
                .HasForeignKey(d => d.OtrIdPedido)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OtrosCarg__OtrId__0E6E26BF");
        });

        modelBuilder.Entity<ParametrosEmail>(entity =>
        {
            entity.HasKey(e => e.PreId).HasName("PK__Parametr__7024CEC9CF44488D");

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
                .HasConstraintName("FK__Parametro__PreId__0F624AF8");
        });

        modelBuilder.Entity<ParametrosGlobales>(entity =>
        {
            entity.HasKey(e => e.PrgId).HasName("PK__Parametr__76A0837B499B0A43");

            entity.Property(e => e.PrgCedula)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PrgImpresora)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PrgNombreNegocio)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.PrgIdBodegaNavigation).WithMany(p => p.ParametrosGlobales)
                .HasForeignKey(d => d.PrgIdBodega)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Parametro__PrgId__114A936A");

            entity.HasOne(d => d.PrgIdTiempoNavigation).WithMany(p => p.ParametrosGlobales)
                .HasForeignKey(d => d.PrgIdTiempo)
                .HasConstraintName("FK__Parametro__PrgId__10566F31");
        });

        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.HasKey(e => e.PddId).HasName("PK__Pedido__98F0BE938B3BEFDB");

            entity.ToTable("Pedido");

            entity.Property(e => e.PddFecha).HasColumnType("datetime");
            entity.Property(e => e.PddRazonCancelada).HasColumnType("text");

            entity.HasOne(d => d.PddEstadoNavigation).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.PddEstado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Pedido__PddEstad__123EB7A3");

            entity.HasOne(d => d.PddIdUsuarioNavigation).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.PddIdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Pedido__PddIdUsu__1332DBDC");

            entity.HasOne(d => d.PddProveedorNavigation).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.PddProveedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Pedido__PddProve__14270015");
        });

        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.PsrId).HasName("PK__Persona__4F16F4C895F9CC9F");

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
            entity.HasKey(e => e.PrdId).HasName("PK__Producto__7168B1646D8D5E40");

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
            entity.Property(e => e.PrdPrecioVentaMayorista).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PrdPrecioVentaMinorista).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.PrdIdTipoProductoNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.PrdIdTipoProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Productos__PrdId__151B244E");
        });

        modelBuilder.Entity<Proveedores>(entity =>
        {
            entity.HasKey(e => e.PvdId).HasName("PK__Proveedo__E82C8553F1EF1915");

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
            entity.HasKey(e => e.RluId).HasName("PK__RolUsiar__812CEDA60173B14F");

            entity.ToTable("RolUsiario");

            entity.Property(e => e.RluId).HasColumnName("RluID");
            entity.Property(e => e.RluDescripcion)
                .HasMaxLength(16)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TiempoParaRenovar>(entity =>
        {
            entity.HasKey(e => e.TrpId).HasName("PK__TiempoPa__BDDC11D4B146A0A7");

            entity.ToTable("TiempoParaRenovar");

            entity.Property(e => e.TrrDescricpcion)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TipoMovimiento>(entity =>
        {
            entity.HasKey(e => e.TpmId).HasName("PK__TipoMovi__0637E7B07E20E450");

            entity.ToTable("TipoMovimiento");

            entity.Property(e => e.TpmDescripcion)
                .HasMaxLength(15)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TipoProducto>(entity =>
        {
            entity.HasKey(e => e.TppId).HasName("PK__TipoProd__028B0F701194A95E");

            entity.ToTable("TipoProducto");

            entity.Property(e => e.TppDescripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Tracking>(entity =>
        {
            entity.HasKey(e => e.TrkId).HasName("PK__Tracking__B83DA4B2A1212AC4");

            entity.Property(e => e.TrKtrackin)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("TrKTrackin");
            entity.Property(e => e.TrkCostoMoneda).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TrkFecha).HasColumnType("datetime");
            entity.Property(e => e.TrkMedidaAlturaCm).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TrkMedidaAnchoCm).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TrkMedidaLargoCm).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TrkPesoProveedor).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TrkPesoReal).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TrkValorMoneda).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.TrkEstadoNavigation).WithMany(p => p.Trackings)
                .HasForeignKey(d => d.TrkEstado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Trackings__TrkEs__160F4887");

            entity.HasOne(d => d.TrkIdPedidoNavigation).WithMany(p => p.Trackings)
                .HasForeignKey(d => d.TrkIdPedido)
                .HasConstraintName("FK__Trackings__TrkId__17036CC0");

            entity.HasOne(d => d.TrkMonedaNavigation).WithMany(p => p.Trackings)
                .HasForeignKey(d => d.TrkMoneda)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Trackings__TrkMo__17F790F9");

            entity.HasOne(d => d.TrkProveedorNavigation).WithMany(p => p.Trackings)
                .HasForeignKey(d => d.TrkProveedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Trackings__TrkPr__18EBB532");
        });

        modelBuilder.Entity<TrackingsAsociado>(entity =>
        {
            entity.HasKey(e => e.TraId).HasName("PK__Tracking__E6FDEF50BFF24A9F");

            entity.HasOne(d => d.TraIdTrackinNavigation).WithMany(p => p.TrackingsAsociados)
                .HasForeignKey(d => d.TraIdTrackin)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Trackings__TraId__19DFD96B");
        });

        modelBuilder.Entity<UsuaiosEnvioCorreo>(entity =>
        {
            entity.HasKey(e => e.UecId).HasName("PK__UsuaiosE__2A7A034849752A3C");

            entity.Property(e => e.UecId).ValueGeneratedOnAdd();

            entity.HasOne(d => d.Uec).WithOne(p => p.UsuaiosEnvioCorreo)
                .HasForeignKey<UsuaiosEnvioCorreo>(d => d.UecId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UsuaiosEn__UecId__1AD3FDA4");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsrId).HasName("PK__Usuarios__6A1E3D482D103113");

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
                .HasConstraintName("FK__Usuarios__UsrIdP__1BC821DD");

            entity.HasOne(d => d.UsrIdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.UsrIdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Usuarios__UsrIdR__1CBC4616");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
