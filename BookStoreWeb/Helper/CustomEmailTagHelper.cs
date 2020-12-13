using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace BookStoreWeb.Helper
{
    /// <summary>
    /// 自定义taghelper ： custom-email
    /// </summary>
    public class CustomEmailTagHelper :TagHelper
    {
        public string MyEmail { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";
            // base.Process(context, output);
            output.Attributes.SetAttribute("href",$"mailto:{MyEmail}");
            output.Attributes.Add("id","my-email-id");
            output.Content.SetContent("my-email");
        }
    }
}
