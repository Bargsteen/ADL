using System.Collections.Generic;
using System.Linq;
using ADL.Controllers;
using ADL.Models;
using ADL.Models.ViewModels;
using Moq;
using ADL.Models.Assignments;
using ADL.Models.Repositories;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace ADL.Tests
{
    public class CouplingControllerTests
    {
        private readonly Mock<IAssignmentSetRepository> _mockAssignmentSetRepository = new Mock<IAssignmentSetRepository>();
        private readonly Mock<ILocationRepository> _mockLocationRepository = new Mock<ILocationRepository>();
        private readonly Mock<IClassRepository> _mockClassRepository = new Mock<IClassRepository>();
        private readonly Mock<ITempDataDictionary> _tempData = new Mock<ITempDataDictionary>();
        private readonly CouplingController _couplingController;
        public CouplingControllerTests()
        {
            _mockAssignmentSetRepository = new Mock<IAssignmentSetRepository>();

            #region Setups assignmentsetrepository's assignmentsSets property
            _mockAssignmentSetRepository.Setup(ma => ma.AssignmentSets).Returns(new[]
            {
                new AssignmentSet {
                    AssignmentSetId = 1, Title = "TestTitle", Description = "TestDescription", Assignments = new List<Assignment>()
                    {
                        new Assignment
                        {
                            AssignmentId = 5, Text = "TestText"
                        },
                        new Assignment
                        {
                            AssignmentId = 1, Text = "TestText"
                        },
                        new Assignment
                        {
                            AssignmentId = 2, Text = "TestText"
                        },
                        new Assignment
                        {
                            AssignmentId = 3, Text = "TestText"
                        },
                        new Assignment
                        {
                            AssignmentId = 4, Text = "TestText"
                        }

                    }
                }
             });
            #endregion

            _mockLocationRepository = new Mock<ILocationRepository>();

            #region Setups locationrepository locations property and used methods
            _mockLocationRepository.SetupGet(ml => ml.Locations).Returns(new List<Location>()
            {
                    new Location()
                    {
                        Description = "TestDescription",
                        LocationId = 1, SchoolId = 1,
                        Title = "TestTitle"
                    },
                    new Location()
                    {
                        Description = "TestDescription2",
                        LocationId = 2, SchoolId = 1,
                        Title = "TestTitle2"
                    },
                    new Location()
                    {
                        Description = "TestDescription",
                        LocationId = 3, SchoolId = 1,
                        Title = "TestTitle"
                    },
                    new Location()
                    {
                        Description = "TestDescription",
                        LocationId = 4, SchoolId = 1,
                        Title = "TestTitle"
                    },
                    new Location()
                    {
                        Description = "TestDescription",
                        LocationId = 4, SchoolId = 3,
                        Title = "TestTitle"
                    }

            });

            _mockLocationRepository.Setup(
                        l => l.RemoveAllCouplingsForSpecificPersonOnLocation(It.IsAny<int>(), It.IsAny<string>()));
            _mockLocationRepository.Setup(l => l.AddCouplingsToLocation(It.IsAny<int>(), It.IsAny<List<PersonAssignmentCoupling>>()));
            #endregion

            _mockClassRepository = new Mock<IClassRepository>();

            #region Setups ClassRepository classes property
            _mockClassRepository.Setup(mc => mc.Classes).Returns(new[]
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
            #endregion


            _couplingController = new CouplingController(_mockLocationRepository.Object, _mockClassRepository.Object, _mockAssignmentSetRepository.Object) { TempData = _tempData.Object };
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

        [Fact]
        public void TestDifferentiate()
        {
            var result = _couplingController.Differentiate(1, 2);
            int chosenSchoolId = (result.Model as DifferentiateViewModel).ChosenClass.SchoolId;
            //Number of people connected to class 2 is 4, tests if differentiate gets students only
            Assert.Equal(3, (result.Model as DifferentiateViewModel).ChosenClass.People.Count);
            Assert.Equal(chosenSchoolId, (result.Model as DifferentiateViewModel).CurrentSchoolId);
            Assert.Equal(1, (result.Model as DifferentiateViewModel).ChosenAssignmentSet.AssignmentSetId);
            //Instantiates dvm with result of calling Differentiate with chosenAssignmentSetId and chosenClass
        }

        [Fact]
        public void TestDifferentiateWithDifferentiateViewModelInput()
        {

            //Arrange
            DifferentiateViewModel dvm = new DifferentiateViewModel();

            #region Initialises fields in viewmodel

            dvm.ChosenAssignmentSet =
                _mockAssignmentSetRepository.Object.AssignmentSets.First(a => a.AssignmentSetId == 1);
            dvm.ChosenClass = _mockClassRepository.Object.Classes.First(c => c.ClassId == 1);
            dvm.CurrentSchoolId = 1;
            dvm.PersonAssignmentCouplings = new List<PersonIdAssignmentIdCoupling>()
            {

                //Person 1 gets 5/5 assignments
                new PersonIdAssignmentIdCoupling()
                {
                    AssignmentId = 1,
                    IsChosen = true,
                    PersonId = "TestId1"
                },
                new PersonIdAssignmentIdCoupling()
                {
                    AssignmentId = 2,
                    IsChosen = true,
                    PersonId = "TestId1"
                },
                new PersonIdAssignmentIdCoupling()
                {
                    AssignmentId = 3,
                    IsChosen = true,
                    PersonId = "TestId1"
                },
                new PersonIdAssignmentIdCoupling()
                {
                    AssignmentId = 4,
                    IsChosen = true,
                    PersonId = "TestId1"
                },
                new PersonIdAssignmentIdCoupling()
                {
                    AssignmentId = 5,
                    IsChosen = true,
                    PersonId = "TestId1"
                },

                //Person 2 gets 4/5 assignments
                new PersonIdAssignmentIdCoupling()
                {
                    AssignmentId = 1,
                    IsChosen = true,
                    PersonId = "TestId2"
                },
                new PersonIdAssignmentIdCoupling()
                {
                    AssignmentId = 2,
                    IsChosen = true,
                    PersonId = "TestId2"
                },
                new PersonIdAssignmentIdCoupling()
                {
                    AssignmentId = 3,
                    IsChosen = true,
                    PersonId = "TestId2"
                },
                new PersonIdAssignmentIdCoupling()
                {
                    AssignmentId = 5,
                    IsChosen = true,
                    PersonId = "TestId2"
                },

                //Person 3 gets 1/5 assignments
                new PersonIdAssignmentIdCoupling()
                {
                    AssignmentId = 1,
                    IsChosen = true,
                    PersonId = "TestId3"
                }

                //Person 4 gets no assignments
            };

            #endregion

            //Act
            var result = _couplingController.Differentiate(dvm);
            //Assert
            Assert.IsType(typeof(ViewResult), result);
            ViewResult vresult = result as ViewResult;
            Assert.Equal("ChooseLocations", vresult.ViewName);
            ChooseLocationsViewModel clvm = vresult.Model as ChooseLocationsViewModel;

            //Test if correct amount of assignments are assigned to testpeople

            Assert.Equal(5, clvm.PersonAssignmentCouplings.Count(pac => pac.PersonId == "TestId1"));
            Assert.Equal(4, clvm.PersonAssignmentCouplings.Count(pac => pac.PersonId == "TestId2"));
            Assert.Equal(1, clvm.PersonAssignmentCouplings.Count(pac => pac.PersonId == "TestId3"));
            Assert.Equal(0, clvm.PersonAssignmentCouplings.Count(pac => pac.PersonId == "TestId4"));

            Assert.True(TestIfPersonAssignmentCouplingsContainDuplicateAssignmentsAux(clvm, "TestId1"));
            Assert.True(TestIfPersonAssignmentCouplingsContainDuplicateAssignmentsAux(clvm, "TestId2"));
            Assert.True(TestIfPersonAssignmentCouplingsContainDuplicateAssignmentsAux(clvm, "TestId3"));
        }

        private bool TestIfPersonAssignmentCouplingsContainDuplicateAssignmentsAux(ChooseLocationsViewModel clvm, string personId)
        {
            bool containsDuplicates = false;
            //Test if correct assignments are sent with and no duplicates assignments
            foreach (
                PersonAssignmentCoupling personAssignmentCoupling in
                clvm.PersonAssignmentCouplings.Where(pac => pac.PersonId == personId))
            {
                bool isDuplicate = false;
                foreach (
                    PersonAssignmentCoupling pac in
                    clvm.PersonAssignmentCouplings.Where(pac => pac.PersonId == personId))
                {
                    if (personAssignmentCoupling.AssignmentId == pac.AssignmentId)
                        isDuplicate = !isDuplicate;
                }
                containsDuplicates = isDuplicate;
            }
            return containsDuplicates;
        }

        [Fact]
        public void TestFinishCoupling()
        {
            ChooseLocationsViewModel clvm = new ChooseLocationsViewModel();
            DifferentiateViewModel dvm = new DifferentiateViewModel();
            #region Initialises fields in ViewModel

            dvm.ChosenAssignmentSet =
                _mockAssignmentSetRepository.Object.AssignmentSets.First(a => a.AssignmentSetId == 1);
            dvm.ChosenClass = _mockClassRepository.Object.Classes.First(c => c.ClassId == 1);
            dvm.CurrentSchoolId = 1;
            dvm.PersonAssignmentCouplings = new List<PersonIdAssignmentIdCoupling>()
            {

                //Person 1 gets 5/5 assignments
                new PersonIdAssignmentIdCoupling()
                {
                    AssignmentId = 1,
                    IsChosen = true,
                    PersonId = "TestId1"
                },
                new PersonIdAssignmentIdCoupling()
                {
                    AssignmentId = 2,
                    IsChosen = true,
                    PersonId = "TestId1"
                },
                new PersonIdAssignmentIdCoupling()
                {
                    AssignmentId = 3,
                    IsChosen = true,
                    PersonId = "TestId1"
                },
                new PersonIdAssignmentIdCoupling()
                {
                    AssignmentId = 4,
                    IsChosen = true,
                    PersonId = "TestId1"
                },
                new PersonIdAssignmentIdCoupling()
                {
                    AssignmentId = 5,
                    IsChosen = true,
                    PersonId = "TestId1"
                },

                //Person 2 gets 4/5 assignments
                new PersonIdAssignmentIdCoupling()
                {
                    AssignmentId = 1,
                    IsChosen = true,
                    PersonId = "TestId2"
                },
                new PersonIdAssignmentIdCoupling()
                {
                    AssignmentId = 2,
                    IsChosen = true,
                    PersonId = "TestId2"
                },
                new PersonIdAssignmentIdCoupling()
                {
                    AssignmentId = 3,
                    IsChosen = true,
                    PersonId = "TestId2"
                },
                new PersonIdAssignmentIdCoupling()
                {
                    AssignmentId = 5,
                    IsChosen = true,
                    PersonId = "TestId2"
                },

                //Person 3 gets 1/5 assignments
                new PersonIdAssignmentIdCoupling()
                {
                    AssignmentId = 1,
                    IsChosen = true,
                    PersonId = "TestId3"
                }

                //Person 4 gets no assignments
            };


            #endregion

            var result = _couplingController.Differentiate(dvm) as ViewResult;
            clvm = result.Model as ChooseLocationsViewModel;
            clvm.AvailableLocations = _mockLocationRepository.Object.Locations.ToList();
            clvm.ChosenLocations = new List<ChosenLocation>()
            {
                new ChosenLocation() {IsChosen = true, LocationId = 1 },
                new ChosenLocation() {IsChosen = true, LocationId = 2 },
                new ChosenLocation() {IsChosen = true, LocationId = 3 },
            };
            var finishCouplingResult = _couplingController.FinishCoupling(clvm);
            foreach (string s in clvm.PersonAssignmentCouplings.Select(pac => pac.PersonId).Distinct())
            {
                foreach (ChosenLocation location in clvm.ChosenLocations)
                {
                    _mockLocationRepository.Verify(
                        l => l.RemoveAllCouplingsForSpecificPersonOnLocation(location.LocationId, s),
                        Times.Once);
                }
            }
            //Tests if expected personassignmentcouplings input to function 'AddCouplingsToLocations' actual input 
            List<PersonAssignmentCoupling> firstCallList = new List<PersonAssignmentCoupling>();
            firstCallList.AddRange(clvm.PersonAssignmentCouplings.Where(pac => pac.PersonId == "TestId1" && (pac.AssignmentId == 1 || pac.AssignmentId == 4)));
            firstCallList.AddRange(clvm.PersonAssignmentCouplings.Where(pac => pac.PersonId == "TestId2" && (pac.AssignmentId == 1 || pac.AssignmentId == 5)));
            firstCallList.Add(clvm.PersonAssignmentCouplings.First(pac => pac.PersonId == "TestId3"));
            _mockLocationRepository.Verify(l => l.AddCouplingsToLocation(1, firstCallList), Times.Once);
            var secondCallList = new List<PersonAssignmentCoupling>();
            secondCallList.Add(clvm.PersonAssignmentCouplings.First(pac => pac.PersonId == "TestId1" && pac.AssignmentId == 2));
            secondCallList.Add(clvm.PersonAssignmentCouplings.First(pac => pac.PersonId == "TestId1" && pac.AssignmentId == 5));
            secondCallList.Add(clvm.PersonAssignmentCouplings.First(pac => pac.PersonId == "TestId2" && pac.AssignmentId == 2));
            _mockLocationRepository.Verify(l => l.AddCouplingsToLocation(2, secondCallList), Times.Once);
            var thirdCallList = new List<PersonAssignmentCoupling>();
            thirdCallList.Add(clvm.PersonAssignmentCouplings.First(pac => pac.PersonId == "TestId1" && pac.AssignmentId == 3));
            thirdCallList.Add(clvm.PersonAssignmentCouplings.First(pac => pac.PersonId == "TestId2" && pac.AssignmentId == 3));
            _mockLocationRepository.Verify(l => l.AddCouplingsToLocation(3, thirdCallList), Times.Once);

        }
    }
}
