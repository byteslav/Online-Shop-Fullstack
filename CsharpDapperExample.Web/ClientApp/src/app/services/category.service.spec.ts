import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from "@angular/common/http/testing";

import { CategoryService } from './category.service';
import {HttpClient} from "@angular/common/http";
import {Category} from "../models/category";

describe('CategoryService', () => {
  let service: CategoryService;
  let httpMock: HttpTestingController;
  let httpClient: HttpClient;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [CategoryService]
    });
    service = TestBed.inject(CategoryService);
    httpMock = TestBed.inject(HttpTestingController);
    httpClient = TestBed.inject(HttpClient);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('Get all categories', () => {
    const categories: Category[] = [{id: 3, name: 'Kek'}];

    service.getCategoriesList().subscribe(cat => {
      expect(cat).toEqual(categories);
    });
    const request = httpMock.expectOne('https://localhost:5001/api/Category');
    expect(request.request.method).toEqual('GET');
  });

  it('Add new category', () => {
    const mockCategory: Category = {id: 123, name: 'Test category'};

    service.addCategory(mockCategory).subscribe(cat => {
      expect(cat).toEqual(mockCategory);
    });

    const request = httpMock.expectOne('https://localhost:5001/api/Category');
    expect(request.request.method).toEqual('POST');
    expect(request.request.body).toEqual(mockCategory);
  });

  it('Update category', () => {
    const mockCategory: Category = {id: 3, name: 'hah'};

    service.updateCategory(mockCategory).subscribe(cat => {
      expect(cat).toEqual(mockCategory);
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
