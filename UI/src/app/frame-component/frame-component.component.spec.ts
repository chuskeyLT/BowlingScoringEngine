import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FrameComponentComponent } from './frame-component.component';

describe('FrameComponentComponent', () => {
  let component: FrameComponentComponent;
  let fixture: ComponentFixture<FrameComponentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FrameComponentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FrameComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
