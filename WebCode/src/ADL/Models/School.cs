using System.ComponentModel.DataAnnotations;

namespace ADL.Models
{
    public class School
    {
        [Key]
        public int SchoolId {get;set;}
        public string SchoolName { get; set; }
        public int InstitutionNumber { get; set; }

    }
}
