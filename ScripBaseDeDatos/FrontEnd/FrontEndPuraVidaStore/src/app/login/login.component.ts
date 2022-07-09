import { Component, OnInit } from '@angular/core';
import { activo } from '../activo';
import { UsuarioServiceService } from '../services/usuario-service.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  Usuario: string = '';
  Contrasena: string = '';
  constructor(private servicio: UsuarioServiceService) { }

  ngOnInit(): void {
  }

  validar() {
    this.servicio.login(this.Usuario, this.Contrasena).subscribe((u => {

      activo.usuarioPrograma=u;
    }), (_error => {
      
      try {
        console.log(_error.error);
      } catch (error) {
        
      }
      
      
    }));


  }

}
