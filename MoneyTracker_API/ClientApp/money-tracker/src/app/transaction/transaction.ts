import { Component, effect, signal, untracked } from '@angular/core';
import { Transactions } from '../services/transactions';
import { CommonModule } from '@angular/common';
import { SignalFormControl } from '@angular/forms/signals/compat';

@Component({
  selector: 'app-transaction',
  imports: [CommonModule],
  templateUrl: './transaction.html',
  styleUrl: './transaction.scss',
})
export class Transaction {
  currentSortBy = signal<string>("TransactionDate");
  currentSortOrder= signal<string>("asc");
  // transactionList: any[] = []; // Define the list property
  transactionList = signal<any[]>([]);
  constructor(private transactionsService:Transactions){  
    // effect(()=>{
    //       this.LoadTransactions(this.currentSortBy(),this.currentSortOrder());
    // });


  //    effect(() => {
  // const sortBy = this.currentSortBy();
  // const order = this.currentSortOrder();

  // untracked(() => {
  //   this.LoadTransactions(sortBy, order);});});
  }
  

  ngOnInit(){
   this.LoadTransactions(this.currentSortBy(), this.currentSortOrder());
   }
  LoadTransactions(sortBy:string,orderBy:string){
  this.transactionsService.TransactionList(sortBy,orderBy).subscribe((data:any)=>{
      // this.transactionList = data; 
      this.transactionList.set([...data]);
    //  transactionDate: data.transactionDate ? new Date(data.transactionDate) : null
      console.log(data);
    })
  }
  onSort(column:string){
    if(column === this.currentSortBy()) {
      // const newOrder =  this.currentSortOrder() === "asc" ? "desc" : "asc";
      // this.currentSortOrder.set(newOrder);
      this.currentSortOrder.update(order => order === "asc" ? "desc" : "asc");
    }else{
      this.currentSortBy.set(column);
      this.currentSortOrder.set("asc");
    }
    // modifiy this with the effect() 
   this.LoadTransactions(this.currentSortBy(),this.currentSortOrder());
  }
  exportToExcel() {
  this.transactionsService.UploadExcelSheet(this.currentSortBy(),this.currentSortOrder()).subscribe({
    next: (blob: Blob) => {
      const url = window.URL.createObjectURL(blob);
      const a = document.createElement('a');
      a.href = url;
      a.download = `Transactions_${new Date().toLocaleDateString()}.xlsx`;
      document.body.appendChild(a);
      a.click();
      
      window.URL.revokeObjectURL(url);
      a.remove();
    },
    error: (err) => {
      console.error("Export failed:", err);
    }
  });
 
}

}

