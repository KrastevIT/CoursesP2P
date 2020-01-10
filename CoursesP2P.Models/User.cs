using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CoursesP2P.Models
{
    public class User : IdentityUser
    {
        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [Range(0, 120)]
        public int Age { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string City { get; set; }
    }
}
