﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADLApp.Models
{
    class Answer
    {
        public Answer(int chosenAnswerOption, int assignmentId)
        {
            ChosenAnswerOption = chosenAnswerOption;
            TimeAnswered = DateTime.Now;
            AnsweredAssignmentId = assignmentId;
        }
        public int AnswerId { get; set; }
        public int ChosenAnswerOption { get; set; }
        public DateTime TimeAnswered { get; set; }
        public int AnsweredAssignmentId { get; set; }
    }
}
