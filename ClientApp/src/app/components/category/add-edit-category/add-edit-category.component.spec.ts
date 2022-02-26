import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddEditCategoryComponent } from './add-edit-category.component';
import {HttpClientTestingModule} from "@angular/common/http/testing";
import {CategoryService} from "../../../services/category.service";
import {Category} from "../../../models/category";
import {of} from "rxjs";

describe('AddEditCategoryComponent', () => {
  let component: AddEditCategoryComponent;
  let fixture: ComponentFixture<AddEditCategoryComponent>;
  let categoryService: CategoryService;

  let mockCategory: Category = { id: 2, name: 'Groceries' };

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ HttpClientTestingModule ],
      declarations: [ AddEditCategoryComponent ],
      providers: [CategoryService]
    })
    .compileComponents();

    categoryService = TestBed.inject(CategoryService);
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddEditCategoryComponent);
    component = fixture.componentInstance;
    component.category = mockCategory;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should add category', () => {
    spyOn(categoryService, 'addCategory').and.returnValue(of(mockCategory));
    component.addCategory();

    expect(component.category).toBe(mockCategory);
  });

  it('should update category', () => {
    spyOn(categoryService, 'updateCategory').and.returnValue(of(mockCategory));
    component.updateCategory();

    expect(component.category).toBe(mockCategory);
  });
});
