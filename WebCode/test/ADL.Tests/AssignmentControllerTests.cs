using Xunit;
using ADL.Models;
<<<<<<< HEAD
using ADL.Models.Assignments;
using ADL.Models.Repositories;
using System.Linq;
using System.Collections.Generic;
=======
using Moq;
>>>>>>> adcd4d38632890ace3a62baa868c9f8a286e09d0
using ADL.Models.ViewModels;
using ADL.Models.Repositories;
using Microsoft.AspNetCore.Identity;
<<<<<<< HEAD
using System.Threading.Tasks;
using static ADL.Models.EnumCollection;
using Moq;
using ADL.Controllers;

=======
using ADL.Models.Assignments;
>>>>>>> adcd4d38632890ace3a62baa868c9f8a286e09d0

namespace ADL.Tests
{

    public class AssignmentControllerTests
    {
        private Mock<IAssignmentSetRepository> assignmentSetRepositoryMock;
        private Mock<ILocationRepository> locationRepositoryMock;

<<<<<<< HEAD
        Person currentUser;

=======
        private Mock<UserManager<Person>> userManager;
        private AssignmentController assignmentController;
>>>>>>> adcd4d38632890ace3a62baa868c9f8a286e09d0
        public AssignmentControllerTests()
        {
            assignmentSetRepositoryMock = new Mock<IAssignmentSetRepository>();

            assignmentSetRepositoryMock.Setup(m => m.AssignmentSets).Returns(new AssignmentSet[]
            {
<<<<<<< HEAD
                new AssignmentSet {AssignmentSetId = 1, Title = "Set 1", Description = "d1", Assignments = {new Assignment {Text = "test", AssignmentId = 1234, }}  }
=======
                new AssignmentSet {AssignmentSetId = 1, Title = "Set 1", Description = "d1", Assignments = {new Assignment { AssignmentId = 1234, }}  }
>>>>>>> adcd4d38632890ace3a62baa868c9f8a286e09d0

            });

            locationRepositoryMock = new Mock<ILocationRepository>();

            locationRepositoryMock.Setup(m => m.Locations).Returns(new Location[]
            {
                new Location {LocationId = 1},
                new Location {LocationId = 2}
            });

<<<<<<< HEAD
            

            assignmentController = new AssignmentController(assignmentSetRepositoryMock.Object, locationRepositoryMock.Object, );   
=======
            assignmentController = new AssignmentController(assignmentSetRepositoryMock.Object, locationRepositoryMock.Object, userManager.Object);
>>>>>>> adcd4d38632890ace3a62baa868c9f8a286e09d0
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
            /*AssignmentToLocationAttachment requestedAttachment = assignmentController.AttachAssignmentToLocation(ChosenAssignmentId).ViewData.Model as AssignmentToLocationAttachment;
            requestedAttachment.ChosenAssignmentId = ChosenAssignmentId;
            requestedAttachment.Locations = locationRepositoryMock.Object.Locations;
            // Assert
            Assert.Equal(requestedAttachment.ChosenAssignmentId, ChosenAssignmentId);
            Assert.Null(requestedAttachment.ChosenLocationId);*/

        }
    }
}
