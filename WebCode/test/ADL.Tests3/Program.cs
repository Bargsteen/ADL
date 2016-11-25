using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADL.Tests
{
    public class Program
    {
        public static void Main(string[] args)
        {
            AssignmentControllerTests act = new AssignmentControllerTests();
            act.Can_List_Assignments();
        }
    }
}
