using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ADL.Controllers;
using ADL.Models;
using ADL.Models.Answers;
using ADL.Models.Assignments;
using ADL.Models.Repositories;
using ADL.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;

namespace ADL.Tests
{
    public class StatisticsControllerTests
    {

        private Mock<IAnswerRepository> mockAnswerRepository = new Mock<IAnswerRepository>();
        private Mock<IAssignmentSetRepository> mockAssignmentSetRepository = new Mock<IAssignmentSetRepository>();
        private Mock<UserManager<Person>> mockUserManager = new Mock<UserManager<Person>>();
        private Mock<IClassRepository> mockClassRepository = new Mock<IClassRepository>();
        private readonly StatisticsController _statisticsController;
        public StatisticsControllerTests()
        {
            mockAssignmentSetRepository = new Mock<IAssignmentSetRepository>();
            mockAssignmentSetRepository.Setup(m => m.AssignmentSets).Returns(new[]
             {
                new AssignmentSet {AssignmentSetId = 1,
                                   Title = "TestTitle",
                                   Description = "TestDescription",
                                   Assignments = new List<Assignment>(){new Assignment { AssignmentId = 7, Text = "TestText"}}  }

             });

            mockAnswerRepository = new Mock<IAnswerRepository>();
            mockAnswerRepository.Setup(m => m.Answers).Returns(new[]
            {
                new Answer {AnswerId = 1,
                            AnsweredAssignmentId = 1,
                            AnswerText = "testText",
                            ChosenAnswer = 1,
                            TimeAnswered = DateTime.Now,
                            Type = EnumCollection.AssignmentType.ExclusiveChoice, }
            });

            mockClassRepository = new Mock<IClassRepository>();
            mockClassRepository.Setup(m => m.Classes).Returns(new[]
            {
                new Class {ClassId = 1,
                           Name = "testClass",
                           People = new List<Person>() {new Person { Firstname = "test Firestname", Lastname = "test Lastname" }},
                           SchoolId = 1,
                           StartYear = 2016}
            });

            var userStore = new Mock<IUserStore<Person>>();
            mockUserManager = new Mock<UserManager<Person>>(userStore);

            UserManager<Person> um = new UserManager<Person>(userStore.Object, null, null, null, null, null, null, null, null);

            _statisticsController = new StatisticsController(mockAssignmentSetRepository.Object
                , mockAnswerRepository.Object, um, mockClassRepository.Object);
        }

        [Fact]
        public async Task TeststatisticsViewModel()
        {
          //  var answers = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> {new Claim(ClaimTypes.Name)}));
            // Act
            var result = await _statisticsController.Index();
            // Assert
            Assert.Equal(mockAnswerRepository.Object.Answers,(result.Model as StatisticsViewModel).Answers);

        }
        //var user = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> { new Claim(ClaimTypes.Name, "homer.simpson") }));
        //var requirement = new OperationAuthorizationRequirement { Name = "Read" };
    }
}
