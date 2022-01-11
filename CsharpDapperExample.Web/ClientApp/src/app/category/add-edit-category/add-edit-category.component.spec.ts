import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddEditCategoryComponent } from './add-edit-category.component';

describe('AddEditCategoryComponent', () => {
  let component: AddEditCategoryComponent;
  let fixture: ComponentFixture<AddEditCategoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddEditCategoryComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddEditCategoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
