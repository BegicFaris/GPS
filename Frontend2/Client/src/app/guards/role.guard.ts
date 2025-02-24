import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, Router } from '@angular/router';
import { AccountService } from '../_services/account.service';

@Injectable({
  providedIn: 'root',
})
export class RoleGuard implements CanActivate {
  constructor(private accountService: AccountService, private router: Router) {}

  canActivate(route: ActivatedRouteSnapshot): boolean {
    const user = this.accountService.currentUser(); // Fetch the logged-in user's details
    const requiredRole = route.data['role']; // Role required for this route

    if (user?.role === requiredRole) {
      return true; // Allow access
    }

    // Redirect to an unauthorized page or homepage if the role doesn't match
    this.router.navigate(['/unauthorized']);
    return false;
  }
}
