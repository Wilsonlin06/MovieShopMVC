import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateCastComponent } from './create-cast.component';

describe('CreateCastComponent', () => {
  let component: CreateCastComponent;
  let fixture: ComponentFixture<CreateCastComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreateCastComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateCastComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
