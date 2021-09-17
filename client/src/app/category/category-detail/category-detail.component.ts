import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { Observable, Subscription } from 'rxjs';
import { Category } from 'src/app/_models/CategoryModel';
import { CategoryService } from 'src/app/_services/category.service';

@Component({
  selector: 'app-category-detail',
  templateUrl: './category-detail.component.html',
  styleUrls: ['./category-detail.component.css']
})
export class CategoryDetailComponent implements OnInit, OnDestroy {
  category: Category;
  sub : Subscription;
  constructor(private categoryService: CategoryService, private route: ActivatedRoute) { }


  ngOnInit(): void {
    this.loadCategory();
  }

  ngOnDestroy() {
    this.sub.unsubscribe();
  }

  loadCategory() {
    this.categoryService.getCategory(this.route.snapshot.paramMap.get('id')).subscribe(category => {
      this.category = category;
    } );  
  }
}
