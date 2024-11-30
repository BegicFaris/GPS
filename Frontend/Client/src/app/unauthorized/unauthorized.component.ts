import { Component, inject } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { Router } from '@angular/router';

@Component({
  selector: 'app-unauthorized',
  standalone: true,
  imports: [],
  templateUrl: './unauthorized.component.html',
  styleUrl: './unauthorized.component.css'
})
export class UnauthorizedComponent {
  private router = inject(Router);
  private titleService = inject(Title);

  ngOnInit()
  {
    this.titleService.setTitle("Unauthorized");
  }
  homepage(){
    this.router.navigateByUrl('/home');
  }
}
