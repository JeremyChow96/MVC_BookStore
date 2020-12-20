using System.ComponentModel.DataAnnotations;

namespace BookStoreWeb.Repository
{
    public class ForgotPasswordModel
    {
        [Required,EmailAddress,Display(Name ="Register email address" )]
        public string Email { get; set; }

        public bool EmailSent { get; set; }
    }
}