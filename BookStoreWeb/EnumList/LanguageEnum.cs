using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace BookStoreWeb.EnumList
{
    public enum LanguageEnum
    {
        [Display(Name = "中文")]
        Chinese,
        English,
        [Display(Name= "日本語")]
        Japanese,
        Others
    }
}