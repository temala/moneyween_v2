export class NodeField {
  public name: string;
  public type: InputTypes;
  public isRequired: boolean;
  public isComputed: boolean;
  public value: string;

  constructor(name:string,type=InputTypes.Text) {
    this.name = name;
    this.type = type;
  }
}

export enum InputTypes {
  Text = "text",
  Number = "number"
}
