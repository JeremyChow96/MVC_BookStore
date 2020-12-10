﻿using System.ComponentModel.DataAnnotations;
using BookStoreWeb.EnumList;

namespace BookStoreWeb.Models
{
    public class BookModel
    {
        [DataType(DataType.DateTime)]
        [Display(Name="Custom Name")]
        public string Myfield { get; set; }

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

        public int LanguageId { get; set; }

        //[Required(ErrorMessage = "选择语言ok不")]
        //public LanguageEnum LanguageEnum { get; set; }
    }
}