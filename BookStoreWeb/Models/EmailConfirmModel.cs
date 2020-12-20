namespace BookStoreWeb.Models
{
    public class EmailConfirmModel
    {
        public string  Email{ get; set; }
        public bool IsConfirmed { get; set; }
        public bool EmailSent { get; set; }
        public bool EmailVerify { get; set; }
    }
}