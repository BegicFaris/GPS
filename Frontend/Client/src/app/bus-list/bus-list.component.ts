// src/app/bus-list/bus-list.component.ts
import { Component, OnInit } from '@angular/core';
import { Bus } from '../models/bus.model';
import { BusService } from '../bus.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-bus-list',
  templateUrl: './bus-list.component.html',
  styleUrls: ['./bus-list.component.css'],
  standalone: true,
  imports: [CommonModule, FormsModule]
})
export class BusListComponent implements OnInit {
  buses: Bus[] = [];
  selectedBus: Bus | null = null;

  constructor(private busService: BusService) { }

  ngOnInit(): void {
    this.loadBuses();
  }

  loadBuses(): void {
    this.busService.getAllBuses().subscribe((data: Bus[]) => {
      this.buses = data;
    });
  }

  selectBus(bus: Bus): void {
    this.selectedBus = { ...bus }; // Make a copy for editing
  }

  deleteBus(id: number): void {
    this.busService.deleteBus(id).subscribe(() => {
      this.loadBuses(); // Reload buses after delete
    });
  }

  // Edit a bus (update bus details)
  editBus(): void {
    if (this.selectedBus) {
      this.busService.updateBus(this.selectedBus).subscribe(() => {
        this.loadBuses(); // Reload buses after edit
        this.selectedBus = null; // Clear the selected bus after update
      });
    }
  }

  resetSelectedBus(): void {
    this.selectedBus = null;
  }

  // Cancel editing
  cancelEdit(): void {
    this.selectedBus = null; // Reset the selected bus without saving
  }

  // TrackBy function to optimize rendering performance in *ngFor
  trackById(index: number, bus: Bus): number {
    return bus.id; // Track by bus id to ensure unique identification
  }
}
