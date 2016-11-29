using Xunit;
using ADL.Controllers;
using ADL.Models;
using Moq;
using System.Collections.Generic;
using System.Linq;
using ADL.Models.ViewModels;

namespace ADL.Tests
{
    public class LocationControllerTests
    {
        private Mock<ILocationRepository> locationRepositoryMock;
        private LocationController LocationController;

        public LocationControllerTests()
        {
            locationRepositoryMock = new Mock<ILocationRepository>();

            locationRepositoryMock.Setup(m => m.Locations).Returns(new Location[]
            {
                new Location {LocationId = 1, Title = "t1", Description = "d1"},
                new Location {LocationId = 2, Title = "t2", Description = "d2"},
                new Location {LocationId = 3, Title = "t3", Description = "d3"}
            });


            LocationController = new LocationController(locationRepositoryMock.Object);   
        }

        [Fact]
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

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void Can_Edit_Existing_Location_HttpGet(int id)
        {

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
            // Act
            Location requestedLocation = LocationController.Edit(id).ViewData.Model as Location;
            // Assert
            Assert.Equal(requestedLocation, null);
        }
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void Can_Delete_Existing_Location(int id)
        {
            // Act
            LocationController.Delete(id);
            // Assert
            locationRepositoryMock.Verify(m => m.DeleteLocation(id));
        }
    }

}