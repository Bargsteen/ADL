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
            Mock<IAssignmentRepository> mockAssigntment = new Mock<IAssignmentRepository>();
            Mock<ILocationRepository> mockLocation = new Mock<ILocationRepository>();

            mockAssigntment.Setup(m => m.Assignments).Returns(new Assignment[]
            {
                new Assignment {Headline = "h1", Question = "Hej"},
                new Assignment {Headline = "h2", Question = "hej2"},
                new Assignment {Headline = "h3", Question = "hej3"}
            });

            AssignmentController controller = new AssignmentController(mockAssigntment.Object, mockLocation.Object);
            // Act
            IEnumerable<Assignment> result =
                   controller.List().ViewData.Model as IEnumerable<Assignment>;

            // Assert
         
            Assignment[] assignArray = result.ToArray();
            Assert.Equal("h1", assignArray[0].Headline);
            Assert.Equal("h3", assignArray[1].Headline);

        }


    }
}