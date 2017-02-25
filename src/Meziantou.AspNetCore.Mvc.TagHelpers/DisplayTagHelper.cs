using Microsoft.AspNetCore.Razor.TagHelpers;
using System;

namespace Meziantou.AspNetCore.Mvc.TagHelpers
{
    public class DisplayTagHelper : TagHelper
    {
        [HtmlAttributeName("value")]
        public bool? Value { get; set; }

        [HtmlAttributeName("defaultValue")]
        public bool DefaultValue { get; set; } = true;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            bool value = Value.GetValueOrDefault(DefaultValue);
            if (!value)
            {
                output.SuppressOutput();
                return;
            }

            output.TagName = null;
        }
    }
}
