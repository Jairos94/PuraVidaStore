import { Component, OnInit } from '@angular/core';
import { MenuItem, MessageService, PrimeNGConfig } from 'primeng/api';

@Component({
  selector: 'app-usuarios',
  templateUrl: './usuarios.component.html',
  styleUrls: ['./usuarios.component.css'],
  providers: [MessageService]
})
export class UsuariosComponent implements OnInit {

  constructor(private messageService: MessageService,private primengConfig: PrimeNGConfig) { }
  items: MenuItem[] = [];
  ngOnInit(): void {
    this.primengConfig.ripple = true;

    this.items = [{
        label: 'Options',
        items: [{
            label: 'Lista',
            icon: 'pi pi-refresh',
            command: () => {
                this.update();
            }
        },
        {
            label: 'Nuevo',
            icon: 'pi pi-times',
            command: () => {
                this.delete();
            }
        },

        {
          label: 'Delete',
          icon: 'pi pi-times',
          command: () => {
              this.delete();
          }
      },
        
        ]},
    ];
  }
  update() {
    this.messageService.add({severity:'success', summary:'Success', detail:'Data Updated'});
}

delete() {
    this.messageService.add({severity:'warn', summary:'Delete', detail:'Data Deleted'});
}

  
}
