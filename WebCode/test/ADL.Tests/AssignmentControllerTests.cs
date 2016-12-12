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
        private Mock<IAssignmentSetRepository> assignmentSetRepositoryMock;
        private Mock<ILocationRepository> locationRepositoryMock;

        Person currentUser;

        public AssignmentControllerTests()
        {
            assignmentSetRepositoryMock = new Mock<IAssignmentSetRepository>();

            assignmentSetRepositoryMock.Setup(m => m.AssignmentSets).Returns(new AssignmentSet[]
            {
                new AssignmentSet {AssignmentSetId = 1, Title = "Set 1", Description = "d1", Assignments = {new Assignment {Text = "test", AssignmentId = 1234, }}  }

            });

            locationRepositoryMock = new Mock<ILocationRepository>();

            locationRepositoryMock.Setup(m => m.Locations).Returns(new Location[]
            {
                new Location {LocationId = 1},
                new Location {LocationId = 2}
            });

            

            assignmentController = new AssignmentController(assignmentSetRepositoryMock.Object, locationRepositoryMock.Object, );   
        }
    [Fact]
        public void Can_List_Assignments()
        {
            // Arrange is done in ctor 

           // Act
        //    Assignment[] results = (assignmentController.List().ViewData.Model as AssignmentAndLocationListViewModel).Assignments.ToArray();

            // Asser
            /*
            
            Assert.Equal(results.Length, 3);
            Assert.Equal(results[0].AssignmentId, 1);
            Assert.Equal(results[1].AssignmentId, 2);
            Assert.Equal(results[2].AssignmentId, 3);
        */
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void Can_Edit_Existing_Assignment_HttpGet(int id)
        {
            // Act
            Assignment requestedAssignment = assignmentController.Edit(id).ViewData.Model as Assignment;

            // Assert
;
        }

        [Theory]
        [InlineData(100)]
        [InlineData(200)]
        [InlineData(300)]
        public void Can_Not_Edit_Non_Existing_Assignment_HttpGet(int id)
        {
            // Act
            Assignment requestedAssignment = assignmentController.Edit(id).ViewData.Model as Assignment;

            // Assert
            Assert.Equal(requestedAssignment, null);
        }
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
            assignmentSetRepositoryMock.Verify(m => m.DeleteAssignmentSet(id));
        }
        public void Can_AttachAssignment_To_ExistingLocation_HTTPGet(int ChosenAssignmentId)
        {
            // Arrange is done in ctor
            // Act
            AssignmentToLocationAttachment requestedAttachment = assignmentController.AttachAssignmentToLocation(ChosenAssignmentId).ViewData.Model as AssignmentToLocationAttachment;
            requestedAttachment.ChosenAssignmentId = ChosenAssignmentId;
            requestedAttachment.Locations = locationRepositoryMock.Object.Locations;
            // Assert
            Assert.Equal(requestedAttachment.ChosenAssignmentId, ChosenAssignmentId);
            Assert.Null(requestedAttachment.ChosenLocationId);
            
        }
    }
}
