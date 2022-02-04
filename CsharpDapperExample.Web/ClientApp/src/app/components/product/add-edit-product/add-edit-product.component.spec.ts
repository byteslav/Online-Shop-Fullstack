import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddEditProductComponent } from './add-edit-product.component';
import {HttpClientTestingModule} from "@angular/common/http/testing";
import {ProductService} from "../../../services/product.service";
import {Product} from "../../../models/product";
import {of} from "rxjs";
import {Category} from "../../../models/category";
import {CategoryService} from "../../../services/category.service";

describe('AddEditProductComponent', () => {
  let component: AddEditProductComponent;
  let fixture: ComponentFixture<AddEditProductComponent>;
  let productService: ProductService;
  let categoryService: CategoryService;

  let mockProduct: Product = { id: 2, name: 'Bread', price: 50, description: 'Yummy', categoryId: 1, category: {id: 1, name: 'groceries'} };
  const mockCategories: Category[] = [{ id: 1, name: 'Food' }, {id: 3, name: 'Sport'}];

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      declarations: [ AddEditProductComponent ],
      providers: [ProductService, CategoryService]
    })
    .compileComponents();

    productService = TestBed.inject(ProductService);
    categoryService = TestBed.inject(CategoryService);
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddEditProductComponent);
    component = fixture.componentInstance;
    component.product = mockProduct;

    spyOn(categoryService, 'getCategoriesList').and.returnValue(of(mockCategories));
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should add product', () => {
    spyOn(productService, 'addProduct').and.returnValue(of(mockProduct));
    component.addProduct();

    expect(component.product).toBe(mockProduct);
  });

  it('should update product', () => {
    spyOn(productService, 'updateProduct').and.returnValue(of(mockProduct));
    component.updateProduct();

    expect(component.product).toBe(mockProduct);
  });
});
