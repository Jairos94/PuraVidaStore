import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductoModel } from 'src/app/models/producto-model';
import { TipoProductoModel } from 'src/app/models/tipo-producto';
import { TipoProductoService } from 'src/app/services/tipo-producto.service';

@Component({
  selector: 'app-agregar-editar',
  templateUrl: './agregar-editar.component.html',
  styleUrls: ['./agregar-editar.component.css']
})
export class AgregarEditarComponent implements OnInit {

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

  constructor(
    private servicioTipoProducto: TipoProductoService,
    private ruta: Router,
    private route: ActivatedRoute,
  ) { }

  ngOnInit(): void {
    this.listaTipoProductoFiltrado();
    const parametroId = this.route.snapshot.paramMap.get('id');
  }

  archivo(evento: any) {
    const archivo = <File>evento.currentFiles[0];
    this.archivos.push(archivo);

    //Metodo para colocar en variable


  }




  listaTipoProductoFiltrado() {
    this.servicioTipoProducto.listaTipoProductoFiltrado().subscribe((tp => {
      this.listaTipoProductos = tp;
    }), (_e => console.log(_e)));
  }
}
