using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using GreenLifeLib;
using Microsoft.EntityFrameworkCore;

namespace GLFinder
{
    /// <summary>
    /// Логика взаимодействия для FinderWindow.xaml
    /// </summary>
    public partial class FinderWindow : Window
    {
        #region [Fields]

        Account? _account = null;
        Question? _question = null;
        List<Account> accounts;
        List<Question> questions;

        #endregion

        #region [Constructors]

        public FinderWindow(Question question)
        {
            InitializeComponent();
            _question = question;
            SearchTitle.Content = "Введите текст вопроса";
        }

        public FinderWindow(Account account)
        {
            InitializeComponent();
            _account = account;
            SearchTitle.Content = "Введите почту аккаунта";
        }

        #endregion

        #region [WindowEvents]

        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            ResultGrid.Items.Clear();
            ErrorMsg.Text = "";
            if (SearchText.Text.Equals(""))
            {
                ErrorMsg.Text = "Вы не ввели параметр для поиска!";
            }
            else if (_account != null)
            {
                Search(_account);
            }
            else if (_question != null)
            {
                Search(_question);
            }
        }

        #endregion

        #region [Search]

        private class GridItem
        { 
            public int Num { get; set; }
            public int Id { get; set; } = 0;
            public string? Info { get; set; } = null;

        }

        private void ResultGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ResultGrid.Items.Count != 0)
            {
                GridItem row = (GridItem)ResultGrid.SelectedItems[0]!;
                int id = row.Id;
                using (Context db = new())
                {
                    if (_account != null)
                    {
                        var result = db.Account.Where(p => p.Id == id).First();
                        AccountWindow aw = new(result);
                        aw.Show();
                    }
                    else if (_question != null)
                    {
                        var result = db.Question.Where(p => p.Id == id).Include(p => p.Answers).First();
                        QuestionWindow qw = new(result);
                        qw.Show();
                    }
                }
            }
            else return;
        }

        private async void Search(Account account)
        {

            string email = SearchText.Text;
            await using (Context db = new())
            {
                accounts = db.Account.Where(p => (p.Email.StartsWith(email) || p.Email.EndsWith(email)) 
                //&& p.RoleId == 1
                ).ToList();
                if (accounts.Count > 0)
                {
                    foreach (Account acc in accounts)
                    {
                        int index = accounts.IndexOf(acc) + 1;
                        int id = acc.Id;
                        string accountInfo = "Email: " + acc.Email + "; Имя: " + acc.Name + "; Дата регистрации: " + acc.RegDate.ToShortDateString() + "; Сумма баллов: " + acc.ScoreSum + ";";
                        ResultGrid.Items.Add(new GridItem() { Num = index, Id=id, Info = accountInfo });

                    }
                }
                else 
                {
                    ErrorMsg.Text = "Такой записи не существует!";
                    return;
                }
            }
        }

        private async void Search(Question question)
        { 
            string questText = SearchText.Text;
            await using (Context db = new())
            {
                questions = db.Question.Where(p => p.QuestionText.ToLower().StartsWith(questText.ToLower()) || p.QuestionText.ToLower().EndsWith(questText.ToLower())).Include(p => p.Answers).ToList();
                if (questions.Count > 0)
                {
                    foreach (Question quest in questions)
                    {
                        int index = questions.IndexOf(quest) + 1;
                        int id = quest.Id;
                        string questionInfo = "Вопрос: " + quest.QuestionText + "; Ответов: (" + quest.Answers.Count.ToString() + ");";
                        ResultGrid.Items.Add(new GridItem() { Num = index, Id = id, Info = questionInfo });

                    }
                }
                else
                {
                    ErrorMsg.Text = "Такой записи не существует!";
                    return;
                }
            }
        }

        #endregion
    }
}
