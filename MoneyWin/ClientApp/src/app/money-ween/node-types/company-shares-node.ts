import {MoneyNode} from "./money-node";
import {InputTypes, NodeField} from "./node-field";
import {IComputeVisitor, IDelayedComputeNode, IUserIncome} from "../computeManager/compute-manager";

export class CompanySharesNode extends MoneyNode implements IDelayedComputeNode, IUserIncome {
  iteration: number;
  shares: NodeField;

  constructor(name: string) {
    super(name);
    this.iteration = 0;
    this.backgroundColor = "rgba(204, 0, 153, 0.15)";
    this.icon = "fa fa-edit";
    this.primaryColor = "rgb(204, 0, 153)";
    this.secondaryColor = "rgba(204, 0, 153, 0.3)";

    this.initInputs();
  }

  init() {
    this.balance = null;
    this.total = null;
    this.iteration = 0;
  }

  private initInputs() {
    this.shares = new NodeField("Valeur net impot", InputTypes.Text);
    this.shares.isComputed = true;

    let value = new NodeField("Dividende %", InputTypes.Number);
    value.isRequired = true;
    value.value = "100";

    this.inputs.push(value);
    this.labels.push(this.shares);
  }

  accept(manager: IComputeVisitor) {
    this.iteration++;
    this.isComputing = true;
    manager.computeCompanyShares(this);
    this.isComputing = false;

  }

  getTotalIncomeAmount() {
    return this.balance - this.balance * 0.33;
  }
}
