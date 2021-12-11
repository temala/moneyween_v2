import {MoneyNode} from "./money-node";
import {InputTypes, NodeField} from "./node-field";
import {IComputeVisitor, IDelayedComputeNode, IUserIncome} from "../computeManager/compute-manager";

export class Employee extends MoneyNode implements IUserIncome, IDelayedComputeNode {

  value: number;

  salaryNet: NodeField;
  salaryRaw: NodeField;
  salaryCost: NodeField;
  salaryAnnualNet: NodeField;
  salaryAnnualCost: NodeField;
  salaryAnnualRaw: NodeField;
  nbMonths: NodeField;
  iteration: number;

  constructor(name: string) {
    super(name);
    this.iteration = 0;
    this.backgroundColor = "rgba(255, 102, 102, 0.15)";
    this.icon = "fa fa-user";
    this.primaryColor = "rgb(255, 102, 102)";
    this.secondaryColor = "rgba(255, 102, 102, 0.3)";
    this.initInputs();
  }

  init() {
    this.balance = null;
    this.total = null;
    this.iteration = 0;
  }

  getTotalIncomeAmount() {
    let net = parseFloat(this.salaryNet.value);
    let months = parseFloat(this.nbMonths.value);

    return net * months;
  }

  private initInputs() {
    this.salaryNet = new NodeField("Salaire Net / Mois", InputTypes.Number);
    this.salaryNet.isRequired = true;

    this.nbMonths = new NodeField("Nombre de mois", InputTypes.Number);
    this.nbMonths.isRequired = true;
    this.nbMonths.value = "12";

    this.salaryAnnualNet = new NodeField("Salaire Net / An", InputTypes.Text);
    this.salaryAnnualNet.isComputed = true;

    this.salaryRaw = new NodeField("Salaire Brut / Mois", InputTypes.Text);
    this.salaryRaw.isComputed = true;

    this.salaryCost = new NodeField("Cout du salaire / Mois", InputTypes.Text);
    this.salaryCost.isComputed = true;

    this.salaryAnnualRaw = new NodeField("Brut / An", InputTypes.Text);
    this.salaryAnnualRaw.isComputed = true;

    this.salaryAnnualCost = new NodeField("Cout du salaire / An", InputTypes.Text);
    this.salaryAnnualCost.isComputed = true;

    this.inputs.push(this.salaryNet);
    this.inputs.push(this.nbMonths);

    this.labels.push(this.salaryAnnualNet)

    this.labels.push(this.salaryRaw);
    this.labels.push(this.salaryAnnualRaw);

    this.labels.push(this.salaryCost);
    this.labels.push(this.salaryAnnualCost);
  }

  accept(manager: IComputeVisitor) {
    this.iteration++;
    this.isComputing = true;
    manager.computeEmployee(this);
    this.isComputing = false;
  }
}
