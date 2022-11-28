import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { activo } from '../activo';
import { UsuarioServiceService } from '../services/usuario-service.service';
import { MessageService, PrimeNGConfig } from 'primeng/api';
import { EncripDesencrip } from '../utils/EncripDesencrip';
import { BodegaService } from '../services/bodega.service';
import { BodegaModel } from '../models/bodega-model';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  providers: [MessageService]
})
export class LoginComponent implements OnInit {
  Usuario: string = '';
  Contrasena: string = '';
  listaBodegas: BodegaModel[] = [];
  bodegaSeleccionada: BodegaModel = {
    bdgId: 0,
    bdgDescripcion: "",
    bdgVisible: true
  }
  constructor(private servicio: UsuarioServiceService,
    private servicioBodega: BodegaService,
    private route: Router,
    private messageService: MessageService,
    private primengConfig: PrimeNGConfig) { }

  ngOnInit(): void {

    this.primengConfig.ripple = true;
    this.obtenerBodegas();
  }

  validar() {
    this.servicio.login(this.Usuario, EncripDesencrip.encryptUsingAES256(this.Contrasena)).subscribe((u => {

      activo.usuarioPrograma = u.usuario;
      activo.token = u.token;
      activo.bodegaIngreso = this.bodegaSeleccionada;
      
      this.route.navigate(['/principal'])

    }), (_error => {

      let datoError = '';
      try {
        datoError = _error.error;
        this.showError(datoError);
      } catch (error) {
        datoError = 'Error de conexcion'
      }

      console.log(datoError);


    }));
  }

  obtenerBodegas() {
    this.servicioBodega.listaUsuarios()
      .subscribe((x => {
        this.listaBodegas = [];
        this.listaBodegas = x;
      }), (_e => console.error(_e)));
  }

  showError(MensajeError: string) {
    this.messageService.add({ severity: 'error', summary: 'Error', detail: MensajeError });
  }

}
