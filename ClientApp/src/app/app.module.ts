import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './components/app.component';
import {ProductService} from "./services/product.service";

import {HttpClientModule} from "@angular/common/http";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import { ProductComponent } from './components/product/product.component';
import { CategoryService } from "./services/category.service";
import { CategoryComponent } from './components/category/category.component';
import { ShowCategoryComponent } from './components/category/show-category/show-category.component';
import { AddEditCategoryComponent } from './components/category/add-edit-category/add-edit-category.component';
import { ShowProductComponent } from './components/product/show-product/show-product.component';
import { AddEditProductComponent } from './components/product/add-edit-product/add-edit-product.component';

@NgModule({
  declarations: [
    AppComponent,
    ProductComponent,
    CategoryComponent,
    ShowCategoryComponent,
    AddEditCategoryComponent,
    ShowProductComponent,
    AddEditProductComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [ProductService, CategoryService],
  bootstrap: [AppComponent]
})
export class AppModule { }
