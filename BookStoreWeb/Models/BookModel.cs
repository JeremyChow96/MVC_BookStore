using System.ComponentModel.DataAnnotations;

namespace BookStoreWeb.Models
{
    public class BookModel
    {
        public int Id { get; set; }

        [StringLength(100,MinimumLength = 5)]
        [Required(ErrorMessage = "Please enter the title of book")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Please enter the author name")]

        public string Author { get; set; }

        public string Category { get; set; }

        public string Description { get; set; }
        
        [Required(ErrorMessage = "Please enter the total pages")]
        [Display(Name = "Total Pages of book")]
        public int? TotalPages { get; set; }

        public string Language { get; set; }
    }
}