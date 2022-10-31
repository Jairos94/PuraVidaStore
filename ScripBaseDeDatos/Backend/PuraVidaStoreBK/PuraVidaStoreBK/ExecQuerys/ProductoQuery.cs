using PuraVidaStoreBK.Models;
using PuraVidaStoreBK.Models.DbContex;

namespace PuraVidaStoreBK.ExecQuerys
{
    public class ProductoQuery
    {
        public object Guardar(ProductosModel producto) 
        {
            try
            {
                using (PuraVidaStoreContext db = new PuraVidaStoreContext()) 
                {
                    var foto= new object();
                    var productoGuardar = new Producto
                    {
                        PrdId = producto.PrdId,
                        PrdCodigo = producto.PrdCodigo,
                        PdrVisible = producto.PdrVisible,
                        PrdCodigoProvedor = producto.PrdCodigoProvedor,
                        //PrdFoto = Convert.ToByte.(producto.PrdFoto.ToArray());
                        PrdIdTipoProducto = producto.PrdIdTipoProducto,
                        PrdNombre=producto.PrdNombre,
                        PrdPrecioVentaMayorista=producto.PrdPrecioVentaMayorista,
                        PrdPrecioVentaMinorista=producto.PrdPrecioVentaMinorista,
                        PrdUnidadesMinimas=producto.PrdUnidadesMinimas,
                    };
                    if (producto.PrdId>0) 
                    {
                        db.Productos.Update(productoGuardar);
                    }
                    else { db.Add(productoGuardar); }
                    db.SaveChanges();
                    producto.PrdId = productoGuardar.PrdId;
                    return producto;
                };
            }
            catch (Exception ex)
            {

                return ex;
            }
        }
    }
}
