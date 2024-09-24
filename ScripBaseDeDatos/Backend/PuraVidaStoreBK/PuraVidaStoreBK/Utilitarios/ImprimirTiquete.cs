using System.Drawing;
using System.Drawing.Printing;
using PuraVidaStoreBK.Models.DbContex;
using PuraVidaStoreBK.Utilitarios.Interfase;

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
			PrintDocument printDocument = new PrintDocument();
			printDocument.PrinterSettings.PrinterName = parametros.PrgImpresora;

			// Manejar el evento PrintPage
			printDocument.PrintPage += (sender, e) => OnPrintPage(sender, e, factura);

			// Iniciar la impresión
			try
			{
				printDocument.Print();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error al imprimir: {ex.Message}");
			}
		}

		private void OnPrintPage(object sender, PrintPageEventArgs e, Factura factura)
		{
			Graphics graphics = e.Graphics;

			// Establecer la fuente y el tamaño
			Font font = new Font("Arial", 10);
			float yPos = 0;
			float leftMargin = e.MarginBounds.Left;

			// Imprimir el encabezado
			yPos += PrintHeader(graphics, factura, font, 0, yPos);

			// Imprimir los productos
			yPos += PrintProducts(graphics, factura.DetalleFacturas, font, 0, ref yPos, e);

			// Imprimir totales
			yPos += PrintTotals(graphics, factura, font, 0, yPos);

			// Espacio adicional para el corte
			yPos += 20;

			// Comprobar si hay más contenido
			e.HasMorePages = yPos > e.MarginBounds.Bottom; // Si excede el límite, hay más páginas
		}

		private float PrintProducts(Graphics graphics, ICollection<DetalleFactura> productos, Font font, float leftMargin, ref float yPos, PrintPageEventArgs e)
		{
			foreach (var producto in productos)
			{
				if (producto.DtfIdProducto1 != null)
				{
					var total = producto.DtfCantidad * (producto.DtfPrecio + (producto.DtfMontoImpuestos != null ? producto.DtfMontoImpuestos : 0)); 
					string line = $"{producto.DtfIdProducto1.PrdCodigo} - {producto.DtfIdProducto1.PrdNombre} - " +
								  $"{producto.DtfCantidad} - {total.ToString("N0")}";
					graphics.DrawString(line, font, Brushes.Black, leftMargin, yPos);
					yPos += 20;
				}
			}

			// Agregar una línea de separación después de los productos
			graphics.DrawString(new string('-', 60), font, Brushes.Black, leftMargin, yPos);
			yPos += 5; // Incrementar la posición después de la línea

			return yPos; // Retornar la posición actual para continuar con los totales
		}


		private float PrintHeader(Graphics graphics, Factura factura, Font font, float leftMargin, float yPos)
		{
			graphics.DrawString($"Cédula: {_configuration["NumeroCedula"]}", font, Brushes.Black, leftMargin, yPos);
			yPos += font.GetHeight(graphics);

			graphics.DrawString($"Tienda: {_configuration["NombreTienda"]}", font, Brushes.Black, leftMargin, yPos);
			yPos += font.GetHeight(graphics);

			graphics.DrawString($"Sucursal: {factura.FtrBodegaNavigation.BdgDescripcion}", font, Brushes.Black, leftMargin, yPos);
			yPos += font.GetHeight(graphics);

			graphics.DrawString($"Fecha: {factura.FtrFecha.ToString("MM/dd/yyyy hh:mm tt")}", font, Brushes.Black, leftMargin, yPos);
			yPos += font.GetHeight(graphics);

			graphics.DrawString($"Factura No: {factura.FtrCodigoFactura}", font, Brushes.Black, leftMargin, yPos);
			yPos += font.GetHeight(graphics);

			graphics.DrawString(new string('-', 60), font, Brushes.Black, leftMargin, yPos);
			yPos += font.GetHeight(graphics);

			return yPos;
		}

		private float PrintTotals(Graphics graphics, Factura factura, Font font, float leftMargin, float yPos)
		{
			var resumen = factura.FacturaResumen.FirstOrDefault(); // Asegúrate de que hay un resumen

			if (resumen != null)
			{
				graphics.DrawString($"Subtotal: {Math.Round(resumen.FtrMontoTotal - resumen.FtrMontoImpuestos, 0):C}", font, Brushes.Black, leftMargin, yPos);
				yPos += font.GetHeight(graphics);
				graphics.DrawString($"Monto Impuestos: {Math.Round(resumen.FtrMontoImpuestos, 0):C}", font, Brushes.Black, leftMargin, yPos);
				yPos += font.GetHeight(graphics);
				graphics.DrawString($"Total: {Math.Round(resumen.FtrMontoTotal, 0):C}", font, Brushes.Black, leftMargin, yPos);
				yPos += font.GetHeight(graphics);
			}

			// Espacio adicional antes del mensaje final
			yPos += 10; // Mantener el espacio antes del mensaje
			graphics.DrawString("Gracias por su compra!", font, Brushes.Black, leftMargin, yPos);
			yPos += font.GetHeight(graphics); // Aumentar la posición después del mensaje

			return yPos;
		}



	}
}
