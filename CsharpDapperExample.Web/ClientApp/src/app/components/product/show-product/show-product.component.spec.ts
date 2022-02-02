import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShowProductComponent } from './show-product.component';
import {Category} from "../../../models/category";
import {HttpClientTestingModule} from "@angular/common/http/testing";
import {ProductService} from "../../../services/product.service";
import {Product} from "../../../models/product";
import {of} from "rxjs";

describe('ShowProductComponent', () => {
  let component: ShowProductComponent;
  let fixture: ComponentFixture<ShowProductComponent>;
  let productService: ProductService;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ HttpClientTestingModule ],
      declarations: [ ShowProductComponent ],
      providers: [ProductService]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ShowProductComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();

    productService = TestBed.inject(ProductService);
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should open Add modal window', () => {
    expect(component.ModalTitle).toBe('');
    expect(component.ActivateAddEditProduct).toBe(false);

    component.addClick();

    expect(component.ModalTitle).toBe('Add product');
    expect(component.ActivateAddEditProduct).toBe(true);
  });

  it('should open Edit modal window', () => {
    const mockProduct: Product = { id: 1, name: 'Bread', price: 10, description: 'Tasty', categoryId: 1, category: {id:2, name: 'Food'}};
    expect(component.ModalTitle).toBe('');
    expect(component.ActivateAddEditProduct).toBe(false);

    component.editClick(mockProduct);

    expect(component.ModalTitle).toBe('Edit product');
    expect(component.ActivateAddEditProduct).toBe(true);
    expect(component.product).toBe(mockProduct);
  });

  it('should close modal window', () => {
    component.ActivateAddEditProduct = true;
    component.closeClick();
    expect(component.ActivateAddEditProduct).toBe(false);
  });

  it('should delete product', () => {
    const mockCategory: Product = { id: 4, name: 'Ball', price: 10, description: 'Norm', categoryId: 2, category: {id: 2, name: 'Sport'} };
    const mockCategories: Product[] = [{ id: 3, name: 'Boots', price: 10, description: 'Norm', categoryId: 2, category: {id: 2, name: 'Sport'} }, { id: 4, name: 'Ball', price: 10, description: 'Norm', categoryId: 2, category: {id: 2, name: 'Sport'} }];
    spyOn(component, 'refreshProductList').and.callFake(function () {
      component.productList = mockCategories;
    });
    spyOn(productService, 'deleteProduct').and.callFake(function (id: number) {
      mockCategories.pop();
      return of(mockCategories);
    });
    component.refreshProductList();
    component.deleteClick(mockCategory);

    expect(component.productList).toBe(mockCategories);
  });
});
