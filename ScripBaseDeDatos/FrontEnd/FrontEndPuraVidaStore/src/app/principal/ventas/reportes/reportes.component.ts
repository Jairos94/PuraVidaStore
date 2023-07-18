import { Component, OnInit } from '@angular/core';
import jsPDF from 'jspdf';
import autoTable from 'jspdf-autotable';
import { activo } from 'src/app/activo';
import { BodegaModel } from 'src/app/models/bodega-model';
import { FacturaReporteModel } from 'src/app/models/factura-reporte-model';
import { UsuarioModel } from 'src/app/models/usuario-model';
import { BodegaService } from 'src/app/services/bodega.service';
import { ReportesService } from 'src/app/services/reportes.service';

@Component({
  selector: 'app-reportes',
  templateUrl: './reportes.component.html',
  styleUrls: ['./reportes.component.css'],
})
export class ReportesComponent implements OnInit {
  usuarios:UsuarioModel[]=[];
  usuarioSeleccionado= activo.usuarioPrograma;
  listaBodegas: BodegaModel[] = [];
  fechaInicio: Date | undefined;
  fechaFin: Date | undefined;
  bodegaSeleccionada: BodegaModel = activo.bodegaIngreso;
  reporte: FacturaReporteModel = {
    montoTotal: 0,
    montoTotalNulas: 0,
    lista: [],
  };

  constructor(
    private bodegasServices: BodegaService,
    private reportesServices: ReportesService,
  ) {}

  ngOnInit(): void {
    this.obtenerBodegas();
  }

  obtenerBodegas() {
    this.bodegasServices.listaBodegas().subscribe({
      next: (x) => {
        this.listaBodegas = [];
        this.listaBodegas = x;
      },
      error: (_e) => console.log(_e),
    });
  }



  limpiarFacturas(){
    this.reporte = {
      montoTotal: 0,
      montoTotalNulas: 0,
      lista: [],
    };
  }
  consultarFacturaPorBodegas() {
    this.reportesServices.obteneReporteVentasPorBodega(activo.bodegaIngreso.bdgId,this.fechaInicio!,this.fechaFin!)
      .subscribe(
        {
          next:x=>{
            this.limpiarFacturas();
            this.reporte=x;
            this.ExportarPDFVentas();
          },
          error:_e=>console.log(_e)

        });
  }

  ExportarPDFVentas() {
    let fecha = new Date().toLocaleString('es-CR', {
      hour12: true,
      dateStyle: 'short',
      timeStyle: 'short',
    });
    const doc = new jsPDF();

    const generateHeader = () => {
      // Logo
      const logoImg = new Image();
      logoImg.src = '../../../../assets/logoNegro.png';
      doc.addImage(logoImg, 'PNG', 10, 10, 30, 30);

      // Nombre del reporte y bodega
      doc.setFontSize(16);
      const reporteText = 'Reporte de Ventas';
      const bodegaText = this.bodegaSeleccionada.bdgDescripcion;
      const textWidth =
        (doc.getStringUnitWidth(reporteText) * doc.internal.scaleFactor) /
        doc.internal.scaleFactor;
      const textOffset = (doc.internal.pageSize.getWidth() - textWidth) / 2;
      doc.text(reporteText, textOffset - 20, 20, { align: 'justify' });
      doc.text(bodegaText, textOffset, 30, { align: 'justify' });

      // Nombre de la empresa, cédula y fecha

      doc.setFontSize(12);
      doc.setTextColor(100, 100, 100);
      doc.text('Pura Vida Store', doc.internal.pageSize.getWidth() - 10, 20, {
        align: 'right',
      });
      doc.text('1-1119-0707', doc.internal.pageSize.getWidth() - 10, 28, {
        align: 'right',
      });
      doc.text(fecha, doc.internal.pageSize.getWidth() - 10, 36, {
        align: 'right',
      });
    };

    // Generar el contenido del reporte
    const titulos: string[] = [
      'Código',
      'Usuario',
      'Fecha',
      'Monto Factura',
      'Descripcion'
        ];
    let listaBody: any[] = [];
    this.reporte.lista.forEach((x) => {
      let dataBody = [
        x.codigoFactura,
        x.usuario,
        new Date(x.fecha).toLocaleString('es-CR', {
          hour12: true,
          dateStyle: 'short',
          timeStyle: 'short',
        }),
        x.montoFactura,
        x.descripcionFactura
      ];
      listaBody.push(dataBody);
    });

    autoTable(doc, {
      startY: 40,
      headStyles: {
        fillColor: [255, 255, 255], // Establece el color de fondo del encabezado a blanco
        textColor: [0, 0, 0], // Establece el color del texto del encabezado a negro
        fontStyle: 'bold', // Establece el estilo de fuente en negrita para el encabezado
      },
      bodyStyles: {
        fillColor: [255, 255, 255], // Establece el color de fondo del cuerpo de la tabla a blanco
        textColor: [0, 0, 0], // Establece el color del texto del cuerpo de la tabla a negro
      },
      tableLineColor: [0, 0, 0], // Establece el color de borde de la tabla a negro
      //tableLineWidth: 0.2, // Establece el ancho del borde de la tabla
      head: [titulos],
      body: listaBody,

    });

    // Generar el encabezado en cada página
    const totalPages = doc.internal.pages.length;
    for (let i = 1; i <= totalPages; i++) {
      doc.setPage(i);
      generateHeader();
    }

    doc.save('Ventas.pdf');
  }
}
