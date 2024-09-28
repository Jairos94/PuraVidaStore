import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { activo } from '../activo';
import { UsuarioServiceService } from '../services/usuario-service.service';
import { MessageService, PrimeNGConfig } from 'primeng/api';
import { EncripDesencrip } from '../utils/EncripDesencrip';
import { BodegaService } from '../services/bodega.service';
import { BodegaModel } from '../models/bodega-model';
import { ParametrosService } from '../services/parametros.service';
import { firstValueFrom } from 'rxjs'; // Importar firstValueFrom

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  providers: [MessageService],
})
export class LoginComponent implements OnInit {
  Usuario: string = '';
  Contrasena: string = '';
  listaBodegas: BodegaModel[] = [];
  bodegaSeleccionada: BodegaModel = {
    bdgId: 0,
    bdgDescripcion: '',
    bdgVisible: true,
  };

  constructor(
    private servicio: UsuarioServiceService,
    private servicioBodega: BodegaService,
    private servicioParametros: ParametrosService,
    private route: Router,
    private messageService: MessageService,
    private primengConfig: PrimeNGConfig
  ) {}

  async ngOnInit() {
    this.primengConfig.ripple = true;
    this.obtenerBodegas();
  }

  async validar() {
    try {
      const u = await firstValueFrom(
        this.servicio.login(this.Usuario, EncripDesencrip.encryptUsingAES256(this.Contrasena))
      );
  
      activo.usuarioPrograma = u.usuario;
      activo.token = u.token;
      activo.bodegaIngreso = this.bodegaSeleccionada;
  
      await this.obtenerParametrosGlabales(activo.bodegaIngreso.bdgId);
      this.route.navigate(['/principal']);
    } catch (error: any) { // Especificamos que 'error' puede ser de cualquier tipo
      let datoError = '';
      if (error.error) {
        datoError = error.error; // Aquí aseguramos que existe 'error'
      } else {
        datoError = 'Error de conexión';
      }
  
      this.showError(datoError);
      console.log(datoError);
    }
  }
  

  obtenerBodegas() {
    this.servicioBodega.listaBodegas().subscribe(
      (x) => {
        this.listaBodegas = x;
      },
      (_e) => console.error(_e)
    );
  }

  async obtenerParametrosGlabales(idBodega: number) {
    try {
      const x = await firstValueFrom(this.servicioParametros.ObtenerPametros(idBodega));
      activo.parametrosGlobales = x;
      activo.listaImpuestos = activo.parametrosGlobales.impuestosPorParametros
        ?.filter(impuesto => impuesto.impPidImpuestoNavigation != null)
        .map(impuesto => impuesto.impPidImpuestoNavigation!) || [];
    } catch (_e) {
      console.log(_e);
    }
  }

  showError(MensajeError: string) {
    this.messageService.add({
      severity: 'error',
      summary: 'Error',
      detail: MensajeError,
    });
  }
}
