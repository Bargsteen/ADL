using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using ADL.controllers;
using Moq;


namespace ADL.Tests
{
    class AssignmentControllerTests

    {
        [Fact]
        public void Can_List_Assignments()
        {
            // Arrange
            Mock<IAssignmentRepository> mock = new Mock<IAssignmentRepository>();
            mock.Assignments.Returns(Assignment[3] = new Assignment { Headline = "A1", Headline = "A2", Headline = "A3" });




            /* { 
                 new Assignment() = { Headline = "A1"};
                 new Assignment() = { Headline = "A2"};
                 new Assignment() = { Headline = "A3"};
             }
             */

            AssignmentController controller = new AssignmentController(mock.Object());
        

            // Act
            Assignment[] result = (controller.List() as ViewModel).ToArray();

            // Assert
            Assert.Equal(result.Length, 3);
            Assert.True(result[0].Headline == "A1");
            Assert.True(result[1].Headline == "A2");
            Assert.True(result[2].Headline == "A3");



        }


    }
}