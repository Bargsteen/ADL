using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using static ADL.Models.EnumCollection;

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

    public class EditModel {
        public string Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        [Required]
        public PersonTypes PersonType { get; set; }
        [Required]
        public int SchoolId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class RoleEditModel
    {
        public IdentityRole Role { get; set; }
        public IEnumerable<Person> Members { get; set; }
        public IEnumerable<Person> NonMembers { get; set; }
    }
    public class RoleModificationModel
    {
        [Required]
        public string RoleName { get; set; }
        public string RoleId { get; set; }
        public string[] IdsToAdd { get; set; }
        public string[] IdsToDelete { get; set; }
    }
}
