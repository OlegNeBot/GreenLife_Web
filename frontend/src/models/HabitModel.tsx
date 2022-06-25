export interface HabitModel {
  Id: number,
  HabitName: string,
  Total: number,
  HabitPhrase?: {
    PhraseText: string
  },
  HabitPerformance: [
    {
      NumOfExecs: number,
      Executed: boolean
    }
  ]
}