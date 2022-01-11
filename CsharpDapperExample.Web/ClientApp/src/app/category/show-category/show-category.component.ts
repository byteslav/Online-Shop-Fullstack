import { Component, OnInit } from '@angular/core';
import {CategoryService} from "../../category.service";

@Component({
  selector: 'app-show-category',
  templateUrl: './show-category.component.html',
  styleUrls: ['./show-category.component.css']
})
export class ShowCategoryComponent implements OnInit {

  categoriesList: any = [];
  ModalTitle: string = '';
  ActivateAddEditCategory: boolean = false;
  category: any;

  constructor(private categoryService: CategoryService) { }

  ngOnInit(): void {
    this.refreshCategoriesList();
  }

  refreshCategoriesList() {
    this.categoryService.getCategoriesList().subscribe(data => {
      this.categoriesList = data;
    })
  }

  addClick() {
    console.log('hi')
    this.category = {
      id: 0,
      name: ''
    }
    this.ModalTitle = 'Add category'
    this.ActivateAddEditCategory = true;
  }


  closeClick() {
    this.ActivateAddEditCategory = false;
    this.refreshCategoriesList();
  }
}
