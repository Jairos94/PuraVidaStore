import { Component, OnInit } from '@angular/core';
import { FormBuilder, MinValidator, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { activo } from 'src/app/activo';

import { ProductoModel } from 'src/app/models/producto-model';
import { TipoProductoModel } from 'src/app/models/tipo-producto';
import { ProductoServiceService } from 'src/app/services/producto-service.service';
import { TipoProductoService } from 'src/app/services/tipo-producto.service';
import { Archivo } from 'src/app/utils/Archivos';

@Component({
  selector: 'app-agregar-editar',
  templateUrl: './agregar-editar.component.html',
  styleUrls: ['./agregar-editar.component.css']
})
export class AgregarEditarComponent implements OnInit {

  EsImagen: boolean = false
  HayImagen: boolean = false
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
      pdrTieneExistencias : false,
      prdIdTipoProductoNavigation: null
    };

  productoForm = this.fb.group({
    NombreProducto: [this.productoEditarAgregar.prdNombre, [Validators.required]],
    PrecioVentaMayorista: [this.productoEditarAgregar.prdPrecioVentaMayorista, [Validators.required]],
    prcioVentaMinorista: [this.productoEditarAgregar.prdPrecioVentaMinorista, [Validators.required]],
    UnidadesMinimas: [this.productoEditarAgregar.prdUnidadesMinimas, [Validators.required]],
    IdTipoProducto: [this.productoEditarAgregar.prdIdTipoProducto, [Validators.required]],
    CodigoProveedor: [this.productoEditarAgregar.prdCodigoProvedor],
    Foto: [this.productoEditarAgregar.pdrFoto]

  });

  parametroId: any;

  constructor(
    //private messageService: MessageService,
    private servicioTipoProducto: TipoProductoService,
    private ruta: Router,
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private servicioProducto: ProductoServiceService,

  ) { }

  ngOnInit(): void {
    this.listaTipoProductoFiltrado();
    this.parametroId = this.route.snapshot.paramMap.get('id');
    if (this.parametroId > 0) {
      this.obtenerProducto();
    }
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

    this.productoEditarAgregar.prdId = parseInt(this.parametroId);
    this.productoEditarAgregar.prdNombre = this.productoForm.get('NombreProducto')?.value!;
    this.productoEditarAgregar.prdPrecioVentaMayorista = this.productoForm.get('PrecioVentaMayorista')?.value!;
    this.productoEditarAgregar.prdPrecioVentaMinorista = this.productoForm.get('prcioVentaMinorista')?.value!;
    this.productoEditarAgregar.prdUnidadesMinimas = this.productoForm.get('UnidadesMinimas')?.value!;
    this.productoEditarAgregar.prdIdTipoProducto = this.productoForm.get('IdTipoProducto')?.value!;
    this.productoEditarAgregar.prdCodigoProvedor = this.productoForm.get('CodigoProveedor')?.value!;

if(this.productoEditarAgregar.prdCodigo===null|| this.productoEditarAgregar.prdCodigo===undefined ||this.productoEditarAgregar.prdCodigo===''){
  this.productoEditarAgregar.prdCodigo==null;
}

    this.listaTipoProductos.forEach(x => {
      if (x.tppId === this.productoEditarAgregar.prdIdTipoProducto) {
        this.productoEditarAgregar.prdIdTipoProductoNavigation = x

      }
    });

    this.servicioProducto.GuardarProducto(this.productoEditarAgregar,activo.usuarioPrograma.usrId).subscribe((x => {
      console.log(x);
      //this.showSuccess();
      this.ruta.navigate(['./principal/productos/lista-productos'])

    }),
      (_e => {
        console.log(_e);
      }));

  }


  listaTipoProductoFiltrado() {
    this.servicioTipoProducto.listaTipoProductoFiltrado().subscribe((tp => {
      this.listaTipoProductos = tp;
    }), (_e => console.log(_e)));
  }

  obtenerProducto() {
    this.servicioProducto.ProductoPorID(this.parametroId).subscribe((x => {
      this.productoEditarAgregar = x;
      if (this.productoEditarAgregar.pdrFoto != null) {
        this.imagen = Archivo.lectorImagen(this.productoEditarAgregar.pdrFoto)
        this.HayImagen = true;
      }
      this.productoForm = this.fb.group({
        NombreProducto: [this.productoEditarAgregar.prdNombre, [Validators.required]],
        PrecioVentaMayorista: [this.productoEditarAgregar.prdPrecioVentaMayorista, [Validators.required]],
        prcioVentaMinorista: [this.productoEditarAgregar.prdPrecioVentaMinorista, [Validators.required]],
        UnidadesMinimas: [this.productoEditarAgregar.prdUnidadesMinimas, [Validators.required]],
        IdTipoProducto: [this.productoEditarAgregar.prdIdTipoProducto, [Validators.required]],
        CodigoProveedor: [this.productoEditarAgregar.prdCodigoProvedor],
        Foto: [this.productoEditarAgregar.pdrFoto]

      });
    }), (_e => console.log(_e)));
  }

  eliminarImagen() {
    this.productoEditarAgregar.pdrFoto = null
    this.imagen = ''
    this.imagen = Archivo.lectorImagen(this.imagen);
    this.HayImagen = false
  }
  showSuccess() {
    //this.messageService.add({severity:'success', summary: 'Success', detail: 'Message Content'});
  }

}
