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
        private Mock<IAssignmentSetRepository> mockAssignmentSetRepository;
        private Mock<ILocationRepository> MocklocationRepository;
        private AssignmentController assignmentController;
        UserManager<Person> userManager;


        public AssignmentControllerTests()
        {
            mockAssignmentSetRepository = new Mock<IAssignmentSetRepository>();

            mockAssignmentSetRepository.Setup(m => m.AssignmentSets).Returns(new[]
             {
                new AssignmentSet {AssignmentSetId = 1, Title = "Set 1", Description = "d1",  CreatorId = "1", Assignments = new List<Assignment>(){new Assignment { AssignmentId = 1, Text = "Test1"}}  },
                new AssignmentSet {AssignmentSetId = 2, Title = "Set 2", Description = "d2", CreatorId = "2", Assignments = new List<Assignment>(){new Assignment { AssignmentId = 2, Text = "Test2"}}  },
                new AssignmentSet {AssignmentSetId = 3, Title = "Set 3", Description = "d3", CreatorId = "3", Assignments = new List<Assignment>(){new Assignment { AssignmentId = 3, Text = "Test3"}}  }

             });
            
            MocklocationRepository
             = new Mock<ILocationRepository>();

            MocklocationRepository
            .Setup(m => m.Locations).Returns(new List<Location>
            {
                new Location {LocationId = 1},
                new Location {LocationId = 2}
            });

             List<Person> users = new List<Person>
             {
                 new Person() {Firstname = "Eigil", Lastname = "maaalt", SchoolId = 1, PersonType = PersonTypes.Student, Id = "1"},
                 new Person() {Firstname = "Jonas", Lastname = "Saxegaard", SchoolId = 1, PersonType = PersonTypes.Student, Id = "2"},
                 new Person() {Firstname = "Ivan", Lastname = "Lorenzen", SchoolId = 2, PersonType = PersonTypes.Student, Id = "3"}
             };

            var userStore = new Mock<IUserStore<Person>>();
            UserManager<Person> um = new UserManager<Person>(userStore.Object, null, null, null, null, null, null, null, null);
            userStore.Setup(u => u.FindByIdAsync(users.First().Id, default(CancellationToken))).Returns(new Task<Person>(() => users.First()));


            assignmentController = new AssignmentController(mockAssignmentSetRepository.Object, MocklocationRepository
            .Object, um);   
        }
        /*
                public void Can_List_Assignments()
                {
                    // Arrange is done in ctor 

                   // Act
                //    Assignment[] results = (assignmentController.List().ViewData.Model as AssignmentAndLocationListViewModel).Assignments.ToArray();

                    // Asser


                    Assert.Equal(results.Length, 3);
                    Assert.Equal(results[0].AssignmentId, 1);
                    Assert.Equal(results[1].AssignmentId, 2);
                    Assert.Equal(results[2].AssignmentId, 3);

                }
                */
/*

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void Can_Edit_Existing_Assignment_HttpGet(int id)
        {
            // Act
            AssignmentSet requestedAssignmentSet = assignmentController.Edit(id).ViewData.Model as AssignmentSet;

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
            AssignmentSet requestedAssignmentSet = assignmentController.Edit(id).ViewData.Model as AssignmentSet;

            // Assert
            Assert.Equal(requestedAssignmentSet, null);
        }

/*
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]

        
        public void Can_Delete_Existing_Assignment(int id)
        {
            // Arrange is done in ctor 
            // Act
            assignmentController.DeleteAssignmentSet(id);
            // Assert
            mockAssignmentSetRepository.Verify(m => m.DeleteAssignmentSet(id));
        }
*/


        /*
        public void Can_AttachAssignment_To_ExistingLocation_HTTPGet(int ChosenAssignmentId)
        {
            // Arrange is done in ctor
            // Act
            AssignmentToLocationAttachment requestedAttachment = assignmentController.AttachAssignmentToLocation(ChosenAssignmentId).ViewData.Model as AssignmentToLocationAttachment;
            requestedAttachment.ChosenAssignmentId = ChosenAssignmentId;
            requestedAttachment.Locations = MocklocationRepository
            .Object.Locations;
            // Assert
            Assert.Equal(requestedAttachment.ChosenAssignmentId, ChosenAssignmentId);
            Assert.Null(requestedAttachment.ChosenLocationId);
            
        }
        */
    }
}
