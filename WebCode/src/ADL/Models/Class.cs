using System.Collections.Generic;

namespace ADL.Models
{
    public class Class
    {
        public int ClassId { get; set; }
        public int SchoolId { get; set; }
        public int StartYear { get; set; }
        public string Name { get; set; }
        public List<Person> People { get; set; }
    }
}