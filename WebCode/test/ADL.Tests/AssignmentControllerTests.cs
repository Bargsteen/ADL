using Xunit;
using ADL.Controllers;
using ADL.Models;
using Moq;
using System.Collections.Generic;
using System.Linq;
using ADL.Models.ViewModels;

namespace ADL.Tests
{
    public class AssignmentControllerTests
    {
        private Mock<IAssignmentRepository> assignmentRepositoryMock;
        private Mock<ILocationRepository> locationRepositoryMock;
        private AssignmentController assignmentController;
        public AssignmentControllerTests()
        {
            assignmentRepositoryMock = new Mock<IAssignmentRepository>();

            assignmentRepositoryMock.Setup(m => m.Assignments).Returns(new Assignment[]
            {
                new Assignment {AssignmentId = 1, Headline = "h1", Question = "q1"},
                new Assignment {AssignmentId = 2, Headline = "h2", Question = "q2"},
                new Assignment {AssignmentId = 3, Headline = "h3", Question = "q3"}
            });

            locationRepositoryMock = new Mock<ILocationRepository>();

            locationRepositoryMock.Setup(m => m.Locations).Returns(new Location[]
            {
                new Location {LocationId = 1},
                new Location {LocationId = 2}
            });

            assignmentController = new AssignmentController(assignmentRepositoryMock.Object, locationRepositoryMock.Object);   
        }
        [Fact]
        public void Can_List_Assignments()
        {
            // Arrange is done in ctor 

            // Act
            Assignment[] results = (assignmentController.List().ViewData.Model as AssignmentAndLocationListViewModel).Assignments.ToArray();

            // Assert
            Assert.Equal(results.Length, 3);
            Assert.Equal(results[0].AssignmentId, 1);
            Assert.Equal(results[1].AssignmentId, 2);
            Assert.Equal(results[2].AssignmentId, 3);
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
            Assert.Equal(requestedAssignment.AssignmentId, id);
            Assert.Equal(requestedAssignment.Headline, "h" + id);
            Assert.Equal(requestedAssignment.Question, "q" + id);
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

        
    }
}
