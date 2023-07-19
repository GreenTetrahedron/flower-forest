import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CatalogueListComponent } from './catalogue-list.component';

describe('CatalogueListComponent', () => {
  let component: CatalogueListComponent;
  let fixture: ComponentFixture<CatalogueListComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CatalogueListComponent]
    });
    fixture = TestBed.createComponent(CatalogueListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
