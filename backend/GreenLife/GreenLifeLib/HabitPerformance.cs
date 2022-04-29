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
        public Habit Habit { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        #endregion

        #region [Methods]

        public static void NewExecution(int habitId, int userId)
        {
            using (Context db = new())
            {
                var habit = db.Habit.Where(p => p.Id == habitId).First();
                var habitPerf = db.HabitPerformance
                        .Where(p => p.HabitId == habit.Id)
                        .Where(p => p.UserId == userId)
                        .First();

                if (!habitPerf.Executed)
                {
                    habitPerf.NumOfExecs++;
                    habitPerf.DateOfExec = DateTime.Now;
                    if (habitPerf.NumOfExecs == habit.NumsNeeded)
                    {
                        habitPerf.Executed = true;
                        try
                        {
                            var account = db.Account.Where(p => p.UserId == userId).First();
                            account.ScoreSum += habit.Score;
                            db.Account.Update(account);
                        }
                        catch (InvalidOperationException)
                        { }
                    }
                    db.HabitPerformance.Update(habitPerf);
                    db.SaveChanges();
                }
            }
        }

        public static HabitPerformance GetExecution(int habitId, int userId)
        {
            using (Context db = new())
            {
                var _exec = db.HabitPerformance
                    .Where(p => p.Habit.Id == habitId)
                    .Where(p => p.User.Id == userId)
                    .First();
                return _exec;
            }
        }

        #endregion
    }
}

