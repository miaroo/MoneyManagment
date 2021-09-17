import { Component, OnInit } from '@angular/core';
import { Category } from 'src/app/_models/CategoryModel';
import { Pagination } from 'src/app/_models/pagination';
import { User } from 'src/app/_models/User';
import { UserParams } from 'src/app/_models/UserParams';
import { CategoryService } from 'src/app/_services/category.service';

@Component({
  selector: 'app-category-list',
  templateUrl: './category-list.component.html',
  styleUrls: ['./category-list.component.css']
})
export class CategoryListComponent implements OnInit {
  categories: Category[];
  pagination: Pagination;
  userParams: UserParams;
  user: User;

  constructor(private categoryService: CategoryService) { 
    this.userParams = this.categoryService.getUserParams(); 
  }

  ngOnInit(): void {
    this.loadCategories();
  }

  loadCategories() {
    this.categoryService.setUserParams(this.userParams);
    this.categoryService.getCategories(this.userParams).subscribe( response => {
      this.categories = response.result;
      this.pagination = response.pagination;
    })

  }

  pageChanged(event: any){
    this.userParams.pageNumber = event.page;
    this.categoryService.setUserParams(this.userParams);
    this.loadCategories();
  }

}
