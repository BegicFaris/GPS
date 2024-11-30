import { Component, inject } from '@angular/core';
import { Title } from '@angular/platform-browser';

@Component({
  selector: 'app-buy-ticket',
  standalone: true,
  imports: [],
  templateUrl: './buy-ticket.component.html',
  styleUrl: './buy-ticket.component.css'
})
export class BuyTicketComponent {
  private titleService = inject(Title);

  ngOnInit()
  {
    this.titleService.setTitle("Buy ticket");
  }
}
