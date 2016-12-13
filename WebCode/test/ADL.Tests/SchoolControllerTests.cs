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
            schoolController.Delete(1);
            mockSchoolRepository.Verify(s => s.DeleteSchool(1));
        }

    }
}
