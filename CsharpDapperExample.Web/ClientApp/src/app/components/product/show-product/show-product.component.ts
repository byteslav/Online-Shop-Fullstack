import { Component, OnInit } from '@angular/core';
import { ProductService } from "../../../services/product.service";
import { Product } from "../../../models/product";

@Component({
  selector: 'app-show-product',
  templateUrl: './show-product.component.html',
  styleUrls: ['./show-product.component.css']
})
export class ShowProductComponent implements OnInit {

  productList: Product[] = [];
  ModalTitle: string = '';
  ActivateAddEditProduct: boolean = false;
  product!: Product;

  constructor(private productService: ProductService) { }

  ngOnInit(): void {
    this.refreshProductList();
  }

  addClick() {
    this.product = {
      id: 0,
      name: '',
      price: 0,
      description: '',
      categoryId: 0,
      category: {
        id: 0,
        name: ''
      }
    }
    this.ModalTitle = 'Add product';
    this.ActivateAddEditProduct = true;
  }

  editClick(item: Product) {
    this.product = item;
    this.ModalTitle = 'Edit product';
    this.ActivateAddEditProduct = true;
  }

  closeClick() {
    this.ActivateAddEditProduct = false;
    this.refreshProductList();
  }

  deleteClick(product: Product) {
    if(confirm('Are you sure you want delete this product?')) {
      this.productService.deleteProduct(product.id).subscribe(data => {
        alert(data.toString());
        this.refreshProductList();
      });
    }
  }

  refreshProductList() {
    this.productService.getProductsList().subscribe(data => {
      this.productList = data;
    });
  }
}
