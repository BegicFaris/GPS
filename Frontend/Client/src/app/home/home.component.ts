import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RegisterComponent } from '../register/register.component';
import { RouterLink } from '@angular/router';
import { AccountService } from '../_services/account.service';

interface Station {
  id: string;
  name: string;
  zone: number;
  busLines: string[];
}

interface Tour {
  id: number;
  title: string;
  description: string;
  image: string;
}

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, FormsModule, RegisterComponent, RouterLink],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit{
  accountService = inject(AccountService);

  stations: Station[] = [
    { id: "1", name: "Central Station", zone: 1, busLines: ["101", "102", "103"] },
    { id: "2", name: "Westfield", zone: 2, busLines: ["201", "202"] },
    { id: "3", name: "Riverside", zone: 3, busLines: ["301", "302", "303", "304"] },
    { id: "4", name: "Hill Valley", zone: 2, busLines: ["201", "204", "205"] },
  ];
  selectedStationId: string = '';
  get selectedStation(): Station | undefined {
    return this.stations.find(station => station.id === this.selectedStationId);
  }

  http = inject(HttpClient);
  registerMode = false;
  users: any;

  ngOnInit(): void {}

  registerToggle() {
    this.registerMode = !this.registerMode;
  }

  cancelRegisterMode(event: boolean) {
    this.registerMode = event;
  }

  tours: Tour[] = [
    {
      id: 1,
      title: "Kravica Waterfalls",
      description: "Explore the stunning Kravica Waterfalls. Enjoy a relaxing day at this natural wonder located just outside of Mostar.",
      image: "kravica.jpg"
    },
    {
      id: 2,
      title: "Počitelj Village",
      description: "A charming medieval village near Mostar, Počitelj is perfect for a cultural trip and historical exploration.",
      image: "pocitelj.jpg"
    },
    {
      id: 3,
      title: "Medjugorje",
      description: "Visit the famous pilgrimage site of Medjugorje, known for its spiritual significance and beautiful surroundings.",
      image: "medjugorje.jpg"
    }
  ];
 
  learnMore(tourId: number): void {
    console.log(`Learn more about tour ${tourId}`);
    // Implement navigation or modal opening logic here
  }
}

