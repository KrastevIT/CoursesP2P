using System.ComponentModel.DataAnnotations;

namespace CoursesP2P.Models
{
    public class PaymentToInstructor
    {
        public int Id { get; set; }

        [Required]
        public string InstructorEmail { get; set; }

        public decimal Amount { get; set; }

        public string InstructorId { get; set; }

        [Required]
        public User Instructor { get; set; }
    }
}
