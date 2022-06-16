export const Paths : any = {
    Landing: { path: '/' },
    SignIn: { path: '/auth/signin' },
    SignUp: { path: '/auth/signup' },
    ForgotPassword: { path: '/auth/forgotpassword' },
    NotFound: {path: '*'},
    HomePage: {path: '/home'},
    Account: { path: 'account' },
    CheckListHabits: { path: ':id' },
    CheckLists: { path: 'checklists' },
    HabitCatalog: { path: 'habits' },
    HistoryAndAchievements: { path: 'history' },
    Memos: { path: 'memos' },
    Settings: { path: 'settings' }
}