using CoursesP2P.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace CoursesP2P.Models
{
    public class Payment
    {
        public int Id { get; set; }

        [Required]
        public string StudentEmail { get; set; }

        [Required]
        public string PaymentId { get; set; }

        public decimal Amount { get; set; }

        [Required]
        public string StudentId { get; set; }

        public User Student { get; set; }

        public PaymentStatus Status { get; set; }
    }
}
