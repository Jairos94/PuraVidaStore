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

		public void Imprimir(Factura factura, ParametrosGlobales parametros, bool esReimprimir)
		{
			var pd = new PrintDocument();
			PrinterSettings ps = new PrinterSettings();

			// Establecer la impresora
			ps.PrinterName = parametros.PrgImpresora;
			pd.PrinterSettings = ps;

			// Pasar la factura a ImprimirDocumento
			pd.PrintPage += (sender, e) => ImprimirDocumento(sender, e, factura, parametros,esReimprimir);

			// Iniciar impresión
			pd.Print();
		}


		private void ImprimirDocumento(object sender, PrintPageEventArgs e, Factura factura, ParametrosGlobales parametros,bool esReImpresion) 
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

			string rutaLogo = _configuration["rutaLogo"];
			if (File.Exists(rutaLogo))
			{
				Image logo = Image.FromFile(rutaLogo);
				e.Graphics.DrawImage(logo, new RectangleF(100, y, 100, 50));
				y+=60;  // Ajusta el incremento
			}

			e.Graphics.DrawString(_configuration["NombreTienda"], fuenteTitulo, Brushes.Black, new RectangleF(50,y,ancho,20));
			y +=30;

			/*Estan en la misma linea*/
			e.Graphics.DrawString(factura.FtrBodegaNavigation.BdgDescripcion, sistemaTitulo, Brushes.Black, new RectangleF(0,y,ancho,20));
			e.Graphics.DrawString(factura.FtrFecha.ToString("dd/MM/yyyy"), sistemaTitulo, Brushes.Black, new RectangleF(150,y,ancho,20));
			y += 20;
			
			/*Esta en la misma linea*/
			e.Graphics.DrawString(_configuration["Telefono"], sistemaTitulo, Brushes.Black, new RectangleF(0, y, ancho, 20));
			e.Graphics.DrawString(factura.FtrFecha.ToString("h:mm tt").ToLower(), sistemaTitulo, Brushes.Black, new RectangleF(150, y, ancho, 20));
			y += 20;

			e.Graphics.DrawString($"Cédula: {_configuration["NumeroCedula"]}", sistemaTitulo, Brushes.Black, new RectangleF(0, y, ancho, 20));
			y += 20;

			e.Graphics.DrawString(separador, sistemaTitulo, Brushes.Black, new RectangleF(0, y, ancho +100, 20));
			y += 20;

			var numeroRecibo = $"Recibo: {factura.FtrCodigoFactura}";
			e.Graphics.DrawString(numeroRecibo, sistemaTitulo, Brushes.Black, new RectangleF(0, y, ancho, 20));
			if (esReImpresion) 
			{
				Font fontReimpresion = new Font("Arial", 12, FontStyle.Bold, GraphicsUnit.Point);
				e.Graphics.DrawString("Reimpresión", fontReimpresion, Brushes.Black, new RectangleF(150, y, ancho, 20));
			}
			y += 20;

			e.Graphics.DrawString(separador, sistemaTitulo, Brushes.Black, new RectangleF(0, y, ancho + 100, 20));
			y += 20;

			var listaArticus = factura.DetalleFacturas;

			/*Fonts para los articlos*/
			Font resaltar = new Font("Arial", 10, FontStyle.Bold, GraphicsUnit.Point);
			Font fontForeache = new Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point);

			e.Graphics.DrawString("Cant", resaltar, Brushes.Black, new RectangleF(0, y, ancho, 20));
			e.Graphics.DrawString("Precio", resaltar, Brushes.Black, new RectangleF(50, y, ancho, 20));
			e.Graphics.DrawString("Total", resaltar, Brushes.Black, new RectangleF(200, y, ancho, 20));
			y += 30;
			listaArticus = listaArticus.OrderBy(x=>x.DtfLinea).ToList();
			foreach (var articulo in listaArticus) 
			{
				decimal total = articulo.DtfCantidad * articulo.DtfPrecio;
				total += articulo.DtfMontoImpuestos != null ? articulo.DtfMontoImpuestos : 0;
				e.Graphics.DrawString(articulo.DtfCantidad.ToString(), resaltar, Brushes.Black, new RectangleF(0, y, ancho, 20));
				e.Graphics.DrawString(articulo.DtfPrecio.ToString("N0"), fontForeache, Brushes.Black, new RectangleF(50, y, ancho, 20));
				e.Graphics.DrawString(total.ToString("N2"), fontForeache, Brushes.Black, new RectangleF(200, y, ancho, 20));
				y += 20;

				e.Graphics.DrawString(articulo.DtfIdProducto1.PrdCodigo, resaltar, Brushes.Black, new RectangleF(0, y, ancho, 20));
				e.Graphics.DrawString(articulo.DtfIdProducto1.PrdNombre, fontForeache, Brushes.Black, new RectangleF(100, y, ancho, 40));
				y += 60;
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

			e.Graphics.DrawString(separador, sistemaTitulo, Brushes.Black, new RectangleF(0, y, ancho + 100, 20));
			y += 20;

			if (parametros.PrgLeyenda != null) 
			{
				string[] lineas = parametros.PrgLeyenda.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

				if (lineas.Length > 0)
				{
					foreach (var linea in lineas)
					{
						if (!string.IsNullOrEmpty(linea)) 
						{
							e.Graphics.DrawString(linea, sistemaTitulo, Brushes.Black, new RectangleF(0, y, 300, 40));
							y += 20;
						}
					}
				}
				else 
				{
					e.Graphics.DrawString(parametros.PrgLeyenda, sistemaTitulo, Brushes.Black, new RectangleF(0, y, 300, 60));
					y += 20;
				}
			}
			y += 250;
			e.Graphics.DrawString(separador, sistemaTitulo, Brushes.Black, new RectangleF(0, y, ancho + 100, 20));
			y += 20;
		}
	}
}
