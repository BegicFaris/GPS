import { Component, inject } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { FooterComponent } from "../footer/footer.component";
import { SafeUrlPipe } from "./safe-url.pipe";

@Component({
  selector: 'app-faqs',
  standalone: true,
  imports: [FooterComponent, SafeUrlPipe],
  templateUrl: './about-us.component.html',
  styleUrl: './about-us.component.css'
})
export class AboutUsComponent {
  private titleService = inject(Title);

  ngOnInit()
  {
    this.titleService.setTitle("About Us");
  }
  email: string = 'contact@mostarbus.com';
  phone: string = '+387-123-4567';
  locationUrl: string =
    'https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d2871.6566638791314!2d17.814195815764468!3d43.343774479132024!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x134b16f13a4cb3bf%3A0x23a3e8f0d2d1bc1f!2sMostar%2C%20Bosnia%20and%20Herzegovina!5e0!3m2!1sen!2s!4v1693832822833!5m2!1sen!2s';

}
