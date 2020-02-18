using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestingSystem.Models.Interfaces;

namespace TestingSystem.Web.Helpers
{
    public static class PaginationHelpers
    {
        public static MvcHtmlString Pagination(this HtmlHelper html, IPagedList list)
        {
            TagBuilder pagination = new TagBuilder("ul");
            pagination.AddCssClass("pagination");

            if (list.HavePreviousPage)
            {
                TagBuilder prev = new TagBuilder("li");
                prev.AddCssClass("page-item page-item-prev");

                TagBuilder prevInner = new TagBuilder("span");
                prevInner.AddCssClass("page-link");
                prevInner.InnerHtml += "<i class='fa fa-chevron-left'></i>";

                prev.InnerHtml += prevInner.ToString();
                pagination.InnerHtml += prev.ToString();
            }

            for (int i = 1; i <= list.TotalPages; i++)
            {
                TagBuilder item = new TagBuilder("li");
                item.AddCssClass("page-item");
                if (i == list.PageIndex)
                    item.AddCssClass("active");

                TagBuilder itemInner = new TagBuilder("span");
                itemInner.AddCssClass("page-link");
                itemInner.InnerHtml += i.ToString();

                item.InnerHtml = itemInner.ToString();
                pagination.InnerHtml += item.ToString();
            }

            if (list.HaveNextPage)
            {
                TagBuilder next = new TagBuilder("li");
                next.AddCssClass("page-item page-item-next");

                TagBuilder nextInner = new TagBuilder("span");
                nextInner.AddCssClass("page-link");
                nextInner.InnerHtml += "<i class='fa fa-chevron-right'></i>";

                next.InnerHtml += nextInner.ToString();
                pagination.InnerHtml += next.ToString();
            }

            return MvcHtmlString.Create(pagination.ToString());
        }
    }
}