import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { switchMap } from 'rxjs/operators';
import { activo } from 'src/app/activo';
import { PersonaModel } from 'src/app/models/persona-model';
import { RolModel } from 'src/app/models/rol-model';
import { UsuarioModel } from 'src/app/models/usuario-model';
import { RolServiceService } from 'src/app/services/rol-service.service';
import { UsuarioServiceService } from 'src/app/services/usuario-service.service';

@Component({
  selector: 'app-editar-nuevo',
  templateUrl: './editar-nuevo.component.html',
  styleUrls: ['./editar-nuevo.component.css']
})
export class EditarNuevoComponent implements OnInit {
  identificacion='';
  usuarioEdtitar: UsuarioModel | undefined;
  personaM!: PersonaModel ;
  listaRoles:RolModel[]=[];


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
    { this.servicio.usuarioPorId(parameto).subscribe((x=>
      {
        this.cargarDatos(x)
      }),
     (_error=>console.log(_error)));

     
    }
  }

  cargarDatos(usuario:UsuarioModel)
  {
    this.usuarioEdtitar=usuario
    this.personaM=usuario.persona
    this.identificacion= this.personaM.psrIdentificacion;
  }

  gestionInicio(){
    
    this.servicioRol.listaRoles().subscribe((x=>{
      this.listaRoles=x;
      
    }),(_error=> console.log(_error)
    ));
    
  }
}
