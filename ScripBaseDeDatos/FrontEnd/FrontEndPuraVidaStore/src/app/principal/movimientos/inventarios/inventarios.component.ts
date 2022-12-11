import { Component, OnInit } from '@angular/core';
import { InventariosModel } from 'src/app/models/inventarios-model';
import * as XLSX from 'xlsx';

@Component({
  selector: 'app-inventarios',
  templateUrl: './inventarios.component.html',
  styleUrls: ['./inventarios.component.css']
})
export class InventariosComponent implements OnInit{
  fileName= 'ExcelSheet.xlsx';
  cols: any[] = [];
  listaArticulos: InventariosModel[]=[]
  exportColumns: any[] =[];

  constructor() {}

  ngOnInit(){
  }

  exportexcel(): void
  {
    /* pass here the table id */
    let element = document.getElementById('excel-table');
    const ws: XLSX.WorkSheet =XLSX.utils.table_to_sheet(this.listaArticulos);

    /* generate workbook and add the worksheet */
    const wb: XLSX.WorkBook = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb, ws, 'Sheet1');

    /* save to file */
    XLSX.writeFile(wb, this.fileName);

  }


}
