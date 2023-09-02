using iTextSharp.text;
using iTextSharp.text.pdf;
using PuraVidaStoreBK.Utilitarios.Interfase;
using System.Net.Mail;
using System.Net;
using PuraVidaStoreBK.Models.DbContex;
using Serilog;
using System.IO;

namespace PuraVidaStoreBK.Utilitarios
{
    public class EnviarCorreo: IEnvioCorreo
    {
        private readonly IConfiguration _configuration;

        public EnviarCorreo(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void EnviarFactura(Factura factura,ParametrosEmail email, List<string> correosDestinatarios) 
        {
            MemoryStream memoryStream = new MemoryStream();
            Document doc = new Document();
            PdfWriter writer = PdfWriter.GetInstance(doc, memoryStream);
            doc.Open();

            AgregarEncabezado(doc, factura);
            if (factura.DetalleFacturas != null) 
            {
                AgregarTablaProductos(doc, factura.DetalleFacturas);
            }
            if (factura.ImpuestosPorFacturas!=null) 
            {
                AgregarTablaImpuestos(doc, factura.ImpuestosPorFacturas);
            }
           
            AgregarTotales(doc, factura);
            AgregarCodigoBarras(doc, writer, factura.FtrCodigoFactura);

            doc.Close();

            byte[] pdfBytes = memoryStream.ToArray();
            memoryStream.Close();
            try
            {
                EnviarCorreoConPDF(factura, pdfBytes, email, correosDestinatarios, factura.FtrCodigoFactura);
            }
            catch (Exception ex)
            {

                Log.Error(ex.StackTrace);
            }
         
        }
        private void AgregarEncabezado(Document doc, Factura factura)
        {

            // Crear una tabla para el encabezado
            PdfPTable encabezado = new PdfPTable(1);
            encabezado.WidthPercentage = 100;

            // Nombre de la empresa (en el centro)
            PdfPCell nombreEmpresaCell = new PdfPCell(new Phrase("Nombre de la empresa"));
            nombreEmpresaCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nombreEmpresaCell.Border = PdfPCell.NO_BORDER;
            encabezado.AddCell(nombreEmpresaCell);

            // Sucursal
            PdfPCell sucursalCell = new PdfPCell(new Phrase("Sucursal: " + factura.FtrBodegaNavigation.BdgDescripcion));
            sucursalCell.HorizontalAlignment = Element.ALIGN_CENTER;
            sucursalCell.Border = PdfPCell.NO_BORDER;
            encabezado.AddCell(sucursalCell);

            // Número de factura
            PdfPCell numeroFacturaCell = new PdfPCell(new Phrase($"Número de Factura: {factura.FtrCodigoFactura}"));
            numeroFacturaCell.HorizontalAlignment = Element.ALIGN_CENTER;
            numeroFacturaCell.Border = PdfPCell.NO_BORDER;
            encabezado.AddCell(numeroFacturaCell);

            // Fecha de factura
            PdfPCell fechaFacturaCell = new PdfPCell(new Phrase($"Fecha de Factura: {factura.FtrFecha.ToString("MM/dd/yyyy hh:mm tt")}"));
            fechaFacturaCell.HorizontalAlignment = Element.ALIGN_CENTER;
            fechaFacturaCell.Border = PdfPCell.NO_BORDER;
            encabezado.AddCell(fechaFacturaCell);

            // Agregar la tabla de encabezado al documento
            doc.Add(encabezado);

            // Agregar espacio en blanco
            doc.Add(new Paragraph(" "));
        }

        private void AgregarTablaProductos(Document doc, ICollection<DetalleFactura> productos)
        {
            PdfPTable tablaProductos = new PdfPTable(5);
            tablaProductos.WidthPercentage = 100;
            tablaProductos.SetWidths(new float[] { 1f, 3f, 1f, 1f, 1f });

            // Agregar encabezados de la tabla
            tablaProductos.AddCell("Código");
            tablaProductos.AddCell("Descripción");
            tablaProductos.AddCell("Cantidad");
            tablaProductos.AddCell("Precio");
            tablaProductos.AddCell("Monto Impuestos");

            // Agregar productos
            foreach (var producto in productos)
            {
                if (producto.DtfIdProducto1 != null) 
                {
                    tablaProductos.AddCell(producto.DtfIdProducto1.PrdCodigo);
                    tablaProductos.AddCell(producto.DtfIdProducto1.PrdNombre);
                    tablaProductos.AddCell(producto.DtfCantidad.ToString());
                    tablaProductos.AddCell(producto.DtfPrecio.ToString());
                    tablaProductos.AddCell(producto.DtfMontoImpuestos.ToString());
                }
                ;
            }

            doc.Add(tablaProductos);

            // Agregar espacio en blanco
            doc.Add(new Paragraph(" "));
        }

        private void AgregarTablaImpuestos(Document doc, ICollection<ImpuestosPorFactura> impuestos)
        {
            PdfPTable tablaImpuestos = new PdfPTable(2);
            tablaImpuestos.WidthPercentage = 50;
            tablaImpuestos.SetWidths(new float[] { 2f, 1f });

            tablaImpuestos.AddCell("Nombre Impuesto");
            tablaImpuestos.AddCell("Porcentaje");

            foreach (var impuesto in impuestos)
            {
                if(impuesto.IpfIdImpuestoNavigation != null) 
                {
                    tablaImpuestos.AddCell(impuesto.IpfIdImpuestoNavigation.ImpDescripcion);
                    tablaImpuestos.AddCell(impuesto.IpfIdImpuestoNavigation.ImpPorcentaje.ToString() +"%");
                }
                
            }

            doc.Add(tablaImpuestos);

            // Agregar espacio en blanco
            doc.Add(new Paragraph(" "));
        }

        private void AgregarTotales(Document doc, Factura factura)
        {
            // Agregar totales
            PdfPTable tablaTotales = new PdfPTable(2);
            tablaTotales.DefaultCell.Border = PdfPCell.NO_BORDER;
            tablaTotales.DefaultCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            tablaTotales.WidthPercentage = 50;
            tablaTotales.SetWidths(new float[] { 1f, 1f });
            FacturaResumen resumen = new FacturaResumen();
            foreach (var r in factura.FacturaResumen) 
            {
                resumen = r;
            }
            tablaTotales.AddCell("Subtotal");
            var subTotal = resumen.FtrMontoTotal - resumen.FtrMontoImpuestos;
            tablaTotales.AddCell(subTotal.ToString());

            tablaTotales.AddCell("Monto Impuestos");
            tablaTotales.AddCell(resumen.FtrMontoImpuestos.ToString());

            tablaTotales.AddCell("Monto Total");
            tablaTotales.AddCell(resumen.FtrMontoTotal.ToString());

            tablaTotales.AddCell("Forma de pago");
            tablaTotales.AddCell(factura.FtrFormaPagoNavigation.FrpDescripcion);

            doc.Add(tablaTotales);
        }

        private void AgregarCodigoBarras(Document doc, PdfWriter writer, string codigoFactura)
        {
            // Crear un objeto Barcode128 de iTextSharp para el código de barras CODE-128
            Barcode128 barcode = new Barcode128();
            barcode.Code = codigoFactura;
            barcode.StartStopText = false; // Opcional: para quitar el texto de inicio y final del código

            // Crear una imagen del código de barras
            iTextSharp.text.Image barcodeImage = barcode.CreateImageWithBarcode(
                writer.DirectContent,
                BaseColor.BLACK,
                BaseColor.BLACK
            );

            // Ajustar el tamaño de la imagen del código de barras si es necesario
            barcodeImage.ScaleToFit(200, 50); // Ancho y alto del código de barras

            // Obtener el tamaño de la página
            float pageSizeWidth = doc.PageSize.Width;

            // Calcular la posición X para centrar horizontalmente
            float x = (pageSizeWidth - barcodeImage.ScaledWidth) / 2;

            // Establecer la posición absoluta en el centro horizontal
            barcodeImage.SetAbsolutePosition(x, barcodeImage.AbsoluteY);

            // Agregar el código de barras al documento
            doc.Add(barcodeImage);
        }

        private void EnviarCorreoConPDF(Factura factura,byte[] pdfBytes, ParametrosEmail email, List<string>  correosDestinatarios , string numeroFactura)
        {
            // Configurar los detalles del correo
            string smtpHost = email.PreHost;
            int smtpPort = email.PrePuerto;
            string smtpUsername = email.PreUser;
            string smtpPassword = email.PreClave;
            //string destinatario = correoDestino;

            MailMessage correo = new MailMessage();
            SmtpClient smtp = new SmtpClient(smtpHost, smtpPort);

            correo.From = new MailAddress(smtpUsername);
            foreach (var c in correosDestinatarios) 
            {
                correo.To.Add(c);
            }
            //correosDestinatarios.ForEach(x => 
            //{
            //    correo.To.Add(x);
            //});

            correo.Subject =string.Format("Factura {0}",numeroFactura) ;
            correo.Body = "Adjuntamos la factura en formato PDF.";

            // Adjuntar el PDF al correo
            MemoryStream pdfStream = new MemoryStream(pdfBytes);
            correo.Attachments.Add(new Attachment(pdfStream, "Factura "+numeroFactura+".pdf"));

            // Autenticación para el servidor SMTP
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
            smtp.EnableSsl = email.PreSsl;
            smtp.Host = smtpHost;

            try { 
                smtp.Send(correo);
            }
            catch (Exception ex)
            {
                // Manejo de errores al enviar el correo
                Console.WriteLine("Error al enviar el correo: " + ex.Message);
            }
            finally
            {
                correo.Dispose();
                pdfStream.Dispose();
            }
        }
    }
}
