import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {Product} from "../models/product";

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  readonly APIUrl: string = 'https://localhost:5001/api/Product';

  constructor(private http:HttpClient) { }

  getProductsList():Observable<Product[]> {
    return this.http.get<Product[]>(this.APIUrl);
  }

  addProduct(value: Product) {
    return this.http.post(this.APIUrl, value);
  }

  updateProduct(value: Product) {
    return this.http.put(this.APIUrl, value);
  }

  deleteProduct(value: number) {
    return this.http.delete(this.APIUrl +'/'+ value);
  }
}
