import { AfterViewInit, Component, OnInit, QueryList, TemplateRef, ViewChild, ViewChildren } from '@angular/core';
import { AccountParams } from '../_models/accountsParams';
import { BankAccount } from '../_models/BankAccountModel';
import { Pagination } from '../_models/pagination';
import { BankAccountsService } from '../_services/bank-accounts.service';
import { MatSort } from '@angular/material/sort';
import { Observable } from 'rxjs';
import { UserParams } from 'src/app/_models/UserParams';
import { CategoryService } from 'src/app/_services/category.service';
import { MatTable, MatTableDataSource } from '@angular/material/table';
import { Operation } from '../_models/OperationModel';
import { animate, state, style, transition, trigger } from '@angular/animations';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-accounts',
  templateUrl: './accounts.component.html',
  styleUrls: ['./accounts.component.css'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({ height: '0px', minHeight: '0' })),
      state('expanded', style({ height: '*' })),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})
export class AccountsComponent implements OnInit, AfterViewInit {
  public columnsToDisplay: string[] = ['id', 'name', 'lastActive', 'actions'];
  public innerDisplayedColumns: string[] = ['id', 'name', 'description', 'amount', 'date', 'categoryId']
  bankAccountsList: BankAccount[] = [];
  pagination: Pagination;
  accountParams: AccountParams;
  dataSource = new MatTableDataSource<BankAccount>();
  expandedElement: Operation | null;
  modalRef?: BsModalRef;
  message?: string;

  @ViewChild(MatSort) sort: MatSort;

  constructor(private bankAccountService: BankAccountsService, private modalService: BsModalService) {
    this.accountParams = this.bankAccountService.getUserParams();
   }
  ngAfterViewInit(): void {
    this.dataSource.sort = this.sort;
  }

  ngOnInit(): void {
    this.loadBankAccounts();
  }

  loadBankAccounts() {
    this.bankAccountService.setUserParams(this.accountParams);
    this.bankAccountService.getBankAccounts(this.accountParams).subscribe(bankAccountsList => {
      this.bankAccountsList = bankAccountsList.result;
      this.pagination = bankAccountsList.pagination;
      this.dataSource.data = this.bankAccountsList;
    });

  }

  pageChanged(event: any){
    this.accountParams.pageNumber = event.page;
    this.bankAccountService.setUserParams(this.accountParams);
    this.loadBankAccounts();
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
}

openModal(template: TemplateRef<any>) {
  this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
}

confirm(): void {
  this.message = 'Confirmed!';
  this.modalRef?.hide();
}

decline(): void {
  this.message = 'Declined!';
  this.modalRef?.hide();
}

}
