import { Component, Input, OnInit } from '@angular/core';
import { Category } from 'src/app/_models/CategoryModel';
import { CategoryService } from 'src/app/_services/category.service';

@Component({
  selector: 'app-category-card',
  templateUrl: './category-card.component.html',
  styleUrls: ['./category-card.component.css']
})
export class CategoryCardComponent implements OnInit {
  @Input() category: Category;
  constructor(private categoryService: CategoryService) { }

  ngOnInit(): void {
  }

}
