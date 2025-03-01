import { Component, inject } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { BusService } from '../../_services/bus.service';
import { Router } from '@angular/router';
import { Title } from '@angular/platform-browser';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { LettersNumbersDashesValidatorDirective } from '../../validators/letters-numbers-dashes.validator';
import { firstValueFrom } from 'rxjs';


@Component({
  selector: 'app-bus-create',
  standalone: true,
  imports: [FormsModule, LettersNumbersDashesValidatorDirective],
  templateUrl: './bus-create.component.html',
  styleUrl: './bus-create.component.css',
})
export class BusCreateComponent {
  currentYear: number = new Date().getFullYear();
  
  private router = inject(Router);
  private busService = inject(BusService);
  private titleService = inject(Title);
  private snackBar=inject(MatSnackBar);




  ngOnInit() {
    this.titleService.setTitle('Add bus');
  }
  busCreate: any = {};
  async addNewBus(newBusForm: NgForm) {

    if (newBusForm.valid) {
      if (this.busCreate.isActive === undefined) {
        this.busCreate.isActive = false;
      }
      await firstValueFrom(this.busService.createBus(this.busCreate));
      this.cancel();

      // this.busService.createBus(this.busCreate).subscribe({
      //   next: (response) => {
      //     console.log(response);
      //     this.snackBar.open('Bus added successfully!', 'Close', {
      //       panelClass: ['.success-snackbar'],
      //       duration: 3000, // Duration in milliseconds
      //       horizontalPosition: 'center', // Can be 'start', 'center', 'end', 'left', 'right'
      //       verticalPosition: 'top', // Can be 'top' or 'bottom'
      //     });
      //     this.cancel();
      //   },
      // });
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
