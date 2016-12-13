using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using ADL.Controllers;
using ADL.Models;
using ADL.Models.ViewModels;
using Moq;
using ADL.Models.Assignments;
using ADL.Models.Repositories;
using Xunit;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Xunit.Sdk;

namespace ADL.Tests
{
    public class SchoolControllerTests
    {
        private Mock<ISchoolRepository> mockSchoolRepository;
        private Mock<ITempDataDictionary> tempData;
        private SchoolController schoolController; 
        public SchoolControllerTests()
        {
            tempData = new Mock<ITempDataDictionary>();
            mockSchoolRepository = new Mock<ISchoolRepository>();
            mockSchoolRepository.SetupGet(s => s.Schools).Returns(new List<School>()
            {
                new School() {InstitutionNumber = 11, SchoolId = 1, SchoolName = "TestSchoolOne"},
                new School() {InstitutionNumber = 22, SchoolId = 2, SchoolName = "TestSchoolTwo"}
            });
            mockSchoolRepository.Setup(m => m.DeleteSchool(It.IsAny<int>()));
            mockSchoolRepository.Setup(m => m.SaveSchool(It.IsAny<School>()));

            schoolController = new SchoolController(mockSchoolRepository.Object);
            schoolController.TempData = tempData.Object;

        }

        [Fact]
        public void TestModelValidation()
        {
            // Arrange
            var incorrectModel = new School();
            var incorrectModelContext = new ValidationContext(incorrectModel, null, null);
            var incorrectResult = new List<ValidationResult>();
            var modelWithInstNumber = new School() {InstitutionNumber = 123123};
            var incorrectModelContextWithInstNumber = new ValidationContext(modelWithInstNumber, null, null);
            var resultWithName = new List<ValidationResult>();
            var correctModel = new School() { SchoolName = "Test", InstitutionNumber = 123};
            var correctContext = new ValidationContext(correctModel, null, null);
            var correctResult = new List<ValidationResult>();

            // Act
            var isModelWithNothingValid = Validator.TryValidateObject(incorrectModel, incorrectModelContext, incorrectResult, true);
            var isModelWithInstNumberValid = Validator.TryValidateObject(modelWithInstNumber, incorrectModelContextWithInstNumber, resultWithName, true);
            var isCorrectModelValid = Validator.TryValidateObject(correctModel, correctContext, correctResult, true);


            Assert.False(isModelWithNothingValid);
            Assert.False(isModelWithInstNumberValid);
            Assert.True(isCorrectModelValid);
        }

        [Fact]
        public void TestEditSchoolWithSchoolInput()
        {
            // Arrange
            School testSchool = new School();
            testSchool.SchoolName = "test school";
            testSchool.InstitutionNumber = 123456;

            School invalidTestSchool = new School();

            // Act
            //Calls method with correct information
            var result = schoolController.Edit(testSchool);
            //Calls method with school not having name and institution number. Proof that controller checks model state,
            //need addittional proof that model contains validating properties. 
            schoolController.ModelState.AddModelError("", "error");
            var invalidResult = schoolController.Edit(invalidTestSchool);

            // Assert
            Assert.IsType(typeof(RedirectToActionResult), result);
            Assert.IsType(typeof(ViewResult), invalidResult);
        }

        [Fact]
        public void TestDeleteSchool()
        {
            //Act
            var result1 = schoolController.Delete(1) as RedirectToActionResult;
            mockSchoolRepository.Verify(s => s.DeleteSchool(1), Times.Once);
            var result2 = schoolController.Delete(2) as RedirectToActionResult;
            mockSchoolRepository.Verify(s => s.DeleteSchool(2), Times.Once);
            //Assert
            Assert.Equal("List", result1.ActionName);
            Assert.Equal("List", result2.ActionName);
        }

    }
}
