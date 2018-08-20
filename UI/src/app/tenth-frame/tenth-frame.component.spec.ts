import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TenthFrameComponent } from './tenth-frame.component';

describe('TenthFrameComponent', () => {
  let component: TenthFrameComponent;
  let fixture: ComponentFixture<TenthFrameComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TenthFrameComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TenthFrameComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
