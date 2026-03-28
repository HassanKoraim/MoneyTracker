import { Component, signal } from '@angular/core'; // Import signal
import { Transactions } from '../app/services/transactions';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';


@Component({
  selector: 'app-nav-bar',
  imports: [CommonModule,RouterLink],
  templateUrl: './nav-bar.html',
  styleUrl: './nav-bar.scss',
})
export class NavBar {
  // Change these to signals
  totalBalance = signal<number>(0);
  isBalanceVisible = signal<boolean>(false);
  isLoading = signal<boolean>(false);

  constructor(private transactionsService: Transactions) {}

  toggleBalance() {
    this.isBalanceVisible.set(!this.isBalanceVisible());
    if (this.isBalanceVisible()) {
      this.GetTotalBalance();
    }
  }

  GetTotalBalance() {
    this.isLoading.set(true);
    
    this.transactionsService.GetAmount("Income").subscribe((income: any) => {
      this.transactionsService.GetAmount("Expense").subscribe((expense: any) => {
        this.totalBalance.set(income - expense);
        this.isLoading.set(false); // This will now trigger the UI immediately
      });
    });
  }
}