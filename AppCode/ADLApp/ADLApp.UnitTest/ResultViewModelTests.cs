using System;
using System.Collections.Generic;
using ADLApp.ViewModel;
using ADLApp.Views;
using ADLApp.Models;
using ADLApp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ADLApp.UnitTest
{
    [TestClass]
    public class ResultViewModelTests
    {
        [TestMethod]
        public void TestMultipleChoiceResullt()
        {
            ExclusiveChoiceAssignment mpAssignment = new ExclusiveChoiceAssignment();
            mpAssignment.CorrectAnswer = 0;
            mpAssignment.Headline = "Test Headline";
            mpAssignment.Question = "Test Question";
            mpAssignment.AnswerOptions = new List<AnswerOption>()
            {
                new AnswerOption() {AnswerOptionID = 333, Text = "AO1"},
                new AnswerOption() {AnswerOptionID = 44, Text = "AO2"}
            };

            var resultViewModel = new ResultViewModel(mpAssignment, 1);
            
            Assert.AreEqual(resultViewModel.CorrectAnswer, mpAssignment.AnswerOptions[mpAssignment.CorrectAnswer].Text);
        }

        [TestMethod]
        public void TestFeedback()
        {
            ExclusiveChoiceAssignment mpAssignment = new ExclusiveChoiceAssignment();
            mpAssignment.CorrectAnswer = 2;
        }
    }
}
