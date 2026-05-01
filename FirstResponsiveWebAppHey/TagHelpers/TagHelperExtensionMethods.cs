using Microsoft.AspNetCore.Razor.TagHelpers;

namespace FirstResponsiveWebAppHey.TagHelpers
{
    public static class TagHelperExtensions
    {
        public static void AppendCssClass(this TagHelperAttributeList list, 
            string newCssClasses)
        {
            string oldCssClasses = list["class"]?.Value.ToString() ?? "";
            string cssClasses = (string.IsNullOrEmpty(oldCssClasses)) ? 
                newCssClasses : $"{oldCssClasses} {newCssClasses}";
            list.SetAttribute("class", cssClasses);
        }
    }
}
