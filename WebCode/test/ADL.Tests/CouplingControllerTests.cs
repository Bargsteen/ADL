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
    public class CouplingControllerTests
    {
        private Mock<IAssignmentSetRepository> mockAssignmentSetRepository = new Mock<IAssignmentSetRepository>();
        private Mock<ILocationRepository> mockLocationRepository = new Mock<ILocationRepository>();
        private Mock<IClassRepository> mockClassRepository = new Mock<IClassRepository>();
        private readonly CouplingController _couplingController;
        public CouplingControllerTests()
        {
            mockAssignmentSetRepository = new Mock<IAssignmentSetRepository>();
            mockAssignmentSetRepository.Setup(ma => ma.AssignmentSets).Returns(new[]
                        {
                new AssignmentSet {AssignmentSetId = 1, Title = "TestTitle", Description = "TestDescription", Assignments = new List<Assignment>(){new Assignment { AssignmentId = 7, Text = "TestText"}}  }
             });
            mockLocationRepository = new Mock<ILocationRepository>();
            mockLocationRepository.SetupGet(ml => ml.Locations).Returns(new List<Location>()
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
            mockClassRepository = new Mock<IClassRepository>();
            mockClassRepository.Setup(mc => mc.Classes).Returns(new[]
            {
                new Class()
                {
                    ClassId = 1, SchoolId = 1, Name = "Test1", StartYear = 2000, People = new List<Person>()
                    {
                        new Person()
                        {
                            Firstname = "TestFN1", Lastname = "TestLN1", Id = "TestId1", SchoolId = 1
                        },
                         new Person()
                        {
                            Firstname = "TestFN2", Lastname = "TestLN2", Id = "TestId2", SchoolId = 1
                        },
                          new Person()
                        {
                            Firstname = "TestFN3", Lastname = "TestLN3", Id = "TestId3", SchoolId = 1
                        },
                           new Person()
                        {
                            Firstname = "TestFN4", Lastname = "TestLN4", Id = "TestId4", SchoolId = 1
                        },
                    }
                }, new Class()
                {
                    ClassId = 2, SchoolId = 1, Name = "Test2", StartYear = 2005, People = new List<Person>()
                    {
                        new Person()
                        {
                            Firstname = "TestFN1", Lastname = "TestLN1", Id = "TestId1", SchoolId = 1
                        },
                         new Person()
                        {
                            Firstname = "TestFN2", Lastname = "TestLN2", Id = "TestId2", SchoolId = 1
                        },
                          new Person()
                        {
                            Firstname = "TestFN3", Lastname = "TestLN3", Id = "TestId3", SchoolId = 1
                        },
                           new Person()
                        {
                            Firstname = "TestFN4", Lastname = "TestLN4", Id = "TestId4", SchoolId = 1
                        },
                    }
                }
            });
            _couplingController = new CouplingController(mockLocationRepository.Object, mockClassRepository.Object, mockAssignmentSetRepository.Object);
        }
        [Fact]
        public void TestChooseClass()
        {
            //Act
            var result = _couplingController.ChooseClass(1, 1);
            //Assert
            bool isClassesAppropriateAccordingToSchoolId =
                (result.Model as ChooseClassViewModel).AvailableClasses.All(c => c.SchoolId == 1);
            Assert.True(isClassesAppropriateAccordingToSchoolId);
        }
    }
}
