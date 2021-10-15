import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute } from '@angular/router';
import { AccountParams } from '../_models/accountsParams';
import { BankAccount } from '../_models/BankAccountModel';
import { Operation } from '../_models/OperationModel';
import { OperationParams } from '../_models/OperationParams';
import { Pagination } from '../_models/pagination';
import { BankAccountsService } from '../_services/bank-accounts.service';
import { OperationService } from '../_services/operation.service';

@Component({
  selector: 'app-accounts-details',
  templateUrl: './accounts-details.component.html',
  styleUrls: ['./accounts-details.component.css']
})
export class AccountsDetailsComponent implements OnInit, AfterViewInit {
  public displayedColumns: string[] = ['id', 'description', 'amount', 'date', 'name', 'categoryId', 'actions'];
  bankAccount: BankAccount;
  operations: Operation[] = [];
  pagination: Pagination;
  operationParams: OperationParams = new OperationParams();
  accountParams: AccountParams;
  dataSource = new MatTableDataSource<Operation>();

  @ViewChild(MatSort) sort: MatSort;
  constructor(private bankAccountService: BankAccountsService,
     private operationsService: OperationService, private route: ActivatedRoute ) {
    this.accountParams = this.bankAccountService.getUserParams();
   }
  ngAfterViewInit(): void {
    this.dataSource.sort = this.sort;
  }

  ngOnInit(): void {
    this.loadBankAccount();
    this.loadOperations();
  }

  loadBankAccount() {
    this.bankAccountService.setUserParams(this.accountParams);
    this.bankAccountService.getBankAccount(this.route.snapshot.paramMap.get('id')).subscribe(bank => {
      this.bankAccount = bank;
    }
    );
  }

  loadOperations() {
    this.operationParams.bankAccountId = Number(this.route.snapshot.paramMap.get('id'))
    this.operationsService.setUserParams(this.operationParams);
    this.operationsService.getOperations(this.operationParams).subscribe(operations =>
      {
         this.operations = operations.result;
         this.pagination = operations.pagination;
         this.dataSource.data = operations.result;
      })
  }

  pageChanged(event: any){
    this.accountParams.pageNumber = event.page;
    this.bankAccountService.setUserParams(this.accountParams);
    this.loadOperations();
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
}
}
