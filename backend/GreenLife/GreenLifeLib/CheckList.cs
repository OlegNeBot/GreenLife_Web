using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace GreenLifeLib
{
    public class CheckList
    {
        #region [Props]

        public int Id { get; set; }
        public bool ExecutionStatus { get; set; }

        #endregion

        #region [Rels]

        public int TypeId { get; set; }
        public Type Type { get; set; }

        public List<Habit> Habit { get; set; } = new();

        public int AccountId { get; set; }
        public Account Account { get; set; } = null!;

        public int CheckListNameId { get; set; }
        public CheckListName CheckListName { get; set; } = null!;

        #endregion

        #region [Methods]

        /// <summary>
        /// Gets CheckLists by Account id.
        /// </summary>
        /// <param name="id">Account Id.</param>
        /// <returns>List of Account CheckLists.</returns>
        public static List<CheckList> GetCheckLists(int id)
        {
            using (Context db = new())
            {
                var checkLists = db.CheckList.Where(p => p.AccountId == id).ToList();
                return checkLists;
            }
        }

        /// <summary>
        /// Creates checklists when an account registers.
        /// </summary>
        /// <param name="account">Account</param>
        public static void CreateAccountCheckLists(Account account)
        {
            using (Context db = new())
            {
                List<CheckList> checklists = new List<CheckList>();

                //Getting types of checklists and habits
                var types = db.Type.ToList();

                //For each type creates a checklist with it content
                foreach (Type type in types)
                {
                    int typeId = type.Id;

                    List<Habit> habits = db.Habit.Where(p => p.TypeId == typeId).ToList();
                    //For each habit creates its account performance
                    foreach (Habit habit in habits)
                    {
                        HabitPerformance habitPerformance = new(habit.Id, account.Id);
                        account.HabitPerformance.Add(habitPerformance);
                        db.Account.Update(account);
                        db.SaveChanges();
                    }

                    //Adding an info to checklist
                    CheckListName name = db.CheckListName.Where(p => p.Id == typeId).First();
                    CheckList checklist = new CheckList() { TypeId = typeId, Habit = habits, AccountId = account.Id, CheckListNameId = name.Id};
                    checklists.Add(checklist);
                }
                //Then CheckLists are adding into database
                account.CheckList.AddRange(checklists);
                db.Account.Update(account);
                db.SaveChanges();
            }
        }

        #endregion
    }
}
