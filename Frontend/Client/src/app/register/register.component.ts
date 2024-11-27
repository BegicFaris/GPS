import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { catchError, tap, throwError } from 'rxjs';
import { TenantService } from '../_services/tenant.service';
import { Tenant } from '../_models/tenant';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css',
})
export class RegisterComponent {
  model: any = {
    firstName: '',
    lastName: '',
    email: '',
    password: '',
    birthDate: '',
    address: '',
    discountId: null,
    tenantId: null,
    image: null,
  };

  tenantService = inject(TenantService);

  tenants: Tenant[] = []; // Array to hold tenant data
  errorMessage: string = '';
  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.fetchTenants();
  }

  fetchTenants(): void {
    this.tenantService.fetchTenants().subscribe({
      next: (data) => {
        if (Array.isArray(data)) {
          this.tenants = data;
        } else {
          console.error('Unexpected data format:', data);
          this.errorMessage = 'Received unexpected data format from server.';
        }
      },
      error: (error) => {
        console.error('Error fetching tenants:', error);
        this.errorMessage = error.message || 'Failed to load tenants. Please try again later.';
      },
    });
  }

  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input?.files?.length) {
      this.model.image = input.files[0]; // Store the selected file in the model
    }
  }

  register() {
    console.log(this.model);
  }

  cancel() {
    console.log('canceled');
  }
}
