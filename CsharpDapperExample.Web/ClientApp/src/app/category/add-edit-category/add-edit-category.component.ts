import { Component, OnInit, Input } from '@angular/core';
import {CategoryService} from "../../services/category.service";
import {Category} from "../../models/category";
import {FormControl, FormGroup} from "@angular/forms";

@Component({
  selector: 'app-add-edit-category',
  templateUrl: './add-edit-category.component.html',
  styleUrls: ['./add-edit-category.component.css']
})
export class AddEditCategoryComponent implements OnInit {

  @Input() category!: Category;

  categoryForm: FormGroup = new FormGroup({
    id: new FormControl(''),
    name: new FormControl(''),
  });

  constructor(private categoryService: CategoryService) { }

  ngOnInit(): void {
    this.categoryForm.patchValue({
      id: this.category.id,
      name: this.category.name,
    });
  }

  addCategory() {
    let newCategory: Category = {
      id: this.categoryForm.value.id,
      name: this.categoryForm.value.name,
    };

    this.categoryService.addCategory(newCategory).subscribe(
      result => {
        alert(result.toString());
      }
    );
  }

  updateCategory() {
    let updatedCategory: Category = {
      id: this.categoryForm.value.id,
      name: this.categoryForm.value.name,
    };

    this.categoryService.updateCategory(updatedCategory).subscribe(
      result => {
        alert(result.toString());
      }
    );
  }
}
