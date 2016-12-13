using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ADL.Models;
using ADL.Models.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using static ADL.Models.EnumCollection;

namespace ADL.Tests
{
    public class ClassRepositoryTests
    {
        SqliteConnection connection = new SqliteConnection("DataSource=:memory:");
        [Fact]
        public void Can_Save_OneNewClass()
        {

            // Arrange 

            // In-memory database only exists while the connection is open 
            connection.Open();

            try
            {
                var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseSqlite(connection)
                    .Options;

                // Create the schema in the database
                using (var context = new ApplicationDbContext(options))
                {
                    context.Database.EnsureCreated();
                }

                // Act
                using (var context = new ApplicationDbContext(options))
                {
                    var repository = new EFClassRepository(context);
                    Class newClass = new Class()
                    {
                        ClassId = 0,
                        SchoolId = 1,
                        StartYear = 2015,
                        Name = "newClass"
                    };
                    repository.SaveClass(newClass);
                }

                // Assert
                // Use a separate instance of the context to verify correct data was saved to database
                using (var context = new ApplicationDbContext(options))
                {
                    Assert.Equal(1, context.Classes.Count());
                    Assert.Equal("newClass", context.Classes.Single().Name);
                }
            }
            finally
            {
                connection.Close();
            }
        }

        /* [Fact]
         public void Can_Retrieve_Classes_With_Included_People()
         {

             // Arrange 

             // In-memory database only exists while the connection is open 
             connection.Open();

             try
             {
                 var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                     .UseSqlite(connection)
                     .Options;

                 // Create the schema in the database
                 using (var context = new ApplicationDbContext(options))
                 {
                     context.Database.EnsureCreated();
                 }

                 // Act
                 using (var context = new ApplicationDbContext(options))
                 {
                     var repository = new EFClassRepository(context);
                     Class newClass = new Class()
                     {
                         ClassId = 0,
                         SchoolId = 1,
                         StartYear = 2015,
                         Name = "newClass"
                     };
                     repository.SaveClass(newClass);
                 }

                 // Assert
                 // Use a separate instance of the context to verify correct data was saved to database
                 using (var context = new ApplicationDbContext(options))
                 {
                     Class classFromDb = context.Classes.Single();
                     Assert.Equal("newClass", classFromDb.Name);
                     Assert.NotNull(classFromDb.People);
                 }
             }
             finally
             {
                 connection.Close();
             }
         }*/



        [Fact]
        public void Can_Save_MultipleNewClasses()
        {

            // Arrange 

            // In-memory database only exists while the connection is open 
            connection.Open();

            try
            {
                var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseSqlite(connection)
                    .Options;

                // Create the schema in the database
                using (var context = new ApplicationDbContext(options))
                {
                    context.Database.EnsureCreated();
                }

                // Act
                using (var context = new ApplicationDbContext(options))
                {
                    var repository = new EFClassRepository(context);
                    Class classOne = new Class()
                    {
                        ClassId = 0,
                        SchoolId = 1,
                        StartYear = 2015,
                        Name = "classOne"
                    };
                    Class classTwo = new Class()
                    {
                        ClassId = 0,
                        SchoolId = 1,
                        StartYear = 2016,
                        Name = "classTwo"
                    };

                    repository.SaveClass(classOne);
                    repository.SaveClass(classTwo);

                }

                // Assert
                // Use a separate instance of the context to verify correct data was saved to database
                using (var context = new ApplicationDbContext(options))
                {
                    Assert.Equal(2, context.Classes.Count());
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public void Can_AutoIncrement_ClassId_Starting_At_One()
        {

            // Arrange 

            // In-memory database only exists while the connection is open 
            connection.Open();

            try
            {
                var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseSqlite(connection)
                    .Options;

                // Create the schema in the database
                using (var context = new ApplicationDbContext(options))
                {
                    context.Database.EnsureCreated();
                }

                // Act
                using (var context = new ApplicationDbContext(options))
                {
                    var repository = new EFClassRepository(context);
                    Class classOne = new Class()
                    {
                        ClassId = 0,
                        SchoolId = 1,
                        StartYear = 2015,
                        Name = "classOne"
                    };
                    Class classTwo = new Class()
                    {
                        ClassId = 0,
                        SchoolId = 1,
                        StartYear = 2016,
                        Name = "classTwo"
                    };

                    repository.SaveClass(classOne);
                    repository.SaveClass(classTwo);

                }

                // Assert
                // Use a separate instance of the context to verify correct data was saved to database
                using (var context = new ApplicationDbContext(options))
                {
                    Assert.Equal(2, context.Classes.Count());
                    Assert.Equal(1, context.Classes.FirstOrDefault(c => c.Name == "classOne")?.ClassId);
                    Assert.Equal(2, context.Classes.FirstOrDefault(c => c.Name == "classTwo")?.ClassId);
                }
            }
            finally
            {
                connection.Close();
            }
        }


        [Fact]
        public void Can_DeleteClass()
        {

            // Arrange 

            // In-memory database only exists while the connection is open 
            connection.Open();

            try
            {
                var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseSqlite(connection)
                    .Options;

                // Create the schema in the database
                using (var context = new ApplicationDbContext(options))
                {
                    context.Database.EnsureCreated();
                }

                // Save new class
                using (var context = new ApplicationDbContext(options))
                {
                    var repository = new EFClassRepository(context);
                    Class classToBeDeleted = new Class()
                    {
                        ClassId = 0,
                        SchoolId = 1,
                        StartYear = 2015,
                        Name = "classToBeDeleted"
                    };
                    Class classToStay = new Class()
                    {
                        ClassId = 0,
                        SchoolId = 1,
                        StartYear = 2015,
                        Name = "classToStay"
                    };
                    repository.SaveClass(classToBeDeleted);
                    repository.SaveClass(classToStay);

                    // Act
                    repository.DeleteClass(1); // Can_AutoIncrement_ClassId_Starting_At_One shows that it will get id = 1
                }

                // Assert
                // Use a separate instance of the context to verify correct data was saved to database
                using (var context = new ApplicationDbContext(options))
                {
                    Assert.Equal(1, context.Classes.Count());
                    Assert.Equal("classToStay", context.Classes.Single().Name); // Takes first one. And this should only be classToStay now.
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public async void Can_Add_People_To_Class()
        {

            // Arrange 
            connection = new SqliteConnection("Filename=./ADLTEST.db");
            // In-memory database only exists while the connection is open 
            connection.Open();

            try
            {
                var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseSqlite(connection)
                    .Options;

                // Create the schema in the database
                using (var context = new ApplicationDbContext(options))
                {
                    context.Database.EnsureCreated();
                }

                // Save new class
                using (var context = new ApplicationDbContext(options))
                {
                    var classRepository = new EFClassRepository(context);
                    UserStore<Person> userStore = new UserStore<Person>(context);
                    UserManager<Person> userManager = new UserManager<Person>(userStore, null, null, null, null, null, null, null, null);

                    Person newPerson = new Person
                    {
                        Firstname = "John",
                        Lastname = "Doe",
                        PersonType = PersonTypes.Student,
                        SchoolId = 1,
                        UserName = "johndoe",
                        Email = "john@doe.com"
                    };

                    userManager.CreateAsync(newPerson).Wait();

                    var personFromDb = await userManager.FindByNameAsync("johndoe");

                    Class theClass = new Class()
                    {
                        ClassId = 0,
                        SchoolId = 1,
                        StartYear = 2015,
                        Name = "theClass",
                        People = new List<Person>() { personFromDb }
                    };

                    classRepository.SaveClass(theClass);
                }

                /*using (var context = new ApplicationDbContext(options))
                {
                    var classRepository = new EFClassRepository(context);
                    UserStore<Person> userStore = new UserStore<Person>(context);
                    UserManager<Person> userManager = new UserManager<Person>(userStore, null, null, null, null, null, null, null, null);

                    // Act 
                    Person personFromDb = await userManager.FindByNameAsync("johndoe");
                    classRepository.AddPersonToClass(classRepository.Classes.Single().ClassId, personFromDb);
                }*/

                // Assert
                // Use a separate instance of the context to verify correct data was saved to database
                using (var context = new ApplicationDbContext(options))
                {
                    Class classFromDb = context.Classes.Single();
                    Assert.NotNull(classFromDb);
                    Assert.NotNull(classFromDb.People);
                }
            }
            finally
            {
                connection.Close();
            }
        }






    }
}