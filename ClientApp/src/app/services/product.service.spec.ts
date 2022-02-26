import { TestBed } from '@angular/core/testing';

import { ProductService } from './product.service';
import {HttpClientTestingModule, HttpTestingController} from "@angular/common/http/testing";
import {Product} from "../models/product";

describe('SharedService', () => {
  let service: ProductService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [ProductService]
    });
    service = TestBed.inject(ProductService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  it('Should be created', () => {
    expect(service).toBeTruthy();
  });

  it('Get all products', () => {
    const mockProducts: Product[] = [{id: 3, name: 'testing product', price: 100, description: 'desc', categoryId: 1, category: { id: 1, name: 'cat'}}];
    service.getProductsList().subscribe(products => {
      expect(products).toBe(mockProducts);
    });

    const request = httpMock.expectOne('https://localhost:5001/api/Product');
    expect(request.request.method).toEqual('GET');
    request.flush(mockProducts);
  });

  it('Add new product', () => {
    const mockProduct: Product = {id: 3, name: 'added product', price: 100, description: 'desc', categoryId: 1, category: { id: 1, name: 'category'}};

    service.addProduct(mockProduct).subscribe(products => {
      expect(products).toEqual(mockProduct);
    });

    const request = httpMock.expectOne('https://localhost:5001/api/Product');
    expect(request.request.method).toEqual('POST');
    expect(request.request.body).toEqual(mockProduct);
  });

  it('Update new product', () => {
    const mockProduct: Product = {id: 3, name: 'updated product', price: 100, description: 'desc', categoryId: 1, category: { id: 1, name: 'category'}};

    service.updateProduct(mockProduct).subscribe(products => {
      expect(products).toEqual(mockProduct);
    });

    const request = httpMock.expectOne('https://localhost:5001/api/Product');
    expect(request.request.method).toEqual('PUT');
    expect(request.request.body).toEqual(mockProduct);
  });

  it('Delete product', () => {
    const mockId: number = 3;

    service.deleteProduct(mockId).subscribe(id => {
      expect(id).toEqual(mockId);
    });

    const request = httpMock.expectOne('https://localhost:5001/api/Product/' + mockId);
    expect(request.request.method).toEqual('DELETE');
  });
});
