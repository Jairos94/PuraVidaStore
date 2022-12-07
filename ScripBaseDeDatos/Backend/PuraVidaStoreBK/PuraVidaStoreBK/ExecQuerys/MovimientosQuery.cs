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

        public MovimientosQuery(IDataBase conexion)
        {
            _conexion = conexion;
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
