using Microsoft.AspNetCore.Html;
using CommonMark;

namespace TheDocsNetCore.Extensions
{
    public static class Extensions
    {
        /// <summary>
        /// Extension method to convert stringified HTML to an HtmlString
        /// </summary>
        /// <param name="htmlString">Stringified HTML</param>
        /// <returns></returns>
        public static HtmlString ToHTMLString (this string htmlString)
        {
            HtmlString result = new HtmlString(htmlString);

            return result;
        }

        /// <summary>
        /// Extension method to convert MarkDown into HTML
        /// </summary>
        /// <param name="markDown">MarkDown string</param>
        /// <returns></returns>
        public static string ToHTML (this string markDown)
        {
            string html = CommonMarkConverter.Convert(markDown);

            return html;
        }
    }
}
