import { TicketService } from '../_services/ticket.service'; // Adjust path if needed
import { Component, AfterViewInit, ViewChild, ElementRef } from '@angular/core';
import {
  Chart,
  LineController,
  LineElement,
  PointElement,
  LinearScale,
  Title,
  Tooltip,
  Legend,
  CategoryScale
} from 'chart.js';

  // Register the necessary components
  Chart.register(
    LineController,
    LineElement,
    PointElement,
    LinearScale,
    Title,
    Tooltip,
    Legend,
    CategoryScale
  );


@Component({
  standalone: true,
  selector: 'app-ticket-graph',
  templateUrl: './ticket-graph.component.html',
  styleUrls: ['./ticket-graph.component.css'],
})
export class TicketGraphComponent implements AfterViewInit {
  @ViewChild('ticketChart') ticketChart!: ElementRef;

  chart: Chart | null = null;

  constructor(private ticketService: TicketService) {}

  ngAfterViewInit(): void {
    this.loadChartData();
  }

  loadChartData(): void {
    this.ticketService.getTicketsOverTime().subscribe(
      (data) => {
        const labels = data.map((item) => item.month);
        const counts = data.map((item) => item.count);

        this.renderChart(labels, counts);
      },
      (error) => {
        console.error('Error loading chart data:', error);
      }
    );
  }

  renderChart(labels: string[], data: number[]): void {
    if (this.chart) {
      this.chart.destroy();
    }

    this.chart = new Chart(this.ticketChart.nativeElement, {
      type: 'line',
      data: {
        labels,
        datasets: [
          {
            label: 'Tickets Bought',
            data,
            borderColor: 'blue',
            backgroundColor: 'rgba(0, 123, 255, 0.5)',
          },
        ],
      },
      options: {
        responsive: true,
        maintainAspectRatio: false,
      },
    });
  }
}
