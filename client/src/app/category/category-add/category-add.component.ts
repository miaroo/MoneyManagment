import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { Category } from 'src/app/_models/CategoryModel';
import { User } from 'src/app/_models/User';
import { CategoryService } from 'src/app/_services/category.service';
import {FormsModule} from '@angular/forms';
import { OperationType } from 'src/app/_models/OperationTypeMode';
import { ControlValueAccessor, NgControl } from '@angular/forms';
import { UserParams } from 'src/app/_models/UserParams';
import { AddCategoryModel } from 'src/app/_models/AddCategoryModel';

@Component({
  selector: 'app-category-add',
  templateUrl: './category-add.component.html',
  styleUrls: ['./category-add.component.css']
})
export class CategoryAddComponent implements OnInit {
  addCategoryModel: AddCategoryModel;
  user: User;
  addCategoryForm: FormGroup;
  categories: Observable<Category[]>;
  validationErrors: string[] = [];
  OperationTypes: OperationType[] = [
    {"Id": 1, "name": "Income"},
    {"Id": 2, "name": "Expense"}
  ]

  constructor(private categoryService: CategoryService, private toastr: ToastrService, private fb: FormBuilder, private router: Router) { }

  ngOnInit(): void {
    this.initializeForm();
    this.getCategories();
  }

  initializeForm() {
    this.addCategoryForm = this.fb.group({
      operationType: ['', Validators.required],
      name: ['', Validators.required],
      parentCategoryId: [''],

    })
  }

  createCategory() {
    this.addCategoryModel = 
    {
      name: this.addCategoryForm.value.name,
      parentCategoryId: this.addCategoryForm.value.parentCategoryId,
      operationTypeId: this.addCategoryForm.value.operationType
    }
    return this.categoryService.addCategory(this.addCategoryModel).subscribe(response => {
      this.router.navigateByUrl('/categories');
    },error => {
      this.validationErrors = error;
    })
   }

   getCategories() {
     this.categories = this.categoryService.getCategoryList();
   }

}
