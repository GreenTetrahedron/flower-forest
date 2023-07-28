import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CatalogueDashboardComponent } from './catalogue-dashboard.component';

describe('CatalogueDashboardComponent', () => {
  let component: CatalogueDashboardComponent;
  let fixture: ComponentFixture<CatalogueDashboardComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CatalogueDashboardComponent]
    });
    fixture = TestBed.createComponent(CatalogueDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
