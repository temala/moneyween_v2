import { MoneyNode} from "./money-node";
import {IComputeVisitor} from "../computeManager/compute-manager";

export class State extends MoneyNode {


  constructor(name: string) {
    super(name);
    this.backgroundColor = "rgba(0, 51, 102, 0.15)";
    this.icon = "fa fa-flag-usa";
    this.primaryColor = "rgb(0, 51, 102)";
    this.secondaryColor = "rgba(0, 51, 102, 0.3)";
  }

  accept(manager: IComputeVisitor) {
    this.isComputing = true;
    manager.computeState(this);
    this.isComputing = false;
  }
}
