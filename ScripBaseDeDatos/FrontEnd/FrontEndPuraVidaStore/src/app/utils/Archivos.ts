import { Observable } from "rxjs/internal/Observable";
import { ReplaySubject } from "rxjs/internal/ReplaySubject";

export class Archivo 
{
    base64Output : string = '';
    
    
     public static convertFile(file : File) : Observable<string> {
        const result = new ReplaySubject<string>(1);
        const reader = new FileReader();
        reader.readAsBinaryString(file);
        reader.onload = (event:any) => 
        
        result.next(btoa(event.target.result.toString()));
        return result;
      }

      public static lectorImagen(imagenBase64:string):string
      {
        let imagen : string ='data:image/png;base64,';
        imagen = imagen+imagenBase64;
        return imagen;
      }
  
}