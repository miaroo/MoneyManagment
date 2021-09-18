import { Component, OnInit, TemplateRef } from '@angular/core';
import { Observable } from 'rxjs';
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
  selectedId: number;
  categoriesList: Observable<Category[]>;


  constructor(private categoryService: CategoryService) { 
    this.userParams = this.categoryService.getUserParams(); 
  }

  ngOnInit(): void {
    this.loadCategories();
    this.getCategories();
  }

  loadCategories() {
    this.categoryService.setUserParams(this.userParams);
    this.categoryService.getCategories(this.userParams).subscribe(response => {
      this.categories = response.result;
      this.pagination = response.pagination;
    })

  }

  pageChanged(event: any){
    this.userParams.pageNumber = event.page;
    this.categoryService.setUserParams(this.userParams);
    this.loadCategories();
  }

  getCategories() {
    this.categoriesList = this.categoryService.getCategoryList();
  }

}
