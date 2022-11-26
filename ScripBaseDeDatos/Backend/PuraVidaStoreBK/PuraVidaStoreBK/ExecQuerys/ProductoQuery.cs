using Microsoft.EntityFrameworkCore;
using PuraVidaStoreBK.ExecQuerys.Interfaces;
using PuraVidaStoreBK.Models.DbContex;
using Serilog;

namespace PuraVidaStoreBK.ExecQuerys
{
    public class ProductoQuery : IProductoQuery
    {
        public async Task<Producto> GuardarProducto(Producto producto)
        {
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
                        db.Add(producto);
                        await db.SaveChangesAsync();

                        producto.PrdCodigo = "PV" + producto.PrdIdTipoProducto.ToString() + "C" + producto.PrdId.ToString();
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

        public async Task<List<Producto>> ListaProductos()
        {
            var listaProducto = new List<Producto>();
            try
            {
                using (PuraVidaStoreContext db = new PuraVidaStoreContext()) 
                {
                    listaProducto = await db.Productos
                        .Where(x=>x.PdrVisible==true)
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
                                  x.PrdCodigo.Contains(Descripcion)
                                  )
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
    }
}
