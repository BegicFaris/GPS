import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, ElementRef, inject, OnInit, Renderer2 } from '@angular/core';
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
    { id: "1", name: "Musala", zone: 1, busLines: ["101", "102", "103"] },
    { id: "2", name: "Šarića džamija", zone: 2, busLines: ["201", "202"] },
    { id: "3", name: "Narodno pozorište", zone: 3, busLines: ["301", "302", "303", "304"] },
    { id: "4", name: "Španski trg", zone: 2, busLines: ["201", "204", "205"] },
  ];
  selectedStationId: string = '';
  get selectedStation(): Station | undefined {
    return this.stations.find(station => station.id === this.selectedStationId);
  }

  http = inject(HttpClient);
  registerMode = false;
  users: any;


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

