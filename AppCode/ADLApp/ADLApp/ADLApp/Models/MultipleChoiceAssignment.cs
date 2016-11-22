﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADLApp.Models
{
    public class MultipleChoiceAssignment : Assignment
    {
        public List<AnswerOption> AnswerOptions { get; set; }
        public int CorrectAnswer { get; set; }
    }
}
