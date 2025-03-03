import { Component, inject, Inject, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MAT_DIALOG_DATA, MatDialogModule } from '@angular/material/dialog';
import {MatIconModule} from '@angular/material/icon';
import { TicketService } from '../_services/ticket.service';
import { AccountService } from '../_services/account.service';
import { CommonModule } from '@angular/common';
import { Ticket } from '../_models/ticket';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';

@Component({
  selector: 'app-success-dialog',
  standalone: true,
  imports: [MatDialogModule, MatButtonModule, MatIconModule, CommonModule],
  template: `
  <div class="dialog-container">
    <div class="dialog-icon">
      <mat-icon>check_circle</mat-icon>
    </div>
    <div mat-dialog-content>
      <p>{{ data.message }}</p>
      <p>Ticket QR code:</p>
      <div *ngIf="qrCodeImage" class="dialog-content">
        <img [src]="qrCodeImage" alt="Ticket QR Code" class="qr-code">
      </div>
      <p>You will be redirected to user profile page where you can access and see your ticket history</p>
    <div mat-dialog-actions>
      <button mat-raised-button color="primary" mat-dialog-close>OK</button>
    </div>
  </div>
`,
styles: [
    `
    .dialog-container {
      width: 100%;
      display: flex;
      flex-direction: column;
      align-items: center;
      justify-content: center;
      text-align: center;
      padding: 20px;
      font-family: 'Arial', sans-serif;
      animation: fadeIn 0.5s ease-in-out;
      border-radius: 15px;
    }
    .dialog-icon {
      font-size: 60px;
      line-height: 1;
      color: #4caf50;
      margin-bottom: 10px;
      animation: bounce 0.8s infinite;
    }
    mat-icon {
      font-size: inherit;
      width: 60px;
      height: 60px;
    }
    mat-dialog-title {
      width: 100%;
      text-align: center;
      font-size: 24px;
      font-weight: bold;
      margin-bottom: 10px;
      overflow-wrap: break-word;
    }
    mat-dialog-content {
      display: flex;
      flex-direction: column;
      align-items: center;
      justify-content: center;
      width: 100%;
      text-align: center;
    }
    mat-dialog-content p {
      font-size: 10px;
      margin-bottom: 20px;
      word-wrap: break-word;
    }
    mat-dialog-actions {
      width: 100%;
      margin-top: 20px;
    }
    button {
      margin: 0;
      white-space: nowrap;
    }
    @keyframes fadeIn {
      from { opacity: 0; }
      to { opacity: 1; }
    }
    @keyframes bounce {
      0%, 100% { transform: scale(1); }
      50% { transform: scale(1.2); }
    }
    .dialog-content {
      display: flex;
      justify-content: center;
      align-items: center;
      padding: 20px;
    }
    .qr-code {
      width: 200px;
      height: 200px;
      object-fit: contain;
      border: 1px solid #ccc;
      border-radius: 8px;
      box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
    }
    ::ng-deep .mdc-dialog__actions {
      display: flex !important;
      justify-content: center !important;
      padding: 8px 0 !important;
    }

    ::ng-deep .mat-mdc-dialog-actions {
      justify-content: center;
    }

    ::ng-deep .mdc-dialog__content {
      display: flex !important;
      flex-direction: column !important;
      align-items: center !important;
      text-align: center !important;
    }
    ::ng-deep .mdc-dialog__content {
      display: flex !important;
      flex-direction: column !important;
      align-items: center !important;
      text-align: center !important;
      overflow: hidden !important;
    }
    ::ng-deep .mat-mdc-dialog-content {
  max-height: unset !important;
}
    `,
  ],
})
export class SuccessDialogComponent implements OnInit {
    private ticketService = inject(TicketService);
  private accountService = inject(AccountService);
  private sanitizer = inject(DomSanitizer);
  qrCodeImage: SafeUrl | null = null;

  constructor(@Inject(MAT_DIALOG_DATA) public data: { message: string }) {}
  ngOnInit() {
    this.loadLastTicket();
  }

 
    loadLastTicket() {
        const userEmail = this.accountService.getUserEmail();
        if (userEmail) {
          this.ticketService.getUserTickets(userEmail).subscribe({
            next: (tickets: Ticket[]) => {
              if (tickets.length > 0) {
                const lastTicket = tickets[tickets.length - 1];
                this.qrCodeImage = this.decodeQRCode(lastTicket.qrCode);
              }
            },
            error: (error) => {
              console.error('Error fetching user tickets:', error);
            }
          });
        }
    }
    

    decodeQRCode(qrCodeBlob: any): string {
        if (typeof qrCodeBlob === 'string') {
          return qrCodeBlob.startsWith('data:') 
            ? qrCodeBlob 
            : `data:image/png;base64,${qrCodeBlob}`;
        }
      
        if (qrCodeBlob instanceof Uint8Array) {
          const binary = Array.from(qrCodeBlob, byte => String.fromCharCode(byte)).join('');
          return `data:image/png;base64,${btoa(binary)}`;
        }
      
        return '';
      }
}


