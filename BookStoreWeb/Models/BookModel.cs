using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;
using BookStoreWeb.EnumList;
using BookStoreWeb.Helper;
using Microsoft.AspNetCore.Http;

namespace BookStoreWeb.Models
{
    /// <summary>
    /// Server side validation
    /// 还需要客户端验证:  Js Library:
    /// jquery.js  jquery.validate.js jquery.validate.unobtrusive.js
    /// </summary>
    public class BookModel
    {
        [DataType(DataType.DateTime)]
        [Display(Name="Custom Name")]
        public string Myfield { get; set; }

        public int Id { get; set; }

        [StringLength(100, MinimumLength = 5)]
        [Required(ErrorMessage = "Please enter the title of book")]
        // [MyCustomValidation(txt:"oooo",ErrorMessage = "custom error msg")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Please enter the author name")]

        public string Author { get; set; }

        public string Category { get; set; }

        public string Description { get; set; }
        
        public int LanguageId { get; set; }

        public string Language { get; set; }

        [Required(ErrorMessage = "Please enter the total pages")]
        [Display(Name = "Total Pages of book")]
        public int? TotalPages { get; set; }
        //[Required(ErrorMessage = "选择语言ok不")]
        //public LanguageEnum LanguageEnum { get; set; }

        [Display(Name="Choose the cover photo of your book")]
       // [Required]
        public IFormFile CoverPhoto { get; set; }

       public string CoverImageUrl { get; set; }

       [Display(Name = "Choose the gallery images of your book")]
       // [Required]
       public  IFormFileCollection GalleryFiles { get; set; }

       public List<GalleryModel> Gallery { get; set; }


       [Display(Name = "upload your book in pdf format")]
       [Required]
       public IFormFile BookPdf { get; set; }

       public string BookPdfUrl { get; set; }

    }
}