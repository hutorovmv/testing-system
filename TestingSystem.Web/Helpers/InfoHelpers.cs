using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestingSystem.Web.Helpers
{
    public static class InfoHelpers
    {
        public static MvcHtmlString InfoCard(this HtmlHelper html, string icon, string title, string message)
        {
            TagBuilder header = new TagBuilder("h2");
            header.AddCssClass("text-dark");
            header.InnerHtml = title;

            TagBuilder content = new TagBuilder("p");
            content.AddCssClass("text-muted");
            content.InnerHtml = message;

            TagBuilder card = new TagBuilder("div");
            card.AddCssClass("mx-auto bg-white rounded shadow p-4 text-center");
            
            if (!string.IsNullOrEmpty(icon))
            {
                card.InnerHtml += icon;
                header.AddCssClass("mt-3");
            }
            if (!string.IsNullOrEmpty(title))
            {
                card.InnerHtml += header.ToString();
                content.AddCssClass("mt-3");
            }
            if (!string.IsNullOrEmpty(message))
                card.InnerHtml += content.ToString();

            return MvcHtmlString.Create(card.ToString());
        }

        public static MvcHtmlString Preloader(this HtmlHelper html, string id, string classes = "")
        {
            TagBuilder inner = new TagBuilder("i");
            inner.AddCssClass("fa fa-refresh fa-spin fa-2x");

            TagBuilder outer = new TagBuilder("div");
            outer.Attributes.Add("id", id);
            outer.AddCssClass("text-primary text-center");
            outer.AddCssClass(classes);
            outer.InnerHtml += inner.ToString();

            return MvcHtmlString.Create(outer.ToString());
        }

        public static MvcHtmlString InfoString<T>(this HtmlHelper html, string name, T value)
        {
            TagBuilder strong = new TagBuilder("strong");
            strong.InnerHtml += $"{name}: ";

            TagBuilder em = new TagBuilder("em");
            em.InnerHtml += value;

            TagBuilder p = new TagBuilder("p");
            p.AddCssClass("mb-2 info-string");
            p.InnerHtml += strong.ToString();
            p.InnerHtml += em.ToString();

            return MvcHtmlString.Create(p.ToString());
        }
    }
}