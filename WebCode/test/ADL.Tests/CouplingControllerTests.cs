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
using Microsoft.AspNetCore.Mvc.ViewFeatures;
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
        private Mock<ITempDataDictionary> tempData = new Mock<ITempDataDictionary>();
        private readonly CouplingController _couplingController;
        public CouplingControllerTests()
        {
            mockAssignmentSetRepository = new Mock<IAssignmentSetRepository>();
            mockAssignmentSetRepository.Setup(ma => ma.AssignmentSets).Returns(new[]
            {
                new AssignmentSet {
                    AssignmentSetId = 1, Title = "TestTitle", Description = "TestDescription", Assignments = new List<Assignment>()
                    {
                        new Assignment
                        {
                            AssignmentId = 7, Text = "TestText"
                        }
                    }
                }
             });
            mockLocationRepository = new Mock<ILocationRepository>();
            mockLocationRepository.SetupGet(ml => ml.Locations).Returns(new List<Location>()
            {
                    new Location()
                    {
                        Description = "TestDescription",
                        LocationId = 30, SchoolId = 42,
                        Title = "TestTitle"
                    },
                    new Location()
                    {
                        Description = "TestDescription2",
                        LocationId = 31, SchoolId = 42,
                        Title = "TestTitle2"
                    },
                    new Location()
                    {
                        Description = "TestDescription",
                        LocationId = 329, SchoolId = 42,
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
                },
                new Class()
                {
                    ClassId = 2, SchoolId = 1, Name = "Test2", StartYear = 2005, People = new List<Person>()
                    {
                        new Person()
                        {
                            Firstname = "TestFN1", Lastname = "TestLN1", Id = "TestId1", SchoolId = 1, PersonType = EnumCollection.PersonTypes.Teacher
                        },
                         new Person()
                        {
                            Firstname = "TestFN2", Lastname = "TestLN2", Id = "TestId2", SchoolId = 1, PersonType = EnumCollection.PersonTypes.Student
                        },
                          new Person()
                        {
                            Firstname = "TestFN3", Lastname = "TestLN3", Id = "TestId3", SchoolId = 1, PersonType = EnumCollection.PersonTypes.Student
                        },
                           new Person()
                        {
                            Firstname = "TestFN4", Lastname = "TestLN4", Id = "TestId4", SchoolId = 1, PersonType = EnumCollection.PersonTypes.Student
                        },
                    }
                },
                new Class()
                {
                    ClassId = 3, SchoolId = 2, Name = "Test2", StartYear = 2005, People = new List<Person>()
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
                },
                new Class()
                {
                    ClassId = 5, SchoolId = 2, Name = "Test2", StartYear = 2005, People = new List<Person>()
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
                },
                new Class()
                {
                    ClassId = 4, SchoolId = 2, Name = "Test2", StartYear = 2005, People = new List<Person>()
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
            _couplingController = new CouplingController(mockLocationRepository.Object, mockClassRepository.Object, mockAssignmentSetRepository.Object) { TempData = tempData.Object };
        }
        [Fact]
        public void TestChooseClassForAvailableClassesIsCorrect()
        {
            //Act
            var result1 = _couplingController.ChooseClass(1, 1);
            bool isClassesAppropriateAccordingToSchoolId1 =
                            (result1.Model as ChooseClassViewModel).AvailableClasses.All(c => c.SchoolId == 1);
            //Assert
            Assert.True(isClassesAppropriateAccordingToSchoolId1);
            Assert.Equal(1, (result1.Model as ChooseClassViewModel).ChosenAssignmentSetId);

            var result2 = _couplingController.ChooseClass(42, 1);

            Assert.Empty((result2.Model as ChooseClassViewModel).AvailableClasses);
            Assert.Equal(1, (result2.Model as ChooseClassViewModel).ChosenAssignmentSetId);

            var result3 = _couplingController.ChooseClass(2, 1);
            bool isClassesAppropriateAccordingToSchoolId2 =
                (result3.Model as ChooseClassViewModel).AvailableClasses.All(c => c.SchoolId == 2);

            Assert.True(isClassesAppropriateAccordingToSchoolId2);
            Assert.Equal(1, (result3.Model as ChooseClassViewModel).ChosenAssignmentSetId);
        }

        private DifferentiateViewModel dvm;
        [Fact]
        public void TestDifferentiate()
        {
            var result = _couplingController.Differentiate(1, 2);
            //Number of people connected to class 2 is 4, tests if differentiate gets students only
            Assert.Equal(3, (result.Model as DifferentiateViewModel).ChosenClass.People.Count);
            int chosenSchoolId = (result.Model as DifferentiateViewModel).ChosenClass.SchoolId;
            Assert.Equal(chosenSchoolId, (result.Model as DifferentiateViewModel).CurrentSchoolId);
            Assert.Equal(1, (result.Model as DifferentiateViewModel).ChosenAssignmentSet.AssignmentSetId);
            //Instantiates dvm with result of calling Differentiate with chosenAssignmentSetId and chosenClass
            dvm = result.Model as DifferentiateViewModel;

        }
    }
}
