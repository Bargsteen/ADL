using System;
using System.Collections.Generic;
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
        public void TestEditSchoolWithSchoolInput()
        {
            // Arrange
            School testSchool = new School();
            testSchool.SchoolName = "test school";
            testSchool.InstitutionNumber = 123456;

            School invalidTestSchool = new School();

            // Act
            var result = schoolController.Edit(testSchool);
            //var invalidResult = schoolController.Edit(invalidTestSchool);

            // Assert
            Assert.IsType(typeof(RedirectToActionResult), result);
            //Assert.IsType(typeof(ViewResult), invalidTestSchool);
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
