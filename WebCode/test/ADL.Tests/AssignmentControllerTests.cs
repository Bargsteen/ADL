using Xunit;
using ADL.Models;
using ADL.Models.Assignments;
using ADL.Models.Repositories;
using System.Linq;
using System.Collections.Generic;
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
        private Mock<IAssignmentSetRepository> MockassignmentSetRepository;
        private Mock<ILocationRepository> MocklocationRepository;
        private AssignmentController assignmentController;
        UserManager<Person> userManager;


        public AssignmentControllerTests()
        {
            MockassignmentSetRepository = new Mock<IAssignmentSetRepository>();

            MockassignmentSetRepository.Setup(m => m.AssignmentSets).Returns(new AssignmentSet[]
            {
                new AssignmentSet {AssignmentSetId = 1, Title = "Set 1", Description = "d1", CreatorId = "1" , SchoolId = 1, PublicityLevel = PublicityLevel.Internal,  Assignments = {new Assignment {Text = "test1a", AssignmentId = 1 }, new Assignment {Text = "test1b", AssignmentId = 2}} },
                new AssignmentSet {AssignmentSetId = 2, Title = "Set 2", Description = "d2", CreatorId = "2" , SchoolId = 1, PublicityLevel = PublicityLevel.Private, Assignments = {new Assignment {Text = "test2a", AssignmentId = 3 }, new Assignment {Text = "test2b", AssignmentId = 4}} },
                new AssignmentSet {AssignmentSetId = 3, Title = "Set 3", Description = "d3", CreatorId = "3" , SchoolId = 2, PublicityLevel = PublicityLevel.Public, Assignments = {new Assignment {Text = "test3a", AssignmentId = 5 }, new Assignment {Text = "test3b", AssignmentId = 6}} }

            });

            MocklocationRepository
             = new Mock<ILocationRepository>();

            MocklocationRepository
            .Setup(m => m.Locations).Returns(new Location[]
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
            

            assignmentController = new AssignmentController(MockassignmentSetRepository.Object, MocklocationRepository
            .Object, userManager);   
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
        [Fact]
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
            MockassignmentSetRepository.Verify(m => m.DeleteAssignmentSet(id));
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
