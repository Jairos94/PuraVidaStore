import { TipoProductoModel } from 'src/app/models/tipo-producto';
import { activo } from 'src/app/activo';
import { MovimientosService } from './../../../services/movimientos.service';
import { Component, OnInit } from '@angular/core';
import { InventariosModel } from 'src/app/models/inventarios-model';
import * as XLSX from 'xlsx';
import jsPDF from 'jspdf';
import autoTable from 'jspdf-autotable';
import { InventariosVisualizarModels } from 'src/app/models/inventarios-visualizar-models';

@Component({
  selector: 'app-inventarios',
  templateUrl: './inventarios.component.html',
  styleUrls: ['./inventarios.component.css'],
})
export class InventariosComponent implements OnInit {
  fileName = 'Lista de existencias';
  cols: any[] = [];
  listaArticulos: InventariosModel[] = [];
  exportColumns: any[] = [];
  buscardor: string = '';

  constructor(private servicio: MovimientosService) {}

  ngOnInit() {
    this.existencias();
  }

  exportarExcel(): void {
    let element = document.getElementById('Inventarios');
    const ws: XLSX.WorkSheet = XLSX.utils.table_to_sheet(element);

    /* generate workbook and add the worksheet */
    const wb: XLSX.WorkBook = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb, ws, 'Hoja 1');

    /* save to file */
    XLSX.writeFile(wb, this.fileName + '.xlsx');
  }

  exportarPDF(): void {
    let fecha = new Date().toLocaleString('es-CR', {
      hour12: true,
      dateStyle: 'short',
      timeStyle: 'short',
    });
    const generateHeader = () => {
      // Logo
      const logoImg = new Image();
      logoImg.src = '../../../../assets/logoNegro.png';
      doc.addImage(logoImg, 'PNG', 10, 10, 30, 30);

      // Nombre del reporte y bodega
      doc.setFontSize(16);
      const reporteText = 'Productos con Existencia';
      const bodegaText = activo.bodegaIngreso.bdgDescripcion;
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

    const titulos: string[] = [
      'Nombre del artículo',
      'Código',
      'Código del proveedor',
      'Tipo producto',
      'Existencias',
    ];

    const doc = new jsPDF();


    let lista: any[] = [];

    this.listaArticulos.forEach((x, i) => {
      var tipo: TipoProductoModel = {
        tppId: 0,
        tppDescripcion: '',
        tppVisible: true,
      };

      if (x.producto.prdIdTipoProductoNavigation != null) {
        tipo = x.producto.prdIdTipoProductoNavigation;

        let data: InventariosVisualizarModels={
          nombreProducto: x.producto.prdNombre,
          codigoProducto: x.producto.prdCodigo||'',
          codigoProvedor: x.producto.prdCodigoProvedor || '',
          descripcion : tipo.tppDescripcion || '',
          cantidadExistencia : x.cantidadExistencia
        };

        lista.push([x.producto.prdNombre,x.producto.prdCodigo||'',x.producto.prdCodigoProvedor || '',tipo.tppDescripcion || '',x.cantidadExistencia]);
      }


    });
    // Or use javascript directly:
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
      body: lista,

    });

    // Generar el encabezado en cada página
    const totalPages = doc.internal.pages.length;
    for (let i = 1; i <= totalPages; i++) {
      doc.setPage(i);
      generateHeader();
    }

    doc.save(this.fileName + '.pdf');
  }

  existencias() {
    this.servicio.ProductosExistencias(activo.bodegaIngreso.bdgId).subscribe({
      next: (x) => {
        this.listaArticulos = [];
        this.listaArticulos = x;
      },
      error: (_e) => console.error(_e),
    });
  }

  sugerencias() {
    this.servicio
      .Sugerencias(activo.bodegaIngreso.bdgId, this.buscardor)
      .subscribe({
        next: (x) => {
          if (x.length > 0) {
            this.listaArticulos = [];
            this.listaArticulos = x;
          } else {
            this.existencias();
          }
        },
        error: (_e) => {
          console.error(_e);
          this.existencias();
        },
      });
  }

}
