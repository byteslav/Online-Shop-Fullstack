import { Component, OnInit } from '@angular/core';
import {ProductService} from "../product.service";

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent implements OnInit {

  productList: any=[];
  constructor(private productService: ProductService) { }

  ngOnInit(): void {
    this.refreshProductList();
  }

  refreshProductList() {
    this.productService.getProductsList().subscribe(data => {
      this.productList = data;
    });
  }

}
