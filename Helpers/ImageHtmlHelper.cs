using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace Helpers
{
   public static class ImageHtmlHelper
    {
        public static MvcHtmlString Image(this HtmlHelper htmlHelper,string src, object htmlAttributes)
        {
            var builder = new TagBuilder("img");

            builder.Attributes.Add("src", src);

            builder.MergeAttributes(new RouteValueDictionary(htmlAttributes));

            // Render tag
            return MvcHtmlString.Create(builder.ToString(TagRenderMode.SelfClosing));

        }
        public static MvcHtmlString Image(this HtmlHelper htmlHelper, string src)
        {
            var builder = new TagBuilder("img");

            builder.Attributes.Add("src", src);


            // Render tag
            return MvcHtmlString.Create(builder.ToString(TagRenderMode.SelfClosing));

        }

    }
}
