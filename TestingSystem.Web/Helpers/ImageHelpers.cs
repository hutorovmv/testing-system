using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestingSystem.Web.Helpers
{
    public static class ImageHelpers
    {
        public static MvcHtmlString ImageFromArray(this HtmlHelper html, byte[] image, string classes = "", string id = "", string alt = "")
        {
            string imgStr = "";
            string dataType = "";

            TagBuilder img = new TagBuilder("img");

            if (image != null) { 
                imgStr = Convert.ToBase64String(image);
                dataType = "data:image/jpg;base64";    
            }

            string src = image != null ? $"{dataType}, {imgStr}" : "";
            img.Attributes.Add("id", id);
            img.Attributes.Add("src", src);
            img.Attributes.Add("alt", alt);
            img.AddCssClass(classes);

            return MvcHtmlString.Create(img.ToString(TagRenderMode.SelfClosing));
        }
    }
}