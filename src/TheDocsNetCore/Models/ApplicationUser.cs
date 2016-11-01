using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TheDocsNetCore.Models
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(30)]
        public string FirstName { get; set; }
        [MaxLength(30)]
        public string Surname { get; set; }
    }
}
