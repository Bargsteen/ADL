using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADLApp.Models;
using Xamarin.Forms;

namespace ADLApp.ViewModel
{
    public class MultipleResultViewModel
    {
        public MultipleChoiceAssignment Assignment { get; set; }
        private List<AnswerOption> chosenAnswers;

        public MultipleResultViewModel(List<AnswerOption> chosenAnswers, MultipleChoiceAssignment assignmentToSolve)
        {
            this.chosenAnswers = chosenAnswers;
            Assignment = assignmentToSolve;
            FeedBackList = new List<Tuple<string, Color, string>>();
            foreach (AnswerOption ao in Assignment.AnswerOptions)
            {
                if (chosenAnswers.Exists(a => a == ao))
                {
                    string chosen = "\u2611";
                    if (Assignment.CorrectAnswers.Exists(ca => Assignment.AnswerOptions[ca] == ao))
                    {
                        FeedBackList.Add(new Tuple<string, Color, string>(ao.Text, Color.Green, chosen));
                    }
                    else
                    {
                        FeedBackList.Add(new Tuple<string, Color, string>(ao.Text, Color.Red, chosen));
                    }
                }
                else
                {
                    string notChosen = "\u2610";
                    if (Assignment.CorrectAnswers.Exists(ca => Assignment.AnswerOptions[ca] == ao))
                    {
                        FeedBackList.Add(new Tuple<string, Color, string>(ao.Text, Color.Red, notChosen));
                    }
                    else
                    {
                        FeedBackList.Add(new Tuple<string, Color, string>(ao.Text, Color.Green, notChosen));
                    }
                }
            }
            bool isCorrect = true;
            foreach (int a in Assignment.CorrectAnswers)
            {
                if (!chosenAnswers.Exists(ca => ca == Assignment.AnswerOptions[a])) isCorrect = false;
            }
            if (isCorrect) ResultText = "Det er korrekt, godt gået!";
            else ResultText = "Desværre, det er forkert";
        }
        public string ResultText { get; set; }
        public List<Tuple<string, Color, string>> FeedBackList { get; set; }
    }
}
