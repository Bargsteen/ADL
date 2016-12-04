using System.ComponentModel.DataAnnotations;

namespace ADL.Models
{
    public class PersonAssignmentCoupling
    {
        [Key]
        public int PersonAssignmentCouplingId { get; set; }
        [Required]
        public string PersonId { get; set; }
        [Required]
        public int AssignmentId { get; set; }
    }
}