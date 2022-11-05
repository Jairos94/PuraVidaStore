import { Component, OnInit } from '@angular/core';
import { FormBuilder, MinValidator, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductoModel } from 'src/app/models/producto-model';
import { TipoProductoModel } from 'src/app/models/tipo-producto';
import { TipoProductoService } from 'src/app/services/tipo-producto.service';
import { Archivo } from 'src/app/utils/Archivos';

@Component({
  selector: 'app-agregar-editar',
  templateUrl: './agregar-editar.component.html',
  styleUrls: ['./agregar-editar.component.css']
})
export class AgregarEditarComponent implements OnInit {
  EsImagen:boolean =false
  base64Image: any;
  imagen: string = '';
  listaTipoProductos: TipoProductoModel[] = []
  productoEditarAgregar: ProductoModel =
    {
      prdId: 0,
      prdNombre: '',
      prdPrecioVentaMayorista: 0,
      prdPrecioVentaMinorista: 0,
      prdCodigo: '',
      prdUnidadesMinimas: 0,
      prdIdTipoProducto: 0,
      prdCodigoProvedor: '',
      pdrVisible: true,
      pdrFoto: null,
    };
  archivos: any = [];

  productoForm = this.fb.group({
    NombreProducto: [this.productoEditarAgregar.prdNombre, [Validators.required]],
    PrecioVentaMayorista: [this.productoEditarAgregar.prdPrecioVentaMayorista, [Validators.required]],
    prcioVentaMinorista: [this.productoEditarAgregar.prdPrecioVentaMinorista, [Validators.required]],
    UnidadesMinimas: [this.productoEditarAgregar.prdUnidadesMinimas, [Validators.required]],
    IdTipoProducto: [this.productoEditarAgregar.prdIdTipoProducto, [Validators.required]],
    CodigoProveedor: [this.productoEditarAgregar.prdCodigoProvedor, [Validators.required]],
    Foto: [this.productoEditarAgregar.pdrFoto]

  });

  constructor(
    private servicioTipoProducto: TipoProductoService,
    private ruta: Router,
    private route: ActivatedRoute,
    private fb: FormBuilder,
  ) { }

  ngOnInit(): void {
    this.listaTipoProductoFiltrado();
    const parametroId = this.route.snapshot.paramMap.get('id');
  }

  archivo(evento: any) {
    let ima: string = '';
    const archivo = <File>evento.currentFiles[0];
    this.base64Image = Archivo.convertFile(archivo).subscribe(x => {
      ima = x.toString();
      this.productoEditarAgregar.pdrFoto = ima;
    });
  }

  
  guardar() {
    console.log(this.productoForm.value);
    if(this.productoEditarAgregar.pdrFoto!=null){

      this.imagen = Archivo.lectorImagen(this.productoEditarAgregar.pdrFoto)
    }
  }


  listaTipoProductoFiltrado() {
    this.servicioTipoProducto.listaTipoProductoFiltrado().subscribe((tp => {
      this.listaTipoProductos = tp;
    }), (_e => console.log(_e)));
  }

  
}
