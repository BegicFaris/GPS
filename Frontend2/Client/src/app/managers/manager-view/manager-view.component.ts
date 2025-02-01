import { Component, inject } from '@angular/core';
import { LineService } from '../../_services/line.service';
import { Line } from '../../_models/line';
import { NavigationEnd, Router, RouterLink } from '@angular/router';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { ManagerEditComponent } from '../manager-edit/manager-edit.component';
import { Station } from '../../_models/station';
import { Title } from '@angular/platform-browser';
import { Manager } from '../../_models/manager';
import { ManagerService } from '../../_services/manager.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-manager-view',
  standalone: true,
  imports: [RouterLink, MatDialogModule, MatButtonModule, CommonModule],
  templateUrl: './manager-view.component.html',
  styleUrl: './manager-view.component.css',
})
export class ManagerViewComponent {
  private lineService = inject(LineService);
  private router = inject(Router);
  private dialog = inject(MatDialog);
  private titleService = inject(Title);
  private managerService = inject(ManagerService)
  managers: Manager[] = [];

  ngOnInit() {
    this.titleService.setTitle('Managers');
    this.loadManagers();
    this.router.events.subscribe((event) => {
      if (event instanceof NavigationEnd && event.url === '/managers') {
        this.loadManagers();
      }
    });
  }
  loadManagers() {
    this.managerService.getAllManagers().subscribe((data) => {
      this.managers = data; // or data.lines if it's nested
      console.log(this.managers);
    });
  }
  deleteManager(id: number) {
    if (confirm('Are you sure you want to delete this manager?')) {
      this.managerService.deleteManager(id).subscribe({
        next: (response) => {
          this.loadManagers();
          console.log('Manager deleted successfully', response);
          this.cancel(); // Navigate back after successful deletion
        },
        error: (error) => {
          console.error('Error deleting manager', error);
        },
      });
    }
  }
  openEditDialog(manager: Manager) {
    const dialogRef = this.dialog.open(ManagerEditComponent, {
      height: '800px',
      width: '1000px', // Customize the width of the dialog
      data: {
        id: manager.id,
        firstName: manager.firstName,
        lastName: manager.lastName,
        email: manager.email,
        birthDate: manager.birthDate,
        address: manager.address,
        hireDate: manager.hireDate,
        department: manager.department,
        managerLevel: manager.managerLevel,
      }, // Pass the current data to the dialog
    });
    dialogRef.afterClosed().subscribe((result) => {
      this.loadManagers();
      if (result) {
        console.log('Updated Manager:', result);
      }
    });
  }
  cancel() {
    this.router.navigate(['manager-dashboard/managers']);
  }
}
