using Xunit;
using ADL.Controllers;
using ADL.Models;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using ADL.Models.Repositories;
using Microsoft.AspNetCore.Identity;



namespace ADL.Tests
{
    /*public class LocationControllerTests
    {
        private Mock<ILocationRepository> locationRepositoryMock;
        private LocationController LocationController;

        UserManager<Person> userManager;

        public LocationControllerTests()
        {
            locationRepositoryMock = new Mock<ILocationRepository>();

            locationRepositoryMock.Setup(m => m.Locations).Returns(new Location[]
            {
                new Location {LocationId = 1, Title = "t1", Description = "d1"},
                new Location {LocationId = 2, Title = "t2", Description = "d2"},
                new Location {LocationId = 3, Title = "t3", Description = "d3"}
            });


            LocationController = new LocationController(locationRepositoryMock.Object, userManager);   
        }

        [Fact]

        /*
        public void Can_List_Location()
        {
            // Arrange is done in ctor 

            // Act
            Location[] results = (LocationController.List().ViewData.Model as IEnumerable<Location>).ToArray();

            // Asser
            Assert.Equal(results.Length, 3);
            Assert.Equal(results[0].LocationId, 1);
            Assert.Equal(results[1].LocationId, 2);
            Assert.Equal(results[2].LocationId, 3);
        }
*/
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void Can_Edit_Existing_Location_HttpGet(int id)
        {
            // Arrange is done in ctor 

            // Act
            Location requestedLocation = LocationController.Edit(id).ViewData.Model as Location;
            // Assert
            Assert.Equal(requestedLocation.LocationId, id);
            Assert.Equal(requestedLocation.Title, "t" + id);
            Assert.Equal(requestedLocation.Description, "d" + id);
        }
        [Theory]
        [InlineData(100)]
        [InlineData(200)]
        [InlineData(300)]
        public void Can_Not_Edit_Non_Existing_Location_HttpGet(int id)
        {
            // Arrange is done in ctor 

            // Act
            Location requestedLocation = LocationController.Edit(id).ViewData.Model as Location;
            // Assert
            Assert.Equal(requestedLocation, null);
        }
        [Fact]

        /*
        public void Can_Save_Valid_Changes() {
            // Arrange - create mock repository
            Mock<ILocationRepository> mock = new Mock<ILocationRepository>();
            // Arrange - create mock temp data
            Mock<ITempDataDictionary> tempData = new Mock<ITempDataDictionary>();
            // Arrange - create the controller
            LocationController target = new LocationController(mock.Object) {
                TempData = tempData.Object
            };
            // Arrange - create a product
            Location location = new Location { Title = "Test" };
            // Act - try to save the product
            IActionResult result = target.Edit(location);
            // Assert - check that the repository was called
            mock.Verify(m => m.SaveLocation(location));
            // Assert - check the result type is a redirection
            Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("List", (result as RedirectToActionResult).ActionName);
        }

        */
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void Can_Delete_Existing_Location(int id)
        {
            // Arrange is done in ctor 
            // Act
            LocationController.Delete(id);
            // Assert
            locationRepositoryMock.Verify(m => m.DeleteLocation(id));
        }
        [Theory]
        [InlineData(300)]
        [InlineData(276)]
        [InlineData(69)]
        public void Can_Not_Delete_Non_Existing_Location(int id)
        {
            // Arrange is done in ctor
            // Act
            LocationController.Delete(id);
            // Assert
            locationRepositoryMock.Verify(m => m.DeleteLocation(id));
        }
    }
*/
}