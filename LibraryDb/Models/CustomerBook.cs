using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LibraryDb.Models
{
    public class CustomerBook
    {
        [Key]
        public int CustomerBookId { get; set; }

        [ForeignKey("Customers")]
        public int FkCustomerId { get; set; }
        public Customer? Customers { get; set; }

        [ForeignKey("Books")]
        public int FkBookId { get; set; }
        public Book? Books { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDateOfLoan { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDateOfLoan { get; set; }
    }
}
