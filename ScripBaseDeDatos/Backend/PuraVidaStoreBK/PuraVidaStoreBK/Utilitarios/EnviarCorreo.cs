using iTextSharp.text;
using iTextSharp.text.pdf;
using PuraVidaStoreBK.Utilitarios.Interfase;
using System.Net.Mail;
using System.Net;
using PuraVidaStoreBK.Models.DbContex;
using Serilog;

namespace PuraVidaStoreBK.Utilitarios
{
    public class EnviarCorreo: IEnvioCorreo
    {
        public void EnviarFactura(Factura factura,ParametrosEmail email, List<string> correosDestinatarios) 
        {
            MemoryStream memoryStream = new MemoryStream();
            Document doc = new Document();
            PdfWriter writer = PdfWriter.GetInstance(doc, memoryStream);
            doc.Open();

            AgregarEncabezado(doc, factura);
            if (factura.DetalleFacturas != null) 
            {
                AgregarTablaProductos(doc, (List<DetalleFactura>)factura.DetalleFacturas);
            }
            if (factura.ImpuestosPorFacturas!=null) 
            {
                AgregarTablaImpuestos(doc, (List<ImpuestosPorFactura>)factura.ImpuestosPorFacturas);
            }
           
            AgregarTotales(doc, factura);

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
            // Agregar logo
            //Image logo = Image.GetInstance("ruta_del_logo.png");
            //logo.Alignment = Image.LEFT_ALIGN;
            //doc.Add(logo);

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

        private void AgregarTablaProductos(Document doc, List<DetalleFactura> productos)
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

        private void AgregarTablaImpuestos(Document doc, List<ImpuestosPorFactura> impuestos)
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
            correo.Attachments.Add(new Attachment(pdfStream, "factura.pdf"));

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
