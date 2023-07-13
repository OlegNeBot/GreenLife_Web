using Microsoft.EntityFrameworkCore;

namespace GreenLifeLib
{
    /// <summary>
    /// Чек-лист.
    /// </summary>
    public class CheckList
    {
        #region [Props]

        public int Id { get; set; }
        public bool ExecutionStatus { get; set; }

        #endregion

        #region [Rels]

        public int TypeId { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public Type? Type { get; set; }

        public List<Habit> Habit { get; set; } = new();

        [System.Text.Json.Serialization.JsonIgnore]
        public int AccountId { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public Account Account { get; set; } = null!;

        [System.Text.Json.Serialization.JsonIgnore]
        public int CheckListNameId { get; set; }

        public CheckListName? CheckListName { get; set; }

        #endregion

        #region [Methods]
        // TODO: Изменить метод.
        /// <summary>
        /// Creates checklists when an account registers.
        /// </summary>
        /// <param name="account">Account</param>
        public static async void CreateAccountCheckLists(Account account)
        {
            using (var db = new Context())
            {
                var checklists = new List<CheckList>();

                //Getting types of checklists and habits
                var types = await db.Type.ToListAsync();

                //For each type creates a checklist with it content
                foreach (var type in types)
                {
                    var typeId = type.Id;

                    var habits = await db.Habit
                                            .Where(p => p.TypeId == typeId)
                                            .ToListAsync();

                    //For each habit creates its account performance
                    foreach (var habit in habits)
                    {
                        var habitPerformance = new HabitPerformance(habit.Id, account.Id);

                        account.HabitPerformance.Add(habitPerformance);

                        //db.Account.Update(account);
                    }

                    //Adding an info to checklist
                    var name = await db.CheckListName
                                                .Where(p => p.Id == typeId)
                                                .FirstAsync();
                    var checklist = new CheckList()
                    {
                        TypeId = typeId,
                        Habit = habits,
                        AccountId = account.Id,
                        CheckListNameId = name.Id
                    };

                    checklists.Add(checklist);
                }

                //Then CheckLists are adding into database
                account.CheckList.AddRange(checklists);
                db.Account.Update(account);

                await db.SaveChangesAsync();
            }
        }

        #endregion
    }
}
