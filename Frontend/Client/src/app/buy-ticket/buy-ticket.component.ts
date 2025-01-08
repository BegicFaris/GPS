import { Component, inject, OnInit, Pipe, PipeTransform } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { RouterLink, Routes } from '@angular/router';
import { HomeComponent } from '../home/home.component';
import { GalleryComponent } from '../gallery/gallery.component';
import { lastValueFrom } from 'rxjs';
import { NewsComponent } from '../news/news.component';
import { ScheduleComponent } from '../schedule/schedule.component';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { TicketInfoService } from '../_services/ticket-info.service';
import { TicketService } from '../_services/ticket.service';
import { StripeService } from '../_services/stripe.service';
import { TicketInfo } from '../_models/ticket-info';
import { Ticket } from '../_models/ticket';
import { CommonModule, DecimalPipe } from '@angular/common';
import { TicketType } from '../_models/ticket-type';
import { TicketTypeService } from '../_services/ticket-type.service';
import { PaymentComponent } from "../payment/payment.component";
import { AccountService } from '../_services/account.service';
import { MyAppUser } from '../_models/my-app-user';
import { MyAppUserService } from '../_services/my-app-user.service';
import { Passenger } from '../_models/passenger';
import { CurrencyFormatPipe } from "../_pipes/currency-format.pipe";

@Component({
  selector: 'app-buy-ticket',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule, PaymentComponent, CurrencyFormatPipe],
  templateUrl: './buy-ticket.component.html',
  styleUrl: './buy-ticket.component.css',
})
export class BuyTicketComponent implements OnInit {
  private titleService = inject(Title);

  currentStep = 1;
  ticketTypes: TicketType[] = [];
  selectedTicketType: TicketType | null = null;
  ticketInfos: TicketInfo[] = [];
  selectedTicketInfo: TicketInfo | null = null;
  userDiscount: number = 0;

  constructor(
    private ticketTypeService: TicketTypeService,
    private ticketInfoService: TicketInfoService,
    private accountService: AccountService,
    private myAppUserService: MyAppUserService,
  ) {}

  ngOnInit() {
    this.loadTicketTypes();
    this.loadUserDiscount();
  }

  loadTicketTypes() {
    this.ticketTypeService.getAll().subscribe(
      types => {
        this.ticketTypes = types
        console.log(types);
      }
      ,
      error => console.error('Error loading ticket types:', error)
    );
  }

  selectTicketType(type: TicketType) {
    this.selectedTicketType = type;
    this.loadTicketInfos(type.id);
    this.nextStep();
  }

  loadTicketInfos(typeId: number) {
    this.ticketInfoService.getByTicketTypeId(typeId).subscribe(
      infos => this.ticketInfos = infos,
      error => console.error('Error loading ticket infos:', error)
    );
  }

  selectTicketInfo(info: TicketInfo) {
    this.selectedTicketInfo = info;
    this.nextStep();
  }

  nextStep() {
    this.currentStep++;
  }

  previousStep() {
    this.currentStep--;
  }
  calculateDiscountedPrice(price: number, discount: number): number {
    return discount ? price - (price * discount) : price;
  }

  loadUserDiscount() {
    const userEmail = this.accountService.getUserEmail(); // Replace with your method
    this.myAppUserService.getMyAppUserByEmail(userEmail).subscribe(
      (user: Passenger) => {
        this.userDiscount = user.discount?.discountValue || 0; // Default to 0 if no discount
      },
      error => console.error('Error fetching user discount:', error)
    );
  }
}
