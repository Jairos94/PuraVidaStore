using PuraVidaStoreBK.Models;
using PuraVidaStoreBK.Models.DbContex;
using System.Linq;

namespace PuraVidaStoreBK.ExecQuerys
{
    public class TipoProductoQuery
    {
        public object Guardar(TipoProducto tipoProducto) 
        {
            try
            {
                using (PuraVidaStoreContext db = new PuraVidaStoreContext()) 
                {
                    var dato = new object();
                    if (tipoProducto.TppId==0) 
                    {
                        dato = db.TipoProductos.Add(tipoProducto);
                    }
                    else 
                    {
                        dato = db.TipoProductos.Update(tipoProducto);
                    }
                    
                    db.SaveChanges();
                    var retorno = new TipoProductoModel();
                    retorno.TppId = tipoProducto.TppId;
                    retorno.TppDescripcion = tipoProducto.TppDescripcion;
                    retorno.TppVisible = tipoProducto.TppVisible;
                    return retorno;
                }
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

        public object ListaTipoProducto() 
        {
            try
            {
                using (PuraVidaStoreContext db = new PuraVidaStoreContext()) 
                {
                    return db.TipoProductos.ToList();
                }
            }
            catch (Exception ex)
            {

                return ex;
            }
        }
        public object ListaProductoFiltrado() 
        {
            try
            {
                using (PuraVidaStoreContext db = new PuraVidaStoreContext()) 
                {
                    return db.TipoProductos.Where(tp=>tp.TppVisible==true).ToList();
                }
            }
            catch (Exception ex)
            {

                return ex;
            }
        }

        public object TipoProductoPorId(int id)
        {
            try
            {
                using (PuraVidaStoreContext db = new PuraVidaStoreContext())
                {
                    return db.TipoProductos.FindAsync(id);
                }
            }
            catch (Exception ex)
            {

                return ex;
            }
        }

        public object BuscarTipoProductoPorDescripcion(string descripcion) 
        {
            try
            {
                using (PuraVidaStoreContext db = new PuraVidaStoreContext()) 
                {
                    var listaTipoProductos = db.TipoProductos.Where(x => x.TppDescripcion.Contains(descripcion)).ToList();
                    var productosRetorno = new List<TipoProductoModel>();
                    foreach (var dato in listaTipoProductos) 
                    {
                        var tipoProducto = new TipoProductoModel 
                        {
                            TppId=dato.TppId,
                            TppDescripcion=dato.TppDescripcion,
                            TppVisible=dato.TppVisible,
                        };
                        productosRetorno.Add(tipoProducto);
                    }
                    return productosRetorno;
                }
            }
            catch (Exception ex)
            {

                return ex;
            }
        }
    }
}
