import { Component, inject } from '@angular/core';
import { Title } from '@angular/platform-browser';

@Component({
  selector: 'app-schedule',
  standalone: true,
  imports: [],
  templateUrl: './schedule.component.html',
  styleUrl: './schedule.component.css'
})
export class ScheduleComponent {
  private titleService = inject(Title);

  ngOnInit()
  {
    this.titleService.setTitle("Schedule");
  }
}
