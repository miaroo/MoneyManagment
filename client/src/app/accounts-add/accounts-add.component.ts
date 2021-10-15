import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AddBankAccountModel } from '../_models/AddBankAccountModel';
import { BankAccountsService } from '../_services/bank-accounts.service';

@Component({
  selector: 'app-accounts-add',
  templateUrl: './accounts-add.component.html',
  styleUrls: ['./accounts-add.component.css']
})
export class AccountsAddComponent implements OnInit {
  addBankAccountModel: AddBankAccountModel;
  AddForm = new FormGroup({
    name: new FormControl('', [Validators.required, Validators.minLength(8), Validators.maxLength(50)])
  })
  constructor(private bankAccountService: BankAccountsService, private router: Router) { }

  ngOnInit(): void {
  }

  get name() 
  { 
    return this.AddForm.get('name'); 
  }

  createBankAccount() {
    this.addBankAccountModel = 
    {
      name: this.AddForm.value.name,
    }
    return this.bankAccountService.addBankAccount(this.addBankAccountModel).subscribe(response => {
      this.router.navigateByUrl('/accounts');  
   },error => {
  })
} 
}
