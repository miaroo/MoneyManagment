import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { AccountParams } from '../_models/accountsParams';
import { BankAccount } from '../_models/BankAccountModel';
import { Pagination } from '../_models/pagination';
import { BankAccountsService } from '../_services/bank-accounts.service';
import { MatSort } from '@angular/material/sort';
import { Observable } from 'rxjs';
import { UserParams } from 'src/app/_models/UserParams';
import { CategoryService } from 'src/app/_services/category.service';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'app-accounts',
  templateUrl: './accounts.component.html',
  styleUrls: ['./accounts.component.css']
})
export class AccountsComponent implements OnInit, AfterViewInit {
  public displayedColumns: string[] = ['id', 'name', 'lastActive', 'actions'];
  bankAccountsList: BankAccount[] = [];
  pagination: Pagination;
  accountParams: AccountParams;
  dataSource = new MatTableDataSource<BankAccount>();

  @ViewChild(MatSort) sort: MatSort;
  constructor(private bankAccountService: BankAccountsService) {
    this.accountParams = this.bankAccountService.getUserParams();
   }
  ngAfterViewInit(): void {
    this.dataSource.sort = this.sort;
  }

  ngOnInit(): void {
    this.loadCategories();
  }

  loadCategories() {
    this.bankAccountService.setUserParams(this.accountParams);
    this.bankAccountService.getBankAccounts(this.accountParams).subscribe(bankAccountsList => {
      this.bankAccountsList = bankAccountsList.result;
      this.pagination = bankAccountsList.pagination;
      this.dataSource.data = this.bankAccountsList;
      console.log(this.bankAccountsList);
    });

  }

  pageChanged(event: any){
    this.accountParams.pageNumber = event.page;
    this.bankAccountService.setUserParams(this.accountParams);
    this.loadCategories();
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
}
}
