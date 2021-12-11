import {Component, EventEmitter, Inject, Input, Output} from '@angular/core';
import {NodeField} from "../../app/money-ween/node-types/node-field";

@Component({
  selector: 'money-node-field',
  templateUrl: './money-node-field.component.html',
  styleUrls: []
})
export class MoneyNodeFieldComponent {
  @Input()
  public field: NodeField;

  @Output() onFieldUpdated = new EventEmitter<number>();

  timeout: any = null;
  notifyFieldUpdated() {
    clearTimeout(this.timeout);
    var $this = this;

    this.timeout = setTimeout(function () {
      $this.onFieldUpdated.emit(parseFloat($this.field.value));
    }, 1000);

  }
}
