import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { activo } from "../activo";

@Injectable()
export class Interceptor implements HttpInterceptor{
    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
       const token = activo.token;

       if(token){
        req= req.clone({
            setHeaders:{Authorization: `Bearer ${token}`}
        });
       }

       return next.handle(req);
    }

}