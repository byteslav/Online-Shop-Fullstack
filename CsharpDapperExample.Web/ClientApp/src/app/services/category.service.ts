import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {Category} from "../models/category";

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  readonly APIUrl: string = 'https://localhost:5001/api';

  constructor(private http:HttpClient) { }

  getCategoriesList():Observable<Category[]> {
    return this.http.get<Category[]>(this.APIUrl+'/Category')
  }

  addCategory(value: Category) {
    return this.http.post(this.APIUrl+'/Category', value);
  }

  updateCategory(value: Category) {
    return this.http.put(this.APIUrl+'/Category', value);
  }

  deleteCategory(value: number) {
    return this.http.delete(this.APIUrl+'/Category/' + value);
  }
}
