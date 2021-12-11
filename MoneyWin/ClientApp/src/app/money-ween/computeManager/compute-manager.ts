import {IMoneyNode} from "../node-types/money-node";
import {Company, CompanyTax} from "../node-types/company-node";
import {CompanySharesNode} from "../node-types/company-shares-node";
import {State} from "../node-types/state-node";
import {Contract} from "../node-types/contract-node";
import {ContractPortage} from "../node-types/contract-portage-node";
import {Employee} from "../node-types/employee-node";
import {InvoiceNode} from "../node-types/invoice-node";
import Engine, {formatValue} from "publicodes";
import rules from "modele-social";

export interface IDelayedComputeNode {
  iteration: number;

  init();
}

export interface IUserIncome {

  getTotalIncomeAmount(): number;
}

export interface IComputeVisitor {

  computeCompany(node: Company): void;

  computeCompanyShares(node: CompanySharesNode): void;

  computeState(node: State): void;

  computeContract(node: Contract): void;

  computeContractPortage(node: ContractPortage): void;

  computeEmployee(node: Employee): void;

  computeInvoice(node: InvoiceNode);

  computeCompanyTax(node: CompanyTax);
}

export class ComputeManager implements IComputeVisitor {

  private static isDelayedComputeNode(node: object): node is IDelayedComputeNode {
    return (<IDelayedComputeNode>node).init !== undefined;
  }

  private static isUserIncome(node: object): node is IUserIncome {
    return (<IUserIncome>node).getTotalIncomeAmount !== undefined;
  }

  public init(node: IMoneyNode) {
    if (ComputeManager.isDelayedComputeNode(node)) {
      (<IDelayedComputeNode>node).init();
    }

    node.outcomes.forEach(outcome => {
      this.init(outcome);
    });
  }

  public propagate(node: IMoneyNode) {
    node.accept(this);

    node.outcomes.forEach(outcome => {
      this.propagate(outcome);
    });
  }

  public getUserIncomes(node: IMoneyNode): number {
    let result = 0;
    if (ComputeManager.isUserIncome(node)) {
      result += (<IUserIncome>node).getTotalIncomeAmount();
    }

    node.outcomes.forEach(outcome => {
      result += this.getUserIncomes(outcome);
    });

    return result;
  }

  computeCompany(node: Company) {

    if (node.iteration > 0) {
      let incomeValues = node.incomes.filter(n => n.balance);
      let outcomesValues = node.outcomes.filter(n => n.balance);

      let incomeTotal = incomeValues.length > 0 ? incomeValues.map(n => n.balance).reduce((a, b) => a + b) : 0;
      let outcomeTotal = outcomesValues.length > 0 ? outcomesValues.map(n => n.balance).reduce((a, b) => a + b) : 0;

      let situation = {
        "entreprise . chiffre d'affaires": incomeTotal - outcomeTotal + "€/an",
        "entreprise . imposition . IS": "oui",
      };
      let engine = new Engine(rules);
      let net = <number>engine.setSituation(situation).evaluate("entreprise . imposition . IS . résultat net").nodeValue;
      node.balance = net;
      node.total = net.toLocaleString('fr-FR', {style: 'currency', currency: 'EUR'});
    }
  }

  computeCompanyTax(node: CompanyTax) {
    if (node.iteration == 2) {
      let CA = node.incomes.map(n => n.balance).reduce((a, b) => a + b);
      let situation = {
        "entreprise . chiffre d'affaires": CA + "€/an",
        "entreprise . imposition . IS": "oui",
      };

      let engine = new Engine(rules);
      let net = engine.setSituation(situation).evaluate("entreprise . imposition . IS . impôt sur les sociétés").nodeValue;
      node.total = net.toLocaleString('fr-FR', {style: 'currency', currency: 'EUR'});
    }
  }

  computeCompanyShares(node: CompanySharesNode) {

    if (node.iteration == 3) {
      let CA = node.incomes.map(n => n.balance).reduce((a, b) => a + b);

      if (CA) {

        node.balance = CA * node.inputs.map(i => parseFloat(i.value) / 100).reduce((a, b) => a * b);

        node.total = node.balance.toLocaleString('fr-FR', {style: 'currency', currency: 'EUR'});

        let shares = node.balance - node.balance * 0.3;
        node.shares.value = shares.toLocaleString('fr-FR', {style: 'currency', currency: 'EUR'});
      }
    }
  }

  computeState(node: State) {
    node.balance = node.inputs.map(i => parseFloat(i.value)).reduce((a, b) => a * b)
    node.total = node.balance.toLocaleString('fr-FR', {style: 'currency', currency: 'EUR'});
  }


  public computeContract(node: Contract) {
    node.balance = node.inputs.map(i => parseFloat(i.value)).reduce((a, b) => a * b)
    node.total = node.balance.toLocaleString('fr-FR', {style: 'currency', currency: 'EUR'});
  }

  public computeContractPortage(node: ContractPortage) {

    let commission = parseFloat(node.commission.value);

    let value = node.inputs.filter(i => i != node.commission).map(i => parseFloat(i.value)).reduce((a, b) => a * b);

    node.balance = value + value * commission / 100;

    node.total = node.balance.toLocaleString('fr-FR', {style: 'currency', currency: 'EUR'});
  }

  public computeEmployee(node: Employee) {
    if (node.iteration == 1) {
      const net = parseFloat(node.salaryNet.value);
      const nbMonths = parseFloat(node.nbMonths.value);
      let situation = {
        "contrat salarié . rémunération . net": net + "€/mois",
        "contrat salarié . statut cadre": "oui",
        "entreprise . effectif": 1,
      };
      let engine = new Engine(rules);
      const raw = <number>engine.setSituation(situation).evaluate("contrat salarié . rémunération . brut de base").nodeValue;
      const annualRaw = raw * nbMonths;
      const cost = <number>engine.setSituation(situation).evaluate("contrat salarié . prix du travail").nodeValue;
      const annualCost = cost * nbMonths;
      const annualNet = net * nbMonths;

      if (net && nbMonths) {
        node.balance = annualCost;
        node.total = node.balance.toLocaleString('fr-FR', {style: 'currency', currency: 'EUR'});

        node.salaryAnnualNet.value = annualNet.toLocaleString('fr-FR', {style: 'currency', currency: 'EUR'});
        node.salaryRaw.value = raw.toLocaleString('fr-FR', {style: 'currency', currency: 'EUR'});
        node.salaryAnnualRaw.value = annualRaw.toLocaleString('fr-FR', {style: 'currency', currency: 'EUR'});

        node.salaryCost.value = cost.toLocaleString('fr-FR', {style: 'currency', currency: 'EUR'});
        node.salaryAnnualCost.value = annualCost.toLocaleString('fr-FR', {style: 'currency', currency: 'EUR'});
      }
    }
  }

  public computeInvoice(node: InvoiceNode) {
    node.balance = node.inputs.map(i => parseFloat(i.value)).reduce((a, b) => a * b)
    node.total = node.balance.toLocaleString('fr-FR', {style: 'currency', currency: 'EUR'});
  }
}
