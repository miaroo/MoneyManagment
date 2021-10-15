import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AddOperationModel } from '../_models/AddOperationModel';
import { BankAccount } from '../_models/BankAccountModel';
import { Category } from '../_models/CategoryModel';
import { BankAccountsService } from '../_services/bank-accounts.service';
import { CategoryService } from '../_services/category.service';
import { OperationService } from '../_services/operation.service';

@Component({
  selector: 'app-operation-add',
  templateUrl: './operation-add.component.html',
  styleUrls: ['./operation-add.component.css']
})
export class OperationAddComponent implements OnInit {

  categories: Category[] = [];
  bankAccounts: BankAccount[] = [];
  validationErrors: string[] = [];
  addOperationModule: AddOperationModel;
  AddForm = new FormGroup({
    description: new FormControl('', [Validators.required, Validators.minLength(8), Validators.maxLength(50)]),
    amount: new FormControl('', [Validators.required, Validators.pattern(/^-?(0|[1-9]\d*)?$/)]),
    date: new FormControl('', [Validators.required]),
    name: new FormControl('', [Validators.required, Validators.minLength(5), Validators.maxLength(50)]),
    categoryId: new FormControl('',),
    bankAccountId: new FormControl('', [Validators.required]),
  })
  constructor(private operationService: OperationService, private router: Router,
    private bankAccountService: BankAccountsService, private categoryService: CategoryService) { }

  ngOnInit(): void {
    this.getCategories();
    this.getBankAccounts();
  }

  getCategories() {
    this.categoryService.getCategoryList().subscribe(category => {
      this.categories = category;
    });  
  }

  getBankAccounts() {
    this.bankAccountService.getBankAccountsNotPaginated().subscribe(accounts => {
      this.bankAccounts = accounts;
    });  
  }

  get name() 
  { 
    return this.AddForm.get('name'); 
  }
  get description() 
  { 
    return this.AddForm.get('description'); 
  }
  get amount() 
  { 
    return this.AddForm.get('amount')
  }
  get date() 
  { 
    return this.AddForm.get('date'); 
  }
  get categoryId() 
  { 
    return this.AddForm.get('categoryId'); 
  }
  get bankAccountId() 
  { 
    return this.AddForm.get('bankAccountId'); 
  }

  createOperation() {
    this.addOperationModule = {
      Description: this.AddForm.value.description,
      Amount: Number(this.AddForm.value.amount),
      Date: this.AddForm.value.date,
      Name: this.AddForm.value.name,
      CategoryId: this.AddForm.value.categoryId,
      BankAccountId: this.AddForm.value.bankAccountId
    }
    return this.operationService.addOperation(this.addOperationModule).subscribe(response => {
      this.router.navigateByUrl('/accounts');  
   },error => {
    this.validationErrors = error;
  })
  console.log(this.addOperationModule);
} 
  

}
