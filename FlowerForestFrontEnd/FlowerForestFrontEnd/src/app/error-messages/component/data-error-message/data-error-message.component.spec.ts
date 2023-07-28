import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DataErrorMessageComponent } from './data-error-message.component';

describe('DataErrorMessageComponent', () => {
  let component: DataErrorMessageComponent;
  let fixture: ComponentFixture<DataErrorMessageComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DataErrorMessageComponent]
    });
    fixture = TestBed.createComponent(DataErrorMessageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
