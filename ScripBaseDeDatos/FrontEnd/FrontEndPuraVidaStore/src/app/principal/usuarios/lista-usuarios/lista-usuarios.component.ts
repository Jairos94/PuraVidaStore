import { Component, OnInit } from '@angular/core';
import { UsuarioModel } from 'src/app/models/usuario-model';
import { UsuarioServiceService } from 'src/app/services/usuario-service.service';

//prime ng
import { MessageService, PrimeNGConfig } from 'primeng/api';
import { activo } from 'src/app/activo';

@Component({
  selector: 'app-lista-usuarios',
  templateUrl: './lista-usuarios.component.html',
  styleUrls: ['./lista-usuarios.component.css'],
  providers: [MessageService]
})
export class ListaUsuariosComponent implements OnInit {

  listaUsuario: UsuarioModel[] = [];
  idUsarioBorrar: number = 0;

  constructor(
    private messageService: MessageService,
    private servicio: UsuarioServiceService,
    private primengConfig: PrimeNGConfig,
  ) { }

  ngOnInit(): void {
    this.primengConfig.ripple = true;
    this.llenarUsuarios();

  }

  esVisible(id: number) {
    if (activo.usuarioPrograma.idUsuario != null) {
      if (activo.usuarioPrograma.idUsuario === id) {
        return true
      }
      else { return false }
    }
    else {
      return false;
    }
  }

  llenarUsuarios() {
    this.servicio.listaUsuarios().subscribe(
      (lista => {
        this.listaUsuario = lista;
      }),
      (_e => { console.log(_e); }));
  }

  editarUsuario(id:number){
    activo.ConsultaIdPersona=id;
  }

  showSuccess() {
    this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Message Content' });
  }

  showInfo() {
    this.messageService.add({ severity: 'info', summary: 'Info', detail: 'Message Content' });
  }

  showWarn() {
    this.messageService.add({ severity: 'warn', summary: 'Warn', detail: 'Message Content' });
  }

  showError() {
    this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Message Content' });
  }

  showTopLeft() {
    this.messageService.add({ key: 'tl', severity: 'info', summary: 'Info', detail: 'Message Content' });
  }

  showTopCenter() {
    this.messageService.add({ key: 'tc', severity: 'info', summary: 'Info', detail: 'Message Content' });
  }

  showBottomCenter() {
    this.messageService.add({ key: 'bc', severity: 'info', summary: 'Info', detail: 'Message Content' });
  }

  showConfirm(nombre: string,id:number) {
    this.idUsarioBorrar=id;
    this.messageService.clear();
    this.messageService.add({
      key: 'c',
      sticky: true,
      severity: 'warn',
      summary: '¿Estás seguro de eliminar a ' + nombre + '?',
      detail: 'Confirmar proceso'
    });
  }

  showMultiple() {
    this.messageService.addAll([
      { severity: 'info', summary: 'Message 1', detail: 'Message Content' },
      { severity: 'info', summary: 'Message 2', detail: 'Message Content' },
      { severity: 'info', summary: 'Message 3', detail: 'Message Content' }
    ]);
  }

  showSticky() {
    this.messageService.add({ severity: 'info', summary: 'Sticky', detail: 'Message Content', sticky: true });
  }

  onConfirm() {
    this.messageService.clear('c');
    console.log('se presionó sí ' + this.idUsarioBorrar);

  }

  onReject() {
    this.idUsarioBorrar=0;
    this.messageService.clear('c');
    console.log('se presionó no ' + this.idUsarioBorrar);
  }

  clear() {
    this.messageService.clear();
  }


}
