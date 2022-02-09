import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShowCategoryComponent } from './show-category.component';
import {CategoryService} from "../../../services/category.service";
import {HttpClientTestingModule} from "@angular/common/http/testing";
import {Category} from "../../../models/category";
import {of} from "rxjs";

describe('ShowCategoryComponent', () => {
  let component: ShowCategoryComponent;
  let fixture: ComponentFixture<ShowCategoryComponent>;
  let categoryService: CategoryService;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      declarations: [ ShowCategoryComponent ],
      providers: [CategoryService]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ShowCategoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();

    categoryService = TestBed.inject(CategoryService);
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should open Add modal window', () => {
    component.addClick();

    expect(component.ModalTitle).toBe('Add category');
    expect(component.ActivateAddEditCategory).toBe(true);
  });

  it('should open Edit modal window', () => {
    const mockCategory: Category = { id: 1, name: 'Food' };

    component.editClick(mockCategory);

    expect(component.ModalTitle).toBe('Edit category');
    expect(component.ActivateAddEditCategory).toBe(true);
    expect(component.category).toBe(mockCategory);
  });

  it('should close modal window', () => {
    component.ActivateAddEditCategory = true;
    component.closeClick();
    expect(component.ActivateAddEditCategory).toBe(false);
  });

  it('should delete category', () => {
    const mockCategory: Category = { id: 3, name: 'Sport' };
    const mockCategories: Category[] = [{ id: 1, name: 'Food' }, {id: 3, name: 'Sport'}];
    spyOn(window, 'confirm').and.callFake(function () {
      return true;
    });
    spyOn(categoryService, 'getCategoriesList').and.returnValue(of(mockCategories));
    spyOn(categoryService, 'deleteCategory').and.callFake(function (id: number) {
      mockCategories.pop();
      return of(mockCategories);
    });

    component.refreshCategoriesList();
    component.deleteClick(mockCategory);

    expect(component.categoriesList).toBe(mockCategories);
  });
});
