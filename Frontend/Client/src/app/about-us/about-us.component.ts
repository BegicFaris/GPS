import { Component, inject } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { FooterComponent } from "../footer/footer.component";

@Component({
  selector: 'app-about-us',
  standalone: true,
  imports: [FooterComponent],
  templateUrl: './about-us.component.html',
  styleUrl: './about-us.component.css'
})
export class AboutUsComponent {
  private titleService = inject(Title);

  ngOnInit() {
    this.titleService.setTitle("About us");

  }

}
