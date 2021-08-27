import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PurhcasesComponent } from './purhcases.component';

describe('PurhcasesComponent', () => {
  let component: PurhcasesComponent;
  let fixture: ComponentFixture<PurhcasesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PurhcasesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PurhcasesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
