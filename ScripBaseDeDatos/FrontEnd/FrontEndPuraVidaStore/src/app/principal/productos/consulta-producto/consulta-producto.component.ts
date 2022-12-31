import { activo } from './../../../activo';
import { ProductoModel } from './../../../models/producto-model';
import { ProductoServiceService } from './../../../services/producto-service.service';
import { Component, OnInit } from '@angular/core';
import { TipoProductoModel } from 'src/app/models/tipo-producto';
import { Archivo } from 'src/app/utils/Archivos';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-consulta-producto',
  templateUrl: './consulta-producto.component.html',
  styleUrls: ['./consulta-producto.component.css'],
  providers: [MessageService],
})
export class ConsultaProductoComponent implements OnInit {
  buscadorCodigoDeBarras: string = '';
  displayModal: boolean = false;
  esAdministrador: boolean = activo.esAministrador();
  enfoque: boolean = true;

  tipoProducto: TipoProductoModel = {
    tppId: 0,
    tppDescripcion: '',
    tppVisible: null,
  };

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
    pdrFoto: null,
    pdrTieneExistencias: false,
    prdIdTipoProductoNavigation: this.tipoProducto,
  };
  foto: string = this.leerArchivo(this.producto.pdrFoto);
  listaProductos: ProductoModel[] = [];

  constructor(
    private servicio: ProductoServiceService,
    private messageService: MessageService
  ) {}

  ngOnInit(): void {}

  showModalDialog() {
    this.displayModal = true;
  }

  obtenerPorCodigoDeBarras() {
    this.servicio
      .ObtenerProductoPorCodigo(this.buscadorCodigoDeBarras)
      .subscribe({
        next: (x) => {
          this.producto = x;
          this.foto = this.leerArchivo(this.producto.pdrFoto);
        },
        error: (_e) => {
          this.showError()
          console.log(_e);
        },
      });
  }

  limpiar() {
    this.buscadorCodigoDeBarras = '';
    this.tipoProducto = {
      tppId: 0,
      tppDescripcion: '',
      tppVisible: null,
    };

    this.producto = {
      prdId: 0,
      prdNombre: '',
      prdPrecioVentaMayorista: 0,
      prdPrecioVentaMinorista: 0,
      prdCodigo: '',
      prdUnidadesMinimas: 0,
      prdIdTipoProducto: 0,
      prdCodigoProvedor: '',
      pdrVisible: false,
      pdrFoto: null,
      pdrTieneExistencias: false,
      prdIdTipoProductoNavigation: this.tipoProducto,
    };
    this.foto = this.leerArchivo(this.producto.pdrFoto);
    this.enfoque = true;
  }

  leerArchivo(imagen: any): string {
    if (imagen == null) {
      imagen = '';
    }
    return Archivo.lectorImagen(imagen);
  }

  activarProducto() {
    if (this.producto.prdId > 0) {
      this.servicio
        .GuardarProducto(this.producto, activo.usuarioPrograma.usrId)
        .subscribe({
          next: (x) => {},
          error: (_e) => {
            console.log(_e);
          },
        });
    }
  }

  showError() {
    this.messageService.add({
      severity: 'error',
      summary: 'Error',
      detail: 'No se encontró resultados con el valor indicado',
    });
  }
}
