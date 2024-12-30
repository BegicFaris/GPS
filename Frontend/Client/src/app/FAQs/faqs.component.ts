import { Component, inject } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { FooterComponent } from "../footer/footer.component";

@Component({
  selector: 'app-faqs',
  standalone: true,
  imports: [FooterComponent],
  templateUrl: './faqs.component.html',
  styleUrl: './faqs.component.css'
})
export class FaqsComponent {
  private titleService = inject(Title);

  ngOnInit()
  {
    this.titleService.setTitle("FAQs");
  }


}
