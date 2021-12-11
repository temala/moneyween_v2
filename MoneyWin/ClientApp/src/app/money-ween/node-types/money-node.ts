import {NodeField} from "./node-field";
import {IComputeVisitor} from "../computeManager/compute-manager";
import {EventEmitter} from "@angular/core";

export interface IMoneyNodeViewInfo
{
  name: string;
  primaryColor: string;
  secondaryColor: string;
  backgroundColor: string;
  icon: string;
  total:string;
}

export interface IMoneyNode extends IMoneyNodeViewInfo{
  balance: number;
  onNotify :EventEmitter<number>;
  incomes: IMoneyNode[];
  outcomes: IMoneyNode[];
  inputs: NodeField[];
  labels: NodeField[];
  isComputing: boolean;

  notify();
  accept(manager:IComputeVisitor);

  addChild(outcome:IMoneyNode);
}

export abstract class MoneyNode implements IMoneyNode{
  balance: number;
  onNotify :EventEmitter<number>;
  total: string;
  name: string;
  primaryColor: string;
  secondaryColor: string;
  backgroundColor: string;
  icon: string;
  incomes: IMoneyNode[];
  outcomes: IMoneyNode[];
  inputs: NodeField[];
  labels: NodeField[];

  isComputing: boolean;

  constructor(name: string) {
    this.incomes = [];
    this.outcomes = [];
    this.inputs = [];
    this.labels = [];
    this.name = name;
    this.onNotify = new EventEmitter<number>();

    this.isComputing = false;
  }

  public abstract accept( manager:IComputeVisitor);

  notify(){
    this.onNotify.emit();
  }

  addChild(outcome:IMoneyNode){
    outcome.incomes.push(this);
    this.outcomes.push(outcome);
  }
}


