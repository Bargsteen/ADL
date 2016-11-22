using System;
using System.Collections.Generic;

namespace ADL.Models.ViewModels
{
    public class StatisticsViewModel
    {
        public IEnumerable<Assignment> Assignments { get; set; }
        public IEnumerable<Answer> Answers { get; set; }
        public Dictionary<int,Tuple<int,int>> Stats { get; set; }
    }
}