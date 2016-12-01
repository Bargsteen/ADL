using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace ADL.Models
{
    public enum PersonTypes { Teacher, Student };
    public class Person : IdentityUser
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public School School { get; set; }
        public PersonTypes PersonType { get; set; }
    }
}