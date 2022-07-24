import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { activo } from '../activo';
import { UsuarioServiceService } from '../services/usuario-service.service';
import { MessageService, PrimeNGConfig } from 'primeng/api';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  providers: [MessageService]
})
export class LoginComponent implements OnInit {
  Usuario: string = '';
  Contrasena: string = '';
  constructor(private servicio: UsuarioServiceService,
    private route: Router,
    private messageService: MessageService,
    private primengConfig: PrimeNGConfig) { }

  ngOnInit(): void {

    this.primengConfig.ripple = true;
  }

  validar() {
<<<<<<< HEAD
    try {
      this.servicio.login(this.Usuario, this.Contrasena).subscribe((u => {
        activo.usuarioPrograma = u;
        this.route.navigate(['principal'])
      }), (_error => {
  
        let datoError = '';
          datoError = _error.error;
          this.showError(datoError);
          console.log(datoError);
          
      }));
    } catch (error) {
      this.showError('Error de conexción con el servidor');
    }
   
=======
    this.servicio.login(this.Usuario, this.Contrasena).subscribe((u => {

      activo.usuarioPrograma = u;

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
>>>>>>> feature/Cambios
  }

  showError(MensajeError: string) {
    this.messageService.add({ severity: 'error', summary: 'Error', detail: MensajeError });
  }

}
