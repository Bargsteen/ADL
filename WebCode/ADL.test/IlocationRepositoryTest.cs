using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ADL;
using Moq;
using ADL.Models;
using ADL.Controllers;

namespace ADL.test
{
   
    public class LocationControllerTest
    { 
        [Fact]
    
         public void ShouldSaveLocationsWithIdandAssignment()
        {
            //Arrange
            Mock<IAssignmentRepository> MockLocation = new Mock<IAssignmentRepository>();

            return;
        }


    }
}
