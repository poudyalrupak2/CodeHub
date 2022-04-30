using Microsoft.AspNetCore.Razor.TagHelpers;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
   public   class FirstTagHelper:TagHelper
    {
        public InpCodes codes { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string content = $@"<div class='card'>
		 <h4 class='card-title'><a href='#'>{codes.Topic} </a></h4>
		 <p class='card-position'>{codes.Description}</p>
		 <p class='card-description'>Keynote: Will be announced soon</p>
		 <ul class='social accent-color'>
			    <li><a target='_blank' href='#'>LinkedIn</a>
			    </li>
			    <li><a target='_blank' href='#'>Microsoft</a>
			    </li>
		 </ul>
	     </div>";
            output.Attributes.SetAttribute("class", "col-xs-12 col-sm-6 col-md-4 col-lg-3");
            output.TagName = "div";
            output.Content.SetHtmlContent(content);
        }
    

}
}
