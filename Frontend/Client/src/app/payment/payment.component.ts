import { Component, Input, OnInit } from '@angular/core';
import { TicketInfo } from '../_models/ticket-info';
import { StripeService } from '../_services/stripe.service';
import { TicketService } from '../_services/ticket.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../_services/account.service';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { SuccessDialogComponent } from './success.component';
import { MyAppUserService } from '../_services/my-app-user.service';
import { Passenger } from '../_models/passenger';
import { CurrencyFormatPipe } from "../_pipes/currency-format.pipe";

@Component({
  selector: 'app-payment',
  standalone: true,
  imports: [FormsModule, CommonModule, CurrencyFormatPipe],
  templateUrl: './payment.component.html',
  styleUrl: './payment.component.css'
})
export class PaymentComponent implements OnInit{
  @Input() ticketInfo: TicketInfo | null = null;
  cardElement: any;
  saveCard: boolean = false;
  paymentError: any | null = null;
  email: string | "" = "";
  isProcessing = false;
  userDiscount: number = 0;

  constructor(
    private stripeService: StripeService,
    private ticketService: TicketService,
    private accountService: AccountService,
    private myAppUserService: MyAppUserService,
    private dialog: MatDialog, // Inject MatDialog
    private router: Router // Inject Router
  ) {}

  ngOnInit() {
    this.stripeService.loadStripe().then(() => {
      this.cardElement = this.stripeService.createCardElement();
      this.cardElement.mount('#card-element');
    });
    this.email = this.accountService.getUserEmail();
    this.loadUserDiscount();
  }

  loadUserDiscount() {
    const userEmail = this.accountService.getUserEmail(); // Replace with your method
    this.myAppUserService.getMyAppUserByEmail(userEmail).subscribe(
      (user: Passenger) => {
        this.userDiscount = user.discount?.discountValue || 0;
      },
      error => console.error('Error fetching user discount:', error)
    );
  }
  getDiscountedPrice(): number {
    if (!this.ticketInfo) return 0;
    return this.ticketInfo.price - this.ticketInfo.price * this.userDiscount;
  }

  async onSubmit() {
    if (!this.ticketInfo) return;

    this.paymentError = null;
    this.isProcessing=true;
    const { token, error } = await this.stripeService.createToken(this.cardElement);

    if (error) {
      this.paymentError = error.message;
      this.isProcessing= false;
    } else {
      this.processPayment(token.id);
    }
  }

  processPayment(token: string) {
    if (!this.ticketInfo) return;
    const discountedPrice = this.getDiscountedPrice(); // Use discounted price

    this.ticketService.createTicketWithStripe({
      ticketInfoId: this.ticketInfo.id,
      stripeToken: token,
      amount: discountedPrice,
      email: this.email
    }).subscribe(
      ticket => {
        console.log('Ticket created:', ticket);
        this.isProcessing=false;
        // Show success dialog
        const dialogRef = this.dialog.open(SuccessDialogComponent, {
          width: '400px',
          data: { message: 'Payment successful!' },
        });

        dialogRef.afterClosed().subscribe(() => {
          // Redirect to user component after dialog is closed
          this.router.navigate(['/profile']);
        });
      },
      error => {
        console.error('Error creating ticket:', error);
        this.paymentError = 'Failed to create ticket. Please try again.';
      }
    );
  }
}
