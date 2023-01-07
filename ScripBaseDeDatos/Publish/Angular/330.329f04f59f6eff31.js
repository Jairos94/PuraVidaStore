"use strict";(self.webpackChunkFrontEndPuraVidaStore=self.webpackChunkFrontEndPuraVidaStore||[]).push([[330],{6330:(U,B,i)=>{i.r(B),i.d(B,{BodegasModule:()=>r});var f=i(6895),p=i(8184),e=i(8256),v=i(8861);class l{constructor(){this.items=[]}ngOnInit(){this.items=[{label:"Lista de Bodegas",icon:"pi pi-home",routerLink:"lista-bodegas"}]}}l.\u0275fac=function(t){return new(t||l)},l.\u0275cmp=e.Xpm({type:l,selectors:[["app-bodegas"]],decls:6,vars:1,consts:[[1,"formgrid","grid","my-2"],[1,"field","col-2"],[3,"model"],["menu",""],[1,"field","col"]],template:function(t,o){1&t&&(e.TgZ(0,"div",0)(1,"div",1),e._UZ(2,"p-slideMenu",2,3),e.qZA(),e.TgZ(4,"div",4),e._UZ(5,"router-outlet"),e.qZA()()),2&t&&(e.xp6(2),e.Q6J("model",o.items))},dependencies:[p.lC,v.aj]});var c=i(805),u=i(8590),C=i(9401),Z=i(5593),T=i(1493),_=i(1740),m=i(433),y=i(8396),A=i(2453),x=i(1795);function M(a,t){1&a&&(e.TgZ(0,"tr")(1,"th"),e._uU(2,"#"),e.qZA(),e.TgZ(3,"th"),e._uU(4,"Descripcion"),e.qZA(),e.TgZ(5,"th"),e._uU(6,"Acciones"),e.qZA()())}function J(a,t){if(1&a){const o=e.EpF();e.TgZ(0,"tr")(1,"td"),e._uU(2),e.qZA(),e.TgZ(3,"td"),e._uU(4),e.qZA(),e.TgZ(5,"td")(6,"button",13),e.NdJ("click",function(){const b=e.CHM(o).$implicit,h=e.oxw();return e.KtG(h.EditarBodega(b.bdgId))}),e.qZA(),e.TgZ(7,"button",14),e.NdJ("click",function(){const b=e.CHM(o).$implicit,h=e.oxw();return e.KtG(h.borrarBodega(b))}),e.qZA()()()}if(2&a){const o=t.$implicit,s=e.oxw();e.xp6(2),e.Oqu(o.bdgId),e.xp6(2),e.Oqu(o.bdgDescripcion),e.xp6(3),e.Q6J("disabled",s.deshabilitarEliminarBodegaActual(o.bdgId))}}function I(a,t){if(1&a){const o=e.EpF();e.TgZ(0,"button",15),e.NdJ("click",function(){e.CHM(o);const n=e.oxw();return e.KtG(n.GuardarBodega())}),e.qZA(),e.TgZ(1,"button",16),e.NdJ("click",function(){e.CHM(o);const n=e.oxw();return e.KtG(n.display=!1)}),e.qZA()}}const L=function(){return{"min-width":"50rem"}},S=function(){return{width:"50vw"}};class g{constructor(t,o){this.ServicioBodega=t,this.messageService=o,this.listaBodegas=[],this.display=!1,this.header="",this.buscar="",this.bodega={bdgId:0,bdgDescripcion:"",bdgVisible:!0}}ngOnInit(){this.ObtenerBodegas()}ObtenerBodegas(){this.ServicioBodega.listaUsuarios().subscribe(t=>{this.listaBodegas=[],this.listaBodegas=t},t=>console.error(t))}AgregarBodega(){this.display=!0,this.header="Ingresar Bodega"}EditarBodega(t){this.display=!0,this.header="Editar Bodega",this.ServicioBodega.bodegaPorId(t).subscribe(o=>{this.bodega=o},o=>console.error(o))}deshabilitarEliminarBodegaActual(t){return t===u.o.bodegaIngreso.bdgId}GuardarBodega(){this.ServicioBodega.guardarBodega(this.bodega).subscribe(t=>{this.ObtenerBodegas(),this.display=!1,this.mensajeExito()},t=>console.error(t)),this.bodega.bdgId>0&&this.bodega.bdgId==u.o.bodegaIngreso.bdgId&&(u.o.bodegaIngreso=this.bodega)}borrarBodega(t){t.bdgVisible=!1,this.ServicioBodega.guardarBodega(t).subscribe(o=>{this.mensajeError(),this.ObtenerBodegas()},o=>console.error(o))}buscarPorDescription(){""!=this.buscar?this.ServicioBodega.listaUsuariosPorDescripcion(this.buscar).subscribe(t=>{t.length>0?(this.listaBodegas=[],this.listaBodegas=t):this.ObtenerBodegas()},t=>console.error(t)):this.ObtenerBodegas()}mensajeExito(){this.messageService.add({severity:"success",summary:"\xc9xito al guardar",detail:"Se guard\xf3 con \xe9xito al guargar"})}mensajeError(){this.messageService.add({severity:"error",summary:"Bodega de eliminada",detail:"Se elimin\xf3 la bodega seleccionada"})}}g.\u0275fac=function(t){return new(t||g)(e.Y36(C.W),e.Y36(c.ez))},g.\u0275cmp=e.Xpm({type:g,selectors:[["app-lista-bodegas"]],features:[e._Bn([c.ez])],decls:18,vars:13,consts:[["position","top-left"],[1,"grid","my-3"],["pButton","","pRipple","","type","button","icon","pi pi-plus",1,"col-1","p-button-rounded",3,"click"],[1,"col","p-float-label"],["id","float-input","type","text","pInputText","",3,"ngModel","ngModelChange","input"],["for","float-input"],[3,"value","tableStyle"],["pTemplate","header"],["pTemplate","body"],[3,"header","visible","modal","draggable","resizable","visibleChange"],[1,"p-float-label"],["id","float-input","type","text","pInputText","",3,"ngModel","ngModelChange"],["pTemplate","footer"],["pButton","","pRipple","","type","button","icon","pi pi-pencil",1,"p-button-rounded","p-button-warning",3,"click"],["pButton","","pRipple","","type","button","icon","pi pi-trash",1,"p-button-rounded","p-button-danger",3,"disabled","click"],["icon","pi pi-check","pButton","","type","button","label","Guardar",3,"click"],["icon","pi pi-times","pButton","","type","button","label","Cancelar",3,"click"]],template:function(t,o){1&t&&(e._UZ(0,"p-toast",0),e.TgZ(1,"div",1)(2,"button",2),e.NdJ("click",function(){return o.AgregarBodega()}),e.qZA(),e.TgZ(3,"span",3)(4,"input",4),e.NdJ("ngModelChange",function(n){return o.buscar=n})("input",function(){return o.buscarPorDescription()}),e.qZA(),e.TgZ(5,"label",5),e._uU(6,"Buscar"),e.qZA()()(),e.TgZ(7,"p-table",6),e.YNc(8,M,7,0,"ng-template",7),e.YNc(9,J,8,3,"ng-template",8),e.qZA(),e.TgZ(10,"p-dialog",9),e.NdJ("visibleChange",function(n){return o.display=n}),e.TgZ(11,"h5"),e._uU(12,"Bodega"),e.qZA(),e.TgZ(13,"span",10)(14,"input",11),e.NdJ("ngModelChange",function(n){return o.bodega.bdgDescripcion=n}),e.qZA(),e.TgZ(15,"label",5),e._uU(16,"Nombre de la bodega"),e.qZA()(),e.YNc(17,I,2,0,"ng-template",12),e.qZA()),2&t&&(e.xp6(4),e.Q6J("ngModel",o.buscar),e.xp6(3),e.Q6J("value",o.listaBodegas)("tableStyle",e.DdM(11,L)),e.xp6(3),e.Akn(e.DdM(12,S)),e.s9C("header",o.header),e.Q6J("visible",o.display)("modal",!0)("draggable",!1)("resizable",!1),e.xp6(4),e.Q6J("ngModel",o.bodega.bdgDescripcion))},dependencies:[c.jx,Z.Hq,T.V,_.o,m.Fj,m.JJ,m.On,y.iA,A.FN,x.H]});const E=[{path:"",component:l,children:[{path:"",component:g}]}];class d{}d.\u0275fac=function(t){return new(t||d)},d.\u0275mod=e.oAB({type:d}),d.\u0275inj=e.cJS({imports:[p.Bz.forChild(E),p.Bz]});var N=i(4632);class r{}r.\u0275fac=function(t){return new(t||r)},r.\u0275mod=e.oAB({type:r}),r.\u0275inj=e.cJS({imports:[f.ez,d,N.R]})}}]);