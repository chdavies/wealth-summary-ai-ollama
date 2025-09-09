import { Component } from '@angular/core';
import { DashboardComponent } from './components/dashboard.component';

@Component({
  selector: 'app-root',
  imports: [DashboardComponent],
  template: '<app-dashboard></app-dashboard>',
  styleUrl: './app.scss'
})
export class App {
  title = 'Wealth Summary AI';
}
