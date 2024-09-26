import { Component, OnInit } from '@angular/core';
import { MessageService } from 'primeng/api';
import { activo } from 'src/app/activo';
import { ProductoModel } from 'src/app/models/producto-model';
import { TipoProductoModel } from 'src/app/models/tipo-producto';
import { ProductoServiceService } from 'src/app/services/producto-service.service';
import { Archivo } from 'src/app/utils/Archivos';

@Component({
  selector: 'app-lista-productos',
  templateUrl: './lista-productos.component.html',
  styleUrls: ['./lista-productos.component.css'],
  providers: [MessageService]
})
export class ListaProductosComponent implements OnInit {

  listaProductos: ProductoModel[] = [];
  buscador: string = "";
  esAdministrador: boolean = false;
  constructor(private servicioProducto: ProductoServiceService,private messageService: MessageService) { }

  ngOnInit(): void {
    this.CargarLista();
    this.esAdministrador = activo.esAministrador();
  }

  CargarLista() {
    this.servicioProducto.ListaProductoService().subscribe((x => {
      this.listaProductos = [];
      this.listaProductos = x;

    }), (_e => { console.log(_e); }));
  }

  EliminarProducto(id: number, tipoProducto: TipoProductoModel) {

    this.servicioProducto.ProductoPorID(id).subscribe((x => {

      x.pdrVisible = false;
      this.servicioProducto.GuardarProducto(x, activo.usuarioPrograma.usrId).subscribe((z => {
        this.CargarLista();
        this.showError();
      }), (_e => console.log(_e)));
    }), (_e => console.log(_e)));
  }
  FiltrarPorBuscador() {
    if (this.buscador != "") {
      this.servicioProducto.BuscarPorPalabra(this.buscador).subscribe((x => {

        if (x.length > 0) {
          this.listaProductos = [];
          this.listaProductos = x;
        } else { this.CargarLista(); }

      }), (_e => { console.error(_e) }));
    } else {
      this.CargarLista();
    }


  }

  leerArchivo(imagen: any): string {

    if (imagen == null) {
      imagen = '';
    }
    return Archivo.lectorImagen(imagen);
  }




  showError() {
    this.messageService.add({severity:'error', summary: 'Inactivo', detail: 'Cambió de estado a inactivo'});
}
}
