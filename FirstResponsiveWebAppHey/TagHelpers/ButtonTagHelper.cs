using Microsoft.AspNetCore.Razor.TagHelpers;

namespace FirstResponsiveWebAppHey.TagHelpers
{
    [HtmlTargetElement("button", Attributes = "[type=submit]")]
    [HtmlTargetElement("a", Attributes = "my-button")]
    public class ButtonTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.AppendCssClass("btn btn-success");
        }
    }
}
