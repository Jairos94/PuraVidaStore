import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { switchMap } from 'rxjs/operators';
import { activo } from 'src/app/activo';
import { RolModel } from 'src/app/models/rol-model';
import { UsuarioModel } from 'src/app/models/usuario-model';
import { RolServiceService } from 'src/app/services/rol-service.service';
import { UsuarioServiceService } from 'src/app/services/usuario-service.service';
import { PersonasComponent } from '../../personas/personas.component';

@Component({
  selector: 'app-editar-nuevo',
  templateUrl: './editar-nuevo.component.html',
  styleUrls: ['./editar-nuevo.component.css']
})
export class EditarNuevoComponent implements OnInit {
  id: number = 0 ;
  usuarioEdtitar: UsuarioModel | undefined;
  listaRoles:RolModel[]=[];
  personaComponete:PersonasComponent | undefined


  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private servicio:UsuarioServiceService,
    private servicioRol:RolServiceService,
  ) { }

  ngOnInit(): void {
    const parametroId = this.route.snapshot.paramMap.get('id');
    this.validacin(parametroId);
    this.gestionInicio();
  }


  validacin(parameto: any) {
    if (parameto>0) 
    { 
    
      
      this.servicio.usuarioPorId(parameto).subscribe((x=>
        {
  
          this.usuarioEdtitar=x;
          activo.ConsultaIdPersona=x.idPersona
          activo.limpiarPersona();
          
          activo.personaInteractiva=x.persona
          console.log('antes de valicación persona interactiva');
          this.personaComponete?.validacion();
          console.log('Despues de la calidación');
        }
        ),(_error=>console.log(_error)
      ));
     
    }
  }

  gestionInicio(){
    this.servicioRol.listaRoles().subscribe((x=>{
      this.listaRoles=x;
      
    }),(_error=> console.log(_error)
    ));
    
  }
}
