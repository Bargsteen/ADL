using System.ComponentModel.DataAnnotations;

namespace ADL.Models
{
    public class Location
    {
        public int LocationID { get; set; }

        [Required(ErrorMessage = "Venligst indtast en titel")]
        [MinLengthAttribute(2)]
        public string Titel { get; set; }

        [Required(ErrorMessage = "Venligst vælg HVOR lokationen er!")]
        [MinLengthAttribute(2)]
        public string Description { get; set; }
    }
}
