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
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Xunit.Sdk;

namespace ADL.Tests
{
    public class SchoolControllerTests
    {
        private Mock<ISchoolRepository> mockSchoolRepository;
        private SchoolController schoolController; 
        public SchoolControllerTests()
        {
            mockSchoolRepository = new Mock<ISchoolRepository>();
            mockSchoolRepository.SetupGet(s => s.Schools).Returns(new List<School>()
            {
                new School() {InstitutionNumber = 11, SchoolId = 1, SchoolName = "TestSchoolOne"},
                new School() {InstitutionNumber = 22, SchoolId = 2, SchoolName = "TestSchoolTwo"}
            });
            mockSchoolRepository.Setup(m => m.DeleteSchool(It.IsAny<int>()));

            schoolController = new SchoolController(mockSchoolRepository.Object);
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
