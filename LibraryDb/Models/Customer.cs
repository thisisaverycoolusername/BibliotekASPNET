using System.ComponentModel.DataAnnotations;

namespace LibraryDb.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name should be between 2 and 50 characters")]
        public string Name { get; set; }
        [Range(12, 100, ErrorMessage = "Error:Age should between 12 and 100. Minimum age for borrowing books is 12. If you're over 100, please enter 100")]
        public int Age { get; set; }
        [StringLength(6, MinimumLength = 4, ErrorMessage = "Gender should be between 4 and 6 characters")]
        public string Gender { get; set; }
        [StringLength(60, MinimumLength = 5, ErrorMessage = "Email should be between 5 and 60 characters")]
        public string Email { get; set; }
        public ICollection<CustomerBook>? CustomerBooks { get; set; }
    }
}
