using System;
using System.Collections.Generic;
using ADLApp.ViewModel;
using ADLApp.Views;
using ADLApp;
using ADLApp.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xamarin.Forms;

namespace ADLApp.UnitTest
{
    [TestClass]
    public class ResultViewModelTests
    {
        private ExclusiveResultViewModel rvm = new ExclusiveResultViewModel(exAssignment, 1);

        private static Assignment exAssignment = new Assignment()
        {
            CorrectAnswer = 0,
            Text = "Test question",
            AnswerOptions = new List<AnswerOption>()
            {
                new AnswerOption() {AnswerOptionId = 333, Text = "AO1"},
                new AnswerOption() {AnswerOptionId = 44, Text = "AO2"}
            },
        };
        [TestMethod]
        public void TestIfRVMCorrectAnswerIsCorrect()
        {

            Assert.AreEqual(rvm.CorrectAnswer, exAssignment.AnswerOptions[exAssignment.CorrectAnswer].Text);
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
            ExclusiveResultViewModel rvm = new ExclusiveResultViewModel(exAssignment, 0);
            Assert.AreEqual("Godt gået, det er korrekt!", rvm.Feedback.Item1);
            Assert.AreEqual(Color.Green, rvm.Feedback.Item2);   
        }
    }
}
