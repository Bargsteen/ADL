using System;
using System.Text;
using System.Collections.Generic;
using ADLApp.Models;
using ADLApp.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ADLApp.UnitTest
{
    /// <summary>
    /// Summary description for MultipleResultViewModelTests
    /// </summary>
    [TestClass]
    public class MultipleResultViewModelTests
    {
        private static Assignment mulAssignment = new Assignment()
        {
            AnswerCorrectness = new List<AnswerBool>()
            { new AnswerBool()
                {
                    Value = false
                },
             new AnswerBool()
                {
                    Value = false
                },
             new AnswerBool()
                {
                    Value = false
                }
            },
            Text = "Test question",
            CorrectAnswer = 1,
            AnswerOptions = new List<AnswerOption>()
            {
                    new AnswerOption() {AnswerOptionId = 1, Text = "A01"},
                    new AnswerOption() {AnswerOptionId = 2, Text = "A02"},
                    new AnswerOption() {AnswerOptionId = 3, Text = "A03"}
            }
        };

        private MultipleResultViewModel mul = new MultipleResultViewModel(new List<AnswerBool>()
        {
            new AnswerBool() {Value = true},
            new AnswerBool() {Value = true},
            new AnswerBool() {Value = true}
        }, mulAssignment);

        [TestMethod]
        [TestCategory("Multiple choice")]
        public void TestFeedBackForMultipleIsInCorrect()
        {
            Assert.AreEqual($"Desværre, det er forkert. Du svarede 0 ud af 3 korrekte", mul.ResultText);
        }

        [TestMethod]
        [TestCategory("Multiple choice")]
        public void TestFeedBackForMultipleIsPartialCorrect()
        {
            mul = new MultipleResultViewModel(new List<AnswerBool>()
            {
                new AnswerBool() {Value = false},
                new AnswerBool() {Value = false},
                new AnswerBool() {Value = true}
            }, mulAssignment);
            Assert.AreEqual($"Det er næsten korrekt! Du svarede 2 ud af 3 korrekte", mul.ResultText);
        }

        [TestMethod]
        [TestCategory("Multiple choice")]
        public void TestFeedbackForMultipleIsCorrect()
        {
            mul = new MultipleResultViewModel(new List<AnswerBool>()
            {
                new AnswerBool() {Value = false},
                new AnswerBool() {Value = false},
                new AnswerBool() {Value = false}
            }, mulAssignment);
            Assert.AreEqual("Det er korrekt, godt gået!", mul.ResultText);
        }

    }
}
