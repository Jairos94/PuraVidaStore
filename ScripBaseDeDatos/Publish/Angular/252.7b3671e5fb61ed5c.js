"use strict";(self.webpackChunkFrontEndPuraVidaStore=self.webpackChunkFrontEndPuraVidaStore||[]).push([[252],{7252:(Y,A,a)=>{a.r(A),a.d(A,{MantenimientoProductosModule:()=>u});var h=a(6895),s=a(8184),e=a(433),P=a(8590),v=a(8505),o=a(8256),C=a(9349),f=a(3441),T=a(5593),_=a(2210),M=a(9983),U=a(5047),I=a(1740),b=a(1795);function E(i,t){1&i&&(o.TgZ(0,"span",20),o._uU(1,"Campo requerido"),o.qZA())}function q(i,t){1&i&&(o.TgZ(0,"span",20),o._uU(1,"Campo requerido"),o.qZA())}function x(i,t){if(1&i){const r=o.EpF();o.TgZ(0,"div",9)(1,"h2"),o._uU(2,"Foto"),o.qZA(),o.TgZ(3,"p-fileUpload",21),o.NdJ("onSelect",function(d){o.CHM(r);const m=o.oxw();return o.KtG(m.archivo(d))})("onRemove",function(){o.CHM(r);const d=o.oxw();return o.KtG(d.eliminarImagen())}),o.qZA()()}2&i&&(o.xp6(3),o.Q6J("auto",!0))}function y(i,t){if(1&i){const r=o.EpF();o.TgZ(0,"div",9)(1,"div",22)(2,"div",23),o._UZ(3,"img",24),o.qZA(),o.TgZ(4,"div",25)(5,"div",9)(6,"div",26)(7,"button",27),o.NdJ("click",function(){o.CHM(r);const d=o.oxw();return o.KtG(d.eliminarImagen())}),o.qZA()()()()()()}if(2&i){const r=o.oxw();o.xp6(3),o.Q6J("src",r.imagen,o.LSH),o.xp6(4),o.Q6J("disabled",!r.HayImagen)}}class g{constructor(t,r,n,d,m){this.servicioTipoProducto=t,this.ruta=r,this.route=n,this.fb=d,this.servicioProducto=m,this.EsImagen=!1,this.HayImagen=!1,this.imagen="",this.listaTipoProductos=[],this.productoEditarAgregar={prdId:0,prdNombre:"",prdPrecioVentaMayorista:0,prdPrecioVentaMinorista:0,prdCodigo:"",prdUnidadesMinimas:0,prdIdTipoProducto:0,prdCodigoProvedor:"",pdrVisible:!0,pdrFoto:null,pdrTieneExistencias:!1,prdIdTipoProductoNavigation:null},this.productoForm=this.fb.group({NombreProducto:[this.productoEditarAgregar.prdNombre,[e.kI.required]],PrecioVentaMayorista:[this.productoEditarAgregar.prdPrecioVentaMayorista,[e.kI.required]],prcioVentaMinorista:[this.productoEditarAgregar.prdPrecioVentaMinorista,[e.kI.required]],UnidadesMinimas:[this.productoEditarAgregar.prdUnidadesMinimas,[e.kI.required]],IdTipoProducto:[this.productoEditarAgregar.prdIdTipoProducto,[e.kI.required]],CodigoProveedor:[this.productoEditarAgregar.prdCodigoProvedor,[e.kI.required]],Foto:[this.productoEditarAgregar.pdrFoto]})}ngOnInit(){this.listaTipoProductoFiltrado(),this.parametroId=this.route.snapshot.paramMap.get("id"),this.parametroId>0&&this.obtenerProducto()}archivo(t){let r="";this.base64Image=v.Z.convertFile(t.currentFiles[0]).subscribe(d=>{r=d.toString(),this.productoEditarAgregar.pdrFoto=r})}guardar(){this.productoEditarAgregar.prdId=parseInt(this.parametroId),this.productoEditarAgregar.prdNombre=this.productoForm.get("NombreProducto")?.value,this.productoEditarAgregar.prdPrecioVentaMayorista=this.productoForm.get("PrecioVentaMayorista")?.value,this.productoEditarAgregar.prdPrecioVentaMinorista=this.productoForm.get("prcioVentaMinorista")?.value,this.productoEditarAgregar.prdUnidadesMinimas=this.productoForm.get("UnidadesMinimas")?.value,this.productoEditarAgregar.prdIdTipoProducto=this.productoForm.get("IdTipoProducto")?.value,this.productoEditarAgregar.prdCodigoProvedor=this.productoForm.get("CodigoProveedor")?.value,this.listaTipoProductos.forEach(t=>{t.tppId===this.productoEditarAgregar.prdIdTipoProducto&&(this.productoEditarAgregar.prdIdTipoProductoNavigation=t)}),this.servicioProducto.GuardarProducto(this.productoEditarAgregar,P.o.usuarioPrograma.usrId).subscribe(t=>{console.log(t),this.ruta.navigate(["./principal/productos/"])},t=>{console.log(t)})}listaTipoProductoFiltrado(){this.servicioTipoProducto.listaTipoProductoFiltrado().subscribe(t=>{this.listaTipoProductos=t},t=>console.log(t))}obtenerProducto(){this.servicioProducto.ProductoPorID(this.parametroId).subscribe(t=>{this.productoEditarAgregar=t,null!=this.productoEditarAgregar.pdrFoto&&(this.imagen=v.Z.lectorImagen(this.productoEditarAgregar.pdrFoto),this.HayImagen=!0),this.productoForm=this.fb.group({NombreProducto:[this.productoEditarAgregar.prdNombre,[e.kI.required]],PrecioVentaMayorista:[this.productoEditarAgregar.prdPrecioVentaMayorista,[e.kI.required]],prcioVentaMinorista:[this.productoEditarAgregar.prdPrecioVentaMinorista,[e.kI.required]],UnidadesMinimas:[this.productoEditarAgregar.prdUnidadesMinimas,[e.kI.required]],IdTipoProducto:[this.productoEditarAgregar.prdIdTipoProducto,[e.kI.required]],CodigoProveedor:[this.productoEditarAgregar.prdCodigoProvedor,[e.kI.required]],Foto:[this.productoEditarAgregar.pdrFoto]})},t=>console.log(t))}eliminarImagen(){this.productoEditarAgregar.pdrFoto=null,this.imagen="",this.imagen=v.Z.lectorImagen(this.imagen),this.HayImagen=!1}showSuccess(){}}g.\u0275fac=function(t){return new(t||g)(o.Y36(C.z),o.Y36(s.F0),o.Y36(s.gz),o.Y36(e.qu),o.Y36(f.M))},g.\u0275cmp=o.Xpm({type:g,selectors:[["app-agregar-editar"]],decls:59,vars:12,consts:[[3,"formGroup","ngSubmit"],[1,"field","grid"],[1,"p-inputgroup"],[1,"p-inputgroup-addon"],[1,"pi","pi-user"],["type","text","id","NombreProducto","formControlName","NombreProducto","pInputText","","placeholder","Nombre del producto"],["class","error",4,"ngIf"],[1,"flied","grid"],[1,"formgroup-inline"],[1,"field"],[1,"pi","pi-tags",2,"line-height","1.25"],[1,"pi","pi-shopping-cart",2,"line-height","1.25"],["type","number","id","prcioVentaMinorista","formControlName","prcioVentaMinorista","pInputText","","placeholder","Precio venta"],["type","number","id","PrecioVentaMayorista","formControlName","PrecioVentaMayorista","pInputText","","placeholder","precio venta mayorista"],["type","text","id","CodigoProveedor","formControlName","CodigoProveedor","pInputText","","placeholder","C\xf3digo proveedor"],["mode","decimal","inputId","minmax-buttons","id","UnidadesMinimas","formControlName","UnidadesMinimas","placeholder","Unidades m\xednimas",3,"showButtons","min"],[1,"field","mx-3"],["optionLabel","tppDescripcion","filterBy","tppDescripcion","optionValue","tppId","placeholder","Tipo de producto","id","IdTipoProducto","formControlName","IdTipoProducto",3,"options","filter","showClear"],["class","field",4,"ngIf"],["pButton","","pRipple","","type","submit","label","Guardar",1,"p-button-success",3,"disabled"],[1,"error"],["chooseLabel","Subir","chooseIcon","pi pi-upload","name","myfile[]","accept","image/*",3,"auto","onSelect","onRemove"],[1,"flex","align-items-stretch","flex-wrap","card-container","green-container",2,"min-height","200px"],[1,"flex","align-items-center","justify-content-center",2,"min-width","200px","min-height","50px"],[1,"img",3,"src"],[1,"align-self-center","flex","align-items-center","justify-content-center"],[1,"field","mt-2"],["pButton","","pRipple","","type","button","label","Eliminar Imagen",1,"p-button-danger",3,"disabled","click"]],template:function(t,r){1&t&&(o.TgZ(0,"form",0),o.NdJ("ngSubmit",function(){return r.guardar()}),o.TgZ(1,"div",1)(2,"h3"),o._uU(3,"Nombre del producto"),o.qZA(),o.TgZ(4,"div",2)(5,"span",3),o._UZ(6,"i",4),o.qZA(),o._UZ(7,"input",5),o.qZA(),o._uU(8),o.YNc(9,E,2,0,"span",6),o.qZA(),o.TgZ(10,"h3"),o._uU(11," Precios"),o.qZA(),o.TgZ(12,"div",7)(13,"div",8)(14,"div",9)(15,"div",2)(16,"span",3),o._UZ(17,"i",10),o.qZA(),o.TgZ(18,"span",3),o._UZ(19,"i",11),o.qZA(),o._UZ(20,"input",12),o.TgZ(21,"span",3),o._uU(22,"\xa2"),o.qZA()(),o.TgZ(23,"span"),o._uU(24," Precio de venta Mayorista"),o.qZA(),o.YNc(25,q,2,0,"span",6),o.qZA(),o.TgZ(26,"div",9)(27,"div",2)(28,"span",3),o._UZ(29,"i",10),o.qZA(),o.TgZ(30,"span",3),o._UZ(31,"i",11),o.qZA(),o._UZ(32,"input",13),o.TgZ(33,"span",3),o._uU(34,"\xa2"),o.qZA()(),o.TgZ(35,"span"),o._uU(36," Precio de venta minorista"),o.qZA()()()(),o.TgZ(37,"div",1)(38,"div",8)(39,"div",9)(40,"div",2)(41,"h3"),o._uU(42,"C\xf3digo proveedor"),o.qZA()()(),o.TgZ(43,"div",9)(44,"div",2),o._UZ(45,"input",14),o.qZA()()(),o.TgZ(46,"div",9)(47,"div",2),o._UZ(48,"p-inputNumber",15),o.qZA(),o.TgZ(49,"span"),o._uU(50,"Unidades m\xednimas"),o.qZA()(),o.TgZ(51,"div",16),o._UZ(52,"p-dropdown",17),o.qZA()(),o.YNc(53,x,4,1,"div",18),o.TgZ(54,"div",7)(55,"div",8),o._UZ(56,"div",9),o.YNc(57,y,8,2,"div",18),o.qZA()(),o._UZ(58,"button",19),o.qZA()),2&t&&(o.Q6J("formGroup",r.productoForm),o.xp6(8),o.hij(" ",r.productoEditarAgregar.prdCodigo," "),o.xp6(1),o.Q6J("ngIf",r.productoForm.controls.NombreProducto.invalid&&r.productoForm.controls.NombreProducto.touched),o.xp6(16),o.Q6J("ngIf",r.productoForm.controls.prcioVentaMinorista.invalid&&r.productoForm.controls.prcioVentaMinorista.touched),o.xp6(23),o.Q6J("showButtons",!0)("min",0),o.xp6(4),o.Q6J("options",r.listaTipoProductos)("filter",!0)("showClear",!0),o.xp6(1),o.Q6J("ngIf",!r.HayImagen),o.xp6(4),o.Q6J("ngIf",r.HayImagen),o.xp6(1),o.Q6J("disabled",r.productoForm.invalid))},dependencies:[h.O5,T.Hq,_.Lt,M.p,U.Rn,I.o,e._Y,e.Fj,e.wV,e.JJ,e.JL,b.H,e.sg,e.u]});var Z=a(805),F=a(8396),N=a(2453);function J(i,t){1&i&&(o.TgZ(0,"th"),o._uU(1,"Acciones"),o.qZA())}function V(i,t){if(1&i&&(o.TgZ(0,"tr")(1,"th"),o._uU(2,"Nombre"),o.qZA(),o.TgZ(3,"th"),o._uU(4,"Categor\xeda"),o.qZA(),o.TgZ(5,"th"),o._uU(6,"Imagen"),o.qZA(),o.TgZ(7,"th"),o._uU(8,"C\xf3digo"),o.qZA(),o.TgZ(9,"th"),o._uU(10,"C\xf3digo Proveedor"),o.qZA(),o.TgZ(11,"th"),o._uU(12,"Precio"),o.qZA(),o.TgZ(13,"th"),o._uU(14,"Precio Mayorista"),o.qZA(),o.TgZ(15,"th"),o._uU(16,"Unidades M\xednimas"),o.qZA(),o.YNc(17,J,2,0,"th",5),o.qZA()),2&i){const r=o.oxw();o.xp6(17),o.Q6J("ngIf",r.esAdministrador)}}function L(i,t){if(1&i){const r=o.EpF();o.TgZ(0,"td"),o._UZ(1,"button",7),o.TgZ(2,"button",8),o.NdJ("click",function(){o.CHM(r);const d=o.oxw().$implicit,m=o.oxw();return o.KtG(m.EliminarProducto(d.prdId,d.prdIdTipoProductoNavigation))}),o.qZA()()}if(2&i){const r=o.oxw().$implicit;o.xp6(1),o.MGl("routerLink","../agregar-editar/",r.prdId,""),o.xp6(1),o.Q6J("disabled",r.pdrTieneExistencias)}}function S(i,t){if(1&i&&(o.TgZ(0,"tr")(1,"td"),o._uU(2),o.qZA(),o.TgZ(3,"td"),o._uU(4),o.qZA(),o.TgZ(5,"td"),o._UZ(6,"img",6),o.qZA(),o.TgZ(7,"td"),o._uU(8),o.qZA(),o.TgZ(9,"td"),o._uU(10),o.qZA(),o.TgZ(11,"td"),o._uU(12),o.ALo(13,"currency"),o.qZA(),o.TgZ(14,"td"),o._uU(15),o.ALo(16,"currency"),o.qZA(),o.TgZ(17,"td"),o._uU(18),o.qZA(),o.YNc(19,L,3,2,"td",5),o.qZA()),2&i){const r=t.$implicit,n=o.oxw();o.xp6(2),o.Oqu(r.prdNombre),o.xp6(2),o.Oqu(r.prdIdTipoProductoNavigation.tppDescripcion),o.xp6(2),o.Q6J("src",n.leerArchivo(r.pdrFoto),o.LSH)("alt",r.prdNombre),o.xp6(2),o.Oqu(r.prdCodigo),o.xp6(2),o.Oqu(r.prdCodigoProvedor),o.xp6(2),o.Oqu(o.Dn7(13,10,r.prdPrecioVentaMinorista,"CRC","symbol-narrow")),o.xp6(3),o.Oqu(o.Dn7(16,14,r.prdPrecioVentaMayorista,"CRC","symbol-narrow")),o.xp6(3),o.Oqu(r.prdUnidadesMinimas),o.xp6(1),o.Q6J("ngIf",n.esAdministrador)}}const w=function(){return{"min-width":"60rem"}},H=function(){return[10,25,50]};class p{constructor(t,r){this.servicioProducto=t,this.messageService=r,this.listaProductos=[],this.buscador="",this.esAdministrador=!1}ngOnInit(){this.CargarLista(),this.esAdministrador=P.o.esAministrador()}CargarLista(){this.servicioProducto.ListaProductoService().subscribe(t=>{this.listaProductos=[],this.listaProductos=t},t=>{console.log(t)})}EliminarProducto(t,r){this.servicioProducto.ProductoPorID(t).subscribe(n=>{n.pdrVisible=!1,this.servicioProducto.GuardarProducto(n,P.o.usuarioPrograma.usrId).subscribe(d=>{this.CargarLista(),this.showError()},d=>console.log(d))},n=>console.log(n))}FiltrarPorBuscador(){""!=this.buscador?this.servicioProducto.BuscarPorPalabra(this.buscador).subscribe(t=>{t.length>0?(this.listaProductos=[],this.listaProductos=t):this.CargarLista()},t=>{console.error(t)}):this.CargarLista()}leerArchivo(t){return null==t&&(t=""),v.Z.lectorImagen(t)}showError(){this.messageService.add({severity:"error",summary:"Inactivo",detail:"Cambi\xf3 de estado a inactivo"})}}p.\u0275fac=function(t){return new(t||p)(o.Y36(f.M),o.Y36(Z.ez))},p.\u0275cmp=o.Xpm({type:p,selectors:[["app-lista-productos"]],features:[o._Bn([Z.ez])],decls:5,vars:8,consts:[["type","text","placeholder","Buscar","pInputText","",3,"ngModel","ngModelChange","input"],[3,"value","tableStyle","rows","paginator","rowsPerPageOptions"],["pTemplate","header"],["pTemplate","body"],["position","top-right"],[4,"ngIf"],["width","100",1,"shadow-4",3,"src","alt"],["pButton","","pRipple","","type","button","icon","pi pi-pencil",1,"p-button-rounded","p-button-warning",3,"routerLink"],["pButton","","pRipple","","type","button","icon","pi pi-trash",1,"p-button-rounded","p-button-danger",3,"disabled","click"]],template:function(t,r){1&t&&(o.TgZ(0,"input",0),o.NdJ("ngModelChange",function(d){return r.buscador=d})("input",function(){return r.FiltrarPorBuscador()}),o.qZA(),o.TgZ(1,"p-table",1),o.YNc(2,V,18,1,"ng-template",2),o.YNc(3,S,20,18,"ng-template",3),o.qZA(),o._UZ(4,"p-toast",4)),2&t&&(o.Q6J("ngModel",r.buscador),o.xp6(1),o.Q6J("value",r.listaProductos)("tableStyle",o.DdM(6,w))("rows",10)("paginator",!0)("rowsPerPageOptions",o.DdM(7,H)))},dependencies:[h.O5,s.rH,Z.jx,T.Hq,I.o,e.Fj,e.JJ,e.On,F.iA,N.FN,b.H,h.H9]});class l{constructor(){}ngOnInit(){}}l.\u0275fac=function(t){return new(t||l)},l.\u0275cmp=o.Xpm({type:l,selectors:[["app-mantenimiento-productos"]],decls:1,vars:0,template:function(t,r){1&t&&o._UZ(0,"router-outlet")},dependencies:[s.lC]});const Q=[{path:"",component:l,children:[{path:"agregar-editar/:id",component:g},{path:"lista-productos",component:p},{path:"",component:p},{path:"**",redirectTo:"/"}]}];class c{}c.\u0275fac=function(t){return new(t||c)},c.\u0275mod=o.oAB({type:c}),c.\u0275inj=o.cJS({imports:[s.Bz.forChild(Q),s.Bz]});var B=a(4632);class u{}u.\u0275fac=function(t){return new(t||u)},u.\u0275mod=o.oAB({type:u}),u.\u0275inj=o.cJS({imports:[h.ez,c,B.R,e.UX]})}}]);