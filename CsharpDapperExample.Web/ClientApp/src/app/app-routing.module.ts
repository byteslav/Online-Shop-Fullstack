import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProductComponent } from "./product/product.component";
import {CategoryComponent} from "./category/category.component";

const routes: Routes = [
  { path:'product', component:ProductComponent },
  { path: 'category', component:CategoryComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
