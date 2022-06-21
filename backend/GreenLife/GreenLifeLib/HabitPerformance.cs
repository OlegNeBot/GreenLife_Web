using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

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

        public int HabitId { get; set; }
        public Habit Habit { get; set; } = null!;

        public int AccountId { get; set; }
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
        public static async void NewExecution(int habitId, int accountId)
        {
            await using (Context db = new())
            {
                // Getting a habit by id and its performance by account id
                var habit = db.Habit.Where(p => p.Id == habitId).First();
                var habitPerf = db.HabitPerformance
                        .Where(p => p.HabitId == habit.Id)
                        .Where(p => p.AccountId == accountId)
                        .First();

                // If habit isn't executed - add an execution
                if (!habitPerf.Executed)
                {
                    habitPerf.NumOfExecs++;
                    habitPerf.DateOfExec = DateTime.UtcNow;

                    Account account = db.Account.Where(p => p.Id == accountId).First();
                    AccountAction.NewAction(account, 1);

                    // If habit executed needed number of times - change its status
                    // to "executed" and add to user's score habit's score
                    if (habitPerf.NumOfExecs == habit.Total)
                    {
                        habitPerf.Executed = true;
                        try
                        {
                            AccountAction.NewAction(account, 5);
                            account.ScoreSum += habit.Score;
                            db.Account.Update(account);
                        }
                        catch (InvalidOperationException)
                        { }
                    }
                    db.HabitPerformance.Update(habitPerf);
                    db.SaveChanges();

                    //Checking if checklist is completed
                    //TODO: Check this
                    List<Habit> habits = db.Habit.Where(p => p.CheckList == habit.CheckList).ToList();
                    bool all = true;
                    foreach (Habit h in habits)
                    {
                        HabitPerformance habitPerformance = db.HabitPerformance.Where(p => p.HabitId == h.Id).First();
                        if (!habitPerformance.Executed)
                        {
                            all = false;
                        }
                    }
                    if (all)
                    {
                        AccountAction.NewAction(account, 6);
                    }
                }
            }
        }

        #endregion
    }
}
