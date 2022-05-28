using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace MoneySmart.TagHelpers
{
    /// <summary>
    /// Renders the executing assembly's informational version value.
    /// </summary>
    [HtmlTargetElement("AssemblyInformationalVersion", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class VersionTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = string.Empty;

            var attribute = (AssemblyInformationalVersionAttribute)Assembly
                            .GetExecutingAssembly()
                            .GetCustomAttributes(typeof(AssemblyInformationalVersionAttribute), false)
                            .FirstOrDefault();

            if (attribute != null)
            {
                output.Content.SetContent(attribute.InformationalVersion);
            }
        }
    }
}
