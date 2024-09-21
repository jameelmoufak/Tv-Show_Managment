using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text.RegularExpressions;

namespace Tv_Show_Managment.TagHelper
{
    [HtmlTargetElement("url-with-slug")]
    public class SlugTagHelper : AnchorTagHelper
    {
        public SlugTagHelper(IHtmlGenerator generator) : base(generator)
        {
        }
        [HtmlAttributeName("for-tvshow-id")]
        public Guid TVShowId { get; set; }
        [HtmlAttributeName("for-title")]
        public string Title { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";
            output.TagMode = TagMode.StartTagAndEndTag;
            var slug = Regex.Replace(Title, "[^a-zA-Z-0-9-]+", " ").Trim().Replace(" ", "-").ToLower();
            RouteValues.Add("slug", slug);
            RouteValues.Add("TVShowId", TVShowId.ToString());
            base.Process(context, output);
        }
    }
}
