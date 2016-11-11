using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ADLApp.Models;

namespace ADLApp
{
    public partial class SolvePage : ContentPage
    {
        public SolvePage(Assignment a)
        {
            InitializeComponent();
            currentAssignment = a;
            Headline.Text = a.Headline;
            Question.Text = a.Question;
            AnswerOptionOne.Text = a.AnswerOptionOne;
            AnswerOptionTwo.Text = a.AnswerOptionTwo;
            AnswerOptionThree.Text = a.AnswerOptionThree;
        }
        public Assignment currentAssignment { get; set; }
    }
}
