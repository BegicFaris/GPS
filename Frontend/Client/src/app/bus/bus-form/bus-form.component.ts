import { Component, NgModule, OnInit } from '@angular/core';
import { Bus } from '../bus.model';
import { BusService } from '../bus.service';
import { ActivatedRoute, Router } from '@angular/router';
import { FormsModule } from '@angular/forms'; // <-- Import FormsModule


@Component({
  selector: 'app-bus-form',
  standalone: true,
  imports: [FormsModule], // <-- Add FormsModule here
  templateUrl: './bus-form.component.html',
  styleUrls: ['./bus-form.component.css']
})



export class BusFormComponent implements OnInit {
  bus: Bus = {
    id: 0,
    registrationNumber: '',
    manufacturer: '',
    model: '',
    capacity: '',
    manufactureYear: ''
  };

  constructor(
    private busService: BusService,
    private activatedRoute: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    const id = this.activatedRoute.snapshot.paramMap.get('id');
    if (id) {
      this.busService.getBusById(Number(id)).subscribe((bus: Bus) => {
        this.bus = bus;
      });
    }
  }

  saveBus(): void {
    if (this.bus.id) {
      this.busService.updateBus(this.bus).subscribe(() => {
        this.router.navigate(['/']);
      });
    } else {
      this.busService.createBus(this.bus).subscribe(() => {
        this.router.navigate(['/']);
      });
    }
  }
}
