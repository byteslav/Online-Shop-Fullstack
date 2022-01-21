import {Component, Input, OnInit} from '@angular/core';
import {Product} from "../../models/product";
import {Category} from "../../models/category";
import {ProductService} from "../../services/product.service";
import {CategoryService} from "../../services/category.service";

@Component({
  selector: 'app-add-edit-product',
  templateUrl: './add-edit-product.component.html',
  styleUrls: ['./add-edit-product.component.css']
})
export class AddEditProductComponent implements OnInit {

  @Input() product!: Product;
  productId: number = 0;
  productName: string = '';
  productPrice: number = 0;
  productDescription: string = '';
  productCategoryId: number = 0;
  productCategory!: Category;

  CategoryList: Category[] = [];

  constructor(private productService: ProductService,
              private categoryService: CategoryService) { }

  ngOnInit(): void {
    this.categoryService.getCategoriesList().subscribe((data:Category[]) => {
      this.CategoryList = data;

      this.productId = this.product.id;
      this.productName = this.product.name;
      this.productPrice = this.product.price;
      this.productDescription = this.product.description;
      this.productCategoryId = this.product.categoryId;

      this.productCategory = this.product.category;
    });
  }

  updateProduct() {
    let updatedProduct: Product = {
      id: this.productId,
      name: this.productName,
      price: this.productPrice,
      description: this.productDescription,
      categoryId: this.productCategory.id,
      category: this.productCategory
    };

    this.productService.updateProduct(updatedProduct).subscribe(
      result => {
        alert(result.toString());
      }
    );
  }

  addProduct() {
    let newProduct: Product = {
      id: this.productId,
      name: this.productName,
      price: this.productPrice,
      description: this.productDescription,
      categoryId: this.productCategory.id,
      category: this.productCategory
    };

    this.productService.addProduct(newProduct).subscribe(
      result => {
        alert(result.toString());
      }
    );
  }
}
