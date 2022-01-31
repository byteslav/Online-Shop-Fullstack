import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShowCategoryComponent } from './show-category.component';

describe('ShowCategoryComponent', () => {
  let component: ShowCategoryComponent;
  let fixture: ComponentFixture<ShowCategoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ShowCategoryComponent ]
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
});
