using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace E_commerce.Models
{
    public class ApplicationUser:IdentityUser
    {
        [Required]
        [MaxLength(255)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(255)]
        public string LastName { get; set; }
       
    }
}
