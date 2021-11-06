import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { AddOperationModel } from '../_models/AddOperationModel';
import { BankAccount } from '../_models/BankAccountModel';
import { Operation } from '../_models/OperationModel';
import { OperationParams } from '../_models/OperationParams';
import { Pagination } from '../_models/pagination';
import { User } from '../_models/User';
import { AccountService } from './account.service';
import { getPaginatedResult, getPaginationHeaders } from './PaginationHelper';

@Injectable({
  providedIn: 'root'
})
export class OperationService {
  operations: Operation[];
  pagination: Pagination;
  user: User;
  baseUrl = environment.apiUrl;
  operationParams: OperationParams;

  constructor(private accountService: AccountService, private http: HttpClient) {
    this.operationParams = new OperationParams();
   }

  getUserParams(){
    return this.operationParams;
  }

  resetUserParams(){
    this.operationParams = new OperationParams();
    return this.operationParams;
  }

  setUserParams(params: OperationParams) {
    this.operationParams = params;
  }

  getOperations(operateParams: OperationParams) 
  {
    let params = getPaginationHeaders(operateParams.pageNumber, operateParams.pageSize);
    params = params.append('bankAccountId', operateParams.bankAccountId);
    params = params.append('pagination', operateParams.pagination);
    return getPaginatedResult<Operation[]>(this.baseUrl + 'operation', params, this.http);
 }

 addOperation(model: AddOperationModel) {
  return this.http.post<Operation>(this.baseUrl + 'operation', model);
}
}
