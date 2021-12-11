import { MoneyNode} from "./money-node";
import {IDelayedComputeNode,IComputeVisitor} from "../computeManager/compute-manager";

export class Company extends MoneyNode implements IDelayedComputeNode {

  iteration: number;

  constructor(name: string) {
    super(name);
    this.iteration = 0;
    this.backgroundColor = "rgba(0, 120, 212, 0.15)";
    this.icon = "fa fa-building";
    this.primaryColor = "rgb(0, 120, 212)";
    this.secondaryColor = "rgba(0, 120, 212, 0.3)";
    this.addChild(new CompanyTax());
  }

  init() {
    this.balance=null;
    this.total=null;
    this.iteration = 0;
  }

  accept(manager: IComputeVisitor) {
    this.iteration++;
    manager.computeCompany(this);
  }
}

export class CompanyTax extends MoneyNode implements IDelayedComputeNode{

  iteration: number;

  constructor() {
    super("Impôt sur societe");
    this.iteration = 0;
    this.backgroundColor = "rgba(51, 51, 153, 0.15)";
    this.icon = "fas fa-hand-middle-finger";
    this.primaryColor = "rgb(51, 51, 153)";
    this.secondaryColor = "rgba(51, 51, 153, 0.3)";
  }

  init() {
    this.balance=null;
    this.total=null;
    this.iteration = 0;
  }

  accept(manager: IComputeVisitor) {
    this.iteration++;
    this.isComputing = true;

    manager.computeCompanyTax(this);

    this.isComputing = false;
  }

}
