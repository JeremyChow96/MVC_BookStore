using System;
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

        public DateTime? CreateOn { get; set; }

        public DateTime? UpdateOn { get; set; }

        public Language Language { get; set; }
    }
}
