import {Component} from '@angular/core';
import {Employee} from './node-types/employee-node';
import {Company} from './node-types/company-node';
import {Contract} from './node-types/contract-node';
import {State} from "./node-types/state-node";
import {ContractPortage} from "./node-types/contract-portage-node";
import {InvoiceNode} from "./node-types/invoice-node";
import {CompanySharesNode} from "./node-types/company-shares-node";
import {IMoneyNode, IMoneyNodeViewInfo, MoneyNode} from "./node-types/money-node";
import {ComputeManager} from "./computeManager/compute-manager";

@Component({
  selector: 'money-ween',
  templateUrl: './money-ween.component.html'
})
export class MoneyWeenComponent {
  public userIncomes:number;
  public nodes: IMoneyNode[];

  constructor() {
    this.userIncomes=0;
    this.nodes = [];

    let employee = new Employee("Salaire Ridha");
    employee.salaryNet.value = "2000";
    employee.nbMonths.value = "12";
    employee.onNotify.subscribe(() => this.update());

    let pns = new Company("Portage numerique solidaire");
    pns.addChild(employee);

    let portage = new ContractPortage("Contrat de portage PNS");
    portage.inputs[0].value = "193";
    portage.inputs[1].value = "218";
    portage.commission.value = "5";
    portage.addChild(pns);
    portage.onNotify.subscribe(() => this.update());

    let loyee = new InvoiceNode("Loyer");
    loyee.inputs[0].value = "500";
    loyee.inputs[1].value = "12";

    let faraisKm = new InvoiceNode("Frais km");
    faraisKm.inputs[0].value = "400";
    faraisKm.inputs[1].value = "12";
    faraisKm.onNotify.subscribe(() => this.update());

    let dividende = new CompanySharesNode("Dividende");
    dividende.onNotify.subscribe(() => this.update());

    const company = new Company("RT Consulting");
    company.addChild(portage);
    company.addChild(loyee);
    company.addChild(faraisKm);
    company.addChild(dividende);

    let contract = new Contract(".Net consultant at Suez");
    contract.inputs[0].value = "600";
    contract.inputs[1].value = "244";
    contract.addChild(company);
    contract.onNotify.subscribe(() => this.update());

    this.nodes.push(contract);
  }

  update() {
    this.userIncomes=0;
    let computeManager = new ComputeManager();

    this.nodes.forEach(node => {
      computeManager.init(node);
    });

    for (let i = 0; i < 3; i++) {
      this.nodes.forEach(node => {
        computeManager.propagate(node);
      })
    }

    this.nodes.forEach(node => {
      this.userIncomes += computeManager.getUserIncomes(node);
    });

  }

  get childColClass(): number {
    let size = 12 / this.nodes.length;

    return Math.max(1, size);
  }
}

