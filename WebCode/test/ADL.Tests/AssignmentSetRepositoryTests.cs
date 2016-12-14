using System;
using System.Collections.Generic;
using System.Linq;
using ADL.Models;
using ADL.Models.Assignments;
using ADL.Models.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;
using static ADL.Models.EnumCollection;

namespace ADL.Tests
{
    public class AssignmentSetRepositoryTests
    {
        SqliteConnection connection = new SqliteConnection("DataSource=:memory:");
        [Fact]
        public void Can_SaveNewAssignmentSet()
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
                    var repository = new EFAssignmentSetRepository(context);
                    AssignmentSet newAssignmentSet = new AssignmentSet()
                    {
                        Title = "NewSet",
                        Description = "TestDescription",
                        PublicityLevel = PublicityLevel.Private,
                        CreatorId = "fakeId",
                        SchoolId = 0,
                        LastUpdateDate = DateTime.Now,
                        Assignments = new List<Assignment>()
                        {
                            new Assignment()
                            {
                                Type = AssignmentType.Text,
                                Text = "TextAssignmentText"
                            }
                        }
                    };

                    repository.SaveAssignmentSet(newAssignmentSet);
                }

                // Assert
                // Use a separate instance of the context to verify correct data was saved to database
                using (var context = new ApplicationDbContext(options))
                {
                    AssignmentSet assignmentSetFromDb = context.AssignmentSets.Single();
                    Assert.Equal(assignmentSetFromDb.Title, "NewSet");
                    Assert.Equal(assignmentSetFromDb.Assignments.Single().Text, "TextAssignmentText");

                }
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
