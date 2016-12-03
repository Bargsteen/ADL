using System;
using System.Collections.Generic;

namespace ADL.Models.ViewModels
{
    public class StatisticsViewModel
    {
        public IEnumerable<AssignmentSet> AssignmentSets { get; set; }
        public IEnumerable<Answer> Answers { get; set; }
        public Dictionary<int,Tuple<int,int>> ExclusiveStats { get; set; }
        public Dictionary<int, IEnumerable<double>> MultipleStats { get; set; }
        public Dictionary<int, IEnumerable<string>> TextualAnswers { get; set; }

    }
}