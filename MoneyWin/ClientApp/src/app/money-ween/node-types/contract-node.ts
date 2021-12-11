import {MoneyNode} from "./money-node";
import {InputTypes, NodeField} from "./node-field";
import { IComputeVisitor} from "../computeManager/compute-manager";

export class Contract extends MoneyNode
{
  value:number;

  constructor(name: string) {
    super(name);

    this.backgroundColor = "rgba(0, 204, 153, 0.15)";
    this.icon = "fa fa-edit";
    this.primaryColor = "rgb(0, 204, 153)";
    this.secondaryColor = "rgba(0, 204, 153, 0.3)";

    this.initInputs();
  }

  private initInputs() {
    let tjm = new NodeField("TJM", InputTypes.Number);
    tjm.isRequired = true;

    let nbWorkedDays = new NodeField("Nb jours travaillé", InputTypes.Number);
    nbWorkedDays.isRequired = true;

    this.inputs.push(tjm);
    this.inputs.push(nbWorkedDays);
  }

  accept(manager: IComputeVisitor) {
    this.isComputing = true;
    manager.computeContract(this);
    this.isComputing = false;

  }
}
