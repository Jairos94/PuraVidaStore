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
    const titulos: string[] = [
      'Nombre del artículo',
      'Código',
      'Código del proveedor',
      'Tipo producto',
      'Existencias',
    ];

    const doc = new jsPDF();

    doc.text('Lista de articulos en existencia',70, 10);
    doc.setFontSize(12);
    var fecha =Date.now();
    var fechaHoy = new Date(fecha);
    doc.text(fechaHoy.toLocaleDateString(),150, 10);


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
      head: [titulos],
      body: lista,
    })

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