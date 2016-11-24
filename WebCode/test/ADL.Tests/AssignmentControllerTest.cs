using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using ADL.Models;
using ADL.Controllers;
using ADL.Models.ViewModels;
using Moq;


namespace ADL.Tests
{
    class AssignmentControllerTests
    {

        [Fact]
        public void Can_List_Assignments()
        {

            // Arrange
            Mock<IAssignmentRepository> assignmentMock = new Mock<IAssignmentRepository>();
            Mock<ILocationRepository> locationMock = new Mock<ILocationRepository>();

            assignmentMock.Setup(m => m.Assignments).Returns(new Assignment[]
            {
                new Assignment {Headline = "h1", Question = "q1"},
                new Assignment {Headline = "h2", Question = "q2"},
                new Assignment {Headline = "h3", Question = "q3"}
            });

            AssignmentController controller = new AssignmentController(assignmentMock.Object, locationMock.Object);
            // Act
            IEnumerable<Assignment> result =
                   controller.List().ViewData.Model as IEnumerable<Assignment>;

            // Assert
            Assignment[] resultArray = result.ToArray();
            Assert.Equal(resultArray.Length, 3);
            Assert.Equal("h1", resultArray[0].Headline);
            Assert.Equal("h2", resultArray[1].Headline);
            Assert.Equal("h3", resultArray[2].Headline);
            Assert.Equal("hej", resultArray[2].Question);

        }

        [Fact]
        public void Can_Get_Edit()
        {
            // Arrange
            Mock<IAssignmentRepository> assignmentMock = new Mock<IAssignmentRepository>();
            Mock<ILocationRepository> locationMock = new Mock<ILocationRepository>();

            assignmentMock.Setup(m => m.Assignments).Returns(new Assignment[]
            {
                new Assignment {AssignmentId = 1, Headline = "h1", Question = "q1"},
                new Assignment {AssignmentId = 2, Headline = "h2", Question = "q2"},
                new Assignment {AssignmentId = 3, Headline = "h3", Question = "q3"}
            });

            AssignmentController assignmentController = new AssignmentController(assignmentMock.Object, locationMock.Object);
            // Act
            Assignment a1 = assignmentController.Edit(1).ViewData.Model as Assignment;
            Assignment a2 = assignmentController.Edit(2).ViewData.Model as Assignment;
            Assignment a3 = assignmentController.Edit(3).ViewData.Model as Assignment;

            // Assert
            Assert.Equal(a1.AssignmentId, 1);
            Assert.Equal(a2.Headline, "h2");
            Assert.Equal(a3.Question, "q3");
        }

    }
}
