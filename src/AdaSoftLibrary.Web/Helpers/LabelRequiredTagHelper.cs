using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text.Encodings.Web;

namespace AdaSoftLibrary.Web.Helpers;

/// <summary>
/// Tag helper zmení Label pre povinné položky na tučné písmo a pridá znak "*"
/// </summary>
[HtmlTargetElement("label", Attributes = ForAttributeName)]
public class LabelRequiredTagHelper : LabelTagHelper
{
    private const string ForAttributeName = "asp-for";

    public LabelRequiredTagHelper(IHtmlGenerator generator) : base(generator)
    {
    }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        await base.ProcessAsync(context, output);

        if (For.Metadata.IsRequired)
        {
            var sup = new TagBuilder("sup");
            sup.InnerHtml.Append("*");
            sup.AddCssClass("text-danger");
            output.Content.AppendHtml(sup);
            output.AddClass("fw-bold", HtmlEncoder.Default);
        }
    }
}
