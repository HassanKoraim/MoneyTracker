import { Routes } from '@angular/router';
import { Transaction } from './transaction/transaction';
import { CommingSoon } from './comming-soon/comming-soon';

export const routes: Routes = [
    { path: 'transactions', component: Transaction },
  { path: 'comming-soon', component: CommingSoon }, 
 { path: '', redirectTo: 'transactions', pathMatch: 'full' }
];
