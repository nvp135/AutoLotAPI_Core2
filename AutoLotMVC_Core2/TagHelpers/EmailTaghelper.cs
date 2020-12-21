using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace AutoLotMVC_Core2.TagHelpers
{
    public class EmailTaghelper : TagHelper
    {
        public string EmailName { get; set; }
        public string EmailDomain { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";
            var address = $"{EmailName}@{EmailDomain}";
            output.Attributes.SetAttribute("href", $"mailto:{address}");
            output.Content.SetContent(address);
        }
    }
}
