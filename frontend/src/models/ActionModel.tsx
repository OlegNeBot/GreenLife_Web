export interface ActionModel {
  Id: number,
  ActionDate: string,
  Action: {
    ActionName: string,
    ActionType: {
      TypeName: string,
    }
  }
  Account: {
   Name: string
  }
}