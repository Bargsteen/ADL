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
    public class ApiControllerTests
    {
        private readonly Mock<IAssignmentSetRepository> _mockAssignmentSetRepository;
        private readonly Mock<ILocationRepository> _mockLocationRepository;
        private readonly Mock<IAnswerRepository> _mockAnswerRepository;
        private readonly Mock<SignInManager<Person>> _mockSignInManager;
        private readonly ApiController _apiController;

        readonly Person _testPerson = new Person()
        {
            Id = "TestPerId",
            UserName = "TestUserName",
            Firstname = "TestFN",
            Lastname = "TestLn"
        };
        public ApiControllerTests()
        {
            _mockAssignmentSetRepository = new Mock<IAssignmentSetRepository>();
            _mockAssignmentSetRepository.Setup(m => m.AssignmentSets).Returns(new[]
             {
                new AssignmentSet {AssignmentSetId = 1, Title = "TestTitle", Description = "TestDescription", Assignments = new List<Assignment>(){new Assignment { AssignmentId = 7, Text = "TestText"}}  }

             });
            _mockLocationRepository = new Mock<ILocationRepository>();
            _mockLocationRepository.SetupGet(l => l.Locations).Returns(new List<Location>()
            {
                    new Location()
                    {
                        Description = "TestDescription",
                        LocationId = 30, SchoolId = 42,
                        PersonAssignmentCouplings = new List<PersonAssignmentCoupling>() {
                            new PersonAssignmentCoupling()
                        {
                            AssignmentId = 7, PersonAssignmentCouplingId = 2, PersonId = "TestPerId"

                        }
                        },
                        Title = "TestTitle"
                    },
                    new Location()
                    {
                         Description = "TestDescription2",
                        LocationId = 31, SchoolId = 42,
                        PersonAssignmentCouplings = new List<PersonAssignmentCoupling>() {
                            new PersonAssignmentCoupling()
                        {
                            AssignmentId = 7, PersonAssignmentCouplingId = 2, PersonId = "TestPerId2"
                        },
                            new PersonAssignmentCoupling()
                        {
                            AssignmentId = 7, PersonAssignmentCouplingId = 3, PersonId = "TestPerId"
                        }
                        },
                        Title = "TestTitle2"
                    },
                    new Location()
                    {
                        Description = "TestDescription",
                        LocationId = 329, SchoolId = 42,
                        PersonAssignmentCouplings = new List<PersonAssignmentCoupling>() {
                            new PersonAssignmentCoupling()
                        {
                            AssignmentId = 7, PersonAssignmentCouplingId = 2, PersonId = "TestPerId2"
                        }
                        },
                        Title = "TestTitle"
                    }

            });
            _mockAnswerRepository = new Mock<IAnswerRepository>();
            var userStore = new Mock<IUserStore<Person>>();
            userStore.Setup(u => u.FindByIdAsync(_testPerson.Id, default(CancellationToken))).Returns(new Task<Person>(() => _testPerson));
            UserManager<Person> um = new UserManager<Person>(userStore.Object, null, null, null, null, null, null, null, null);
            _mockSignInManager = new Mock<SignInManager<Person>>();
            _mockSignInManager.Setup(s => s.PasswordSignInAsync(_testPerson, "TestPassword", false, false))
                .Returns(new Task<SignInResult>(() => new SignInResult()
                {

                }));
            _apiController = new ApiController(_mockAssignmentSetRepository.Object, _mockLocationRepository.Object, _mockAnswerRepository.Object, um, null);
        }
        [Fact]
        public void TestGetAssignmentFromLocationIdAndUserId()
        {
            //Act
            string resultWhenInvalidLocationId = _apiController.Location(10, "TestPerId");
            string resultWhenValidPersonIdAndLocationId = _apiController.Location(30, "TestPerId");
            string resultWhenInvalidPersonId = _apiController.Location(30, "NotValidPerId");

            //Assert
            Assert.Equal("Lokationen eksisterer ikke", resultWhenInvalidLocationId);
            Assignment deserializedASsignment = Newtonsoft.Json.JsonConvert.DeserializeObject<Assignment>(resultWhenValidPersonIdAndLocationId);
            Assert.Equal("TestText",deserializedASsignment.Text);
            Assert.Equal("Lokationen har ikke nogen opgave", resultWhenInvalidPersonId);
        }
        [Fact]
        public async Task TestGetLocationListFromInvalidUserId()
        {
            //Act
            string resultWhenInvalidUserId = await _apiController.LocationList("WrongId");
            string resultWhenValidUserId = await _apiController.LocationList("TestPerIds");

            //Assert
            Assert.Equal("Brugeren blev ikke genkendt.", resultWhenInvalidUserId);
            Assert.Equal("Brugeren blev ikke genkendt.", resultWhenValidUserId);
        }

    }
}

