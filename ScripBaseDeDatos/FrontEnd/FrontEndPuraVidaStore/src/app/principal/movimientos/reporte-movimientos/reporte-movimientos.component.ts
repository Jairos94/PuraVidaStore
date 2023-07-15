import jsPDF from 'jspdf';
import autoTable from 'jspdf-autotable';
import { Component, OnInit } from '@angular/core';
import { activo } from 'src/app/activo';
import { BodegaModel } from 'src/app/models/bodega-model';
import { ReporteMovimientoModel } from 'src/app/models/reporte-movimiento-model';
import { BodegaService } from 'src/app/services/bodega.service';
import { ReportesService } from 'src/app/services/reportes.service';

@Component({
  selector: 'app-reporte-movimientos',
  templateUrl: './reporte-movimientos.component.html',
  styleUrls: ['./reporte-movimientos.component.css'],
})
export class ReporteMovimientosComponent implements OnInit {
  listaReporteMovimientos: ReporteMovimientoModel[] = [];
  listaBodegas: BodegaModel[] = [];

  bodegaSeleccionada: BodegaModel = activo.bodegaIngreso;
  idBodega: number = 0;
  fechaInicio: Date | undefined;
  fechaFin: Date | undefined;

  constructor(
    private reportesServicio: ReportesService,
    private bodegasServicio: BodegaService
  ) {}

  ngOnInit(): void {
    this.cargarBodegas();
  }

  cargarBodegas() {
    this.bodegasServicio.listaBodegas().subscribe({
      next: (x) => {
        this.listaBodegas = [];
        this.listaBodegas = x;
      },
      error: (_e) => console.log(_e),
    });
  }

  ejecutarConsultaBodega() {
    this.reportesServicio
      .obteneReporteMovimientos(
        this.bodegaSeleccionada.bdgId,
        this.fechaInicio!,
        this.fechaFin!
      )
      .subscribe({
        next: (x) => {
          this.listaReporteMovimientos = [];
          this.listaReporteMovimientos = x;
          this.ExportarPDFMovimientosBodega()
        },
        error: (_e) => console.log(),
      });


  }

  ExportarPDFMovimientosBodega(){
    let fecha = new Date().toLocaleString('es-CR', {
      hour12: true,
      dateStyle: 'short',
      timeStyle: 'short'
    });
    const doc = new jsPDF();

    const generateHeader = () => {
      // Logo
      const logoImg = new Image();
      logoImg.src = '../../../../assets/logoNegro.png';
      doc.addImage(logoImg, 'PNG', 10, 10, 30, 30);

      // Nombre del reporte y bodega
      doc.setFontSize(16);
      const reporteText = 'Reporte de movimientos';
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
      'Descrip. del producto',
      'Fecha',
      'Responsable',
      'Bodega',
      'Descrip. del movimiento',
      'Ingreso',
      'Salidas',
    ];
    let listaBody: any[] = [];
    this.listaReporteMovimientos.forEach((x) => {
      let dataBody = [
        x.codigo,
        x.descripcionProducto,
        new Date(x.fecha).toLocaleString('es-CR', {
          hour12: true,
          dateStyle: 'short',
          timeStyle: 'short'
        }),
        x.responsable,
        x.bodega,
        x.descripcion,
        x.ingresos,
        x.salidas,
      ];
      listaBody.push(dataBody);
    });

    autoTable(doc, {
      startY: 40,
      headStyles: {
        fillColor: [255, 255, 255], // Establece el color de fondo del encabezado a blanco
        textColor: [0, 0, 0], // Establece el color del texto del encabezado a negro
        fontStyle: 'bold' // Establece el estilo de fuente en negrita para el encabezado
      },
      bodyStyles: {
        fillColor: [255, 255, 255], // Establece el color de fondo del cuerpo de la tabla a blanco
        textColor: [0, 0, 0] // Establece el color del texto del cuerpo de la tabla a negro
      },
      tableLineColor: [0, 0, 0], // Establece el color de borde de la tabla a negro
      //tableLineWidth: 0.2, // Establece el ancho del borde de la tabla
      head: [titulos],
      body: listaBody,
      columnStyles: {
        2: { cellWidth: 30 }, // Ajusta el ancho de la columna de fecha (índice 2) a 40 unidades
        5: { cellWidth: 40 }, // Ajusta el ancho de la columna de fecha (índice 2) a 40 unidades
      }
    });

    // Generar el encabezado en cada página
    const totalPages = doc.internal.pages.length;
    for (let i = 1; i <= totalPages; i++) {
      doc.setPage(i);
      generateHeader();
    }

    doc.save('Movimientos.pdf');
  }
}
