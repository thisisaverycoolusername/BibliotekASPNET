using System.ComponentModel.DataAnnotations;

namespace LibraryDb.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        [Required]
        [StringLength(70, MinimumLength = 1, ErrorMessage = "Title should be between 1 and 70 charaters")]
        public string Title { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Description should be between 1 and 100 charaters")]
        public string Description { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Author name should be between 1 and 50 charaters")]
        public string Author { get; set; }
        [Range(1, 1000, ErrorMessage = "Number of pages of book should be between 1 and 1000 charaters")]
        public int NumberOfPages { get; set; }
        public bool? IsReturned { get; set; }
        public ICollection<CustomerBook>? CustomerBooks { get; set; }
    }
}
