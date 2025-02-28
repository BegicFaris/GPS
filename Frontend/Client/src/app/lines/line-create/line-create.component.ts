import { Component, inject } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { LineService } from '../../_services/line.service';
import { StationService } from '../../_services/station.service';
import { Router } from '@angular/router';
import { Station } from '../../_models/station';
import { Title } from '@angular/platform-browser';
import { LineNameValidatorDirective } from '../../validators/line-name.validator';

@Component({
  selector: 'app-line-create',
  standalone: true,
  imports: [FormsModule,LineNameValidatorDirective],
  templateUrl: './line-create.component.html',
  styleUrl: './line-create.component.css',
})
export class LineCreateComponent {
  private router = inject(Router);
  private lineService = inject(LineService);
  private stationService = inject(StationService);
  private titleService = inject(Title);
  stations: Station[] = [];
  lineCreate: any = {};

  ngOnInit() {
    this.titleService.setTitle('Add line');
    this.loadStations();
  }
  addNewLine(newLineForm: NgForm) {

    if (newLineForm.valid) {
      if (this.lineCreate.isActive === undefined) {
        this.lineCreate.isActive = false;
      }
      this.lineService.createLine(this.lineCreate).subscribe({
        next: (response) => {
          console.log(response);
          this.cancel();
        },
      });
      this.router.navigate(['/manager-dashboard/lines']);
    }
    else{
      Object.keys(newLineForm.controls).forEach(field => {
        const control = newLineForm.controls[field];
        control.markAsTouched({ onlySelf: true });
      });
    }
  }
  cancel() {
    this.router.navigate(['/manager-dashboard/lines']);
  }
  loadStations() {
    this.stationService.getAllStations().subscribe((data) => {
      this.stations = data; // or data.lines if it's nested
    });
  }
}
