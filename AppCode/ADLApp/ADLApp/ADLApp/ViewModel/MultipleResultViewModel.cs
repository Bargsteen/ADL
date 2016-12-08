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
        public Assignment Assignment { get; set; }

        public MultipleResultViewModel(List<AnswerBool> chosenAnswers, Assignment assignmentToSolve)
        {
            Assignment = assignmentToSolve;
            FeedBackList = new List<Tuple<string, Color, string>>();
            int counter = 0;
            foreach (AnswerBool ca in chosenAnswers)
            {
                if (ca.Value)
                {
                    string chosen = "\u2611";
                    FeedBackList.Add(Assignment.AnswerCorrectness[counter].Value
                        ? new Tuple<string, Color, string>(Assignment.AnswerOptions[counter].Text, Color.Green,
                            chosen)
                        : new Tuple<string, Color, string>(Assignment.AnswerOptions[counter].Text, Color.Red,
                            chosen));
                }
                else
                {
                    string notChosen = "\u2610";
                    FeedBackList.Add(!Assignment.AnswerCorrectness[counter].Value
                        ? new Tuple<string, Color, string>(Assignment.AnswerOptions[counter].Text, Color.Green, notChosen)
                        : new Tuple<string, Color, string>(Assignment.AnswerOptions[counter].Text, Color.Red, notChosen));
                }
                counter++;
            }


            bool isCorrect = FeedBackList.All(fb => fb.Item2 == Color.Green);
            if (isCorrect) ResultText = "Det er korrekt, godt gået!";
            else
            {
                int correctAnswerCount = FeedBackList.Count(fb => fb.Item2 == Color.Green);
                if (((double)correctAnswerCount / FeedBackList.Count()) > 0.5)
                    ResultText = $"Det er næsten korrekt! Du svarede {correctAnswerCount} ud af {FeedBackList.Count()} korrekte";
                else
                    ResultText = $"Desværre, det er forkert. Du svarede {correctAnswerCount} ud af {FeedBackList.Count()} korrekte";
            }

        }
        public string ResultText { get; set; }
        public List<Tuple<string, Color, string>> FeedBackList { get; set; }
    }
}
