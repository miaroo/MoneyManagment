import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { of } from 'rxjs';
import { map, take } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { AccountParams } from '../_models/accountsParams';
import { AddBankAccountModel } from '../_models/AddBankAccountModel';
import { BankAccount} from '../_models/BankAccountModel';
import { User } from '../_models/User';
import { UserParams } from '../_models/UserParams';
import { AccountService } from './account.service';
import { getPaginatedResult, getPaginationHeaders } from './PaginationHelper';

@Injectable({
  providedIn: 'root'
})
export class BankAccountsService {
  baseUrl = environment.apiUrl;
  bankAccounts: BankAccount[] = [];
  Cache = new Map();
  user: User;
  accountParams: AccountParams;
  
  constructor(private http: HttpClient, private accountService: AccountService) {
    this.accountService.currentUser$.pipe(take(1)).subscribe(user =>{
      this.user = user;
      this.accountParams = new AccountParams(user);
    })
   }

   getUserParams(){
    return this.accountParams;
  }

  resetUserParams(){
    this.accountParams = new AccountParams(this.user);
    return this.accountParams;
  }

  setUserParams(params: AccountParams) {
    this.accountParams = params;
  }

  getBankAccount(id: string){
    var NumberId = Number(id);
    return this.http.get<BankAccount>(this.baseUrl + 'bankAccount/' + NumberId);
  }


  getBankAccounts(accountParams: AccountParams)
  {
    var response = this.Cache.get(Object.values(accountParams).join('-'));
    if (response){
      return of(response);
    }
    let params = getPaginationHeaders(accountParams.pageNumber, accountParams.pageSize);
    params = params.append('orderBy', accountParams.orderBy);
    params = params.append('pagination', accountParams.pagination);
    return getPaginatedResult<BankAccount[]>(this.baseUrl + 'bankAccount', params, this.http).pipe(map(response => {
    this.Cache.set(Object.values(accountParams).join('-'), response);
    return response;
    }))
    }

    addBankAccount(model: AddBankAccountModel) {
      return this.http.post<BankAccount>(this.baseUrl + 'BankAccount', model);
    }

    getBankAccountsNotPaginated() {
      return this.http.get<BankAccount[]>(this.baseUrl + 'BankAccount');
    }

    UpdateBankAccount(bankAccount: BankAccount) {
      return this.http.put<BankAccount>(this.baseUrl + 'BankAccount', bankAccount).pipe(
        map( () => {
          const index = this.bankAccounts.indexOf(bankAccount);
          this.bankAccounts[index] = bankAccount;
        })
      )
    }

    deleteBankAccount(bankAccountId: number) {
      return this.http.delete(this.baseUrl + 'BankAccount/' + bankAccountId).pipe(
        map(() => {

        })
      )
    }

}
