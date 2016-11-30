using System;
using System.Collections.Generic;
using ADLApp.ViewModel;
using ADLApp.Views;
using ADLApp.Models;
using ADLApp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xamarin.Forms;

namespace ADLApp.UnitTest
{
    [TestClass]
    public class ResultViewModelTests
    {
        private ResultViewModel rvm = new ResultViewModel(mpAssignment, 1);

        private static MultipleChoiceAssignment mpAssignment = new MultipleChoiceAssignment()
        {
            CorrectAnswer = 0,
            Headline = "Test Headline",
            Question = "Test question",
            AnswerOptions = new List<AnswerOption>()
            {
                new AnswerOption() {AnswerOptionID = 333, Text = "AO1"},
                new AnswerOption() {AnswerOptionID = 44, Text = "AO2"}
            },
        };
        [TestMethod]
        public void TestIfRVMCorrectAnswerIsCorrect()
        {

            Assert.AreEqual(rvm.CorrectAnswer, mpAssignment.AnswerOptions[mpAssignment.CorrectAnswer].Text);
        }

        [TestMethod]
        public void TestFeedbackIsIncorrect()
        {
            Assert.AreEqual($"Desværre, det rigtige svar var \"{rvm.CorrectAnswer}\"", rvm.Feedback.Item1);
            Assert.AreEqual(Color.Red, rvm.Feedback.Item2);
            
        }

        [TestMethod]
        public void TestFeedbackisCorrect()
        {
            ResultViewModel rvm = new ResultViewModel(mpAssignment, 0);
            Assert.AreEqual("Godt gået, det er korrekt!", rvm.Feedback.Item1);
            Assert.AreEqual(Color.Green, rvm.Feedback.Item2);   
        }
    }
}
