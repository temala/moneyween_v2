import {MoneyNode} from "./money-node";
import {InputTypes, NodeField} from "./node-field";
import {IComputeVisitor, IUserIncome} from "../computeManager/compute-manager";

export class InvoiceNode extends MoneyNode implements IUserIncome {
  value: number;

  constructor(name: string) {
    super(name);

    this.backgroundColor = "rgba(153, 51, 255, 0.15)";
    this.icon = "fa fa-edit";
    this.primaryColor = "rgb(153, 51, 255)";
    this.secondaryColor = "rgba(153, 51, 255, 0.3)";

    this.initInputs();
  }

  getTotalIncomeAmount() {
    return this.balance;
  }

  private initInputs() {
    let value = new NodeField("Valeur", InputTypes.Number);
    value.isRequired = true;

    let recurrence = new NodeField("Recurrence", InputTypes.Number);
    recurrence.isRequired = true;
    recurrence.value = "1";

    this.inputs.push(value);
    this.inputs.push(recurrence);
  }

  accept(manager: IComputeVisitor) {
    this.isComputing = true;
    manager.computeInvoice(this);
    this.isComputing = false;
  }
}
