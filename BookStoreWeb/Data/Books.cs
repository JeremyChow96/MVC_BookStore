using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStoreWeb.Data
{
    public class Books
    {
        public int Id { get; set; }

        public string Title { get; set; }
 
        public string Author { get; set; }

        public string Category { get; set; }

        public string Description { get; set; }

        public int LanguageId { get; set; }

        public int TotalPages { get; set; }

        public string CoverImageUrl{ get; set; }

        public string BookPdfUrl { get; set; }

        public DateTime? CreateOn { get; set; }

        public DateTime? UpdateOn { get; set; }

        public Language Language { get; set; }

        public  ICollection<BookGallery> bookGallery { get; set; }
    }
}
