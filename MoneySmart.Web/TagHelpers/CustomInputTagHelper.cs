using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace MoneySmart.TagHelpers;

/// <summary>
/// Extend built-in InputTagHelper to add custom behavior.
/// </summary>
[HtmlTargetElement("input", Attributes = ForAttributeName, TagStructure = TagStructure.WithoutEndTag)]
public class CustomInputTagHelper : InputTagHelper
{
    private const string ForAttributeName = "asp-for";

    public CustomInputTagHelper(IHtmlGenerator generator) : base(generator)
    {
    }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        base.Process(context, output);

        // Check if the model type is decimal
        if (For.ModelExplorer.ModelType == typeof(decimal) || For.ModelExplorer.ModelType == typeof(decimal?))
        {
            output.Attributes.SetAttribute("inputmode", "decimal");
        }
    }
}