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
        private readonly PuraVidaStoreContext dbContex;

        public TipoProductoQuery(PuraVidaStoreContext _dbContex)
        {
            dbContex = _dbContex;
        }

        public async Task<TipoProducto> Guardar(TipoProducto producto)
        {
            
            try
            {
                    if (producto.TppId == 0)
                    {
                       await dbContex.AddAsync(producto);
                    }
                    else 
                    {
                         dbContex.Update(producto);
                    }
                    await dbContex.SaveChangesAsync();
                    
                
                   
            }
            catch (Exception ex)
            {

                Log.Error("Se presentó unn error en Guardar _producto\n"+ex.StackTrace);
                producto = null;
            }
            return producto;
        }

        public async Task<List<TipoProducto>> ListaTipoProducto()
        {
            
                var lista = await dbContex.TipoProductos.ToListAsync();
                return lista;
            
        }

        public async Task<List<TipoProducto>> ListaProductoFiltrado()
        {
            
                var lista = await dbContex.TipoProductos
                    .Where(x=>x.TppVisible==true)
                    .ToListAsync();
                return lista;
            
        }

        public async Task<TipoProducto> ProductoPorId(int id) 
        {
           
                var tipoProducto = await dbContex.TipoProductos.FindAsync(id);
                return tipoProducto;
            
        }

        public async Task<List<TipoProducto>> BuscarTipoProductoPorDescripcion(string busqueda)
        {
            var listaSugerencias = new TipoProducto();
            
                
                    var lista = await dbContex.TipoProductos
                        .Where(x => x.TppDescripcion.Contains(busqueda) ||
                               x.TppId.ToString().Contains(busqueda))
                        .ToListAsync();
                    return lista;
                

            }

            
        }
    }

