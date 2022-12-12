using Microsoft.EntityFrameworkCore;
using PuraVidaStoreBK.ExecQuerys.Interfaces;
using PuraVidaStoreBK.Models.DbContex;
using Serilog;
using XAct.Library.Settings;

namespace PuraVidaStoreBK.ExecQuerys
{
    public class ProductoQuery : IProductoQuery
    {
       

        public async Task<Producto> GuardarProducto(Producto producto, int idUsuario)
        {
            producto.PrdIdTipoProductoNavigation = null;
            if (producto.PrdId>0) 
            {
                await GuardarHistorial(producto, idUsuario);

            }

            try
            {
                using (PuraVidaStoreContext db = new PuraVidaStoreContext()) 
                {
                    if (producto.PrdId>0) 
                    {

                        db.Productos.Update(producto);

                   
                    }
                    else 
                    {
                        db.Productos.Add(producto);
                        await db.SaveChangesAsync();

                        producto.PrdCodigo = "PVS" + producto.PrdIdTipoProducto.ToString() + "C" + producto.PrdId.ToString();
                        db.Productos.Update(producto);
                        
                    }
                    await db.SaveChangesAsync();

                }
                return producto;
            }
            catch (Exception ex)
            {
                //retornoProducto = null;
                Log.Error("Se presentó un error en GuardarProducto\n"+ex.Source);
            }
            return producto;
        }
        public async Task<Producto> BuscarProductoPorCodigo(string codigo)
        {
            using (PuraVidaStoreContext db = new PuraVidaStoreContext()) 
            {
             return await  db.Productos
                    .Where(x=>x.PrdCodigo== codigo || x.PrdCodigoProvedor == codigo)
                    .Include(x=>x.PrdIdTipoProductoNavigation)
                    .FirstAsync();
            }
        }
        public async Task<List<Producto>> ListaProductos()
        {
            var listaProducto = new List<Producto>();
            try
            {
                using (PuraVidaStoreContext db = new PuraVidaStoreContext()) 
                {
                    listaProducto = await db.Productos
                        .Where(x=>x.PdrVisible==true)
                        .Include(x=>x.PrdIdTipoProductoNavigation)
                        .ToListAsync();
                    return listaProducto;
                }
            }
            catch (Exception ex)
            {

                Log.Error(ex.Message);
            }
            return listaProducto;
        }

        public async Task<List<Producto>> ProductoPorDescripcion(string Descripcion)
        {
            var listaProducto = new List<Producto>();
            try
            {
                using (PuraVidaStoreContext db = new PuraVidaStoreContext())
                {
                    listaProducto = await db.Productos
                        .Where(x=>x.PrdNombre.Contains(Descripcion)||
                                  x.PrdCodigo.Contains(Descripcion)||
                                  x.PrdIdTipoProductoNavigation.TppDescripcion.Contains(Descripcion)&&
                                  x.PdrVisible == true
                                  )
                        .Include(x=>x.PrdIdTipoProductoNavigation)
                        .ToListAsync();
                    return listaProducto;
                }
            }
            catch (Exception ex)
            {

                Log.Error("Se presentó un error en ProductoPorDescripcion\n" + ex.Message);
            }
            return listaProducto;
        }

        public async Task<Producto> ProductoPorId(int id)
        {
            var productoRetorno = new Producto();
            try
            {
                using (PuraVidaStoreContext db = new PuraVidaStoreContext()) 
                {
                    productoRetorno = await db.Productos.FindAsync(id);
                    
                }
            }
            catch (Exception ex)
            {

                Log.Error("Se presentó un error en ProductoPorId\n" + ex.StackTrace);
            }
            return productoRetorno;
        }

        private async Task<bool>  GuardarHistorial(Producto producto,int IdUsuario) 
        {
            try
            {
                using (PuraVidaStoreContext db = new PuraVidaStoreContext()) 
                {
                    var productoConsulta = await db.Productos.FindAsync(producto.PrdId);


                    if (productoConsulta.PrdPrecioVentaMayorista != producto.PrdPrecioVentaMayorista ||
                        productoConsulta.PrdPrecioVentaMinorista != producto.PrdPrecioVentaMinorista&&productoConsulta!=null)
                    {
                        var Historial = new HistorialPrecio
                        {
                            HlpIdProducto = producto.PrdId,
                            HlpFecha = DateTime.Now,
                            HlpIdUsuario = IdUsuario,
                            HlpPrecioMayorista = producto.PrdPrecioVentaMayorista,
                            HlpPrecioMinorista = producto.PrdPrecioVentaMinorista,
                            HlpPrecioMayoristaAnterior = productoConsulta.PrdPrecioVentaMayorista,
                            HlpPrecioMinoristaAnterior = productoConsulta.PrdPrecioVentaMinorista

                        };
                        db.HistorialPrecios.Add(Historial);
                        await db.SaveChangesAsync();
                        
                    }
                    return true;
                }
                    
            }
            catch (Exception ex)
            {
                return false;
                Log.Error("Se presentó un error en GuardarHistorial\n" + ex.Message);
            }
            
        }
    }
}
