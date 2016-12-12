using ADL.Models;
using ADL.Models.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace ADL.Tests
{
    public class ClassRepositoryTests
    {
        Mock<DbSet<Class>> mockClass;
        Mock<ApplicationDbContext> mockContext;
        EFClassRepository repository;

        public ClassRepositoryTests()
        {
            // Arrange - common
            mockClass = new Mock<DbSet<Class>>();
            mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(m => m.Classes).Returns(mockClass.Object);

            repository = new EFClassRepository(mockContext.Object);


            /*
             var mockSet = new Mock<DbSet<Blog>>(); 
 
            var mockContext = new Mock<BloggingContext>(); 
            mockContext.Setup(m => m.Blogs).Returns(mockSet.Object); 
 
            var service = new BlogService(mockContext.Object); 
            service.AddBlog("ADO.NET Blog", "http://blogs.msdn.com/adonet"); */

        }
        [Fact]
        public void Can_Save_Class()
        {
            // Act
            Class newClass = new Class()
            {
                ClassId = 1,
                SchoolId = 1,
                StartYear = 2015,
                Name = "Test School One"
            };
            repository.SaveClass(newClass);

            // Assert
            mockClass.Verify(m => m.Add(It.IsAny<Class>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }
    }

}
