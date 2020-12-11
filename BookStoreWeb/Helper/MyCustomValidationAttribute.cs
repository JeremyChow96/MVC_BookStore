using System.ComponentModel.DataAnnotations;

namespace BookStoreWeb.Helper
{
    public class MyCustomValidationAttribute : ValidationAttribute
    {
        public MyCustomValidationAttribute(string txt)
        {
            Text = txt;
        }
        public string Text { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string bookName = value.ToString();
                if (bookName.Contains(Text))
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("BookName does not contains the desired value");
                }
            }

            return new ValidationResult(ErrorMessage ?? "Value is empty");
        }
    }
}