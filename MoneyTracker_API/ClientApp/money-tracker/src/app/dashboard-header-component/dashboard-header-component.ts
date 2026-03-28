import { Component } from '@angular/core';
import { IncomeOutcome } from './income-outcome/income-outcome';
import { BarChartComponent } from './bar-chart-component/bar-chart-component';

@Component({
  selector: 'app-dashboard-header-component',
  imports: [IncomeOutcome,BarChartComponent],
  templateUrl: './dashboard-header-component.html',
  styleUrl: './dashboard-header-component.scss',
})
export class DashboardHeaderComponent {

}
