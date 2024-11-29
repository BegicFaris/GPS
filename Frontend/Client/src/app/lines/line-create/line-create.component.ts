import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { LineService } from '../../_services/line.service';
import { StationService } from '../../_services/station.service';
import { Router } from '@angular/router';
import { Station } from '../../_models/station';


@Component({
  selector: 'app-line-create',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './line-create.component.html',
  styleUrl: './line-create.component.css'
})
export class LineCreateComponent {
  private router=inject(Router);
  private lineService = inject(LineService);
  private stationService= inject(StationService)
  stations:Station[]=[];
  ngOnInit(){
    this.loadStations();
  }
  lineCreate:any = {};
  addNewLine() {
    if (this.lineCreate.isActive === undefined) {
      this.lineCreate.isActive = false;
    }
    this.lineService.createLine(this.lineCreate).subscribe({
      next: response=>{
        console.log(response);
        this.cancle();
      }
    })
    this.router.navigate(['/lines'])
  }
  cancle(){
    this.router.navigate(['/lines'])
  }
  loadStations(){
    this.stationService.getAllStations().subscribe(
      (data) => {
        this.stations = data; // or data.lines if it's nested
      },
    );

  }
}
