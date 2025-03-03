import { Component, inject } from '@angular/core';
import { DiscountService } from '../../_services/discount.service';
import { Discount } from '../../_models/discount';
import { NavigationEnd, Router, RouterLink } from '@angular/router';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { DiscountEditComponent } from '../discount-edit/discount-edit.component';
import { Title } from '@angular/platform-browser';
import { firstValueFrom } from 'rxjs';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-discount-view',
  standalone: true,
  imports: [RouterLink, MatDialogModule, MatButtonModule],
  templateUrl: './discount-view.component.html',
  styleUrl: './discount-view.component.css',
})
export class DiscountViewComponent {
  private discountService = inject(DiscountService);
  private router = inject(Router);
  private dialog = inject(MatDialog);
  private titleService = inject(Title);
  private snackBar= inject(MatSnackBar);
  discounts: Discount[] = [];

  async ngOnInit() {
    this.titleService.setTitle('Discounts');
    await this.loadDiscounts();
    this.router.events.subscribe(async (event) => {
      if (event instanceof NavigationEnd && event.url === '/discounts') {
        await this.loadDiscounts();
      }
    });
  }
  async loadDiscounts() {
    try{
    this.discounts = await firstValueFrom(this.discountService.getAllDiscounts());
    }
    catch(err){
      console.error(err);
    }
  }
  deleteDiscount(id: number) {
    if (confirm('Are you sure you want to delete this discount?')) {
      this.discountService.deleteDiscount(id).subscribe({
        next: async () => {
          await this.loadDiscounts();
          this.snackBar.open('Discount deleted sucessfully', 'Ok', {
            duration: 4000, // Keep it visible for 4 seconds
            verticalPosition: 'top', // Show at the top
            horizontalPosition: 'center' // Centered horizontally
          });
          this.cancel(); // Navigate back after successful deletion
        },
        error: (error) => {
          console.error('Error deleting discount', error);
        },
      });
    }
  }
  openEditDialog(discount: Discount) {
    const dialogRef = this.dialog.open(DiscountEditComponent, {
      height: '800px',
      width: '1000px', // Customize the width of the dialog
      data: {
        id: discount.id,
        discountName: discount.discountName,
        discountValue: discount.discountValue,
      }, // Pass the current data to the dialog
    });
    dialogRef.afterClosed().subscribe(async (result) => {
      await this.loadDiscounts();
      if (result) {
        this.snackBar.open('Discount updated sucessfully', 'Ok', {
          duration: 4000, // Keep it visible for 4 seconds
          verticalPosition: 'top', // Show at the top
          horizontalPosition: 'center' // Centered horizontally
        });
      }
    });
  }
  cancel() {
    this.router.navigate(['manager-dashboard/discounts']);
  }
}
