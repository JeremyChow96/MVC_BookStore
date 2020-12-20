using System.ComponentModel.DataAnnotations;

namespace BookStoreWeb.Models
{
    public class ResetPasswordModel
    {
        [Required] public string UserId { get; set; }

        [Required] public string Token { get; set; }

        [Required, DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required, DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "两次输入密码不一致！")]
        public string ConfirmNewPassword { get; set; }

        public bool IsSuccess { get; set; }
    }


}