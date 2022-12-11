import { activo } from 'src/app/activo';
import { MovimientosService } from './../../../services/movimientos.service';
import { Component, OnInit } from '@angular/core';
import { InventariosModel } from 'src/app/models/inventarios-model';
import * as XLSX from 'xlsx';
import jsPDF from 'jspdf';
import autoTable from 'jspdf-autotable';

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

  constructor(private servio: MovimientosService) {}

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
    const titulos: string[] = [
      'Nombre del artículo',
      'Código',
      'Código del proveedor',
      'Tipo producto',
      'Existencias',
    ];

    const listaExistencia: string[] = [];

    this.listaArticulos.forEach((x) => {
      var datoAGuardar: any = {
        prdNombre: x.producto.prdNombre,
        prdCodigo: x.producto.prdCodigo,
        prdCodigoProvedor: x.producto.prdCodigoProvedor,
        Tipo: x.producto.PrdIdTipoProductoNavigation?.TppDescripcion.toString,
        cantidadExistencia: x.cantidadExistencia.toString,
      };
      listaExistencia.push(datoAGuardar);
    });

    const doc = new jsPDF();

    autoTable(doc, {
      head: [titulos],
    });

    this.listaArticulos.forEach((x) => {
      autoTable(doc, {
        body: [
          [
            x.producto.prdNombre,
            x.producto.prdCodigo,
            x.producto.prdCodigoProvedor || '',
            x.producto.PrdIdTipoProductoNavigation?.TppDescripcion || '',
            x.cantidadExistencia,
          ],
        ],
      });
    });
    // Or use javascript directly:

    doc.save(this.fileName + '.pdf');
  }

  existencias() {
    this.servio.ProductosExistencias(activo.bodegaIngreso.bdgId).subscribe({
      next: (x) => {
        this.listaArticulos = [];
        this.listaArticulos = x;
      },
      error: (_e) => console.error(_e),
    });
  }
}
