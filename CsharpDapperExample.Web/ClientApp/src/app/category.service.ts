import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  readonly APIUrl: string = 'https://localhost:5001/api';

  constructor(private http:HttpClient) { }

  getCategoriesList():Observable<any[]> {
    return this.http.get<any>(this.APIUrl+'/Category')
  }

  addCategory(value: any) {
    return this.http.post(this.APIUrl+'/Category', value);
  }

  updateCategory(value: any) {
    return this.http.put(this.APIUrl+'/Category', value);
  }

  deleteCategory(value: any) {
    return this.http.delete(this.APIUrl+'/Category/'+ value);
  }
}
