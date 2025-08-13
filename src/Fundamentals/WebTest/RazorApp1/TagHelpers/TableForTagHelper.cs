using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Collections;

namespace RazorApp1.TagHelpers;

[HtmlTargetElement("table-for", Attributes = "items")]
public class TableForTagHelper : TagHelper
{
    public IEnumerable? Items { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        // Ensure we always output a full <table>...</table>
        output.TagName = "table";
        output.TagMode = TagMode.StartTagAndEndTag; // <-- This makes self-closing syntax work
        output.Attributes.SetAttribute("class", "table table-bordered");

        if (Items == null || !Items.Cast<object>().Any())
        {
            output.Content.SetHtmlContent("<tr><td>No data</td></tr>");
            return;
        }

        var firstItem = Items.Cast<object>().First();
        var props = firstItem.GetType().GetProperties();

        var html = "<thead><tr>";
        foreach (var prop in props)
            html += $"<th>{prop.Name}</th>";
        html += "</tr></thead><tbody>";

        foreach (var item in Items)
        {
            html += "<tr>";
            foreach (var prop in props)
            {
                var value = item is null ? "" : prop.GetValue(item) ?? "<b>null</b>";
                html += $"<td>{value}</td>";
            }
            html += "</tr>";
        }

        html += "</tbody>";
        output.Content.SetHtmlContent(html);
    }
}