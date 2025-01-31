import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TaskITemDeleteModalComponent } from './task-item-delete-modal.component';

describe('TaskTemDeleteModalComponent', () => {
  let component: TaskITemDeleteModalComponent;
  let fixture: ComponentFixture<TaskITemDeleteModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TaskITemDeleteModalComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TaskITemDeleteModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
