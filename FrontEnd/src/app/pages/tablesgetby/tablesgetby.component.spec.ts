import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TablesgetbyComponent } from './tablesgetby.component';

describe('TablesgetbyComponent', () => {
  let component: TablesgetbyComponent;
  let fixture: ComponentFixture<TablesgetbyComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TablesgetbyComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TablesgetbyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
