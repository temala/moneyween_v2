import {Component, Inject, Input} from '@angular/core';
import {IMoneyNode, MoneyNode} from "../../app/money-ween/node-types/money-node";
import {ComputeManager, IComputeVisitor} from "../../app/money-ween/computeManager/compute-manager";

@Component({
  selector: 'money-node',
  templateUrl: './money-node.component.html',
  styleUrls: ['./money-node.component.css', "../../../node_modules/font-awesome/css/font-awesome.css"]
})
export class MoneyNodeComponent {

  @Input()
  public node: IMoneyNode;

  public showContent: boolean;

  public toggleContent() {
    this.showContent = this.node.inputs.length > 0 && !this.showContent;
  }

  get hasChilds(): boolean {
    return this.node.outcomes.length > 0;
  }

  get childColClass(): number {
    let size = 12 / this.node.outcomes.length;

    return Math.max(1, Math.trunc(size));
  }
}

