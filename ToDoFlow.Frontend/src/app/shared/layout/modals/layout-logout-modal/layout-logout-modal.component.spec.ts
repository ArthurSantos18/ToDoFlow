import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LayoutLogoutModalComponent } from './layout-logout-modal.component';

describe('LayoutLogoutModalComponent', () => {
  let component: LayoutLogoutModalComponent;
  let fixture: ComponentFixture<LayoutLogoutModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LayoutLogoutModalComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(LayoutLogoutModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
