import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, ElementRef, inject, OnInit, Renderer2 } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RegisterComponent } from '../register/register.component';
import { Router, RouterLink, Routes } from '@angular/router';
import { AccountService } from '../_services/account.service';
import { Title } from '@angular/platform-browser';
import { LazyLoadDirective } from '../lazy-load.directive';
import { Station } from '../_models/station';
import { StationService } from '../_services/station.service';
import { LineService } from '../_services/line.service';
import { Line } from '../_models/line';
import { first, firstValueFrom } from 'rxjs';


interface Tour {
  id: number;
  title: string;
  description: string;
  image: string;
}

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, FormsModule, RegisterComponent, RouterLink, LazyLoadDirective],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit{
buyATicket() {
  this.router.navigate(['/buy-ticket']);

}
private titleService = inject(Title);
private stationService = inject(StationService);
private lineService = inject(LineService);
private router = inject(Router)

  accountService = inject(AccountService);

  stations: Station[] = [];
  lines: Line[] = [];

  selectedStationId: number | null=null;
  selectedStation: Station | undefined;

  http = inject(HttpClient);
  registerMode = false;
  users: any;

 async watchSelectedStation(): Promise<void> {
    // Check if `selectedStationId` exists
    if (this.selectedStationId) {
      this.selectedStation=await firstValueFrom(this.stationService.getStation(this.selectedStationId));




      if(this.selectedStation){
        this.loadLines();
      }
    }}

    loadLines(){
        if(this.selectedStation)
        {
          this.lineService.getAllLinesByStationId(this.selectedStation?.id).subscribe(
            (data)=>{
              this.lines=data;
            }
          );
        }
    }

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

  private imageOverlay: HTMLElement | null = null;
  private enlargedImage: HTMLImageElement | null = null;
  private globalStyleElement: HTMLStyleElement | null = null;

  constructor(private renderer: Renderer2, private el: ElementRef) {}

  ngOnInit() {
    this.titleService.setTitle("Home");
    this.loadStations();


    this.imageOverlay = this.el.nativeElement.querySelector('#imageOverlay');
    this.enlargedImage = this.el.nativeElement.querySelector('#enlargedImage') as HTMLImageElement;

    const routeImages = this.el.nativeElement.querySelectorAll('.route-image');
    routeImages.forEach((img: HTMLElement) => {
      this.renderer.listen(img, 'click', (event) => this.showEnlargedImage(event));
    });

    if (this.imageOverlay) {
      this.renderer.listen(this.imageOverlay, 'click', () => this.hideEnlargedImage());
    }
  }
  loadStations() {
    this.stationService.getAllStations().subscribe((data)=>
      {
        this.stations=data;
      });   
  }

  showEnlargedImage(event: Event) {
    const clickedImage = event.target as HTMLImageElement;
    if (this.enlargedImage && this.imageOverlay) {
      this.enlargedImage.src = clickedImage.src;
      this.enlargedImage.alt = clickedImage.alt;
      this.renderer.addClass(this.imageOverlay, 'active');
      this.addGlobalStyle(); 
    }
  }

  hideEnlargedImage() {
    if (this.imageOverlay) {
      this.renderer.removeClass(this.imageOverlay, 'active');
      this.removeGlobalStyle();
    }
  }
  private addGlobalStyle(): void {
    if (!this.globalStyleElement) {
      this.globalStyleElement = this.renderer.createElement('style');
      if (this.globalStyleElement) {
        this.globalStyleElement.textContent = `
          body.overlay-active {
            overflow: hidden;
          }
        `;
        this.renderer.appendChild(document.head, this.globalStyleElement);
        this.renderer.addClass(document.body, 'overlay-active');
      }
    }
  }

  private removeGlobalStyle(): void {
    if (this.globalStyleElement) {
      this.renderer.removeChild(document.head, this.globalStyleElement);
      this.globalStyleElement = null;
      this.renderer.removeClass(document.body, 'overlay-active');
    }
  }
  

}

