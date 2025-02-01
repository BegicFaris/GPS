import { Component, inject } from '@angular/core';
import { CreditCardService } from '../../_services/credit-card.service';
import { CreditCard } from '../../_models/credit-card';
import { NavigationEnd, Router, RouterLink } from '@angular/router';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { CreditCardEditComponent } from '../creditCard-edit/creditCard-edit.component';
import { Title } from '@angular/platform-browser';

@Component({
  selector: 'app-creditCard-view',
  standalone: true,
  imports: [RouterLink, MatDialogModule, MatButtonModule],
  templateUrl: './creditCard-view.component.html',
  styleUrl: './creditCard-view.component.css',
})
export class CreditCardViewComponent {
  private creditCardService = inject(CreditCardService);
  private router = inject(Router);
  private dialog = inject(MatDialog);
  private titleService = inject(Title);
  creditCards: CreditCard[] = [];

  ngOnInit() {
    this.titleService.setTitle('creditCards');
    this.loadCreditCards();
    this.router.events.subscribe((event) => {
      if (event instanceof NavigationEnd && event.url === '/creditCards') {
        this.loadCreditCards();
      }
    });
  }
  loadCreditCards() {
    this.creditCardService.getAllCreditCards().subscribe((data) => {
      this.creditCards = data; // or data.creditCards if it's nested
      console.log(this.creditCards);
    });
  }
  deleteCreditCard(id: number) {
    if (confirm('Are you sure you want to delete this creditCard?')) {
      this.creditCardService.deleteCreditCard(id).subscribe({
        next: (response) => {
          this.loadCreditCards();
          console.log('creditCard deleted successfully', response);
          this.cancel(); // Navigate back after successful deletion
        },
        error: (error) => {
          console.error('Error deleting creditCard', error);
        },
      });
    }
  }
  openEditDialog(creditCard: CreditCard) {
    const dialogRef = this.dialog.open(CreditCardEditComponent, {
      height: '800px',
      width: '1000px', // Customize the width of the dialog
      data: {
        id: creditCard.id,
        cardNumber: creditCard.cardNumber,
        expirationDate: creditCard.expirationDate,
        cardName: creditCard.cardName,
        ccv: creditCard.ccv,
      }, // Pass the current data to the dialog
    });
    dialogRef.afterClosed().subscribe((result) => {
      this.loadCreditCards();
      if (result) {
        console.log('Updated creditCard:', result);
      }
    });
  }
  cancel() {
    this.router.navigate(['manager-dashboard/creditCards']);
  }
}
