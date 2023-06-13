using Microsoft.EntityFrameworkCore;
using PuraVidaStoreBK.ExecQuerys.Interfaces;
using PuraVidaStoreBK.Models.DbContex;
using Serilog;
using XAct.Library.Settings;

namespace PuraVidaStoreBK.ExecQuerys
{
    public class ProductoQuery : IProductoQuery
    {
        private readonly PuraVidaStoreContext dbContex;

        public ProductoQuery(PuraVidaStoreContext _dbContex)
        {
            dbContex = _dbContex;
        }
        public async Task<Producto> GuardarProducto(Producto producto, int idUsuario)
        {
            producto.PrdIdTipoProductoNavigation = null;

            try
            {
                
                    if (producto.PrdId>0) 
                    {

                        dbContex.Productos.Update(producto);

                   
                    }
                    else 
                    {
                        dbContex.Productos.Add(producto);
                        await dbContex.SaveChangesAsync();

                        producto.PrdCodigo = "PVS" + producto.PrdIdTipoProducto.ToString() + "C" + producto.PrdId.ToString();
                        dbContex.Productos.Update(producto);
                        
                    }
                    await dbContex.SaveChangesAsync();

                
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
            try
            {
                
                    var retorno = dbContex.Productos
                           .Where(x => x.PrdCodigo == codigo || x.PrdCodigoProvedor == codigo)
                           .Include(x => x.PrdIdTipoProductoNavigation)
                           .FirstAsync();
                    return await retorno;
                
            }
            catch (Exception ex)
            {
                Log.Error(ex.StackTrace);
                return new Producto();
            }
            
        }
        public async Task<List<Producto>> ListaProductos()
        {
            var listaProducto = new List<Producto>();
            try
            {
                
                    listaProducto = await dbContex.Productos
                        .Include(x=>x.PrdIdTipoProductoNavigation)
                        .ToListAsync();
                    return listaProducto;
                
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
              
                    listaProducto = await dbContex.Productos
                        .Where(x=>(x.PrdNombre.Contains(Descripcion)||
                                  x.PrdCodigo==Descripcion||
                                  x.PrdCodigoProvedor==Descripcion||
                                  x.PrdIdTipoProductoNavigation.TppDescripcion.Contains(Descripcion))&&
                                  x.PdrVisible == true
                                  )
                        .Include(x=>x.PrdIdTipoProductoNavigation)
                        .ToListAsync();
                    return listaProducto;
                
            }
            catch (Exception ex)
            {

                Log.Error (ex.Message,ex);
            }
            return listaProducto;
        }

        public async Task<Producto> ProductoPorId(long id)
        {
            var productoRetorno = new Producto();
            try
            {
                
                    productoRetorno = await dbContex.Productos.FindAsync(id);
                    
                
            }
            catch (Exception ex)
            {

                Log.Error("Se presentó un error en ProductoPorId\n" + ex.StackTrace);
            }
            return productoRetorno;
        }

        public async Task<bool>  GuardarHistorial(Producto producto,int IdUsuario) 
        {
            try
            {
                producto.PrdIdTipoProductoNavigation = null;

                var productoConsulta = await dbContex.Productos.FindAsync(producto.PrdId);


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
                        dbContex.HistorialPrecios.Add(Historial);
                        await dbContex.SaveChangesAsync();
                        
                    }
                    return true;
                
                    
            }
            catch (Exception ex)
            {
                return false;
                Log.Error("Se presentó un error en GuardarHistorial\n" + ex.Message);
            }
            
        }

        public async Task<List<Producto>> ListaProductosFiltrada()
        {
            var listaProducto = new List<Producto>();
            try
            {
                
                    listaProducto = await dbContex.Productos
                        .Where(x => x.PdrVisible == true)
                        .Include(x => x.PrdIdTipoProductoNavigation)
                        .ToListAsync();
                    return listaProducto;
                
            }
            catch (Exception ex)
            {

                Log.Error(ex.Message);
            }
            return listaProducto;
        }

        public async Task<List<Producto>> ListaProductosNoFiltrada()
        {
            var listaProducto = new List<Producto>();
            try
            {
                
                    listaProducto = await dbContex.Productos
                        .Include(x => x.PrdIdTipoProductoNavigation)
                        .ToListAsync();
                    return listaProducto;
                
            }
            catch (Exception ex)
            {

                Log.Error(ex.Message);
            }
            return listaProducto;
        }

        public async Task<List<Producto>> ProductoPorNoFiltradaDescripcion(string Descripcion)
        {
            var listaProducto = new List<Producto>();
            try
            {
               
                    listaProducto = await dbContex.Productos
                        .Where(x => x.PrdNombre.Contains(Descripcion) ||
                                  x.PrdCodigo==Descripcion ||
                                  x.PrdCodigoProvedor==Descripcion||
                                  x.PrdIdTipoProductoNavigation.TppDescripcion.Contains(Descripcion)
                                  )
                        .Include(x => x.PrdIdTipoProductoNavigation)
                        .ToListAsync();
                    return listaProducto;
                
            }
            catch (Exception ex)
            {

                Log.Error("Se presentó un error en ProductoPorDescripcion\n" + ex.Message);
            }
            return listaProducto;
        }
    }
}
