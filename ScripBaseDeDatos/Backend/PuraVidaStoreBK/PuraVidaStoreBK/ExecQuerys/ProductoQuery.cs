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
                        PdrFoto = producto.PdrFoto
                    };
                    if (producto.PrdId>0) 
                    {
                        db.Productos.Update(productoGuardar);
                        db.SaveChanges();
                    }
                    else 
                    { 
                        db.Productos.Add(productoGuardar);
                        db.SaveChanges();

                        productoGuardar.PrdCodigo = "PVS" + productoGuardar.PrdIdTipoProducto.ToString() + "C" + productoGuardar.PrdId.ToString();
                        db.Productos.Update(productoGuardar);
                        db.SaveChanges();

                        producto.PrdCodigo = productoGuardar.PrdCodigo;
                    }

                    producto.PrdId = productoGuardar.PrdId;
                    
                  
                    return producto;
                };
            }
            catch (Exception ex)
            {

                return ex;
            }
        }

        public object ObtenerProductoPorId(int id) 
        {
            try
            {
                ProductosModel ProductoRetorno = new ProductosModel();
                using (PuraVidaStoreContext db = new PuraVidaStoreContext()) 
                {

                    var Producto =db.Productos.Find(id);
                    if(Producto != null) 
                    {
                        ProductoRetorno.PrdId = Producto.PrdId;
                        ProductoRetorno.PrdNombre= Producto.PrdNombre;
                        ProductoRetorno.PrdPrecioVentaMayorista = Producto.PrdPrecioVentaMinorista;
                        ProductoRetorno.PrdPrecioVentaMinorista =Producto.PrdPrecioVentaMinorista;
                        ProductoRetorno.PrdCodigo = Producto.PrdCodigo;
                        ProductoRetorno.PrdCodigoProvedor = Producto.PrdCodigoProvedor;
                        ProductoRetorno.PrdIdTipoProducto = Producto.PrdIdTipoProducto;
                        ProductoRetorno.PrdUnidadesMinimas = Producto.PrdUnidadesMinimas;  
                        ProductoRetorno.PdrFoto = Producto.PdrFoto;
                        ProductoRetorno.PdrVisible = Producto.PdrVisible;

                    }
                  
                }
                return ProductoRetorno;
            }
            catch (Exception ex)
            {

                return ex;
            }
        }
    }
}
