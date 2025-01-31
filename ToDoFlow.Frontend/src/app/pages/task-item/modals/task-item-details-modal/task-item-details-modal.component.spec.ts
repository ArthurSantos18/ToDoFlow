import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TaskItemDetailsModalComponent } from './task-item-details-modal.component';

describe('TaskItemDetailsModalComponent', () => {
  let component: TaskItemDetailsModalComponent;
  let fixture: ComponentFixture<TaskItemDetailsModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TaskItemDetailsModalComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TaskItemDetailsModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
