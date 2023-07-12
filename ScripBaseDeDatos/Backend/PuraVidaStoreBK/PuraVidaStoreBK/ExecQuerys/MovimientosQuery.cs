using Microsoft.EntityFrameworkCore;
using PuraVidaStoreBK.ExecQuerys.Interfaces;
using PuraVidaStoreBK.Models.DbContex;
using Serilog;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace PuraVidaStoreBK.ExecQuerys
{
    public class MovimientosQuery : IMovimientosQuery
    {
        private readonly IDataBase _conexion;
        private readonly IProductoQuery _producto;
        private readonly PuraVidaStoreContext _dbContex;

        public MovimientosQuery(IDataBase conexion,IProductoQuery producto, PuraVidaStoreContext dbContex)
        {
            _conexion = conexion;
            _producto = producto;
            _dbContex = dbContex;
        }

        public async Task<bool> GuardarAjuste(Inventarios inventario, int IdBodega, int idUsuario, int Motivo)
        {
            var retorno = false;
            try
            {
                    var movimiento = new Movimiento
                    {
                        MvmIdProducto = inventario.producto.PrdId,
                        MvmCantidad = inventario.CantidadExistencia,
                        MvmFecha = DateTime.Now,
                        MvmIdMotivoMovimiento = Motivo,
                        MvmIdUsuario = idUsuario,
                        MvmIdBodega = IdBodega,
                    };

                    await IngresarMotimoMovimiento(movimiento);
                   
                    retorno = true;
                

            }
            catch (Exception)
            {

                throw;
            }
            return retorno;
        }

        public async Task<MotivosMovimiento> GuardarMotivoMovimiento(MotivosMovimiento motivosMovimiento)
        {
            try
            {
              
                    if (motivosMovimiento.MtmId==0) 
                    {
                        _dbContex.MotivosMovimientos.Add(motivosMovimiento);
                    } else 
                    { 
                         _dbContex.MotivosMovimientos.Update(motivosMovimiento);

                    }

                    await _dbContex.SaveChangesAsync();
                    return motivosMovimiento;
                
            }
            catch (Exception ex)
            {
                Log.Error(ex.StackTrace);
                return new MotivosMovimiento();
            }
            
        }

        public async Task<bool> IngresarProductosAlInventario(List<Inventarios> Inventarios, int IdBodega, int IdUsuario, int Motivo)
        {
            var retorno = false;
            try
            {
               
                    Inventarios.ForEach(async x =>
                    {

                        var movimiento = new Movimiento
                        {
                            MvmIdProducto = x.producto.PrdId,
                            MvmCantidad = x.CantidadExistencia,
                            MvmFecha = DateTime.Now,
                            MvmIdMotivoMovimiento = Motivo,
                            MvmIdUsuario = IdUsuario,
                            MvmIdBodega = IdBodega,
                        };

                        await IngresarMotimoMovimiento(movimiento);
                    });
                    retorno = true;
               

            }
            catch (Exception)
            {

                throw;
            }
            return retorno;
        }

        public async Task<Movimiento> IngresoDeProductosPorCompra(Movimiento movimiento)
        {

            try
            {
                
                    var MotivosMovimiento = await _dbContex.MotivosMovimientos
                        .Where(x => x.MtmDescripcion.Contains("Ingreso por compra"))
                        .FirstOrDefaultAsync();

                    movimiento.MvmFecha = DateTime.Now;
                    movimiento.MvmIdMotivoMovimiento = MotivosMovimiento.MtmId;

                    return await IngresarMotimoMovimiento(movimiento);
                
            }
            catch (Exception ex)
            {

                Log.Error(ex.StackTrace);
                return new Movimiento();
            }
           
        }

        public async Task<List<Inventarios>> ListaInventarios(int IdBodega)
        {
            var listaIds = await listaIdProductos(IdBodega);
            var ListaProductos = new List<Inventarios>();
            try
            {
                
                    foreach (var ProductoCantidad in listaIds) 
                    {
                        var producto = await _dbContex.Productos.Where(x=>x.PrdId==ProductoCantidad.idProducto)
                                                         .Include(x=>x.PrdIdTipoProductoNavigation)
                                                         .FirstOrDefaultAsync();
                       
                        var inventario = new Inventarios();
                        inventario.producto = producto;
                        inventario.CantidadExistencia = ProductoCantidad.Cantidad;
                        if (inventario.CantidadExistencia != 0)
                        {
                            if (producto.PdrTieneExistencias == false || producto.PdrTieneExistencias == null || producto.PdrVisible == false || producto.PdrVisible == null)
                            {
                                producto.PdrVisible = true;
                                producto.PdrTieneExistencias = true;

                                _dbContex.Productos.Update(producto);
                                await _dbContex.SaveChangesAsync();
                            }
                            ListaProductos.Add(inventario);
                        }
                        else 
                        {
                            producto.PdrTieneExistencias = false;

                            _dbContex.Productos.Update(producto);
                            await _dbContex.SaveChangesAsync();
                        }
                    }
                
                   
            }
            catch (Exception ex)
            {

                Log.Error(ex.StackTrace);
            }
            return ListaProductos;
           
        }

        public async Task<List<MotivosMovimiento>> ObtenerListaMotivoMovimientoPorDescripcion(string descripcion)
        {
            try
            {
                
                    var retorno = await _dbContex.MotivosMovimientos
                           .Where(x =>
                           x.MtmId != 1 &&
                           (x.MtmDescripcion.Contains(descripcion) ||
                           x.MtmIdTipoMovimientoNavigation.TpmDescripcion.Contains(descripcion)))
                           .Include(x => x.MtmIdTipoMovimientoNavigation)
                           .ToListAsync();
                    return retorno;
                
            }
            catch (Exception ex)
            {

                Log.Error(ex.StackTrace);
                return new List<MotivosMovimiento>();
            }
          
        }

        public async Task<List<MotivosMovimiento>> ObtenerListaMotivosMovimiento()
        {
            try
            {
               
                    return await _dbContex.MotivosMovimientos
                           .Where(x=>x.MtmId!=1 && !x.MtmDescripcion.Contains("Traslado"))
                           .Include(x => x.MtmIdTipoMovimientoNavigation)
                           .ToListAsync();
                
            }
            catch (Exception ex)
            {

                Log.Error(ex.StackTrace);
                return new List<MotivosMovimiento>();
            }
        }

        public async Task<List<TipoMovimiento>> ObtenerListaTipoMovimiento()
        {
            try
            {
               
                    var retorno = await _dbContex.TipoMovimientos.ToListAsync();
                    return retorno;
                
            }
            catch (Exception ex)
            {
                Log.Error(ex.StackTrace);
                throw;
            }
        }

        public async Task<MotivosMovimiento> ObtenerMotivoMovmientoPorId(int id)
        {
            try
            {
                
                    return await _dbContex.MotivosMovimientos
                         .Where(x => x.MtmId == id)
                         .Include(x=>x.MtmIdTipoMovimientoNavigation)
                         .FirstOrDefaultAsync();
                
            }
            catch (Exception ex)
            {

                Log.Error(ex.StackTrace);
                return new MotivosMovimiento();
            }
       
        }


        public async Task<List<Inventarios>> PorBusqueda(int IdBodega, string buscador)
        {
            var listaIds = await listaIdProductos(IdBodega);
            var ListaProductos = new List<Inventarios>();
            try
            {
               
                    foreach (var ProductoCantidad in listaIds)
                    {
                        var producto = await _dbContex.Productos.Where(x => 
                                                                x.PrdId == ProductoCantidad.idProducto && (
                                                                x.PrdNombre.Contains(buscador) ||
                                                                x.PrdCodigo.Contains(buscador)||
                                                                x.PrdCodigoProvedor.Contains(buscador)||
                                                                x.PrdIdTipoProductoNavigation.TppDescripcion.Contains(buscador)
                                                                ))
                                                         .Include(x => x.PrdIdTipoProductoNavigation)
                                                         .FirstOrDefaultAsync();
                        var inventario = new Inventarios();
                        inventario.producto = producto;
                        inventario.CantidadExistencia = ProductoCantidad.Cantidad;
                        if (inventario.CantidadExistencia != 0)
                        {
                            ListaProductos.Add(inventario);
                        }
                    }
                

            }
            catch (Exception ex)
            {

                Log.Error(ex.StackTrace);
            }
            return ListaProductos;
        }

        public async Task<List<MotivosMovimiento>> BusquedaPorDescripcion(string descripcion) 
        {
            var lista = await _dbContex.MotivosMovimientos.Where(x=>x.MtmDescripcion==descripcion).ToListAsync();
            return lista;
        }
        public async Task<List<MotivosMovimiento>> GuardarListaMotivos(List<MotivosMovimiento> movimientos) 
        {
            _dbContex.MotivosMovimientos.AddRange(movimientos);
            await _dbContex.SaveChangesAsync();
            return movimientos;
        }

        public async Task<List<Movimiento>> GuardarListaMovimientos(List<Movimiento> movimientos) 
        {
            _dbContex.Movimientos.AddRange(movimientos);
            await _dbContex.SaveChangesAsync();
            return movimientos;
        }


        private async Task<Movimiento> IngresarMotimoMovimiento(Movimiento movimiento) 
        {
            try
            {
              

                    _dbContex.Movimientos.Add(movimiento);
                    await _dbContex.SaveChangesAsync();
                    return movimiento;
                
            }
            catch (Exception ex)
            {
                Log.Error(ex.StackTrace);
                return new Movimiento();
            }
        }

        

        private async Task< List<IdProductosPorCantidad>> listaIdProductos(int idBodega) 
        {
            SqlConnection conn = _conexion.GetConnection();
            List<IdProductosPorCantidad> ListaInventarios = new List<IdProductosPorCantidad>();
            try
            {
                SqlDataReader reader;
                SqlCommand command = conn.CreateCommand();
                conn.Open();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_Inventarios";
                command.Parameters.Add("@IdBodega", SqlDbType.Int).Value = idBodega;
                //command.ExecuteNonQuery();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var IdProductos = new IdProductosPorCantidad 
                    {
                        idProducto = reader.GetInt64(0),
                        Cantidad = reader.GetInt32(1)
                    };
                    ListaInventarios.Add(IdProductos);
                }
                return ListaInventarios;
            }

            catch (Exception ex)
            {
                Log.Information("Se presentó un error en Get Usuario\n" + ex);
                return ListaInventarios;
            }
            finally
            { conn.Close(); }
        }


    }
}
