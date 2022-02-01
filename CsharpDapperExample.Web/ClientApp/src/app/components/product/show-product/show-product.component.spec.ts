import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShowProductComponent } from './show-product.component';
import {Category} from "../../../models/category";
import {HttpClientTestingModule} from "@angular/common/http/testing";
import {ProductService} from "../../../services/product.service";
import {Product} from "../../../models/product";

describe('ShowProductComponent', () => {
  let component: ShowProductComponent;
  let fixture: ComponentFixture<ShowProductComponent>;

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
});
