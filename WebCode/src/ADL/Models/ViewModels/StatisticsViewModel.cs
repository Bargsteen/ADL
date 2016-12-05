using System;
using System.Collections.Generic;
using System.Linq;
using ADL.Models.Answers;

namespace ADL.Models.ViewModels
{
    public class StatisticsViewModel
    {
        public IQueryable<Person> People { get; set; }
        public IEnumerable<AssignmentSet> AssignmentSets { get; set; }
        public IEnumerable<Answer> Answers { get; set; }
        public Dictionary<int, Tuple<int, int>> CorrectVsTotalForExclusiveAssignments { get; set; }
        public Dictionary<int, IEnumerable<Tuple<string, double>>> CorrectPercentagesForMultipleAssignments { get; set; }
        public Dictionary<int, IEnumerable<Tuple<string, string>>> TextualAnswersForAssignments { get; set; }

        public StatisticsViewModel()
        {
            CorrectPercentagesForMultipleAssignments = new Dictionary<int, IEnumerable<Tuple<string, double>>>();
            CorrectVsTotalForExclusiveAssignments = new Dictionary<int, Tuple<int, int>>();
            TextualAnswersForAssignments = new Dictionary<int, IEnumerable<Tuple<string, string>>>();
        }
    }
}