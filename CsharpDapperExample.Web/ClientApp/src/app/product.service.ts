import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  readonly APIUrl: string = 'https://localhost:5001/api';

  constructor(private http:HttpClient) { }

  getProductsList():Observable<any[]> {
    return this.http.get<any>(this.APIUrl+'/Product')
  }

  addProduct(value: any) {
    return this.http.post(this.APIUrl+'/Product', value);
  }

  updateProduct(value: any) {
    return this.http.put(this.APIUrl+'/Product', value);
  }

  deleteProduct(value: any) {
    return this.http.delete(this.APIUrl+'/Product/'+ value);
  }
}
