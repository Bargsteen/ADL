using Xunit;
using ADL.Models;
using ADL.Models.Assignments;
using ADL.Models.Repositories;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using ADL.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using static ADL.Models.EnumCollection;
using Moq;
using ADL.Controllers;


namespace ADL.Tests
{
    public class AssignmentControllerTests
    {
        private readonly Mock<IAssignmentSetRepository> _mockAssignmentSetRepository;
        private readonly Mock<ILocationRepository> _mocklocationRepository;
        private readonly AssignmentController _assignmentController;
        UserManager<Person> _userManager;

        private static List<Person> _users;


        public AssignmentControllerTests()
        {
            _mockAssignmentSetRepository = new Mock<IAssignmentSetRepository>();

            _mockAssignmentSetRepository.Setup(m => m.AssignmentSets).Returns(new[]
             {
                new AssignmentSet {AssignmentSetId = 1, Title = "Set 1", Description = "d1",  CreatorId = "1", PublicityLevel = PublicityLevel.Private, SchoolId = 1, Assignments = new List<Assignment>(){new Assignment { AssignmentId = 1, Text = "Test1"}}  },
                new AssignmentSet {AssignmentSetId = 2, Title = "Set 2", Description = "d2", CreatorId = "2", PublicityLevel = PublicityLevel.Internal, SchoolId = 1, Assignments = new List<Assignment>(){new Assignment { AssignmentId = 2, Text = "Test2"}}  },
                new AssignmentSet {AssignmentSetId = 3, Title = "Set 3", Description = "d3", CreatorId = "3", PublicityLevel = PublicityLevel.Public, SchoolId = 2, Assignments = new List<Assignment>(){new Assignment { AssignmentId = 3, Text = "Test3"}}  }

             });
            
            _mocklocationRepository
             = new Mock<ILocationRepository>();

            _mocklocationRepository
            .Setup(m => m.Locations).Returns(new List<Location>
            {
                new Location {LocationId = 1},
                new Location {LocationId = 2}
            });

             _users = new List<Person>
             {
                 new Person() {Firstname = "Eigil", Lastname = "maaalt", SchoolId = 1, PersonType = PersonTypes.Teacher, Id = "1"},
                 new Person() {Firstname = "Jonas", Lastname = "Saxegaard", SchoolId = 1, PersonType = PersonTypes.Teacher, Id = "2"},
                 new Person() {Firstname = "Ivan", Lastname = "Lorenzen", SchoolId = 2, PersonType = PersonTypes.Teacher, Id = "3"}
             };


            var userStore = new Mock<IUserStore<Person>>();
            UserManager<Person> um = new UserManager<Person>(userStore.Object, null, null, null, null, null, null, null, null);
            userStore.Setup(u => u.FindByIdAsync(_users.First().Id, default(CancellationToken))).Returns(new Task<Person>(() => _users.First()));


            _assignmentController = new AssignmentController(_mockAssignmentSetRepository.Object, _mocklocationRepository
            .Object, um);   
        }
        

            [Theory]
            [InlineData(0)]
            [InlineData(1)]
            [InlineData(2)]            
            public void Can_List_Assignments(int i)
            {
                // Arrange 

                AssignmentSetListViewModel model = new AssignmentSetListViewModel()
                {
                    PublicAssignmentSets = _mockAssignmentSetRepository.Object.AssignmentSets.Where(a => a.PublicityLevel == PublicityLevel.Public),
                    InternalAssignmentSets = _mockAssignmentSetRepository.Object.AssignmentSets.Where(a => a.PublicityLevel == PublicityLevel.Internal && a.SchoolId == _users.ElementAt(i).SchoolId),
                    PrivateAssignmentSets = _mockAssignmentSetRepository.Object.AssignmentSets.Where(a => a.PublicityLevel == PublicityLevel.Private && a.CreatorId == _users.ElementAt(i).Id),
                    CurrentSchoolId = _users.ElementAt(i).SchoolId
                };

                // Assert
                
                // User 1
                if(i == 0)
                {
                    // Test for at bruger 1 kan se interne AssignmentSets
                    Assert.Equal(_mockAssignmentSetRepository.Object.AssignmentSets.ElementAt(1), model.InternalAssignmentSets.FirstOrDefault());
                    // Test for at bruger 1 kan se sin private AssignmentSet
                    Assert.Equal(_mockAssignmentSetRepository.Object.AssignmentSets.ElementAt(0), model.PrivateAssignmentSets.FirstOrDefault());
                }
                // User 2
                else if (i == 1)
                {
                    // Tester for om bruger 2 kan se bruger 1's private AssignmentSet
                    Assert.Equal(null, model.PrivateAssignmentSets.FirstOrDefault());
                    Assert.Equal(_mockAssignmentSetRepository.Object.AssignmentSets.ElementAt(i), model.InternalAssignmentSets.FirstOrDefault());
                }
                // User 3
                else
                {
                    // Tester for om bruger 3 kan se bruger 1's private AssignmentSet
                    Assert.Equal(null, model.PrivateAssignmentSets.FirstOrDefault());
                    // Test for at bruger 3 kan se interne AssignmentSets
                    Assert.Equal(null, model.InternalAssignmentSets.FirstOrDefault());
                }
                     // Test for at bruger 1,2 & 3 kan se offentlige AssignmentSet
                    Assert.Equal(_mockAssignmentSetRepository.Object.AssignmentSets.ElementAt(2), model.PublicAssignmentSets.FirstOrDefault());
            }
                


      [Theory]
      [InlineData(1)]
      [InlineData(2)]
      [InlineData(3)]
        public void Can_Edit_Existing_Assignment_HttpGet(int id)
        {
         
            // Act
            AssignmentSet requestedAssignmentSet = (_assignmentController.Edit(id).ViewData.Model as AssignmentSetViewModel).AssignmentSet;

            // Assert

            Assert.Equal(requestedAssignmentSet.AssignmentSetId, id);
            Assert.Equal(requestedAssignmentSet.CreatorId, id.ToString());
            Assert.Equal(requestedAssignmentSet.Title, "Set " + id);
            Assert.Equal(requestedAssignmentSet.Description, "d"+id);
            // Lav test, som tester Assignmentid I assignmentset

        }
        
        [Theory]
        [InlineData(100)]
        [InlineData(200)]
        [InlineData(300)]
        public void Can_Not_Edit_Non_Existing_Assignment_HttpGet(int id)
        {
            // Act
            AssignmentSet requestedAssignmentSet = (_assignmentController.Edit(id).ViewData.Model as AssignmentSetViewModel).AssignmentSet;

            // Assert
            Assert.Equal(requestedAssignmentSet, null);
        }


        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]

        public void Can_Delete_Existing_Assignment(int id)
        {
            // Arrange is done in ctor 
            // Act
            _assignmentController.DeleteAssignmentSet(id);
            // Assert
            _mockAssignmentSetRepository.Verify(m => m.DeleteAssignmentSet(id));
        }


}
    }
