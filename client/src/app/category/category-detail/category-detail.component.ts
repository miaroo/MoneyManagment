import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable, Subscription } from 'rxjs';
import { Category } from 'src/app/_models/CategoryModel';
import { CategoryService } from 'src/app/_services/category.service';

@Component({
  selector: 'app-category-detail',
  templateUrl: './category-detail.component.html',
  styleUrls: ['./category-detail.component.css']
})
export class CategoryDetailComponent implements OnInit {
  category: Category;
  categories: Observable<Category[]>;
  sub : Subscription;
  constructor(private categoryService: CategoryService, private route: ActivatedRoute, private toastr: ToastrService) { }


  ngOnInit(): void {
    this.loadCategory();
    this.getCategories()
  }


  loadCategory() {
    this.categoryService.getCategory(this.route.snapshot.paramMap.get('id')).subscribe(category => {
      this.category = category;
    } );  
  }

  updateCategory() {
    this.categoryService.UpdateCategory(this.category).subscribe(() => {
      this.toastr.success("Updated");
    });
  }

  getCategories() {
    this.categories = this.categoryService.getCategoryList();
  }

}
