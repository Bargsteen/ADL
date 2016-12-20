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
        private static Assignment _mulAssignment = new Assignment()
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

        private MultipleResultViewModel _mul = new MultipleResultViewModel(new List<AnswerBool>()
        {
            new AnswerBool() {Value = true},
            new AnswerBool() {Value = true},
            new AnswerBool() {Value = true}
        }, _mulAssignment);

        [TestMethod]
        [TestCategory("Multiple choice")]
        public void TestFeedBackForMultipleIsInCorrect()
        {
            Assert.AreEqual($"Desværre, det er forkert. Du svarede 0 ud af 3 korrekte", _mul.ResultText);
        }

        [TestMethod]
        [TestCategory("Multiple choice")]
        public void TestFeedBackForMultipleIsPartialCorrect()
        {
            _mul = new MultipleResultViewModel(new List<AnswerBool>()
            {
                new AnswerBool() {Value = false},
                new AnswerBool() {Value = false},
                new AnswerBool() {Value = true}
            }, _mulAssignment);
            Assert.AreEqual($"Det er næsten korrekt! Du svarede 2 ud af 3 korrekte", _mul.ResultText);
        }

        [TestMethod]
        [TestCategory("Multiple choice")]
        public void TestFeedbackForMultipleIsCorrect()
        {
            _mul = new MultipleResultViewModel(new List<AnswerBool>()
            {
                new AnswerBool() {Value = false},
                new AnswerBool() {Value = false},
                new AnswerBool() {Value = false}
            }, _mulAssignment);
            Assert.AreEqual("Det er korrekt, godt gået!", _mul.ResultText);
        }

    }
}
