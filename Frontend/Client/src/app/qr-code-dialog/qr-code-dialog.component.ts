import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-qr-code-dialog',
  template: `
    <h1 mat-dialog-title class="dialog-title">QR Code</h1>
    <div mat-dialog-content class="dialog-content">
      <img [src]="data.qrCode" alt="QR Code" class="qr-code" />
    </div>
    <div mat-dialog-actions class="dialog-actions">
      <button mat-button (click)="closeDialog()">Close</button>
    </div>
  `,
  styles: [`
    .dialog-title {
      text-align: center;
      font-size: 24px;
      color: #333;
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

    .dialog-actions {
      display: flex;
      justify-content: flex-end;
      padding: 16px;
    }

    .dialog-actions button {
      background-color: #00796b;
      color: white;
      font-weight: bold;
      border-radius: 4px;
      padding: 8px 16px;
      box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
    }

    .dialog-actions button:hover {
      background-color: #004d40;
    }
  `]
})
export class QrCodeDialogComponent {
  constructor(
    @Inject(MAT_DIALOG_DATA) public data: { qrCode: string },
    private dialogRef: MatDialogRef<QrCodeDialogComponent>
  ) {}

  closeDialog(): void {
    this.dialogRef.close();
  }
}
