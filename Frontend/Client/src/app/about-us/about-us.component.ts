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
  phone: string = '+387/123-456-789';
  locationUrl: string =
    'https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d2871.5962879511083!2d17.802233915764478!3d43.34421617913175!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x134b16ec1e5d325f%3A0x9f3e6fdf5dcf4e8!2s8R8H%2BRGH%2C%20Mostar%2088000%2C%20Bosnia%20and%20Herzegovina!5e0!3m2!1sen!2s!4v1701234567890!5m2!1sen!2s';
}
