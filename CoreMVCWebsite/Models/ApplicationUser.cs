using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CoreMVCWebsite.Models
{
    public class ApplicationUser:IdentityUser       // We created ApplicationUser Model to make more columns in AspNerUsers Table. 
    {
        [Required]
        public string Name { get; set; }
        public string? Address { get; set; }
    }
}
