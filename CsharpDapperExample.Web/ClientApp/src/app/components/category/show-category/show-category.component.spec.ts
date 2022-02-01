import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShowCategoryComponent } from './show-category.component';
import {CategoryService} from "../../../services/category.service";
import {HttpClientTestingModule} from "@angular/common/http/testing";
import {Category} from "../../../models/category";

describe('ShowCategoryComponent', () => {
  let component: ShowCategoryComponent;
  let fixture: ComponentFixture<ShowCategoryComponent>;

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
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should open Add modal window', () => {
    expect(component.ModalTitle).toBe('');
    expect(component.ActivateAddEditCategory).toBe(false);

    component.addClick();

    expect(component.ModalTitle).toBe('Add category');
    expect(component.ActivateAddEditCategory).toBe(true);
  });

  it('should open Edit modal window', () => {
    const mockCategory: Category = { id: 1, name: 'Food' };
    expect(component.ModalTitle).toBe('');
    expect(component.ActivateAddEditCategory).toBe(false);

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
});
