import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-data-error-message',
  templateUrl: './data-error-message.component.html',
  styleUrls: ['./data-error-message.component.css']
})
export class DataErrorMessageComponent {
  @Input() message?: string;
}
