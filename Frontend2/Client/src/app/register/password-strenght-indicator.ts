import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-password-strength-indicator',
  standalone: true,
  imports: [CommonModule],
  template: `
    <div class="password-strength-indicator" [class.show]="show">
      <h6>Password Strength:</h6>
      <ul>
        <li [class.text-success]="hasUpperCase">
          {{ hasUpperCase ? '✓' : '•' }} 
          {{ hasUpperCase ? 'Uppercase letter included' : 'Include an uppercase letter' }}
        </li>
        <li [class.text-success]="hasLowerCase">
          {{ hasLowerCase ? '✓' : '•' }} 
          {{ hasLowerCase ? 'Lowercase letter included' : 'Include a lowercase letter' }}
        </li>
        <li [class.text-success]="hasNumber">
          {{ hasNumber ? '✓' : '•' }} 
          {{ hasNumber ? 'Number included' : 'Include a number' }}
        </li>
        <li [class.text-success]="hasSpecialChar">
          {{ hasSpecialChar ? '✓' : '•' }} 
          {{ hasSpecialChar ? 'Special character included' : 'Include a special character ($!%_*?&)' }}
        </li>
        <li [class.text-success]="hasMinLength">
          {{ hasMinLength ? '✓' : '•' }} 
          {{ hasMinLength ? 'Minimum length met' : 'At least 8 characters long' }}
        </li>
      </ul>
    </div>
  `,
  styles: [`
    .password-strength-indicator {
      position: absolute;
      top: 100%;
      left: 0;
      z-index: 1000;
      background-color: #f8f9fa;
      border: 1px solid #ced4da;
      border-radius: 0.25rem;
      padding: 10px;
      margin-top: 5px;
      width: 100%;
      box-shadow: 0 2px 5px rgba(0,0,0,0.1);
      display: none;
    }
    .password-strength-indicator.show {
      display: block;
    }
    .password-strength-indicator ul {
      list-style-type: none;
      padding-left: 0;
      margin-bottom: 0;
    }
    .password-strength-indicator li {
      margin-bottom: 5px;
      color: #dc3545;
    }
    .password-strength-indicator li.text-success {
      color: #28a745;
    }
  `]
})
export class PasswordStrengthIndicatorComponent {
  @Input() show: boolean = false;
  @Input() hasUpperCase: boolean = false;
  @Input() hasLowerCase: boolean = false;
  @Input() hasNumber: boolean = false;
  @Input() hasSpecialChar: boolean = false;
  @Input() hasMinLength: boolean = false;
}
