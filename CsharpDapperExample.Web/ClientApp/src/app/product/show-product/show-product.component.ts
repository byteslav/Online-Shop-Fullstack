import { Component, OnInit } from '@angular/core';
import { ProductService } from "../../services/product.service";
import { Product } from "../../models/product";

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

  refreshProductList() {
    this.productService.getProductsList().subscribe(data => {
      this.productList = data;
    });
  }

  addClick() {
    this.product = {
      id: 0,
      name: '',
      price: 0,
      description: '',

      category: {
        id: 0,
        name: ''
      }
    }
    this.ModalTitle = 'Add Product';
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
}
