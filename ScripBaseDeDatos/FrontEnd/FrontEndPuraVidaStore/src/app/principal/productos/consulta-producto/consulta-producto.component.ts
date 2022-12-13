import { ProductoModel } from './../../../models/producto-model';
import { ProductoServiceService } from './../../../services/producto-service.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-consulta-producto',
  templateUrl: './consulta-producto.component.html',
  styleUrls: ['./consulta-producto.component.css'],
})
export class ConsultaProductoComponent implements OnInit {
  buscadorCodigoDeBarras: string = '';
  displayModal: boolean = false;
  producto: ProductoModel = {
    prdId: 0,
    prdNombre: '',
    prdPrecioVentaMayorista: 0,
    prdPrecioVentaMinorista: 0,
    prdCodigo: '',
    prdUnidadesMinimas: 0,
    prdIdTipoProducto: 0,
    prdCodigoProvedor: '',
    pdrVisible: false,
    pdrFoto: '',
    pdrTieneExistencias: false,
    prdIdTipoProductoNavigation: null,
  };
  listaProductos: ProductoModel[] = [];

  constructor(private servicio: ProductoServiceService) {}

  ngOnInit(): void {}

  showModalDialog() {
    this.displayModal = true;
  }

  obtenerPorCodigoDeBarras() {
    this.servicio
      .ObtenerProductoPorCodigo(this.buscadorCodigoDeBarras)
      .subscribe({
        next: (x) => {
          console.log(x);
          this.producto = x;
        },
        error: (_e) => {
          console.log(_e);
        },
      });
  }
}
