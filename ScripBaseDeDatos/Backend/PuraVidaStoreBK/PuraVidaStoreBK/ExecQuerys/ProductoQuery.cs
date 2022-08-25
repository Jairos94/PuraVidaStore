using PuraVidaStoreBK.Models.DbContex;

namespace PuraVidaStoreBK.ExecQuerys
{
    public class ProductoQuery
    {
        public object IngresarProducto(Producto producto) 
        {
            try
            {
                
                using (PuraVidaStoreContext db = new PuraVidaStoreContext) 
                {
                   var dato= db.Productos.Add(producto);
                    db.SaveChanges();

                    return dato;
                };
            }
            catch (Exception ex)
            {

                return ex;
            }
        }
    }
}
