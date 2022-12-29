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

        public MovimientosQuery(IDataBase conexion,IProductoQuery producto)
        {
            _conexion = conexion;
            _producto = producto;
        }

        public async Task<bool> IngresarProductosAlInventario(List<Inventarios> Inventarios, int IdBodega, int IdUsuario, int Motivo)
        {
            var retorno = false;
            try
            {
                using (PuraVidaStoreContext db = new PuraVidaStoreContext()) 
                {
                    Inventarios.ForEach(async x =>
                    {

                        x.producto.PdrVisible = true;
                        x.producto.PdrTieneExistencias = true;


                        await _producto.GuardarProducto(x.producto,IdUsuario);
                        
                        var movimiento = new Movimiento 
                        {
                            MvmIdProducto = x.producto.PrdId,
                            MvmCantidad = x.CantidadExistencia,
                            MvmFecha = DateTime.Now,
                            MvmIdMotivoMovimiento = Motivo,
                            MvmIdUsuario = IdUsuario,
                            MvmIdBodega = IdBodega,
                        };

                        await ingresarMovimmiento(movimiento);
                    });
                    retorno = true;
                }
                    
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
                using (PuraVidaStoreContext db = new PuraVidaStoreContext())
                {
                    var MotivosMovimiento = await db.MotivosMovimientos
                        .Where(x => x.MtmDescripcion.Contains("Ingreso por compra"))
                        .FirstOrDefaultAsync();

                    movimiento.MvmFecha = DateTime.Now;
                    movimiento.MvmIdMotivoMovimiento = MotivosMovimiento.MtmId;

                    return await ingresarMovimmiento(movimiento);
                }
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
                using (PuraVidaStoreContext db = new PuraVidaStoreContext()) 
                {
                    foreach (var ProductoCantidad in listaIds) 
                    {
                        var producto = await db.Productos.Where(x=>x.PrdId==ProductoCantidad.idProducto)
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

                                db.Productos.Update(producto);
                                await db.SaveChangesAsync();
                            }
                            ListaProductos.Add(inventario);
                        }
                        else 
                        {
                            producto.PdrTieneExistencias = false;

                            db.Productos.Update(producto);
                            await db.SaveChangesAsync();
                        }
                    }
                }
                   
            }
            catch (Exception ex)
            {

                Log.Error(ex.StackTrace);
            }
            return ListaProductos;
           
        }

        public async Task<List<Inventarios>> PorBusqueda(int IdBodega, string buscador)
        {
            var listaIds = await listaIdProductos(IdBodega);
            var ListaProductos = new List<Inventarios>();
            try
            {
                using (PuraVidaStoreContext db = new PuraVidaStoreContext())
                {
                    foreach (var ProductoCantidad in listaIds)
                    {
                        var producto = await db.Productos.Where(x => 
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

            }
            catch (Exception ex)
            {

                Log.Error(ex.StackTrace);
            }
            return ListaProductos;
        }

        private async Task<Movimiento> ingresarMovimmiento(Movimiento movimiento) 
        {
            try
            {
                using (PuraVidaStoreContext db = new PuraVidaStoreContext())
                {

                    db.Movimientos.Add(movimiento);
                    await db.SaveChangesAsync();
                    return movimiento;
                }
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
                        idProducto = reader.GetInt32(0),
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
