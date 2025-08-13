using Microsoft.AspNetCore.Razor.TagHelpers;

namespace RazorApp1.TagHelpers;

// Usage: <badge type="info">Hello</badge>
[HtmlTargetElement("badge", Attributes = "type")]
public sealed class BadgeTagHelper : TagHelper
{
    public BadgeType Type { get; set; } = BadgeType.Info;

    /// <summary>Optional title attribute on the badge</summary>
    public string? Title { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        // Transform <badge> into <span class="badge ...">...</span>
        output.TagName = "span";
        output.TagMode = TagMode.StartTagAndEndTag;

        var css = Type switch
        {
            BadgeType.Info => "badge bg-info",
            BadgeType.Success => "badge bg-success",
            BadgeType.Warning => "badge bg-warning text-dark",
            BadgeType.Danger => "badge bg-danger",
            _ => "badge bg-secondary"
        };

        output.Attributes.SetAttribute("class", css);
        if (!string.IsNullOrWhiteSpace(Title))
            output.Attributes.SetAttribute("title", Title);
    }
}