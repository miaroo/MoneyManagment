import { Component, Input, OnInit, TemplateRef } from '@angular/core';
import { Category } from 'src/app/_models/CategoryModel';
import { CategoryService } from 'src/app/_services/category.service';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-category-card',
  templateUrl: './category-card.component.html',
  styleUrls: ['./category-card.component.css']
})
export class CategoryCardComponent implements OnInit {
  @Input() category: Category;
  modalRef: BsModalRef;
  message: string;
  constructor(private categoryService: CategoryService, private modalService: BsModalService,
    private toastr: ToastrService) { }

  ngOnInit(): void {
  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  confirm(): void {
    this.categoryService.deleteCategory(this.category.id).subscribe( () => {

    });
    this.modalRef.hide();
  }
 
  decline(): void {
    this.modalRef.hide();
  }

}
