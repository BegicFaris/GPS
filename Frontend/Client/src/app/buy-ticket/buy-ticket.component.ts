import { Component, inject } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { FooterComponent } from "../footer/footer.component";

@Component({
  selector: 'app-buy-ticket',
  standalone: true,
  imports: [FooterComponent],
  templateUrl: './buy-ticket.component.html',
  styleUrl: './buy-ticket.component.css'
})
export class BuyTicketComponent {
  private titleService = inject(Title);

  ngOnInit() {
    this.titleService.setTitle("Buy ticket");
  }

}
