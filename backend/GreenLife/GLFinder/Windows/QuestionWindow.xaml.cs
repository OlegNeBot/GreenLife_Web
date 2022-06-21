using GreenLifeLib;
using System;
using System.Collections.Generic;
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

namespace GLFinder
{
    /// <summary>
    /// Логика взаимодействия для QuestionWindow.xaml
    /// </summary>
    public partial class QuestionWindow : Window
    {
        public QuestionWindow(Question question)
        {
            InitializeComponent();

            //Filling text fields
            IdBlock.Text += question.Id;
            QuestionBlock.Text = question.QuestionText;

            var answers = question.Answers;
            foreach (Answer answer in answers)
            {
                int index = answers.IndexOf(answer) + 1;
                AnswersListBox.Items.Add(index + ". " + answer.AnswerText);
            }
        }
    }
}
