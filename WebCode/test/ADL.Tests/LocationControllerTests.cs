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
using System.Threading;
using System.Threading.Tasks;
using static ADL.Models.EnumCollection;

namespace ADL.Tests
{
    public class LocationControllerTests
    {
        private readonly Mock<ILocationRepository> _locationRepositoryMock;
        private readonly LocationController _locationController;

        UserManager<Person> _userManager;
        private static List<Person> _users;

        public LocationControllerTests()
        {

            _locationRepositoryMock = new Mock<ILocationRepository>();

            _locationRepositoryMock.Setup(m => m.Locations).Returns(new Location[]
            {
                new Location {LocationId = 1, Title = "t1", Description = "d1", SchoolId = 1},
                new Location {LocationId = 2, Title = "t2", Description = "d2", SchoolId = 1},
                new Location {LocationId = 3, Title = "t3", Description = "d3", SchoolId = 2}
            });

            _users = new List<Person>
             {
                 new Person() {Firstname = "Eigil", Lastname = "maaalt", SchoolId = 1, PersonType = PersonTypes.Teacher, Id = "1"},
                 new Person() {Firstname = "Jonas", Lastname = "Saxegaard", SchoolId = 1, PersonType = PersonTypes.Teacher, Id = "2"},
                 new Person() {Firstname = "Ivan", Lastname = "Lorenzen", SchoolId = 2, PersonType = PersonTypes.Teacher, Id = "3"}
             };

            var userStore = new Mock<IUserStore<Person>>();
            UserManager<Person> um = new UserManager<Person>(userStore.Object, null, null, null, null, null, null, null, null);
            userStore.Setup(u => u.FindByIdAsync(_users.First().Id, default(CancellationToken))).Returns(new Task<Person>(() => _users.First()));


            _locationController = new LocationController(_locationRepositoryMock.Object, _userManager);   
        }

       [Theory]
       [InlineData(1)]
       [InlineData(2)]
       [InlineData(3)]
       public void Can_Edit_Existing_Location_HttpGet(int id)
       {
           // Arrange is done in ctor 

           // Act
           Location requestedLocation = _locationController.Edit(id).ViewData.Model as Location;
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
           Location requestedLocation = _locationController.Edit(id).ViewData.Model as Location;
           // Assert
           Assert.Equal(requestedLocation, null);
       }
       [Fact]
       public void Can_Save_Valid_Changes() {
            var userStore = new Mock<IUserStore<Person>>();
            UserManager<Person> um = new UserManager<Person>(userStore.Object, null, null, null, null, null, null, null, null);
            userStore.Setup(u => u.FindByIdAsync(_users.First().Id, default(CancellationToken))).Returns(new Task<Person>(() => _users.First()));

           // Arrange - create mock repository
           Mock<ILocationRepository> mock = new Mock<ILocationRepository>();
           // Arrange - create mock temp data
           Mock<ITempDataDictionary> tempData = new Mock<ITempDataDictionary>();
           // Arrange - create the controller
           LocationController target = new LocationController(mock.Object, um) {
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

       
       [Theory]
       [InlineData(1)]
       [InlineData(2)]
       [InlineData(3)]
       public void Can_Delete_Existing_Location(int id)
       {
           // Arrange is done in ctor 
           // Act
           _locationController.Delete(id);
           // Assert
           _locationRepositoryMock.Verify(m => m.DeleteLocation(id));
       }

       [Theory]
       [InlineData(300)]
       [InlineData(276)]
       [InlineData(69)]
       public void Can_Not_Delete_Non_Existing_Location(int id)
       {
           // Arrange is done in ctor
           // Act
           _locationController.Delete(id);
           // Assert
           _locationRepositoryMock.Verify(m => m.DeleteLocation(id));
       }

    }
}