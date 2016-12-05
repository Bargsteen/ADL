using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using static ADL.Models.EnumCollection;

namespace ADL.Models
{

    public class Person : IdentityUser
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int SchoolId { get; set; }
        public PersonTypes PersonType { get; set; }
    }
}