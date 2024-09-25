import { Component, OnInit } from '@angular/core';
import { PrimeNGConfig } from 'primeng/api';
import { TipoProductoModel } from 'src/app/models/tipo-producto';
import { TipoProductoService } from 'src/app/services/tipo-producto.service';
import { MessageService } from 'primeng/api';
import { activo } from 'src/app/activo';

@Component({
  selector: 'app-tipo-producto',
  templateUrl: './tipo-producto.component.html',
  styleUrls: ['./tipo-producto.component.css'],
  providers: [MessageService]
})
export class TipoProductoComponent implements OnInit {
  displayModal: boolean = false;
  esAdministrador: boolean = activo.esAministrador();
  listaTipoProductos: TipoProductoModel[] = [];
  buscador: string = '';

  TipoProductoEditar: TipoProductoModel = {
    tppId: 0,
    tppDescripcion: '',
    tppVisible: false,
  };

  Encabezado: string = '';
  position: string = '';
  constructor(
    private primengConfig: PrimeNGConfig,
    private servicio: TipoProductoService,
    private messageService: MessageService,
  ) { }

  ngOnInit(): void {
    this.primengConfig.ripple = true;
    this.inicio();
  }


  showModalDialog(id: number) {
    if (id > 0) {
      this.Encabezado = 'Editar';
      this.TipoProductoEditar.tppId = id
      this.listaTipoProductos.forEach(r => {
        if (r.tppId === this.TipoProductoEditar.tppId) {
          this.TipoProductoEditar.tppVisible = r.tppVisible
          this.TipoProductoEditar=r
        }
      });
    }
    else {
      this.Encabezado = 'Agregar';
      this.reiniciarEditable();
    }

    this.displayModal = true;
  }

  reiniciarEditable() {
    this.TipoProductoEditar = {
      tppId: 0,
      tppDescripcion: '',
      tppVisible: false,
    };
  }

  async guardar() {
    if (this.TipoProductoEditar.tppId === 0) {
      this.TipoProductoEditar.tppVisible = true;
    }
    await this.servicio.guardarTipoUsuario(this.TipoProductoEditar).subscribe((x => {
      this.TipoProductoEditar = x
      this.inicio();
    }), (_e => { console.log(_e); }));
    this.Exito(this.TipoProductoEditar.tppDescripcion);


    this.displayModal = false;

  }

  inicio() {
    this.servicio.listaTipoProducto().subscribe((x => {
      this.listaTipoProductos = [];
      this.listaTipoProductos = x;

    }), (_e => console.log(_e)
    ));
  }

  Exito(mensaje: string) {
    this.messageService.add({ severity: 'success', summary: 'Se guardó con éxito el tipo de producto', detail: mensaje });
  }

  cambio(parametro: TipoProductoModel) {
    this.servicio.guardarTipoUsuario(parametro).subscribe((x => { }), (_e => { console.log(_e); }));
  }

  sujerencias(evento:any) {
    console.log(evento);
    this.servicio.sugerencias(evento).subscribe((x => {
      this.listaTipoProductos=[];
      this.listaTipoProductos =x;
      this.reiniciarEditable();
    }), (_e =>{

      this.inicio()
      console.log(_e)
    } ))
  }

}
