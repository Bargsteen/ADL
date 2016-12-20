using System.Collections.Generic;
using ADLApp.Models;
using ADLApp.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xamarin.Forms;

namespace ADLApp.UnitTest
{
    /// <summary>
    /// Summary description for ExclusiveResultViewModelTests
    /// </summary>
    [TestClass]
    public class ExclusiveResultViewModelTests
    {
        private ExclusiveResultViewModel _rvm = new ExclusiveResultViewModel(_exAssignment, 1);

        private static Assignment _exAssignment = new Assignment()
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
        [TestCategory("Exclusive choice")]
        public void TestIfRvmCorrectAnswerIsCorrect()
        {
            Assert.AreEqual(_rvm.CorrectAnswer, _exAssignment.AnswerOptions[_exAssignment.CorrectAnswer].Text);
        }

        [TestMethod]
        [TestCategory("Exclusive choice")]
        public void TestFeedbackIsIncorrect()
        {
            Assert.AreEqual($"Desværre, det rigtige svar var \"{_rvm.CorrectAnswer}\"", _rvm.Feedback.Item1);
            Assert.AreEqual(Color.Red, _rvm.Feedback.Item2);

        }

        [TestMethod]
        [TestCategory("Exclusive choice")]
        public void TestFeedbackIsCorrect()
        {
            ExclusiveResultViewModel rvm = new ExclusiveResultViewModel(_exAssignment, 0);
            Assert.AreEqual("Godt gået, det er korrekt!", rvm.Feedback.Item1);
            Assert.AreEqual(Color.Green, rvm.Feedback.Item2);
        }
    }
}
