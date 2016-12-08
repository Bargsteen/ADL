using System.ComponentModel.DataAnnotations;

namespace ADL.Models
{
    public class School
    {
        [Key]
        public int SchoolId {get;set;}
        [Required]
        public string SchoolName { get; set; }
        [Required]
        public int InstitutionNumber { get; set; }

    }
}
