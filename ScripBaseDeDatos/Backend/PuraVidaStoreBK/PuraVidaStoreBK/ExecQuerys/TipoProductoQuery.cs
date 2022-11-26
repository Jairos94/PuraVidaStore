using Microsoft.EntityFrameworkCore;
using PuraVidaStoreBK.ExecQuerys.Interfaces;
using PuraVidaStoreBK.Models;
using PuraVidaStoreBK.Models.DbContex;
using Serilog;
using System.Linq;

namespace PuraVidaStoreBK.ExecQuerys
{
    public class TipoProductoQuery: ITipoProductoQuery
    {
        
       

        public async Task<TipoProducto> Guardar(TipoProducto producto)
        {
            
            try
            {
                using (PuraVidaStoreContext db = new PuraVidaStoreContext())
                {
                    if (producto.TppId == 0)
                    {
                       await db.AddAsync(producto);
                    }
                    else 
                    {
                         db.Update(producto);
                    }
                    await db.SaveChangesAsync();
                    
                }
                   
            }
            catch (Exception ex)
            {

                Log.Error("Se presentó unn error en Guardar producto\n"+ex.StackTrace);
                producto = null;
            }
            return producto;
        }

        public async Task<List<TipoProducto>> ListaTipoProducto()
        {
            using (PuraVidaStoreContext db = new PuraVidaStoreContext()) 
            {
                var lista = await db.TipoProductos.ToListAsync();
                return lista;
            }
        }

        public async Task<List<TipoProducto>> ListaProductoFiltrado()
        {
            using (PuraVidaStoreContext db = new PuraVidaStoreContext())
            {
                var lista = await db.TipoProductos
                    .Where(x=>x.TppVisible==true)
                    .ToListAsync();
                return lista;
            }
        }

        public async Task<TipoProducto> ProductoPorId(int id) 
        {
            using (PuraVidaStoreContext db = new PuraVidaStoreContext()) 
            {
                var tipoProducto = await db.TipoProductos.FindAsync(id);
                return tipoProducto;
            }
        }

        public async Task<List<TipoProducto>> BuscarTipoProductoPorDescripcion(string busqueda)
        {
            var listaSugerencias = new TipoProducto();
            
                using (PuraVidaStoreContext db = new PuraVidaStoreContext())
                {
                    var lista = await db.TipoProductos
                        .Where(x => x.TppDescripcion.Contains(busqueda) ||
                               x.TppId.ToString().Contains(busqueda))
                        .ToListAsync();
                    return lista;
                }

            }

            
        }
    }

