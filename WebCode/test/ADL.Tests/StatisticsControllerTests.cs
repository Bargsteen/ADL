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
using Microsoft.EntityFrameworkCore.Query.Internal;
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
                                   CreatorId = "asd1",
                                   Assignments = new List<Assignment>(){new Assignment { AssignmentId = 7, Text = "TestText"}}  }

             });

            mockAnswerRepository = new Mock<IAnswerRepository>();
            mockAnswerRepository.Setup(m => m.Answers).Returns(new[]
            {
                new Answer {AnswerId = 1,
                            AnsweredAssignmentId = 1,
                            AnswerText = "testText1",
                            ChosenAnswer = 1,
                            TimeAnswered = DateTime.Now,
                            UserId = "asd1",
                            Type = EnumCollection.AssignmentType.ExclusiveChoice, },
                new Answer {AnswerId = 2,
                            AnsweredAssignmentId = 2,
                            AnswerText = "testText2",
                            ChosenAnswer = 2,
                            TimeAnswered = DateTime.Now,
                            UserId = "asd2",
                            Type = EnumCollection.AssignmentType.ExclusiveChoice, },
                new Answer {AnswerId = 3,
                            AnsweredAssignmentId = 3,
                            AnswerText = "testText3",
                            ChosenAnswer = 3,
                            TimeAnswered = DateTime.Now,
                            UserId = "asd3",
                            Type = EnumCollection.AssignmentType.ExclusiveChoice, },
                new Answer {AnswerId = 4,
                            AnsweredAssignmentId = 4,
                            AnswerText = "testText4",
                            ChosenAnswer = 4,
                            TimeAnswered = DateTime.Now,
                            UserId = "asd4",
                            Type = EnumCollection.AssignmentType.ExclusiveChoice, },
                new Answer {AnswerId = 5,
                            AnsweredAssignmentId = 5,
                            AnswerText = "testText5",
                            ChosenAnswer = 5,
                            TimeAnswered = DateTime.Now,
                            UserId = "asd5",
                            Type = EnumCollection.AssignmentType.ExclusiveChoice, }
            });

            mockClassRepository = new Mock<IClassRepository>();
            mockClassRepository.Setup(m => m.Classes).Returns(new[]
            {
                new Class {ClassId = 1,
                           Name = "testClass",
                           People = new List<Person>() {new Person { Firstname = "test Firestname", Lastname = "test Lastname", SchoolId = 1}},
                           SchoolId = 1,
                           StartYear = 2016}

            });

            var userStore = new Mock<IUserStore<Person>>();

            mockUserManager = new Mock<UserManager<Person>>(userStore.Object, null, null, null, null, null, null, null, null);
            mockUserManager.Setup(m => m.Users).Returns(new EnumerableQuery<Person>(new List<Person>()
            {
             new Person {Firstname = "test Firestname1", Lastname = "test Lastname1",SchoolId = 1, Id = "asd1"},
             new Person {Firstname = "test Firestname2", Lastname = "test Lastname2",SchoolId = 1, Id = "asd2"},
             new Person {Firstname = "test Firestname2", Lastname = "test Lastname2",SchoolId = 2, Id = "asd3"}

            }));
            UserManager<Person> um = new UserManager<Person>(userStore.Object, null, null, null, null, null, null, null, null);

            _statisticsController = new StatisticsController(mockAssignmentSetRepository.Object
                , mockAnswerRepository.Object, mockUserManager.Object, mockClassRepository.Object, new Person { Firstname = "test Firestname", Lastname = "test Lastname", SchoolId = 1, Id = "asd1" });




        }

        [Fact]
        public async Task TeststatisticsViewModel()
        {
            // Act
            var result = await _statisticsController.Index();
            // Assert
            Assert.Equal(mockAnswerRepository.Object.Answers, (result.Model as StatisticsViewModel).Answers);
            Assert.True((result.Model as StatisticsViewModel).People.All(p => p.SchoolId == 1));
            Assert.True((result.Model as StatisticsViewModel)
                  .AnswerInformationViewModels
                  .Select(aiv => aiv.AssignmentSet)
                  .All(aset => aset.CreatorId == "asd1"));
            Assert.True((result.Model as StatisticsViewModel)
                  .AnswerInformationViewModels
                  .Select(aiv => aiv.AssignmentAnswers)
                  .All(a => a.All(aa => aa.Item1.AssignmentId == aa.Item2.AnsweredAssignmentId)));
            Assert.True((result.Model as StatisticsViewModel)
                  .AnswerInformationViewModels
                  .All(aiv => aiv.AssignmentAnswers.All(aa => aiv.User.Id == aa.Item2.UserId)));
        }
    }
}
