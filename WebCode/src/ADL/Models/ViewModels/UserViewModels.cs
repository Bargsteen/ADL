using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace ADL.Models {
    public class CreateModel {
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }
        [Required]
        public PersonTypes PersonType { get; set; }
        [Required]
        public int SchoolId { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public IEnumerable<School> AvailableSchools { get; set; }
    }

    public class LoginModel
    {
        [Required]
        [UIHint("username")]
        public string Username { get; set; }
        [Required]
        [UIHint("password")]
        public string Password { get; set; }
    }
}
