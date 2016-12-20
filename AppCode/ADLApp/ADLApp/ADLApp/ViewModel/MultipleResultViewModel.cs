#region Libraries

using System;
using System.Collections.Generic;
using System.Linq;
using ADLApp.Models;
using Xamarin.Forms;

#endregion

namespace ADLApp.ViewModel
{
    public class MultipleResultViewModel
    {
        public MultipleResultViewModel(List<AnswerBool> chosenAnswers, Assignment assignmentToSolve)
        {
            Assignment = assignmentToSolve;
            FeedBackList = new List<Tuple<string, Color, string>>();
            int counter = 0;
            foreach (AnswerBool ca in chosenAnswers)
            {
                if (ca.Value)
                {
                    string checkedCheckBox = "\u2611";
                    FeedBackList.Add(Assignment.AnswerCorrectness[counter].Value
                        ? new Tuple<string, Color, string>(Assignment.AnswerOptions[counter].Text, Color.Green,
                            checkedCheckBox)
                        : new Tuple<string, Color, string>(Assignment.AnswerOptions[counter].Text, Color.Red,
                            checkedCheckBox));
                }
                else
                {
                    string uncheckedCheckBox = "\u2610";
                    FeedBackList.Add(!Assignment.AnswerCorrectness[counter].Value
                        ? new Tuple<string, Color, string>(Assignment.AnswerOptions[counter].Text, Color.Green,
                            uncheckedCheckBox)
                        : new Tuple<string, Color, string>(Assignment.AnswerOptions[counter].Text, Color.Red,
                            uncheckedCheckBox));
                }
                counter++;
            }


            bool isCorrect = FeedBackList.All(fb => fb.Item2 == Color.Green);
            if (isCorrect)
                ResultText = "Det er korrekt, godt gået!";
            else
            {
                int correctAnswerCount = FeedBackList.Count(fb => fb.Item2 == Color.Green);
                if ((double)correctAnswerCount / FeedBackList.Count > 0.5)
                    ResultText =
                        $"Det er næsten korrekt! Du svarede {correctAnswerCount} ud af {FeedBackList.Count} korrekte";
                else
                    ResultText =
                        $"Desværre, det er forkert. Du svarede {correctAnswerCount} ud af {FeedBackList.Count} korrekte";
            }
        }

        public Assignment Assignment { get; }
        public string ResultText { get; }
        public List<Tuple<string, Color, string>> FeedBackList { get; }
    }
}