using System.ComponentModel.DataAnnotations;

namespace ADL.Models
{
    public class Location
    {
        public int LocationId { get; set; }

        [Required(ErrorMessage = "Venligst indtast en titel")]
        [MinLengthAttribute(2)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Venligst vælg HVOR lokationen er!")]
        [MinLengthAttribute(2)]
        public string Description { get; set; }
        public int AttachedAssignmentSetId { get; set; }
        public int AttachedAssignmentId { get; set; }
    }
}
