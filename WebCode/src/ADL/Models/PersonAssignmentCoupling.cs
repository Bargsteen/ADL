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

        public override int GetHashCode()
        {
            return PersonId.GetHashCode() + AssignmentId.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return this.GetHashCode() == obj.GetHashCode();
        }
    }   
}