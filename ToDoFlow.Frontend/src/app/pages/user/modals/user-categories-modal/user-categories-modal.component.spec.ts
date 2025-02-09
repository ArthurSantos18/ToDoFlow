import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserCategoriesModalComponent } from './user-categories-modal.component';

describe('UserCategoriesModalComponent', () => {
  let component: UserCategoriesModalComponent;
  let fixture: ComponentFixture<UserCategoriesModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UserCategoriesModalComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(UserCategoriesModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
