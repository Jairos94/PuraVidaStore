using PuraVidaStoreBK.Models.DbContex;

namespace PuraVidaStoreBK.ExecQuerys
{
    public class TipoProductoQuery
    {
        public object IngresarTipoProducto(TipoProducto tipoProducto) 
        {
            try
            {
                using (PuraVidaStoreContext db = new PuraVidaStoreContext()) 
                {
                    var dato = db.TipoProductos.Add(tipoProducto);
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
