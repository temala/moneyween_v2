import {MoneyNode} from "./money-node";
import {InputTypes, NodeField} from "./node-field";
import { IComputeVisitor} from "../computeManager/compute-manager";

export class ContractPortage extends MoneyNode
{
  value:number;

  commission:NodeField;

  constructor(name: string) {
    super(name);

    this.backgroundColor = "rgba(51, 102, 0, 0.15)";
    this.icon = "fa fa-edit";
    this.primaryColor = "rgb(51, 102, 0)";
    this.secondaryColor = "rgba(51, 102, 0, 0.3)";

    this.initInputs();
  }

  private initInputs() {
    let tjm = new NodeField("TJM", InputTypes.Number);
    tjm.isRequired = true;

    let nbWorkedDays = new NodeField("Nb jours travaillé", InputTypes.Number);
    nbWorkedDays.isRequired = true;

    this.commission = new NodeField("Commission %", InputTypes.Number);
    this.commission.isRequired = true;

    this.inputs.push(tjm);
    this.inputs.push(nbWorkedDays);
    this.inputs.push(this.commission);
  }

  accept(manager: IComputeVisitor) {
    this.isComputing = true;

    manager.computeContractPortage(this);
    this.isComputing = false;

  }
}
