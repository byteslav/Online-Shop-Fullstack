import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from "@angular/common/http/testing";

import { CategoryService } from './category.service';
import {Category} from "../models/category";

describe('CategoryService', () => {
  let service: CategoryService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [CategoryService]
    });
    service = TestBed.inject(CategoryService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('Get all categories', () => {
    const mockCategories: Category[] = [{id: 3, name: 'Sport'}];
    service.getCategoriesList().subscribe(categories => {
      expect(categories).toBe(mockCategories);
    });

    const request = httpMock.expectOne('https://localhost:5001/api/Category');
    expect(request.request.method).toEqual('GET');
    request.flush(mockCategories);
  });

  it('Add new category', () => {
    const mockCategory: Category = {id: 123, name: 'Test category'};

    service.addCategory(mockCategory).subscribe(category => {
      expect(category).toEqual(mockCategory);
    });

    const request = httpMock.expectOne('https://localhost:5001/api/Category');
    expect(request.request.method).toEqual('POST');
    expect(request.request.body).toEqual(mockCategory);
  });

  it('Update category', () => {
    const mockCategory: Category = {id: 3, name: 'Groceries'};

    service.updateCategory(mockCategory).subscribe(mockCategory => {
      expect(mockCategory).toEqual(mockCategory);
    });

    const request = httpMock.expectOne('https://localhost:5001/api/Category');
    expect(request.request.method).toEqual('PUT');
    expect(request.request.body).toEqual(mockCategory);
  });

  it('Delete category', () => {
    const mockId: number = 3;

    service.deleteCategory(mockId).subscribe(id => {
      expect(id).toEqual(mockId);
    });

    const request = httpMock.expectOne('https://localhost:5001/api/Category/' + mockId);
    expect(request.request.method).toEqual('DELETE');
  });
});
