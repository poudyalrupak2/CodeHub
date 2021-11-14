using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Helpers
{
  public static  class FileTagHelper
    {
        public static MvcHtmlString FileFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
        {
            return helper.FileFor(expression, null);
        }
        public static MvcHtmlString File(this HtmlHelper helper, string expression)
        {
            return helper.File(expression, null);
        }
        public static MvcHtmlString File(this HtmlHelper htmlHelper, string expression, object htmlAttributes)
        {
            var builder = new TagBuilder("input");

            //var id = helper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(ExpressionHelper.GetExpressionText(expression));
            ////var id = helper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(ExpressionHelper.GetExpressionText(expression));

            //builder.GenerateId(id);
            //builder.MergeAttribute("name", id);
            builder.MergeAttribute("type", "file");

            builder.MergeAttributes(new RouteValueDictionary(htmlAttributes));

            // Render tag
            return MvcHtmlString.Create(builder.ToString(TagRenderMode.SelfClosing));

        }
       
        
        public static MvcHtmlString FileFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes)
        {
            var builder = new TagBuilder("input");

            //var id = helper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(ExpressionHelper.GetExpressionText(expression));
            ////var id = helper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(ExpressionHelper.GetExpressionText(expression));

            //builder.GenerateId(id);
            //builder.MergeAttribute("name", id);
            builder.MergeAttribute("type", "file");

            builder.MergeAttributes(new RouteValueDictionary(htmlAttributes));

            // Render tag
            return MvcHtmlString.Create(builder.ToString(TagRenderMode.SelfClosing));
        }







        //public static IHtmlString File(string id="",string name="")
        //{
        //    TagBuilder tb = new TagBuilder("input");
        //    tb.Attributes.Add("type", "file");
        //    tb.Attributes.Add("id", id);
        //    tb.Attributes.Add("class", "FormControl");
        //    tb.Attributes.Add("name", name);
        //    return new MvcHtmlString(tb.ToString());

        //}
    }
}
