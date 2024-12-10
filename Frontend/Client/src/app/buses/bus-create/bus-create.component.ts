import { Component, inject } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { BusService } from '../../_services/bus.service';
import { Router } from '@angular/router';
import { Title } from '@angular/platform-browser';

@Component({
  selector: 'app-bus-create',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './bus-create.component.html',
  styleUrl: './bus-create.component.css',
})
export class BusCreateComponent {
  private router = inject(Router);
  private busService = inject(BusService);
  private titleService = inject(Title);




  ngOnInit() {
    this.titleService.setTitle('Add bus');
  }
  busCreate: any = {};
  addNewBus(newBusForm: NgForm) {

    if (newBusForm.valid) {
      if (this.busCreate.isActive === undefined) {
        this.busCreate.isActive = false;
      }
      this.busService.createBus(this.busCreate).subscribe({
        next: (response) => {
          console.log(response);
          this.cancel();
        },
      });
      this.router.navigate(['/manager-dashboard/buses']);
    }
    else{
      Object.keys(newBusForm.controls).forEach(field => {
        const control = newBusForm.controls[field];
        control.markAsTouched({ onlySelf: true });
      });
    }
  }
  cancel() {
    this.router.navigate(['/manager-dashboard/buses']);
  }
}
