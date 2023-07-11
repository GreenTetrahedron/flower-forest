import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AuthenticateUserComponent } from './authenticate-user.component';

describe('AuthenticateUserComponent', () => {
  let component: AuthenticateUserComponent;
  let fixture: ComponentFixture<AuthenticateUserComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AuthenticateUserComponent]
    });
    fixture = TestBed.createComponent(AuthenticateUserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
