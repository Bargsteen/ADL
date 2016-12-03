using System;
using System.Collections.Generic;

namespace ADL.Models.ViewModels
{
    public class StatisticsViewModel
    {
        public IEnumerable<AssignmentSet> AssignmentSets { get; set; }
        public IEnumerable<Answer> Answers { get; set; }
        public Dictionary<int, Tuple<int, int>> CorrectVsTotalForExclusiveAssignments { get; set; }
        public Dictionary<int, IEnumerable<double>> CorrectPercentagesForMultipleAssignments { get; set; }
        public Dictionary<int, IEnumerable<string>> TextualAnswersForAssignments { get; set; }

        public StatisticsViewModel()
        {
            CorrectPercentagesForMultipleAssignments = new Dictionary<int, IEnumerable<double>>();
            CorrectVsTotalForExclusiveAssignments = new Dictionary<int, Tuple<int, int>>();
            TextualAnswersForAssignments = new Dictionary<int, IEnumerable<string>>();
        }
    }
}