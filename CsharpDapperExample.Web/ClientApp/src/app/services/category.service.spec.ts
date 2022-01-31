import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from "@angular/common/http/testing";

import { CategoryService } from './category.service';
import {HttpClient} from "@angular/common/http";
import {Category} from "../models/category";
import {of} from "rxjs";

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

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('Get all categories', () => {
    const mockCategories: Category[] = [{id: 3, name: 'Kek'}];
    spyOn(httpClient, "get").and.returnValue(of(mockCategories));

    service.getCategoriesList().subscribe(categories => {
      expect(categories).toEqual(mockCategories);
    });
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
    const mockCategory: Category = {id: 3, name: 'hah'};

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
