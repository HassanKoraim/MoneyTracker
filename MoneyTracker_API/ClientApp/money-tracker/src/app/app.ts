import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Transaction } from './transaction/transaction';
import { IncomeOutcome } from './dashboard-header-component/income-outcome/income-outcome';
import { DashboardHeaderComponent } from './dashboard-header-component/dashboard-header-component';
import { NavBar } from '../nav-bar/nav-bar';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet,Transaction,IncomeOutcome,DashboardHeaderComponent,NavBar],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App {
  protected readonly title = signal('money-tracker');
}
