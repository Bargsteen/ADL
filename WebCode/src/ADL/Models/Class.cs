using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ADL.Models
{
    public class Class
    {
        [Key]
        public int ClassId { get; set; }
        [Required]
        public int SchoolId { get; set; }
        [Required]
        [Range(1900, 2100)]
        public int StartYear { get; set; }
        [Required]
        public string Name { get; set; }
        public virtual List<Person> People { get; set; }
    }
}