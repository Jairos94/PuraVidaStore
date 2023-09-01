using iTextSharp.text;
using iTextSharp.text.pdf;
using PuraVidaStoreBK.Utilitarios.Interfase;
using System.Net.Mail;
using System.Net;
using PuraVidaStoreBK.Models.DbContex;

namespace PuraVidaStoreBK.Utilitarios
{
    public class EnviarCorreo: IEnviarCorreo
    {
        public void EnviarFactura(Factura factura,ParametrosEmail email, List<string> correosDestinatarios) 
        {
            MemoryStream memoryStream = new MemoryStream();
            Document doc = new Document();
            PdfWriter writer = PdfWriter.GetInstance(doc, memoryStream);
            doc.Open();

            AgregarEncabezado(doc, factura);
            //AgregarTablaProductos(doc, factura.Productos);
            //AgregarTablaImpuestos(doc, factura.Impuestos);
            AgregarTotales(doc, factura);

            doc.Close();

            byte[] pdfBytes = memoryStream.ToArray();
            memoryStream.Close();

            EnviarCorreoConPDF(factura, pdfBytes,email,correosDestinatarios,factura.FtrCodigoFactura);
        }
        private void AgregarEncabezado(Document doc, Factura factura)
        {
            // Agregar logo
            Image logo = Image.GetInstance("ruta_del_logo.png");
            logo.Alignment = Image.LEFT_ALIGN;
            doc.Add(logo);

            // Agregar número de factura y código de barras
            Paragraph numeroFactura = new Paragraph($"Número de Factura: {factura.FtrCodigoFactura}");
            doc.Add(numeroFactura);

            // Agregar sucursal
            Paragraph sucursal = new Paragraph("Sucursal: NombreSucursal");
            doc.Add(sucursal);

            // Agregar nombre de la empresa, cedula y fecha de factura
            PdfPTable infoEmpresa = new PdfPTable(3);
            infoEmpresa.DefaultCell.Border = PdfPCell.NO_BORDER;
            infoEmpresa.DefaultCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            infoEmpresa.WidthPercentage = 100;
            infoEmpresa.SetWidths(new float[] { 1f, 1f, 1f });

            infoEmpresa.AddCell("Nombre de la Empresa");
            infoEmpresa.AddCell("Cédula: 1234567890");
            infoEmpresa.AddCell($"Fecha de Factura: {factura.FtrFecha.ToShortDateString()}");

            doc.Add(infoEmpresa);

            // Agregar espacio en blanco
            doc.Add(new Paragraph(" "));
        }

        private void AgregarTablaProductos(Document doc, List<Producto> productos)
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
                //tablaProductos.AddCell(producto.Codigo);
                //tablaProductos.AddCell(producto.Descripcion);
                //tablaProductos.AddCell(producto.Cantidad.ToString());
                //tablaProductos.AddCell(producto.Precio.ToString());
                //tablaProductos.AddCell(producto.MontoImpuestos.ToString());
            }

            doc.Add(tablaProductos);

            // Agregar espacio en blanco
            doc.Add(new Paragraph(" "));
        }

        private void AgregarTablaImpuestos(Document doc, List<Impuesto> impuestos)
        {
            PdfPTable tablaImpuestos = new PdfPTable(2);
            tablaImpuestos.WidthPercentage = 50;
            tablaImpuestos.SetWidths(new float[] { 2f, 1f });

            tablaImpuestos.AddCell("Nombre Impuesto");
            tablaImpuestos.AddCell("Porcentaje");

            foreach (var impuesto in impuestos)
            {
                //tablaImpuestos.AddCell(impuesto.Nombre);
                //tablaImpuestos.AddCell(impuesto.Porcentaje.ToString());
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

            tablaTotales.AddCell("Subtotal");
            //tablaTotales.AddCell(factura.Subtotal.ToString());

            tablaTotales.AddCell("Monto Impuestos");
            //tablaTotales.AddCell(factura.MontoImpuestos.ToString());

            tablaTotales.AddCell("Monto Total");
            //tablaTotales.AddCell(factura.MontoTotal.ToString());

            doc.Add(tablaTotales);
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
            correosDestinatarios.ForEach(x => 
            {
                correo.To.Add(x);
            });
            
            correo.Subject =string.Format("Factura {0}",numeroFactura) ;
            correo.Body = "Adjuntamos la factura en formato PDF.";

            // Adjuntar el PDF al correo
            MemoryStream pdfStream = new MemoryStream(pdfBytes);
            correo.Attachments.Add(new Attachment(pdfStream, "factura.pdf"));

            // Autenticación para el servidor SMTP
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
            smtp.EnableSsl = email.PreSsl;

            try
            {
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
