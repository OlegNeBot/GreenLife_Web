using Microsoft.EntityFrameworkCore;

namespace GreenLifeLib
{
    public class HabitPerformance
    {
        #region [Props]

        public int Id { get; set; }
        public int NumOfExecs { get; set; }
        public DateTime DateOfExec { get; set; }
        public bool Executed { get; set; }

        #endregion

        #region [Rels]

        [System.Text.Json.Serialization.JsonIgnore]
        public int HabitId { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public Habit Habit { get; set; } = null!;

        [System.Text.Json.Serialization.JsonIgnore]
        public int AccountId { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public Account Account { get; set; } = null!;

        #endregion

        #region [Constructors]

        public HabitPerformance(int habitId, int accountId)
        {
            HabitId = habitId;
            AccountId = accountId;
            DateOfExec = DateTime.UtcNow;
        }

        public HabitPerformance() { }

        #endregion

        #region [Methods]

        /// <summary>
        /// Performs a new execution of habit. If it was the final execution - adds
        /// to user's score habit's score.
        /// </summary>
        /// <param name="habitId">Habit id</param>
        /// <param name="accountId">Account id</param>
        public async void NewExecution()
        {
            using (Context db = new())
            {
                // If habit isn't executed - add an execution
                if (!Executed)
                {
                    NumOfExecs++;
                    DateOfExec = DateTime.UtcNow;

                    Account account = await db.Account.Where(p => p.Id == AccountId).FirstAsync();
                    AccountAction.NewAction(account.Id, 1);

                    // If habit executed needed number of times - change its status
                    // to "executed" and add to user's score habit's score
                    if (NumOfExecs == Habit.Total)
                    {
                        Executed = true;
                        try
                        {
                            if (account.ScoreSum == 0)
                            {
                                AccountAction.NewAction(account.Id, 5);
                            }
                            account.ScoreSum += Habit.Score;
                            db.Account.Update(account);
                        }
                        catch (InvalidOperationException)
                        { }
                    }
                    db.HabitPerformance.Update(this);
                    await db.SaveChangesAsync();

                    //Checking if checklist is completed
                    List<CheckList> checklists = await db.CheckList.Where(p => p.AccountId == AccountId)
                                                             .Include(d => d.Habit)
                                                             .ToListAsync();
                    bool anyCompleted = false;
                    foreach (CheckList cl in checklists)
                    {
                        if (cl.ExecutionStatus)
                        {
                            anyCompleted = true;
                            break;
                        }
                    }

                    CheckList checklist = checklists.Where(p => p.TypeId == Habit.TypeId).First();
                    bool allFormed = true;

                    foreach (Habit h in checklist.Habit)
                    {
                        HabitPerformance habitPerformance = await db.HabitPerformance.Where(p => p.HabitId == h.Id)
                                                                                     .Where(p => p.AccountId == AccountId)
                                                                                     .FirstAsync();
                        if (!habitPerformance.Executed)
                        {
                            allFormed = false;
                            break;
                        }
                    }
                    if (allFormed)
                    {
                        if (!anyCompleted)
                        {
                            AccountAction.NewAction(account.Id, 6);
                        }
                        checklist.ExecutionStatus = true;
                        db.CheckList.Update(checklist);
                        await db.SaveChangesAsync();
                    }
                }
            }
        }

        #endregion
    }
}
