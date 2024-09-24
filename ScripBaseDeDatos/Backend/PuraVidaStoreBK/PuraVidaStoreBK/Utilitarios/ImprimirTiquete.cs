using PuraVidaStoreBK.Models.DbContex;
using PuraVidaStoreBK.Utilitarios.Interfase;
using System.Drawing;
using System.Drawing.Printing;

namespace PuraVidaStoreBK.Utilitarios
{
	public class ImprimirTiquete : IImprimirTiquete
	{
		private readonly IConfiguration _configuration;

		public ImprimirTiquete(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public void Imprimir(Factura factura, ParametrosGlobales parametros)
		{
			var pd = new PrintDocument();
			PrinterSettings ps = new PrinterSettings();

			// Establecer la impresora
			ps.PrinterName = parametros.PrgImpresora;
			pd.PrinterSettings = ps;

			// Pasar la factura a ImprimirDocumento
			pd.PrintPage += (sender, e) => ImprimirDocumento(sender, e, factura);

			// Iniciar impresión
			pd.Print();
		}


		private void ImprimirDocumento(object sender, PrintPageEventArgs e, Factura factura) 
		{
			/*
			 * Fuente para el titulo
			 * Variables para e sistema
			 */
			Font fuenteTitulo = new Font("Arial", 14, FontStyle.Bold, GraphicsUnit.Point);
			Font sistemaTitulo = new Font("Arial", 12, FontStyle.Regular, GraphicsUnit.Point);


			/*
			 * new RectangleF(Espacio en X,Espacio en Y,Ancho del papel,Tamanio del rectangulo a lo ancho)
			 */

			float ancho = 200;
			float y = 10;
			string separador = new string('-', 400);

			e.Graphics.DrawString(_configuration["NombreTienda"], fuenteTitulo, Brushes.Black, new RectangleF(10,y,ancho,20));
			y = +45;

			/*Estan en la misma linea*/
			e.Graphics.DrawString(factura.FtrBodegaNavigation.BdgDescripcion, sistemaTitulo, Brushes.Black, new RectangleF(0,y,ancho,20));
			e.Graphics.DrawString(factura.FtrFecha.ToString("dd/MM/yyyy"), sistemaTitulo, Brushes.Black, new RectangleF(150,y,ancho,20));
			y += 20;
			
			/*Esta en la misma linea*/
			e.Graphics.DrawString(_configuration["Telefono"], sistemaTitulo, Brushes.Black, new RectangleF(0, y, ancho, 20));
			e.Graphics.DrawString(factura.FtrFecha.ToString("h:mm tt").ToLower(), sistemaTitulo, Brushes.Black, new RectangleF(150, y, ancho, 20));
			y += 20;

			e.Graphics.DrawString(separador, sistemaTitulo, Brushes.Black, new RectangleF(0, y, ancho +100, 20));
			y += 20;

			var numeroRecibo = $"Recibo: {factura.FtrCodigoFactura}";
			e.Graphics.DrawString(numeroRecibo, sistemaTitulo, Brushes.Black, new RectangleF(0, y, ancho, 20));
			y += 20;

			e.Graphics.DrawString(separador, sistemaTitulo, Brushes.Black, new RectangleF(0, y, ancho + 100, 20));
			y += 20;

			var listaArticus = factura.DetalleFacturas;

			/*Fonts para los articlos*/
			Font resaltar = new Font("Arial", 12, FontStyle.Bold, GraphicsUnit.Point);

			e.Graphics.DrawString("Cant", resaltar, Brushes.Black, new RectangleF(0, y, ancho, 20));
			e.Graphics.DrawString("Precio Unitario", resaltar, Brushes.Black, new RectangleF(50, y, ancho, 20));
			e.Graphics.DrawString("Total", resaltar, Brushes.Black, new RectangleF(200, y, ancho, 20));
			y += 30;
			foreach (var articulo in listaArticus) 
			{
				decimal total = articulo.DtfCantidad * articulo.DtfPrecio;
				total += articulo.DtfMontoImpuestos != null ? articulo.DtfMontoImpuestos : 0;
				e.Graphics.DrawString(articulo.DtfCantidad.ToString(), resaltar, Brushes.Black, new RectangleF(0, y, ancho, 20));
				e.Graphics.DrawString(articulo.DtfPrecio.ToString("N0"), sistemaTitulo, Brushes.Black, new RectangleF(50, y, ancho, 20));
				e.Graphics.DrawString(total.ToString("N2"), sistemaTitulo, Brushes.Black, new RectangleF(200, y, ancho, 20));
				y += 20;

				e.Graphics.DrawString(articulo.DtfIdProducto1.PrdCodigo, resaltar, Brushes.Black, new RectangleF(0, y, ancho, 20));
				e.Graphics.DrawString(articulo.DtfIdProducto1.PrdNombre, sistemaTitulo, Brushes.Black, new RectangleF(100, y, ancho, 20));
				y += 40;
			}

			e.Graphics.DrawString(separador, sistemaTitulo, Brushes.Black, new RectangleF(0, y, ancho + 100, 20));
			y += 20;

			var resumen = factura.FacturaResumen.FirstOrDefault();
			var subtotal = resumen.FtrMontoTotal - (resumen.FtrMontoImpuestos != null ? resumen.FtrMontoImpuestos : 0);
			var montoTotal = subtotal + (resumen.FtrMontoImpuestos != null ? resumen.FtrMontoImpuestos : 0);
			e.Graphics.DrawString("Subtotal", sistemaTitulo, Brushes.Black, new RectangleF(0, y, ancho, 20));
			e.Graphics.DrawString(subtotal.ToString("N2"), sistemaTitulo, Brushes.Black, new RectangleF(150, y, ancho, 20));
			y += 20;

			
			e.Graphics.DrawString("Impuestos", sistemaTitulo, Brushes.Black, new RectangleF(0, y, ancho, 20));
			e.Graphics.DrawString(resumen.FtrMontoImpuestos.ToString("N2"), sistemaTitulo, Brushes.Black, new RectangleF(150, y, ancho, 20));
			y += 20;

			e.Graphics.DrawString("Total", sistemaTitulo, Brushes.Black, new RectangleF(0, y, ancho, 20));
			e.Graphics.DrawString(montoTotal.ToString("N2"), sistemaTitulo, Brushes.Black, new RectangleF(150, y, ancho, 20));
			y += 20;


			e.Graphics.DrawString("Forma de pago", sistemaTitulo, Brushes.Black, new RectangleF(0, y, ancho, 20));
			e.Graphics.DrawString(factura.FtrFormaPagoNavigation.FrpDescripcion, sistemaTitulo, Brushes.Black, new RectangleF(150, y, ancho, 20));
			y += 20;

			e.Graphics.DrawString("Paga con", sistemaTitulo, Brushes.Black, new RectangleF(0, y, ancho, 20));
			e.Graphics.DrawString(resumen.FtrMontoPagado.HasValue ? resumen.FtrMontoPagado.Value.ToString("N2") : "", sistemaTitulo, Brushes.Black, new RectangleF(150, y, ancho, 20));
			y += 20;

			e.Graphics.DrawString("Cambio", sistemaTitulo, Brushes.Black, new RectangleF(0, y, ancho, 20));
			e.Graphics.DrawString(resumen.FtrCambio.HasValue ? resumen.FtrCambio.Value.ToString("N2") : "", sistemaTitulo, Brushes.Black, new RectangleF(150, y, ancho, 20));
			y += 20;

		}
	}
}
