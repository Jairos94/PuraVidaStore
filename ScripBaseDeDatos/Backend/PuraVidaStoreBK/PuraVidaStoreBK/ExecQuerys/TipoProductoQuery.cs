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
    }
}
