import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IncomeOutcome } from './income-outcome';

describe('IncomeOutcome', () => {
  let component: IncomeOutcome;
  let fixture: ComponentFixture<IncomeOutcome>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [IncomeOutcome]
    })
    .compileComponents();

    fixture = TestBed.createComponent(IncomeOutcome);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
