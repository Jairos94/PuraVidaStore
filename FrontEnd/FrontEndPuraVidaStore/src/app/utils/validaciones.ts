import { AbstractControl } from "@angular/forms";

export class validaciones {


    static esAdministrador(rol: number) {
        return (control: AbstractControl) => {
            if (rol === 1) 
            {
                return { esAdinistrador: true }
            }
            else { return null }
        }

    }


}