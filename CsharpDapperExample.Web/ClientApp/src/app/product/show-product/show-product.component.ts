import { Component, OnInit } from '@angular/core';
import {ProductService} from "../../product.service";

@Component({
  selector: 'app-show-product',
  templateUrl: './show-product.component.html',
  styleUrls: ['./show-product.component.css']
})
export class ShowProductComponent implements OnInit {

  productList: any=[];
  ModalTitle: string = '';
  ActivateAddEditProduct: boolean = false;
  product: any;
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
    this.ModalTitle = 'Add Product';
    this.ActivateAddEditProduct = true;
  }

  closeClick() {

  }
}
