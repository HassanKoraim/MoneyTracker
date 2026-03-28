import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class Transactions {
  constructor(private http:HttpClient){}
  TransactionList(sortBy:string|null = null,orderBy:string|null = null){
    const url = `/api/TransactionApi/GetAll/${sortBy}/${orderBy}`;
    return this.http.get(url)
  }
  GetAmount(transactionType:string){
    const url = `/api/TransactionApi/GetAmount?transactionType=${transactionType}`;
    return this.http.get(url)
  }
UploadExcelSheet(sortBy:string|null=null,sortOrder:string|null = null): Observable<Blob> {
  return this.http.get(`/api/TransactionApi/transactionsExcel/${sortBy}/${sortOrder}`, {
    responseType: 'blob'
  });
}
}
