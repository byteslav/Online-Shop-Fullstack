import { Component, OnInit, Input } from '@angular/core';
import {CategoryService} from "../../category.service";

@Component({
  selector: 'app-add-edit-category',
  templateUrl: './add-edit-category.component.html',
  styleUrls: ['./add-edit-category.component.css']
})
export class AddEditCategoryComponent implements OnInit {

  @Input() category: any;
  CategoryId: number = 0;
  CategoryName: string = '';

  constructor(private categoryService: CategoryService) { }

  ngOnInit(): void {
    this.CategoryId = this.category.id;
    this.CategoryName = this.category.name;
  }

  addCategory() {
    let newCategory = {id:this.CategoryId,
                  name:this.CategoryName};
    this.categoryService.addCategory(newCategory).subscribe(
      result => {
        alert(result.toString());
      }
    );
  }

  updateCategory() {
    let newCategory = {id:this.CategoryId,
      name:this.CategoryName};
    this.categoryService.updateCategory(newCategory).subscribe(
      result => {
        alert(result.toString());
      }
    );
  }

}
