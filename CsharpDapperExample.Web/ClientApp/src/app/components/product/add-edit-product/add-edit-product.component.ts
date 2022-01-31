import {Component, Input, OnInit} from '@angular/core';
import {Product} from "../../../models/product";
import {Category} from "../../../models/category";
import {ProductService} from "../../../services/product.service";
import {CategoryService} from "../../../services/category.service";
import {FormControl, FormGroup} from "@angular/forms";

@Component({
  selector: 'app-add-edit-product',
  templateUrl: './add-edit-product.component.html',
  styleUrls: ['./add-edit-product.component.css']
})
export class AddEditProductComponent implements OnInit {

  @Input() product!: Product;
  CategoryList: Category[] = [];

  // Reactive forms
  productForm: FormGroup = new FormGroup({
    id: new FormControl(''),
    name: new FormControl(''),
    price: new FormControl(''),
    description: new FormControl(''),
    category: new FormControl(''),
  });

  constructor(private productService: ProductService,
              private categoryService: CategoryService) { }

  ngOnInit(): void {
    this.productForm.patchValue({
      id: this.product.id,
      name: this.product.name,
      price: this.product.price,
      description: this.product.description,
      category: this.product.category,
    });

    this.categoryService.getCategoriesList().subscribe((data:Category[]) => {
      this.CategoryList = data;
    });
  }

  updateProduct() {
    let updatedProduct: Product = this.productForm.value;

    this.productService.updateProduct(updatedProduct).subscribe(
      result => {
        alert(result.toString());
      }
    );
  }

  addProduct() {
    let newProduct: Product = this.productForm.value;

    this.productService.addProduct(newProduct).subscribe(
      result => {
        alert(result.toString());
      }
    );
  }
}
