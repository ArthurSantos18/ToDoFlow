import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserTaskItemsModalComponent } from './user-task-items-modal.component';

describe('UserTaskItemsModalComponent', () => {
  let component: UserTaskItemsModalComponent;
  let fixture: ComponentFixture<UserTaskItemsModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UserTaskItemsModalComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(UserTaskItemsModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
