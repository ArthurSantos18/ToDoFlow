import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TaskItemCreateComponent } from './task-item-create.component';

describe('TaskItemCreateComponent', () => {
  let component: TaskItemCreateComponent;
  let fixture: ComponentFixture<TaskItemCreateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TaskItemCreateComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TaskItemCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
