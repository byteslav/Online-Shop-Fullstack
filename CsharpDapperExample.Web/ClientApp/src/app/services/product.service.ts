import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {Product} from "../models/product";

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  readonly APIUrl: string = 'https://localhost:5001/api';

  constructor(private http:HttpClient) { }

  getProductsList():Observable<Product[]> {
    return this.http.get<Product[]>(this.APIUrl+'/Product')
  }

  addProduct(value: Product) {
    return this.http.post(this.APIUrl+'/Product', value);
  }

  updateProduct(value: Product) {
    return this.http.put(this.APIUrl+'/Product', value);
  }

  deleteProduct(value: number) {
    return this.http.delete(this.APIUrl+'/Product/'+ value);
  }
}
