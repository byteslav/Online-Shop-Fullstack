import { Component, OnInit } from '@angular/core';
import {CategoryService} from "../category.service";

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.css']
})
export class CategoryComponent implements OnInit {

  categoriesList: any = [];
  constructor(private categoryService: CategoryService) { }

  ngOnInit(): void {
    this.refreshCategoriesList();
  }

  refreshCategoriesList() {
    this.categoryService.getCategoriesList().subscribe(data => {
      this.categoriesList = data;
    })
  }

}
