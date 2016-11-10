using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADLApp.Model
{
    public class Item
    {
        public int assignmentID { get; }
        public string headline { get; }
        public string question { get; }
        public string[] answerOptions { get; }
        public int correctAnswer { get; }
        public Item()
        {
        }
    }
}
