using PuraVidaStoreBK.Models.DbContex;

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
                    return dato;
                }
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }
    }
}
