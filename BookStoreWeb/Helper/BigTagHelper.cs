using Microsoft.AspNetCore.Razor.TagHelpers;

namespace BookStoreWeb.Helper
{
    /// <summary>
    /// 自定义 taghelper 取重载现有的标签
    /// </summary>
    [HtmlTargetElement("big",Attributes = "big")]
    public class BigTagHelper :TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "h3";
            output.Attributes.RemoveAll("big");
            output.Attributes.SetAttribute("class","h3");
        }
    }
}