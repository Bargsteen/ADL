﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADLApp.Models
{
    public class Answer
    {
        public Answer(int assignmentId, params string[] chosenAnswer)
        {
            ChosenAnswers = chosenAnswer.ToList();
            TimeAnswered = DateTime.Now;
            AnsweredAssignmentId = assignmentId;
        }
        public int AnswerId { get; set; }
        public List<string> ChosenAnswers { get; set; }
        public DateTime TimeAnswered { get; set; }
        public int AnsweredAssignmentId { get; set; }
        public string UserId { get; set; }
    }
}